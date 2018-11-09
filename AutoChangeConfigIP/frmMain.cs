using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Edward;
using System.Net;

namespace AutoChangeConfigIP
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }


        private static string AppFolder = Application.StartupPath + @"\AutoChangeConfigIP";
        private static string AppLog = AppFolder + @"\Log";
        private static string AppConfig = AppFolder + @"\SysConfig.ini";
        private static string ConfigPath = @"D:\RecordColl\RecordCollCfg.ini";
        private static string ServerName = "";
        private static int NetTag = -1;

        int iInterval = 10;
        int MAXINTERVAL = 10;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textbox"></param>
        private void OpenFile(TextBox textbox)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "配置文件|*.ini|全部文件|*.*";
            if (open.ShowDialog() == DialogResult.OK)
                textbox.Text  = open.FileName;

        }




        /// <summary>
        /// 
        /// </summary>
        private void CreateFolder()
        {
            if (!Directory.Exists(AppFolder))
                Directory.CreateDirectory(AppFolder);
            //if (!Directory.Exists(AppLog))
            //    Directory.CreateDirectory(AppLog);
        }


        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            this.Text = "自动更改采集站服务器IP,Ver:" + Application.ProductVersion;
            txtConfigPath.SetWatermark("双击此处选择采集站配置档案");
            txtServerName.SetWatermark("请在此输入采集站服务器的电脑名");  
            CreateFolder();
            CreateIni();
            ReadIni();
            LoadData();
        }


        /// <summary>
        /// 
        /// </summary>
        private void CreateIni()
        {
            if (!File.Exists(AppConfig))
            {
                IniFile.CreateIniFile(AppConfig);
                IniFile.iniFilePathValue = AppConfig;
                IniFile.IniWriteValue("SysConfig", "ConfigPath", ConfigPath, AppConfig);
                IniFile.IniWriteValue("SysConfig", "ServerName", ServerName, AppConfig);
                IniFile.IniWriteValue("SysConfig", "NetTag",NetTag.ToString () , AppConfig);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private void ReadIni()
        {
           // IniFile.iniFilePathValue = AppConfig;
            if (File.Exists(AppConfig))
            {
                ConfigPath = IniFile.IniReadValue("SysConfig", "ConfigPath", AppConfig);
                ServerName = IniFile.IniReadValue("SysConfig", "ServerName", AppConfig);
                try
                {
                    NetTag = Convert.ToInt16(IniFile.IniReadValue("SysConfig", "NetTag", AppConfig));
                }
                catch (Exception)
                {

                    NetTag = -1;
                }
              
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            txtConfigPath.Text = ConfigPath;
            txtServerName.Text = ServerName;
            txtNetTag.Text = NetTag.ToString();

        }


        #region 枚举


        public enum IPType
        {
            IPV4,
            IPV6
        }
        #endregion


        #region 获取IP

        /// <summary>
        /// 获取IP地址,本机IP地址hostname=dns.gethostname(),返回一个IP list
        /// </summary>
        /// <param name="hostname">hostname</param>
        /// <returns>返回一个字符串类型的ip list</returns>
        public static List<string> getIP(string hostname)
        {
            List<string> iplist = new List<string>();
            System.Net.IPAddress[] addressList = Dns.GetHostAddresses(hostname);//会返回所有地址，包括IPv4和IPv6   
            foreach (IPAddress ip in addressList)
            {
                iplist.Add(ip.ToString());
            }
            return iplist;
        }

        /// <summary>
        /// 获取IP地址,本机IP地址hostname=dns.gethostname(),返回一个IP list
        /// </summary>
        /// <param name="hostname">hostname</param>
        /// <param name="iptype">ip地址的类型，IPV4,IPV6</param>
        /// <returns>返回一个字符串类型的ip list</returns>
        public static List<string> getIP(string hostname, IPType iptype,out string exmsg)
        {
            exmsg = string.Empty;
            List<string> iplist = new List<string>();
            try
            {
                IPAddress[] addressList = Dns.GetHostAddresses(hostname);
                foreach (IPAddress ip in addressList)
                {
                    if (iptype == IPType.IPV4)
                    {
                        if (ip.ToString().Contains("."))
                            iplist.Add(ip.ToString());
                    }
                    if (iptype == IPType.IPV6)
                    {
                        if (!ip.ToString().Contains("."))
                            iplist.Add(ip.ToString());
                    }
                }
                exmsg = "OK";
            }
            catch (Exception ex)
            {

                exmsg = ex.Message;
            }
          
   
            return iplist;
        }

        #endregion


        private void UpdateMsg(string msg)
        {
            
            lstMsg.Items.Add(DateTime.Now.ToString("hh:mm:ss") + "->" + @msg);
            lstMsg.SelectedIndex = lstMsg.Items.Count - 1;
        }



        private void txtConfigPath_DoubleClick(object sender, EventArgs e)
        {
            OpenFile(txtConfigPath);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void txtConfigPath_TextChanged(object sender, EventArgs e)
        {
            ConfigPath = txtConfigPath.Text.Trim();
            IniFile.IniWriteValue("SysConfig", "ConfigPath", ConfigPath, AppConfig);
        }

        private void txtServerName_TextChanged(object sender, EventArgs e)
        {
            ServerName = txtServerName.Text.Trim();
            IniFile.IniWriteValue("SysConfig", "ServerName", ServerName, AppConfig);
        }



        private void AutoChangeIP()
        {
            if (!File.Exists(ConfigPath))
            {
                MessageBox.Show(ConfigPath + "不存在,请重新设置.", "Not Find File", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                txtConfigPath.SelectAll();
                txtConfigPath.Focus();
                UpdateMsg(ConfigPath + "不存在,请重新设置.");

                return;
            }
            string databaseip = IniFile.IniReadValue("DataBaseServer", "IP", ConfigPath);
            UpdateMsg("DataBaseIP:" + databaseip);
            string ctrlserverip = IniFile.IniReadValue("CtrlServer", "IP", ConfigPath);
            UpdateMsg("CtrlServer" + ctrlserverip);
            string ftpip = IniFile.IniReadValue("FtpServer", "IP", ConfigPath);
            UpdateMsg("FtpServer" + ftpip);

            string exmsg = string.Empty;
            List<string> iplist = getIP(ServerName, IPType.IPV4, out exmsg);
            if (exmsg == "OK")
            {
                foreach (string item in iplist)
                {
                    UpdateMsg("服务器" + ServerName + "的IP:" + item);
                }
            }
            else
            {
                UpdateMsg("服务器" + ServerName +","+ exmsg);
            }


            if (NetTag <= iplist.Count)
            {
                if (Other.pingIp(iplist[NetTag - 1]))
                {
                    if (databaseip != iplist[NetTag - 1])
                    {
                        IniFile.IniWriteValue("DataBaseServer", "IP", iplist[NetTag - 1], ConfigPath);
                        UpdateMsg("数据库IP自动变更," + databaseip + "->" + iplist[NetTag - 1]);
                    }
                    if (ctrlserverip != iplist[NetTag - 1])
                    {
                        IniFile.IniWriteValue("CtrlServer", "IP", iplist[NetTag - 1], ConfigPath);
                        UpdateMsg("服务器IP自动变更," + ctrlserverip + "->" + iplist[NetTag - 1]);
                    }
                    if (ftpip != iplist[NetTag - 1])
                    {
                        IniFile.IniWriteValue("FtpServer", "IP", iplist[NetTag - 1], ConfigPath);
                        UpdateMsg("FTP IP自动变更," + ftpip + "->" + iplist[NetTag - 1]);
                    }
                }
                else
                {
                    UpdateMsg("设置的服务器的IP为:" + iplist[NetTag - 1] + ",但无法ping通,请确认.");
                }

            }




            



        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!bgwChangeIP.IsBusy)
            {
                //PressStart();
                bgwChangeIP.RunWorkerAsync();
            }

        }




        private void PressStart()
        {
            string exmsg = string.Empty;

          this.Invoke((EventHandler)(delegate
          {
              UpdateMsg("正在测试网卡...");
              if (NetTag == 0)
              {
                  MessageBox.Show("未保证正确获取IP地址，请先点击'测试网卡',侦测服务器网卡地址", "Check First", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                  return;
              }
              else
              {
                
                  List<string> iplist = getIP(ServerName, IPType.IPV4, out exmsg);
                  comboNetIpList.Items.Clear();
                  if (exmsg == "OK")
                  {
                      for (int i = 0; i < iplist.Count; i++)
                      {
                          comboNetIpList.Items.Add("网卡" + (i + 1) + "的IP:" + iplist[i]);
                      }

                      if (NetTag > iplist.Count)
                      {
                          if (iplist.Count > 1)
                          {
                              MessageBox.Show("NetTag:" + NetTag + "大于服务器网卡数,请重新设置", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                              comboNetIpList.Focus();
                              return;
                          }
                          else
                          {
                              UpdateMsg("NetTag:" + NetTag + "大于实际服务器网卡数且服务器网卡数为1,自动修正.");
                              comboNetIpList.SelectedIndex = 0;
                          }
                      }
                      else
                          comboNetIpList.SelectedIndex = NetTag - 1;
                  }
                  else
                  {
                      UpdateMsg("服务器:" + ServerName + "," + exmsg);
                      MessageBox.Show(exmsg);
                      return;
                  }

              }

          }));


          this.Invoke((EventHandler)(delegate
             {
                 if (exmsg == "OK")
                 {
                     timer1.Interval = 1000;
                     timer1.Start();
                     UpdateMsg("正在倒计时等待...");
                     txtSec.ReadOnly = true;
                     txtConfigPath.Enabled = false;
                     txtServerName.Enabled = false;
                     btnStart.Enabled = false;
                     btnStop.Enabled = true;
                     comboNetIpList.Enabled = false;
                     btnDebugNet.Enabled = false;
                 }
             }));

            //
            // MAXINTERVAL = Convert.ToInt16(txtSec.Text.Trim());

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            iInterval = Convert.ToInt16(txtSec.Text.Trim());
            iInterval--;
            txtSec.Text = iInterval.ToString();
            if (iInterval == 0)
            {
                timer1.Stop();
                AutoChangeIP();
                txtSec.Text = MAXINTERVAL.ToString();
                timer1.Start();
            }

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
           // timer1.Interval = 1000;
            timer1.Stop();
            txtSec.ReadOnly = false;
            txtConfigPath.Enabled = true;
            txtServerName.Enabled = true;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            comboNetIpList.Enabled = true;
            btnDebugNet.Enabled = true;
        }

        private void btnDebugNet_Click(object sender, EventArgs e)
        {

                if (!bgwCheckServer.IsBusy)
                {
                    UpdateMsg("正在测试网卡...");
                    bgwCheckServer.RunWorkerAsync();
                }





        }




        private void CheckServer()
        {

            this.Invoke((EventHandler)(delegate
              {
                  if (string.IsNullOrEmpty(txtServerName.Text.Trim()))
                  {
                      MessageBox.Show("服务器名称不能为空,请重新设置", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                      txtServerName.Focus();
                      return;
                  }

                  string exmsg = string.Empty;
                  List<string> iplist = getIP(ServerName, IPType.IPV4, out exmsg);
                  comboNetIpList.Items.Clear();
                  if (exmsg == "OK")
                  {
                      if (iplist.Count > 1)
                      {
                          for (int i = 0; i < iplist.Count; i++)
                          {
                              comboNetIpList.Items.Add("网卡" + (i + 1) + "的IP:" + iplist[i]);
                          }
                          MessageBox.Show("服务器可能存在多张网卡(含虚拟网卡)，请选择采集站需要使用的服务器网卡IP地址", "Muti Net", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                          comboNetIpList.Focus();
                      }
                      else
                      {
                          comboNetIpList.Items.Add("网卡1的IP:" + iplist[0]);
                          comboNetIpList.SelectedIndex = 0;
                          if (Other.pingIp(iplist[0]))
                              UpdateMsg("服务器网卡数为1,已自动设置.");
                          else
                          {
                              UpdateMsg("服务器网卡数为1,但无法ping通其IP:" + iplist[0] + ",请重新确认.");
                              MessageBox.Show("服务器网卡数为1,但无法ping通其IP:" + iplist[0] + ",请重新确认.", "Ping IP Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                          }
                          


                      }
                  }
                  else
                  {
                      UpdateMsg("服务器:" + ServerName + "," + exmsg);
                      MessageBox.Show(exmsg);
                  }
              }));

        }

        private void comboNetIpList_SelectedIndexChanged(object sender, EventArgs e)
        {
            NetTag = comboNetIpList.SelectedIndex + 1;
            txtNetTag.Text = NetTag.ToString();
            IniFile.IniWriteValue("SysConfig", "NetTag", NetTag.ToString(), AppConfig);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否确认退出软件,退出点击是(Y),不退出点击否(N)?", "Exit?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
            else
                e.Cancel = true;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否确认退出软件,退出点击是(Y),不退出点击否(N)?", "Exit?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Environment.Exit(0);
            }


        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new frmHelp();
            f.ShowDialog();
        }

        private void bgwCheckServer_DoWork(object sender, DoWorkEventArgs e)
        {
            CheckServer();
        }

        private void bgwChangeIP_DoWork(object sender, DoWorkEventArgs e)
        {
            PressStart();
        }


    }
}
