﻿using System;
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
        public static List<string> getIP(string hostname, IPType iptype)
        {
            List<string> iplist = new List<string>();
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
            return iplist;
        }

        #endregion


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
    }
}
