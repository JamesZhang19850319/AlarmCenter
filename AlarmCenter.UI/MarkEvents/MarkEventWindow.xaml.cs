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

namespace AlarmCenter.UI.MarkEvents
{
    /// <summary>
    /// MarkEventWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MarkEventWindow : Window
    {
        public delegate void InserDBEventHandler(object serder, EventArgs e);//声明一个插入新[报警事件]的事件的委托
        public event InserDBEventHandler markEventWindow;//声明插入新[报警事件]的事件

        public string account{set;get;}
        public string zoneNum { set; get; }

        public MarkEventWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //加载用户基本信息
            UserDAL userDAL = new UserDAL();
            User user = new User();
            user = userDAL.GetUserInfomationByAccount(account, "用户名称,用户地址,用户类型,主机编号,主机类型,安装日期,通讯格式,安装单位,安装人员,合同期限,收费标准");
            gridBasicInformation.DataContext = user;

            //加载联系人
            ContactsDAL contactsDAL = new ContactsDAL();
            dataGridContacts.ItemsSource = contactsDAL.GetContactsArray(account);

            //加载防区资料
            ZoneDAL zoneDAL = new ZoneDAL();
            Zone zone = new Zone();
            zone=zoneDAL.GetZoneByAccountAddZoneNum(account, Convert.ToInt32(zoneNum));
            tbZoneNum.Text = zoneNum;
            tbInstallSide.Text = zone.InstallSide;
            tbZoneType.Text = zone.ZoneType;
            tbDetectorType.Text = zone.DetectorType;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //将处理结果写入数据库
            MainWindow.isNoMarkEvents.Last().MarkEvent=tbMarkEvent.Text;
            if (rbMarkEvent1.IsChecked == true) { MainWindow.isNoMarkEvents.Last().Classify = "真实报警"; }
            if (rbMarkEvent2.IsChecked == true) { MainWindow.isNoMarkEvents.Last().Classify = "环境误报"; }
            if (rbMarkEvent3.IsChecked == true) { MainWindow.isNoMarkEvents.Last().Classify = "人为误报"; }
            if (rbMarkEvent4.IsChecked == true) { MainWindow.isNoMarkEvents.Last().Classify = "系统误报"; }
            if (rbMarkEvent5.IsChecked == true) { MainWindow.isNoMarkEvents.Last().Classify = "暂未处理"; }

            EventDAL eventDal = new EventDAL();
            eventDal.Update(MainWindow.isNoMarkEvents.Last());

            //处理最近一条事件后，从未处理事件集合中清楚。
            MainWindow.isNoMarkEvents.Remove(MainWindow.isNoMarkEvents.Last());

            //如果是最后一个未处理事件,点击确定后未处理消息提示窗口关闭
            if (MainWindow.isNoMarkEvents.Count <= 0)
            {
                MainWindow.msgWindow.Visibility = Visibility.Collapsed;
            }
            else
            {
                MainWindow.msgWindow.account = MainWindow.isNoMarkEvents.Last().Account;
                MainWindow.msgWindow.tbUserName.Text = MainWindow.isNoMarkEvents.Last().UserName;
                MainWindow.msgWindow.tbAddress.Text = MainWindow.isNoMarkEvents.Last().Address;
                MainWindow.msgWindow.tbEventTpye.Text = MainWindow.isNoMarkEvents.Last().EventTpye;
                MainWindow.msgWindow.tbEventInfomation.Text = MainWindow.isNoMarkEvents.Last().EventInfomation;
                MainWindow.msgWindow.tbZoneNumber.Text = MainWindow.isNoMarkEvents.Last().ZoneNumber;
                MainWindow.msgWindow.tbEventTime.Text = MainWindow.isNoMarkEvents.Last().AlarmTime.ToString();
                //更新消息窗口未处理事件数量
                MainWindow.msgWindow.tbIsNoMarkEventCount.Text = MainWindow.isNoMarkEvents.Count.ToString();
            }

            //插入新[报警事件]的事件
            if (markEventWindow != null)
                markEventWindow(this, new EventArgs());

            this.Visibility = Visibility.Collapsed;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
