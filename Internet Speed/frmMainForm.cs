/*
    Copyleft (c) dreamer2908 2012. All wrongs reserved.

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see http://www.gnu.org/licenses/gpl.html.
	*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;

namespace Internet_Speed
{
    public partial class frmMainForm : Form
    {
        public frmMainForm()
        {
            InitializeComponent();

            updateNetworkInterfaceList();

            // interface stuffs
            bool showTrayIconByDefault = true;
            //showPingInfoInTray = true;
            showPingToolStripMenuItem.Checked = true;
            notifyIcon1.Visible = showTrayIconByDefault;
            keepTrayIcon = showTrayIconByDefault;
            trayToolStripMenuItem.Checked = keepTrayIcon;
            restoreToolStripMenuItem.Text = this.Visible ? "Minimize" : "Restore";

            // start pinging at load time
            if (!bgwPinger1.IsBusy) bgwPinger1.RunWorkerAsync();
            if (!bgwPinger2.IsBusy) bgwPinger2.RunWorkerAsync();
            if (!bgwPinger3.IsBusy) bgwPinger3.RunWorkerAsync();
        }

        #region Global variables

        public delegate void labelDelegate(string Result, Label Dest);

        NetworkInterface networkInterface;
        IPv4InterfaceStatistics interfaceStatistic;
        string currentInterfaceName = "";
        int interfaceRefreshDelay = 2;
        Boolean interfaceSticked = false;
        String stickedInterface = "";

        Boolean allowUpdate = false;
        MouseButtons clickedButton = MouseButtons.Left;
        Boolean mouseClicked = false;
        Boolean keepTrayIcon = false;
        //Boolean showPingInfoInTray = true;

        // ping variable

        Boolean continuePinging = true;
        DateTime ping1 = DateTime.MinValue, ping2 = DateTime.MinValue, ping3 = DateTime.MinValue;
        int ping1Timeout = 3000, ping2Timeout = 7000, ping3Timeout = 15000;
        string defaultIP = "8.8.8.8";
        string lastIPString;
        IPAddress lastIP;

        // transmitting history variables

        long lngBytesSent = 0;
        long lngBytesReceived = 0;

        long[] receivedHistory = new long[60];
        int transHistoryIndex = 0;
        long[] sentHistory = new long[60];
        int historyLength = 0;

        // ping history variables

        struct pingResult {
            public long pingTime;
            public bool OK;
        }

        long ping3Sent = 0, ping3Received = 0, ping3Lost = 0, ping3Max = 0, ping3Min = 9001;
        double ping3Average = 0;

        pingResult[] ping2History = new pingResult[600];
        int ping2HistoryLength = 0;
        int ping2HistoryIndex = 0;
        long ping2Sent = 0, ping2Received = 0, ping2Lost = 0, ping2Max = 0, ping2Min = 9001;
        double ping2Average = 0;
        int ping2TimeoutIndex = 9001;
        
        pingResult[] ping1History = new pingResult[60];
        int ping1HistoryLength = 0;
        int ping1HistoryIndex = 0;
        long ping1Sent = 0, ping1Received = 0, ping1Lost = 0, ping1Max = 0, ping1Min = 9001;
        double ping1Average = 0;
        int ping1TimeoutIndex = 9001;

        #endregion

        #region Invoke stuffs
        private void _UpdateLabel(string Result, Label Dest)
        {
            Dest.Text = Result;
        }

        private void UpdateLabel(string Result, Label Dest)
        {
            if (Dest.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                labelDelegate d = new labelDelegate(_UpdateLabel);
                try
                {
                    this.Invoke(d, new object[] { Result, Dest });
                }
                catch (Exception)
                {
                    // exception only occurs when program is exiting. nothing to do.
                }
            }
            else
            {
                // It's on the same thread, no need for Invoke
                Dest.Text = Result;
            }
        }
        #endregion

        #region Statistics core functions
        private void ShowNetworkInterfaceTraffice()
        {
            interfaceStatistic = networkInterface.GetIPv4Statistics();

            long bytesSentSpeed = (interfaceStatistic.BytesSent - lngBytesSent);
            long bytesReceivedSpeed = (interfaceStatistic.BytesReceived - lngBytesReceived);
            //TimeSpan interval = TimeSpan.FromSeconds(duration); // convert duration from second to hh:mm:ss

            lngBytesSent = interfaceStatistic.BytesSent;
            lngBytesReceived = interfaceStatistic.BytesReceived;

            lblSpeed.Text = String.Format("{0:0.##}", networkInterface.Speed / 1000000.0) + " Mbps";

            lblByteReceived.Text = transmittedSize(lngBytesReceived);
            lblByteSent.Text = transmittedSize(lngBytesSent);

            lblUpload.Text = transmittingSpeed(bytesSentSpeed, lblUpload.Text);
            lblDownLoad.Text = transmittingSpeed(bytesReceivedSpeed, lblDownLoad.Text);

            lblUltilization.Text = ultilizationPercent(bytesSentSpeed, bytesReceivedSpeed, networkInterface.Speed, lblUltilization.Text);

            notifyIcon1.Text = "DL: " + lblDownLoad.Text + " UL: " + lblUpload.Text + ((showPingToolStripMenuItem.Checked) ? " Ping: " + lblPingTime1.Text : "");

            // save to history
            receivedHistory[transHistoryIndex] = bytesReceivedSpeed;
            sentHistory[transHistoryIndex] = bytesSentSpeed;
            transHistoryIndex = (transHistoryIndex < 59) ? transHistoryIndex + 1 : 0;
            // solve first minute problem: how many history should be considered, that is, not trash
            historyLength = (historyLength < 60) ? historyLength + 1 : 60;
        }

       private static string ultilizationPercent(long bytesSentSpeed, long bytesReceivedSpeed, long linkSpeed, string old_val)
        {
            double percent = 800 * (bytesSentSpeed + bytesReceivedSpeed) / linkSpeed;
            if (percent > 100.0) percent = 100.0; // to reduce confusion. it actually can be more than 100%
            if (percent < 0.0) return old_val; 
            // solution for a rare problem when data transfered info is reset in some VPS // blame M$ virtualization technology
            // nope. stats are reset at ~4GB
            return String.Format("{0:0.##}", percent) + " %";
        }

        private static string transmittedSize(long lngByte)
        {
            if (lngByte > 1073741823) return String.Format("{0:0.###}", lngByte / 1073741824.0) + " GiB";
            else if (lngByte > 1048575) return String.Format("{0:0.##}", lngByte / 1048576.0) + " MiB";
            else if (lngByte > 1023) return String.Format("{0:0.##}", lngByte / 1024.0) + " KiB";
            else return lngByte.ToString() + " B";
        }

        private static string transmittingSpeed(long lngSpeed, string old_val)
        {
            if (lngSpeed > 1048575) return String.Format("{0:0.###}", lngSpeed / 1048576.0) + " MiB/s";
            else if (lngSpeed > 1023) return String.Format("{0:0.##}", lngSpeed / 1024.0) + " KiB/s";
            else if (lngSpeed < 0) return old_val; // solution ^^
            else return lngSpeed.ToString() + " B/s";
        }

        private void ResetStats()
        {
            // reset stat // not to 0 but to current
            interfaceStatistic = networkInterface.GetIPv4Statistics();

            lngBytesSent = interfaceStatistic.BytesSent;
            lngBytesReceived = interfaceStatistic.BytesReceived;
        }
        #endregion

        /// <summary>
        /// Main update routine
        /// </summary>
        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            
            if (allowUpdate)
            {
                if ((interfaceSticked && (networkInterface != null)) && (networkInterface.Name != stickedInterface))
                {
                    if (interfaceRefreshDelay == 0)
                    {
                        updateNetworkInterfaceList();
                        interfaceRefreshDelay = 2;
                    }
                    else interfaceRefreshDelay--;
                }

                try
                {
                    ShowNetworkInterfaceTraffice();
                }
                // if any exception happens, disable update
                // updateNetworkInterfaceList() will enable update again
                catch (System.Net.NetworkInformation.NetworkInformationException)
                {
                    allowUpdate = false;
                    updateNetworkInterfaceList();
                }
                catch (System.NullReferenceException)
                {
                    allowUpdate = false;
                    updateNetworkInterfaceList();
                }
                catch (System.Exception) // not recommended but whatever
                {
                    allowUpdate = false;
                    updateNetworkInterfaceList();
                }
            }

            // a silly solution for a mistery bug that never occurs when debugging but running for real
            if (!bgwPinger1.IsBusy) bgwPinger1.RunWorkerAsync();
            if (!bgwPinger2.IsBusy) bgwPinger2.RunWorkerAsync();
            if (!bgwPinger3.IsBusy) bgwPinger3.RunWorkerAsync();

            // calculate average transmitting speed
            long averageReceivingSpeed = 0, averageSendingSpeed = 0;

            {
                averageReceivingSpeed = averageSendingSpeed = 0;
                for (int i = 0; i < historyLength; i++)
                {
                    averageReceivingSpeed += receivedHistory[i];
                    averageSendingSpeed += sentHistory[i];
                }
            }
            averageReceivingSpeed /= historyLength;
            averageSendingSpeed /= historyLength;
            toolTip1.SetToolTip(lblDLSpeed, "Average DL Speed: " + transmittingSpeed(averageReceivingSpeed, "0 B/s").ToString());
            toolTip1.SetToolTip(lblULSpeed, "Average UL Speed: " + transmittingSpeed(averageSendingSpeed, "0 B/s").ToString());

            // tooltip ping
            toolTip1.SetToolTip(lblPingTime1, "Checked at: " + ping1.ToShortTimeString() + "\nTimeout Limit: " + ping1Timeout.ToString() + "ms" + ((bgwPinger1.IsBusy) ? "" : "\n(Inactive)"));
            toolTip1.SetToolTip(lblPingTime2, "Checked at: " + ping2.ToShortTimeString() + "\nTimeout Limit: " + ping2Timeout.ToString() + "ms" + ((bgwPinger2.IsBusy) ? "" : "\n(Inactive)"));
            toolTip1.SetToolTip(lblPingTime3, "Checked at: " + ping3.ToShortTimeString() + "\nTimeout Limit: " + ping3Timeout.ToString() + "ms" + ((bgwPinger3.IsBusy) ? "" : "\n(Inactive)"));

            // tooltip ping stats
            int lostPercent3 = (int) ((ping3Sent > 0) ? (100.0 * ping3Lost / ping3Sent) : 0);
            int lostPercent2 = (int) ((ping2Sent > 0) ? (100.0 * ping2Lost / ping2Sent) : 0);
            int lostPercent1 = (int) ((ping1Sent > 0) ? (100.0 * ping1Lost / ping1Sent) : 0);

            string ping3ToolTipInfo = ("Packets: Sent = " + ping3Sent.ToString() + ", Received = " + ping3Received.ToString() + ", Lost = " + ping3Lost.ToString() + " (" + lostPercent3.ToString() + "% loss)\n");
            ping3ToolTipInfo += ("Times: Min = " + ping3Min.ToString() + "ms, Max = " + ping3Max.ToString() + "ms, Average = " + String.Format("{0:0}", ping3Average) + "ms");
            //ping3ToolTipInfo += ("\nDebug Info: Index = " + ping3HistoryIndex.ToString() + ", Length = " + ping3HistoryLength.ToString() + ", TimeoutIndex = " + ping3TimeoutIndex.ToString());
            
            string ping2ToolTipInfo = ("Packets: Sent = " + ping2Sent.ToString() + ", Received = " + ping2Received.ToString() + ", Lost = " + ping2Lost.ToString() + " (" + lostPercent2.ToString() + "% loss)\n");
            ping2ToolTipInfo += ("Times: Min = " + ping2Min.ToString() + "ms, Max = " + ping2Max.ToString() + "ms, Average = " + String.Format("{0:0}", ping2Average) + "ms");
            //ping2ToolTipInfo += ("\nDebug Info: Index = " + ping2HistoryIndex.ToString() + ", Length = " + ping2HistoryLength.ToString() + ", TimeoutIndex = " + ping2TimeoutIndex.ToString());

            string ping1ToolTipInfo = ("Packets: Sent = " + ping1Sent.ToString() + ", Received = " + ping1Received.ToString() + ", Lost = " + ping1Lost.ToString() + " (" + lostPercent1.ToString() + "% loss)\n");
            ping1ToolTipInfo += ("Times: Min = " + ping1Min.ToString() + "ms, Max = " + ping1Max.ToString() + "ms, Average = " + String.Format("{0:0}", ping1Average) + "ms");
            //ping1ToolTipInfo += ("\nDebug Info: Index = " + ping1HistoryIndex.ToString() + ", Length = " + ping1HistoryLength.ToString() + ", TimeoutIndex = " + ping1TimeoutIndex.ToString());

            toolTip1.SetToolTip(lblPing3, ping3ToolTipInfo);
            toolTip1.SetToolTip(lblPing2, ping2ToolTipInfo);
            toolTip1.SetToolTip(lblPing1, ping1ToolTipInfo);
        }

        #region Interface stuffs

        private void updateNetworkInterfaceList()
        {
            NetworkInterface[] AllNetworkInterface = NetworkInterface.GetAllNetworkInterfaces();
            int n = AllNetworkInterface.Length;
            NetworkInterface[] ActiveInterface = new NetworkInterface[n];
            string[] InterfaceName = new string[n];
            cbbInterfaces.Items.Clear();
            Boolean found = false;
            Boolean StickedFound = false;
            int StickedIndex = 0;

            for (int i = 0; i < n; i++)
            {
                NetworkInterface currentNetworkInterface = AllNetworkInterface[i];
                if (currentNetworkInterface.OperationalStatus == OperationalStatus.Up) // only active ones
                {
                    int ccbIndex = cbbInterfaces.Items.Add(currentNetworkInterface.Name);
                    found = true;
                    if (currentNetworkInterface.Name == stickedInterface)
                    {
                        StickedFound = true;
                        StickedIndex = ccbIndex;
                    }
                }
            }
            if (found)
            {
                cbbInterfaces.SelectedIndex = (interfaceSticked && StickedFound) ? StickedIndex : 0;
            }
            else allowUpdate = false;
        }

        private void cbbInterfaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SelectedInterfaceName = cbbInterfaces.SelectedItem.ToString();
            foreach (NetworkInterface currentNetworkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Chage current networkInterface to selected one
                if (currentNetworkInterface.Name == SelectedInterfaceName)
                {
                    allowUpdate = true;
                    networkInterface = currentNetworkInterface;
                    if (currentInterfaceName != SelectedInterfaceName) ResetStats();
                    currentInterfaceName = SelectedInterfaceName;
                    ShowNetworkInterfaceTraffice();
                    break;
                }
            }
        }

        #endregion

        #region Ping stuffs

        /// <summary>
        /// A ping routime
        /// </summary>
        /// TODO: Some kind of Statistics
        /// <param name="timeout">Timeout limit in ms</param>
        /// <param name="pingTimestamp">DateTime to record timestamp</param>
        /// <param name="target">Target victim, defaultIP only</param>
        private void pingRoutime(int timeout, out DateTime pingTimestamp, out long pingTime, out string result, Label target)
        {
            // Each ping loop has a different timeout
            // int timeout = 3000;

            // Output result
            //long pingTime = 9001;

            // Get timestamp
            DateTime timestamp = DateTime.Now;

            // Ping the target
            result = Ping(defaultIP, timeout, true, out pingTime);

            // Update ping result to the target label
            UpdateLabel(result, target);

            // Update ping timestamp
            pingTimestamp = timestamp;
        }

        /// <summary>
        /// Pings the target _IP within timeout limit and updates the target label with ping result. 
        /// </summary>
        /// <param name="_IP">Target victim, defaultIP only</param>
        /// <param name="timeout">Timeout limit in ms</param>
        /// <param name="limitRate">Limit pinging rate down to 1 per second or less</param>
        /// <param name="pingTime">Output Roundtrip Time</param>
        private string Ping(string _IP, int timeout, Boolean limitRate, out long pingTime)
        {
            // Parse victim address. Failback to 8.8.8.8 if any error occurs. Having some kind of caching for performance
            IPAddress IP;
            if (_IP.Equals(lastIPString)) IP = lastIP;
            else
            {
                try
                {
                    IP = IPAddress.Parse(_IP);
                }
                catch (System.FormatException)
                {
                    lastIPString = "8.8.8.8";
                    IP = IPAddress.Parse("8.8.8.8");
                    lastIP = IP;
                }
                catch (System.ArgumentNullException)
                {
                    lastIPString = "8.8.8.8";
                    IP = IPAddress.Parse("8.8.8.8");
                    lastIP = IP;
                }
            }

            // Create a new instant
            Ping pingSender = new Ping();

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "Ichiban_no_Takaramono_Karuta_Ver";
            byte[] buffer = Encoding.ASCII.GetBytes(data);

            // Set options for transmission:
            // The data can go through 64 gateways or routers before it is destroyed, and the data packet cannot be fragmented.
            PingOptions options = new PingOptions(64, true);

            // True work here
            PingReply reply = pingSender.Send(IP, timeout, buffer, options);

            // Result string goes here
            string pingResult = "";

            // Now compiling result
            if (reply.Status == IPStatus.Success) { pingResult = reply.RoundtripTime.ToString() + " ms"; } // ping succeeded
            else if ((reply.Status == IPStatus.TimedOut) | ((reply.Status == IPStatus.TimeExceeded) | (reply.Status == IPStatus.TtlExpired) | (reply.Status == IPStatus.TtlReassemblyTimeExceeded))) pingResult = "Timed out";
            else if ((reply.Status == IPStatus.DestinationHostUnreachable) | (reply.Status == IPStatus.DestinationNetworkUnreachable) | (reply.Status == IPStatus.DestinationPortUnreachable) | (reply.Status == IPStatus.DestinationProhibited) | (reply.Status == IPStatus.DestinationProtocolUnreachable) | (reply.Status == IPStatus.DestinationScopeMismatch) | (reply.Status == IPStatus.DestinationUnreachable)) pingResult = "Unreachable";
            else pingResult = "Failed"; // Transmission failed. General failure.

            // Get time.
            pingTime = reply.RoundtripTime;

            // Reduce ping rate by delaying more if ping_time is less than 1000ms
            if (limitRate && pingTime < 1000) Thread.Sleep((int)(1000 - pingTime));

            return pingResult;
        }

        private void bgwPinger1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (continuePinging)
            {
                long pingTime;
                string result;
                pingRoutime(ping1Timeout, out ping1, out pingTime, out result, this.lblPingTime1);

                // save to history
                pingSaveHistory(pingTime, result, ref ping1History, ref ping1HistoryIndex, ref ping1HistoryLength);

                // calculate stats
                pingStatsCalc(ping1History, ping1HistoryLength, ref ping1Sent, ref ping1Received, ref ping1Lost, ref ping1Max, ref ping1Min, ref ping1Average, ref ping1TimeoutIndex);
            }
        }

        private void bgwPinger2_DoWork(object sender, DoWorkEventArgs e)
        {
            while (continuePinging)
            {
                long pingTime;
                string result;
                pingRoutime(ping2Timeout, out ping2, out pingTime, out result, this.lblPingTime2);

                // save to history
                pingSaveHistory(pingTime, result, ref ping2History, ref ping2HistoryIndex, ref ping2HistoryLength);

                // calculate stats
                pingStatsCalc(ping2History, ping2HistoryLength, ref ping2Sent, ref ping2Received, ref ping2Lost, ref ping2Max, ref ping2Min, ref ping2Average, ref ping2TimeoutIndex);
            }
        }

        private void bgwPinger3_DoWork(object sender, DoWorkEventArgs e)
        {
            while (continuePinging)
            {
                long pingTime;
                string result;
                pingRoutime(ping3Timeout, out ping3, out pingTime, out result, this.lblPingTime3);

                // calculate stats // ping3 doesn't need any kind of history storage besides stats
                if (result.Contains("ms"))
                {
                    ping3Max = (pingTime > ping3Max) ? pingTime : ping3Max;
                    ping3Min = (pingTime < ping3Min) ? pingTime : ping3Min;
                    ping3Average = ((ping3Average * ping3Received) + pingTime) / (ping3Received + 1);
                    ping3Sent++;
                    ping3Received++;
                }
                else
                {
                    ping3Sent++;
                    ping3Lost++;
                }
            }
        }

        private void pingSaveHistory(long pingTime, string result, ref pingResult[] pingHistory, ref int pingHistoryIndex, ref int pingHistoryLength)
        {
            if (result.Contains("ms"))
            {
                pingHistory[pingHistoryIndex].OK = true;
                pingHistory[pingHistoryIndex].pingTime = pingTime;
            }
            else
            {
                pingHistory[pingHistoryIndex].OK = false;
            }
            pingHistoryIndex = (pingHistoryIndex < pingHistory.Length - 1) ? pingHistoryIndex + 1 : 0;
            pingHistoryLength = (pingHistoryLength < pingHistory.Length) ? pingHistoryLength + 1 : pingHistory.Length;
        }

        private void pingStatsCalc(pingResult[] history, int historyLength, ref long pingSent, ref long pingReceived, ref long pingLost, ref long pingMax, ref long pingMin, ref double pingAverage, ref int pingTimeoutIndex)
        {
            pingSent = historyLength;

            // reset stats
            pingReceived = 0;
            pingLost = 0;
            pingMax = 0;
            pingMin = 9001;
            pingAverage = 0;

            for (int i = 0; i < historyLength; i++)
            {
                pingResult temp = history[i];
                if (temp.OK)
                {
                    if (temp.pingTime > pingMax) pingMax = temp.pingTime;
                    if (temp.pingTime < pingMin) pingMin = temp.pingTime;
                    pingAverage += temp.pingTime;
                    pingReceived++;
                }
                else
                {
                    pingLost++;
                    pingTimeoutIndex = i;
                }
            }
            pingAverage = (pingReceived > 0) ? (pingAverage / pingReceived) : 0;
        }

        #endregion

        #region Sticky stuffs

        private void btnRefresh_MouseDown(object sender, MouseEventArgs e)
        {
            mouseClicked = true;
            clickedButton = e.Button;
        }

        private void btnRefresh_MouseUp(object sender, MouseEventArgs e)
        {            
            if (mouseClicked)
                if (clickedButton == MouseButtons.Right)
                {
                    // stick/unstick interface
                    stickedInterface = cbbInterfaces.Text;
                    interfaceSticked = !interfaceSticked;
                    grbInterface.Text = interfaceSticked ? "Interface (" + stickedInterface + " - sticked)" : "Interface";
                }
                else if (clickedButton == MouseButtons.Left)
                {
                    // unlock interface
                    grbInterface.Text = "Interface";
                    interfaceSticked = false;
                    updateNetworkInterfaceList();
                }
        }

        #endregion

        #region UI and menu stuffs

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changeWindowState();
        }

        private void trayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trayToolStripMenuItem.Checked = keepTrayIcon = !keepTrayIcon;
            notifyIcon1.Visible = !this.Visible || keepTrayIcon;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            changeWindowState();
        }

        private void changeWindowState()
        {
            if (!this.Visible)
            {
                Show();
                WindowState = FormWindowState.Normal;
                notifyIcon1.Visible = keepTrayIcon;
            }
            else
            {
                WindowState = FormWindowState.Minimized;
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void frmMainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
            restoreToolStripMenuItem.Text = this.Visible ? "Minimize" : "Restore";
        }

        #endregion

        #region Useless

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // nothing to do here. everything is handled by btnRefresh_MouseUp
            //updateNetworkInterfaceList();
        }


        /// <summary>
        /// Finds the best matched interface (still under contruction)
        /// </summary>
        /// <param name="keyword">Name to find</param>
        /// <param name="list">Interface list</param>
        /// <returns>Index of selected interface</returns>
        private int InterfaceMatch(string keyword, string[] list)
        {
            return 0;
        }

        #endregion

        #region Ping victim changing stuffs

        private void changePingTargetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string newTarget = Microsoft.VisualBasic.Interaction.InputBox("Enter one IP or website address:", "Change Ping Target to", "8.8.8.8", 234, 234);
            
            IPAddress trash;
            if (newTarget.Length > 0)
            {
                if (IPAddress.TryParse(newTarget, out trash))
                {
                    defaultIP = newTarget;
                }
                else
                {
                    string temp = getIPAddress(getDomainName(newTarget));
                    if (temp.Length > 0) defaultIP = temp;
                }
            }
        }

        private string getDomainName(string link)
        {
            string domainName = string.Empty;
            int linkLength = link.Length;
            int domainNameStartPos = 0;
            int temp = link.IndexOf("//");
            if (temp > -1)
            {
                domainNameStartPos = temp + 2;
            }
            else
            {
                domainNameStartPos = 0;
            }

            for (int i = domainNameStartPos; i < linkLength; i++)
            {
                if (!(link[i] == '/' || link[i] == ':'))
                    domainName += link[i];
                else i = linkLength;
            }

            return domainName;
        }

        private string getIPAddress(string domainName)
        {
            IPAddress[] addressList = new IPAddress[0];
            try
                {
                    addressList = Dns.GetHostAddresses(domainName);
                }
            catch (Exception)//(System.Net.Sockets.SocketException)
            {
                //MessageBox.Show("Host name not found!");
                return "";
            }
            if (addressList.Length > 0)
            {
                //MessageBox.Show(addressList[0].ToString());
                //MessageBox.Show(addressList[0].AddressFamily.ToString());
                return addressList[0].ToString();
            }
            else return "";
        }

        #endregion

    }
}
