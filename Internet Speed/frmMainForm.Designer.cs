namespace Internet_Speed
{
    partial class frmMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblByteSent = new System.Windows.Forms.Label();
            this.lblByteReceived = new System.Windows.Forms.Label();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.lblDLSpeed = new System.Windows.Forms.Label();
            this.lblULSpeed = new System.Windows.Forms.Label();
            this.lblDownLoad = new System.Windows.Forms.Label();
            this.lblUpload = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPing3 = new System.Windows.Forms.Label();
            this.lblPing2 = new System.Windows.Forms.Label();
            this.lblPing1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPingTime3 = new System.Windows.Forms.Label();
            this.lblPingTime2 = new System.Windows.Forms.Label();
            this.lblPingTime1 = new System.Windows.Forms.Label();
            this.lblUltilization = new System.Windows.Forms.Label();
            this.grbInterface = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cbbInterfaces = new System.Windows.Forms.ComboBox();
            this.bgwPinger1 = new System.ComponentModel.BackgroundWorker();
            this.bgwPinger2 = new System.ComponentModel.BackgroundWorker();
            this.bgwPinger3 = new System.ComponentModel.BackgroundWorker();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsTrayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changePingTargetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.grbInterface.SuspendLayout();
            this.cmsTrayMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(17, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Link Speed :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSpeed
            // 
            this.lblSpeed.Location = new System.Drawing.Point(98, 16);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(75, 23);
            this.lblSpeed.TabIndex = 1;
            this.lblSpeed.Text = "900.1 Mbps";
            this.lblSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(17, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Uploaded :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(17, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "Downloaded :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblByteSent
            // 
            this.lblByteSent.Location = new System.Drawing.Point(98, 62);
            this.lblByteSent.Name = "lblByteSent";
            this.lblByteSent.Size = new System.Drawing.Size(75, 23);
            this.lblByteSent.TabIndex = 4;
            this.lblByteSent.Text = "9001 MB";
            this.lblByteSent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblByteReceived
            // 
            this.lblByteReceived.Location = new System.Drawing.Point(98, 39);
            this.lblByteReceived.Name = "lblByteReceived";
            this.lblByteReceived.Size = new System.Drawing.Size(75, 23);
            this.lblByteReceived.TabIndex = 5;
            this.lblByteReceived.Text = "9001 MB";
            this.lblByteReceived.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Enabled = true;
            this.tmrRefresh.Interval = 1000;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // lblDLSpeed
            // 
            this.lblDLSpeed.Location = new System.Drawing.Point(179, 39);
            this.lblDLSpeed.Name = "lblDLSpeed";
            this.lblDLSpeed.Size = new System.Drawing.Size(75, 23);
            this.lblDLSpeed.TabIndex = 6;
            this.lblDLSpeed.Text = "DL Speed :";
            this.lblDLSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblULSpeed
            // 
            this.lblULSpeed.Location = new System.Drawing.Point(179, 62);
            this.lblULSpeed.Name = "lblULSpeed";
            this.lblULSpeed.Size = new System.Drawing.Size(75, 23);
            this.lblULSpeed.TabIndex = 7;
            this.lblULSpeed.Text = "UL Speed :";
            this.lblULSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDownLoad
            // 
            this.lblDownLoad.Location = new System.Drawing.Point(260, 39);
            this.lblDownLoad.Name = "lblDownLoad";
            this.lblDownLoad.Size = new System.Drawing.Size(75, 23);
            this.lblDownLoad.TabIndex = 8;
            this.lblDownLoad.Text = "9001 KB/s";
            this.lblDownLoad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUpload
            // 
            this.lblUpload.Location = new System.Drawing.Point(260, 62);
            this.lblUpload.Name = "lblUpload";
            this.lblUpload.Size = new System.Drawing.Size(75, 23);
            this.lblUpload.TabIndex = 9;
            this.lblUpload.Text = "9001 KB/s";
            this.lblUpload.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblPing3);
            this.groupBox1.Controls.Add(this.lblPing2);
            this.groupBox1.Controls.Add(this.lblPing1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblUpload);
            this.groupBox1.Controls.Add(this.lblPingTime3);
            this.groupBox1.Controls.Add(this.lblPingTime2);
            this.groupBox1.Controls.Add(this.lblPingTime1);
            this.groupBox1.Controls.Add(this.lblUltilization);
            this.groupBox1.Controls.Add(this.lblSpeed);
            this.groupBox1.Controls.Add(this.lblDownLoad);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblULSpeed);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblDLSpeed);
            this.groupBox1.Controls.Add(this.lblByteSent);
            this.groupBox1.Controls.Add(this.lblByteReceived);
            this.groupBox1.Location = new System.Drawing.Point(12, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(495, 98);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Activity";
            // 
            // lblPing3
            // 
            this.lblPing3.Location = new System.Drawing.Point(341, 62);
            this.lblPing3.Name = "lblPing3";
            this.lblPing3.Size = new System.Drawing.Size(50, 23);
            this.lblPing3.TabIndex = 0;
            this.lblPing3.Text = "Ping :";
            this.lblPing3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPing2
            // 
            this.lblPing2.Location = new System.Drawing.Point(341, 39);
            this.lblPing2.Name = "lblPing2";
            this.lblPing2.Size = new System.Drawing.Size(50, 23);
            this.lblPing2.TabIndex = 0;
            this.lblPing2.Text = "Ping :";
            this.lblPing2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPing1
            // 
            this.lblPing1.Location = new System.Drawing.Point(341, 16);
            this.lblPing1.Name = "lblPing1";
            this.lblPing1.Size = new System.Drawing.Size(50, 23);
            this.lblPing1.TabIndex = 0;
            this.lblPing1.Text = "Ping :";
            this.lblPing1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(179, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 23);
            this.label7.TabIndex = 0;
            this.label7.Text = "Ultilization :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPingTime3
            // 
            this.lblPingTime3.Location = new System.Drawing.Point(397, 62);
            this.lblPingTime3.Name = "lblPingTime3";
            this.lblPingTime3.Size = new System.Drawing.Size(75, 23);
            this.lblPingTime3.TabIndex = 1;
            this.lblPingTime3.Text = "0 ms";
            this.lblPingTime3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPingTime2
            // 
            this.lblPingTime2.Location = new System.Drawing.Point(397, 39);
            this.lblPingTime2.Name = "lblPingTime2";
            this.lblPingTime2.Size = new System.Drawing.Size(75, 23);
            this.lblPingTime2.TabIndex = 1;
            this.lblPingTime2.Text = "0 ms";
            this.lblPingTime2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPingTime1
            // 
            this.lblPingTime1.Location = new System.Drawing.Point(397, 16);
            this.lblPingTime1.Name = "lblPingTime1";
            this.lblPingTime1.Size = new System.Drawing.Size(75, 23);
            this.lblPingTime1.TabIndex = 1;
            this.lblPingTime1.Text = "0 ms";
            this.lblPingTime1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUltilization
            // 
            this.lblUltilization.Location = new System.Drawing.Point(260, 16);
            this.lblUltilization.Name = "lblUltilization";
            this.lblUltilization.Size = new System.Drawing.Size(75, 23);
            this.lblUltilization.TabIndex = 1;
            this.lblUltilization.Text = "90.01 %";
            this.lblUltilization.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grbInterface
            // 
            this.grbInterface.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grbInterface.Controls.Add(this.btnRefresh);
            this.grbInterface.Controls.Add(this.cbbInterfaces);
            this.grbInterface.Location = new System.Drawing.Point(12, 12);
            this.grbInterface.Name = "grbInterface";
            this.grbInterface.Size = new System.Drawing.Size(495, 49);
            this.grbInterface.TabIndex = 11;
            this.grbInterface.TabStop = false;
            this.grbInterface.Text = "Interface";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(464, 17);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(25, 26);
            this.btnRefresh.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnRefresh, "Left click to refresh interface list. Right click to stick/unstick current interf" +
                    "ace.");
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.btnRefresh.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnRefresh_MouseDown);
            this.btnRefresh.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnRefresh_MouseUp);
            // 
            // cbbInterfaces
            // 
            this.cbbInterfaces.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbbInterfaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbInterfaces.FormattingEnabled = true;
            this.cbbInterfaces.Location = new System.Drawing.Point(20, 19);
            this.cbbInterfaces.Name = "cbbInterfaces";
            this.cbbInterfaces.Size = new System.Drawing.Size(438, 21);
            this.cbbInterfaces.TabIndex = 0;
            this.toolTip1.SetToolTip(this.cbbInterfaces, "Currently active network interface list");
            this.cbbInterfaces.SelectedIndexChanged += new System.EventHandler(this.cbbInterfaces_SelectedIndexChanged);
            // 
            // bgwPinger1
            // 
            this.bgwPinger1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwPinger1_DoWork);
            // 
            // bgwPinger2
            // 
            this.bgwPinger2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwPinger2_DoWork);
            // 
            // bgwPinger3
            // 
            this.bgwPinger3.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwPinger3_DoWork);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.cmsTrayMenu;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // cmsTrayMenu
            // 
            this.cmsTrayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changePingTargetToolStripMenuItem,
            this.restoreToolStripMenuItem,
            this.trayToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.showPingToolStripMenuItem});
            this.cmsTrayMenu.Name = "cmsTrayMenu";
            this.cmsTrayMenu.Size = new System.Drawing.Size(235, 136);
            // 
            // changePingTargetToolStripMenuItem
            // 
            this.changePingTargetToolStripMenuItem.Name = "changePingTargetToolStripMenuItem";
            this.changePingTargetToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.changePingTargetToolStripMenuItem.Text = "Change ping target...";
            this.changePingTargetToolStripMenuItem.Click += new System.EventHandler(this.changePingTargetToolStripMenuItem_Click);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.restoreToolStripMenuItem.Text = "Restore";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // trayToolStripMenuItem
            // 
            this.trayToolStripMenuItem.Name = "trayToolStripMenuItem";
            this.trayToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.trayToolStripMenuItem.Text = "Keep Tray Icon";
            this.trayToolStripMenuItem.Click += new System.EventHandler(this.trayToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // showPingToolStripMenuItem
            // 
            this.showPingToolStripMenuItem.CheckOnClick = true;
            this.showPingToolStripMenuItem.Name = "showPingToolStripMenuItem";
            this.showPingToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.showPingToolStripMenuItem.Text = "Show Ping info in Tray Icon";
            // 
            // frmMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 177);
            this.Controls.Add(this.grbInterface);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Internet Speed";
            this.Resize += new System.EventHandler(this.frmMainForm_Resize);
            this.groupBox1.ResumeLayout(false);
            this.grbInterface.ResumeLayout(false);
            this.cmsTrayMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblByteSent;
        private System.Windows.Forms.Label lblByteReceived;
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.Label lblDLSpeed;
        private System.Windows.Forms.Label lblULSpeed;
        private System.Windows.Forms.Label lblDownLoad;
        private System.Windows.Forms.Label lblUpload;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grbInterface;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cbbInterfaces;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblUltilization;
        private System.Windows.Forms.Label lblPing1;
        private System.Windows.Forms.Label lblPingTime1;
        private System.Windows.Forms.Label lblPing3;
        private System.Windows.Forms.Label lblPing2;
        private System.Windows.Forms.Label lblPingTime3;
        private System.Windows.Forms.Label lblPingTime2;
        private System.ComponentModel.BackgroundWorker bgwPinger1;
        private System.ComponentModel.BackgroundWorker bgwPinger2;
        private System.ComponentModel.BackgroundWorker bgwPinger3;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip cmsTrayMenu;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePingTargetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPingToolStripMenuItem;
    }
}

