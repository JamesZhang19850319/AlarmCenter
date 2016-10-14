using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AlarmCenter.DAL;
using AlarmCenter.DAL;
using System.Security.Cryptography;

namespace AlarmCenter.UI
{
    /// <summary>
    /// StartWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = tbUserName.Text;
            string password = pbPassword.Password;
            SystemManager sm = new SystemManagerDAL().GetByUserName(username);
            if (sm == null)
            {
                MessageBox.Show("用户名或者密码错误！");
            }
            else
            {
                string dbMd5 = sm.Password; //数据库中存储的密码值
                string mymd5 = password;
                if (dbMd5 == mymd5)
                {
                    //MessageBox.Show("登录成功");                  
                    MainWindow mainwindow = new MainWindow();
                    this.Visibility=Visibility.Hidden;
                    mainwindow.Show();
                    
                }
                else
                {
                    MessageBox.Show("用户名或者密码错误！");
                }
            }
        }
        public static string GetMD5(string sDataIn)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytValue, bytHash;
            bytValue = System.Text.Encoding.UTF8.GetBytes(sDataIn);
            bytHash = md5.ComputeHash(bytValue);
            md5.Clear();
            string sTemp = "";
            for (int i = 0; i < bytHash.Length; i++)
            {
                sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
            }
            return sTemp.ToLower();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbUserName.Text="admin";
            pbPassword.Password="admin";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
