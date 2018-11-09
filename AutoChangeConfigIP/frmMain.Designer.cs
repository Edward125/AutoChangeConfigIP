namespace AutoChangeConfigIP
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNetTag = new System.Windows.Forms.TextBox();
            this.txtConfigPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDebugNet = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboNetIpList = new System.Windows.Forms.ComboBox();
            this.txtSec = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstMsg = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bgwCheckServer = new System.ComponentModel.BackgroundWorker();
            this.bgwChangeIP = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNetTag);
            this.groupBox1.Controls.Add(this.txtConfigPath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnDebugNet);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboNetIpList);
            this.groupBox1.Controls.Add(this.txtSec);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.txtServerName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(472, 137);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtNetTag
            // 
            this.txtNetTag.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetTag.Location = new System.Drawing.Point(408, 92);
            this.txtNetTag.Name = "txtNetTag";
            this.txtNetTag.ReadOnly = true;
            this.txtNetTag.Size = new System.Drawing.Size(45, 26);
            this.txtNetTag.TabIndex = 10;
            this.txtNetTag.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtConfigPath
            // 
            this.txtConfigPath.Location = new System.Drawing.Point(87, 99);
            this.txtConfigPath.Name = "txtConfigPath";
            this.txtConfigPath.Size = new System.Drawing.Size(229, 21);
            this.txtConfigPath.TabIndex = 2;
            this.txtConfigPath.TextChanged += new System.EventHandler(this.txtConfigPath_TextChanged);
            this.txtConfigPath.DoubleClick += new System.EventHandler(this.txtConfigPath_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "配置档地址";
            // 
            // btnDebugNet
            // 
            this.btnDebugNet.Location = new System.Drawing.Point(326, 20);
            this.btnDebugNet.Name = "btnDebugNet";
            this.btnDebugNet.Size = new System.Drawing.Size(75, 30);
            this.btnDebugNet.TabIndex = 3;
            this.btnDebugNet.Text = "测试网卡";
            this.btnDebugNet.UseVisualStyleBackColor = true;
            this.btnDebugNet.Click += new System.EventHandler(this.btnDebugNet_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(326, 56);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 32);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "开始监听";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "网卡列表";
            // 
            // comboNetIpList
            // 
            this.comboNetIpList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboNetIpList.FormattingEnabled = true;
            this.comboNetIpList.Location = new System.Drawing.Point(87, 62);
            this.comboNetIpList.Name = "comboNetIpList";
            this.comboNetIpList.Size = new System.Drawing.Size(229, 20);
            this.comboNetIpList.TabIndex = 1;
            this.comboNetIpList.SelectedIndexChanged += new System.EventHandler(this.comboNetIpList_SelectedIndexChanged);
            // 
            // txtSec
            // 
            this.txtSec.Font = new System.Drawing.Font("Calibri", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSec.Location = new System.Drawing.Point(409, 23);
            this.txtSec.Name = "txtSec";
            this.txtSec.ReadOnly = true;
            this.txtSec.Size = new System.Drawing.Size(45, 50);
            this.txtSec.TabIndex = 6;
            this.txtSec.Text = "10";
            this.txtSec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(326, 95);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 30);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "停止监听";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(87, 26);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(229, 21);
            this.txtServerName.TabIndex = 0;
            this.txtServerName.TextChanged += new System.EventHandler(this.txtServerName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "服务器电脑名";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstMsg);
            this.groupBox2.Location = new System.Drawing.Point(12, 171);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(472, 392);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // lstMsg
            // 
            this.lstMsg.FormattingEnabled = true;
            this.lstMsg.ItemHeight = 12;
            this.lstMsg.Location = new System.Drawing.Point(6, 19);
            this.lstMsg.Name = "lstMsg";
            this.lstMsg.Size = new System.Drawing.Size(460, 364);
            this.lstMsg.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.帮助ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(498, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            this.帮助ToolStripMenuItem.Click += new System.EventHandler(this.帮助ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // bgwCheckServer
            // 
            this.bgwCheckServer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwCheckServer_DoWork);
            // 
            // bgwChangeIP
            // 
            this.bgwChangeIP.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwChangeIP_DoWork);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 570);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.TextBox txtConfigPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListBox lstMsg;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txtSec;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboNetIpList;
        private System.Windows.Forms.Button btnDebugNet;
        private System.Windows.Forms.TextBox txtNetTag;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker bgwCheckServer;
        private System.ComponentModel.BackgroundWorker bgwChangeIP;
    }
}

