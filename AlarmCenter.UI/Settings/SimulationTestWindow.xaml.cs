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

namespace AlarmCenter.UI.Settings
{
    /// <summary>
    /// SimulationTestWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SimulationTestWindow : Window
    {
        public delegate void InserDBEventHandler(object serder, EventArgs e);//声明一个插入新[报警事件]的事件的委托
        public event InserDBEventHandler SimulationInsertDatabase;//声明插入新[报警事件]的事件

        public SimulationTestWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //加载所有用户资料主机编号
            UserDAL userDAL = new UserDAL();
            foreach(string i in userDAL.GetaccountArray())
            {
                cbAccount.Items.Add(i);
            }

            //加载防区/用户编号
            bool flag = true;
            for (int i = 0; i < 999; i++)
                cbZoneOrUser.Items.Add((i+1));

            //加载所有CID代码
            CIDDAL cidDAL = new CIDDAL();
            foreach (CID cid in cidDAL.GetReportFormatArray("DRC_CONTACTID"))
            {
                //取奇数
                flag = !flag;
                if (flag)
                {
                    cbCIDCode.Items.Add(cid.CIDCode);
                }
            }

            //初始化所有可选项
            CID _cid = new CID();
            _cid.EventInformation = "紧急医疗";
            _cid.EventTpye = "紧急医疗报警";
            _cid.IsNewEvent = true;
            gridCID.DataContext = _cid;

            cbZoneOrUser.SelectedIndex = 0;
            if (cbAccount.Items != null)
            {
                cbAccount.SelectedIndex = 0;
            }
            if (cbCIDCode.Items != null)
            {
                cbCIDCode.SelectedIndex = 0;
                
            }
        }

        private void cbCIDCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 是否是新事件
            string isNewEvent;
            if (cbIsNewEvent.IsChecked == true)
            {
                isNewEvent = "E";
            }
            else
            {
                isNewEvent = "R";
            }

            CID cid = new CID();
            CIDDAL cidDAL = new CIDDAL();
            cid = cidDAL.GetEventInformation(isNewEvent, cbCIDCode.SelectedItem.ToString());
            tbEventInformation.Text = cid.EventInformation;
            tbEventTpye.Text = cid.EventTpye;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Event _event = new Event();
            _event.ID=Guid.NewGuid().ToString();
            _event.AlarmTime = DateTime.Now;
            _event.Account = cbAccount.SelectedItem.ToString();//主机编号
            _event.PartitionNumber ="01";//分区编号
            _event.ZoneNumber =cbZoneOrUser.SelectedItem.ToString();//防区编号

            //获取事件信息
            CIDDAL cidDAL = new CIDDAL();
            CID cid = new CID();
            // 是否是新事件
            string isNewEvent;
            if (cbIsNewEvent.IsChecked == true)
            {
                isNewEvent = "E";
            }
            else
            {
                isNewEvent = "R";
            }
            cid = cidDAL.GetEventInformation(isNewEvent, cbCIDCode.SelectedItem.ToString());
            _event.EventInfomation= cid.EventInformation;//获取事件类型
            _event.EventTpye = cid.EventTpye;//获取辅助信息
            
            //获取报警事件用户相关的资料
            UserDAL userDAL = new UserDAL();
            User user = new User();

            user = userDAL.GetUserInfomation(_event.Account, " 主机编号,用户名称,用户地址,主机类型,用户类型 ");
            _event.UserName = user.UserName;
            _event.Address = user.Address;

            _event.PanelType = user.PanelName;

            _event.UserType = user.UserType;

            //获取报警事件防区相关的资料
            ZoneDAL zoneDAL = new ZoneDAL();
            Zone zone = new Zone();
            zone = zoneDAL.GetZoneByAccountAddZoneNum(_event.Account, Convert.ToInt32(_event.ZoneNumber));
            _event.ZoneType = zone.ZoneType;
            _event.InstallSide = zone.InstallSide;
            _event.DetectorType = zone.DetectorType;

            _event.Classify = "模拟测试事件";//归类处理e.Classify
            _event.DataCode = "18" + _event.Account + isNewEvent + cbCIDCode.SelectedItem.ToString() + "01" + _event.ZoneNumber;//通讯代码e.DataCode = "";

            StrategiesDAL strategiesDAL = new StrategiesDAL();
            Strategies strategies = new Strategies();
            strategies = strategiesDAL.GetStrategiesByStrategiesName(cid.StrategiesName);
            _event.EventFontColor = strategies.EventFontColor;//事件字体颜色
            _event.EventBackgroundColor = strategies.EventBackgroundColor;//事件背景颜色

            _event.Operator = "admin";//当前操作员e.Operator = "";
            _event.MarkEvent = "";//处理内容e.Mark = "";
            _event.Side = "00";//站点编号e.Side = "";

            _event.TellNum = "";//来电号码e.TellNum = "";
            _event.TowLeverSide = "0";//二级站点编号e= "";


            EventDAL eventDal = new EventDAL();
            eventDal.Insert(_event);

            //插入新[报警事件]的事件
            if (SimulationInsertDatabase != null)
                SimulationInsertDatabase(this, new EventArgs());

            //弹出消息提示窗口
            if(strategies.NoticeType=="声音和窗口提示")
            {
                MainWindow.isNoMarkEvents.Add(_event);
                ShowWindow(_event);
            }
        }
        public void ShowWindow(Event _event)
        {
            //MessageWindow 是自定义的窗口消息框，width和height一定要赋值

            MainWindow.msgWindow.WindowStartupLocation = WindowStartupLocation.Manual;
            MainWindow.msgWindow.ShowInTaskbar = false;

            MainWindow.msgWindow.account = _event.Account;
            MainWindow.msgWindow.tbUserName.Text = _event.UserName;
            MainWindow.msgWindow.tbAddress.Text = _event.Address;
            MainWindow.msgWindow.tbEventTpye.Text = _event.EventTpye;
            MainWindow.msgWindow.tbEventInfomation.Text = _event.EventInfomation;
            MainWindow.msgWindow.tbZoneNumber.Text = _event.ZoneNumber;
            MainWindow.msgWindow.tbEventTime.Text = _event.AlarmTime.ToString();
            MainWindow.msgWindow.tbIsNoMarkEventCount.Text = MainWindow.isNoMarkEvents.Count.ToString();

            MainWindow.msgWindow.Left = SystemParameters.WorkArea.Width - MainWindow.msgWindow.Width;
            MainWindow.msgWindow.Top = SystemParameters.WorkArea.Height - MainWindow.msgWindow.Height;
            MainWindow.msgWindow.Show();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
