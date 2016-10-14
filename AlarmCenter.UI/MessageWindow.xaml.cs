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
using AlarmCenter.UI.MarkEvents;
using AlarmCenter.DAL;

namespace AlarmCenter.UI
{
    /// <summary>
    /// MessageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MessageWindow : Window
    {
        public string account { set; get; }
        

        public MessageWindow()
        {
            InitializeComponent();
        }

        private void btnMarkEvent_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.markWindow.account = account;
            MainWindow.markWindow.zoneNum = tbZoneNumber.Text;
            MainWindow.markWindow.tbEventTpye.Text = tbEventTpye.Text;
            MainWindow.markWindow.tbEventInfomation.Text = tbEventInfomation.Text;

            MainWindow.markWindow.ShowDialog();

        }

        private void btnStopSound_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnToMap_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}