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
            if (!Directory.Exists(AppLog))
                Directory.CreateDirectory(AppLog);
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
                NetTag = Convert.ToInt16(IniFile.IniReadValue("SysConfig", "NetTag", AppConfig));
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            txtConfigPath.Text = ConfigPath;
            txtServerName.Text = ServerName;

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

            if (databaseip != iplist[0])
            {
                IniFile.IniWriteValue("DataBaseServer", "IP", iplist[0], ConfigPath);
                UpdateMsg("数据库IP自动变更," + databaseip + "->" + iplist[0]);
            }
            if (ctrlserverip != iplist[0])
            {
                IniFile.IniWriteValue("CtrlServer", "IP", iplist[0], ConfigPath);
                UpdateMsg("服务器IP自动变更," + ctrlserverip  + "->" + iplist[0]);
            }
            if (ftpip != iplist[0])
            {
                IniFile.IniWriteValue("FtpServer", "IP", iplist[0], ConfigPath);
                UpdateMsg("FTP IP自动变更," + ftpip + "->" + iplist[0]);
            }




            



        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //AutoChangeIP();
            MAXINTERVAL = Convert.ToInt16(txtSec.Text.Trim());
            timer1.Interval = 1000;
            timer1.Start();
            txtSec.ReadOnly = true;
            txtConfigPath.Enabled = false;
            txtServerName.Enabled = false;
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            iInterval = Convert.ToInt16(txtSec.Text.Trim());
            iInterval--;
            txtSec.Text = iInterval.ToString();
            if (iInterval == 0)
            {
                AutoChangeIP();
                txtSec.Text = MAXINTERVAL.ToString();
                
            }
            timer1.Start();
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
        }

        private void btnDebugNet_Click(object sender, EventArgs e)
        {
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
                }
                else
                {
                    comboNetIpList.Items.Add("网卡1的IP:" + iplist[0]);
                    comboNetIpList.SelectedIndex = 0;
                }

       


            }

        }

        private void comboNetIpList_SelectedIndexChanged(object sender, EventArgs e)
        {
            NetTag = comboNetIpList.SelectedIndex;
            txtNetTag.Text = comboNetIpList.SelectedIndex.ToString();
            IniFile.IniWriteValue("SysConfig", "NetTag", NetTag.ToString(), AppConfig);
        }


    }
}
