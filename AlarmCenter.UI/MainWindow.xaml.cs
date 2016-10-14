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
using AlarmCenter.UI.SystemManagers;
using AlarmCenter.UI.Users;
//using AlarmCenter.UI.Reports;
using AlarmCenter.DAL;
using AlarmCenter.UI.Events;
using AlarmCenter.UI.SystemLogs;
using AlarmCenter.UI.Messages;
using AlarmCenter.UI.Videos;
using AlarmCenter.UI.Records;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock.Layout.Serialization;
using System.Data;
using AlarmCenter.UI.Settings;
using System.IO.Ports;
using AlarmCenter.UI.SerialPortDatas;
using System.Threading;
using System.Windows.Threading;
using AlarmCenter.UI.ReporFormats;
using System.ComponentModel;
using AlarmCenter.UI.MarkEvents;

namespace AlarmCenter.UI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public int pageIndexUser = 1;
        public int pageCountUser;
        public const int pageSizeUser = 100;

        public int pageIndexEvent = 1;
        public int pageCountEvent;
        public const int pageSizeEvent = 100;

        SerialPortData data;
        byte[] ACK = new byte[1] { 0x06 };

        DispatcherTimer refreshEventList = new DispatcherTimer();
        bool timerFlag=false ;

        Thread oThread;

        AnalyseSerialPortData analyseSerialPortData= new AnalyseSerialPortData();
         
        public static SerialPort _SerialPort1 = new SerialPort();
        public static SerialPort _SerialPort2 = new SerialPort();
        public static SerialPort _SerialPort3 = new SerialPort();
        public static SerialPort _SerialPort4 = new SerialPort();
        public static SerialPort _SerialPort5 = new SerialPort();
        public static SerialPort _SerialPort6 = new SerialPort();
        public static SerialPort _SerialPort7 = new SerialPort();
        public static SerialPort _SerialPort8 = new SerialPort();

        public static string _SerialPort1ComNum;
        public static string _SerialPort2ComNum;
        public static string _SerialPort3ComNum;
        public static string _SerialPort4ComNum;
        public static string _SerialPort5ComNum;
        public static string _SerialPort6ComNum;
        public static string _SerialPort7ComNum;
        public static string _SerialPort8ComNum;

        public static MessageWindow msgWindow = new MessageWindow() { Width = 400, Height = 104 };

        //定义未处理事件集合
        public static List<Event> isNoMarkEvents = new List<Event>();
        public static MarkEventWindow markWindow = new MarkEventWindow();
        public MainWindow()   
        {
            InitializeComponent();

            //初始化分析串口数据缓存池数据的定时器
            refreshEventList.Interval = TimeSpan.FromSeconds(1);
            refreshEventList.Tick += new EventHandler(refreshEventList_Tick);

            //这里创建一个线程，使之执行analyseSerialPortData类的AnalyseData()方法
            oThread = new Thread(new ThreadStart(analyseSerialPortData.AnalyseData));
            oThread.Start(); 

            _SerialPort1.DataReceived += new SerialDataReceivedEventHandler(_SerialPort1_DataReceived);
            _SerialPort2.DataReceived += new SerialDataReceivedEventHandler(_SerialPort2_DataReceived);
            _SerialPort3.DataReceived += new SerialDataReceivedEventHandler(_SerialPort3_DataReceived);
            _SerialPort4.DataReceived += new SerialDataReceivedEventHandler(_SerialPort4_DataReceived);
            _SerialPort5.DataReceived += new SerialDataReceivedEventHandler(_SerialPort5_DataReceived);
            _SerialPort6.DataReceived += new SerialDataReceivedEventHandler(_SerialPort6_DataReceived);
            _SerialPort7.DataReceived += new SerialDataReceivedEventHandler(_SerialPort7_DataReceived);
            _SerialPort8.DataReceived += new SerialDataReceivedEventHandler(_SerialPort8_DataReceived);

            analyseSerialPortData.InsertDatabase += new AlarmCenter.UI.SerialPortDatas.AnalyseSerialPortData.InserDBEventHandler(InsertDatabase);
            
            markWindow.markEventWindow += new AlarmCenter.UI.MarkEvents.MarkEventWindow.InserDBEventHandler(markEventWindow);
        }
        private void refreshEventList_Tick(object sender, EventArgs e)
        {
            timerFlag = false;
            refreshEventList.Stop();
            LoadEventData();
        }
        //有新事件刷新事件版
        private void InsertDatabase(object sender, EventArgs e)
        {
            if (timerFlag == false)
            {
                refreshEventList.Start();
                timerFlag = true;
            }
        }
        //有新事件刷新事件版
        private void SimulationInsertDatabase(object sender, EventArgs e)
        {
            LoadEventData();
        }
        //有新事件刷新事件版
        private void markEventWindow(object sender, EventArgs e)
        {
            LoadEventData();
        }

        /// <summary>
        /// 校验接收到的数据
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        private bool CRCCheck(byte[] packet, int length)
        {
            bool flag = false;
            if (packet[length - 1] == 0x14)
            {
                flag = true;
            }
            return flag;
        }

        public void _SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(100);
            int length = _SerialPort1.BytesToRead;
            byte[] bufRAM = new byte[length];
            _SerialPort1.Read(bufRAM, 0, length);

            int packetCount = 0;//数据包数量
            int dataPacketLength = 0;//结束字节索引

            //每个数据包，回一个ACK
            foreach (byte data in bufRAM)
            {
                if (data == 0x14)
                {
                    packetCount++;
                    _SerialPort1.Write(ACK, 0, 1);
                }
            }
            
            for (int i = 0; i < bufRAM.Length; i++)
            {
                dataPacketLength++;
                if (bufRAM[i] == 0x14)
                {
                    byte[] firstData = new byte[dataPacketLength];
                    Array.Copy(bufRAM, i + 1 - dataPacketLength, firstData, 0, dataPacketLength);
                    //读缓存第一条数据包,如果是心跳，废弃，不做解析
                    if (firstData[15] != 0x40)
                    {
                        analyseSerialPortData.SerialPortBufToRAMBuffer(firstData, dataPacketLength);
                    }
                    
                    Dispatcher.Invoke((Action)delegate
                    {
                        //Encoding.ASCII.GetString(bytes);字节数组转换为ASCII字符。
                        data.lbSerialPortData1.Items.Add("时间[" + DateTime.Now.ToString() + "]  串口数据[" + Encoding.ASCII.GetString(firstData) + "]");
                    });
                    dataPacketLength = 0;
                }
            }  
        }
        public void _SerialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(10);
            int length = _SerialPort2.BytesToRead;
            byte[] bufRAM = new byte[length];
            _SerialPort2.Read(bufRAM, 0, length);

            int packetCount = 0;//数据包数量
            int dataPacketLength = 0;//结束字节索引

            //每个数据包，回一个ACK
            foreach (byte data in bufRAM)
            {
                if (data == 0x14)
                {
                    packetCount++;
                    _SerialPort2.Write(ACK, 0, 1);
                }
            }
            analyseSerialPortData.SerialPortBufToRAMBuffer(bufRAM, length);

            for (int i = 0; i < bufRAM.Length; i++)
            {
                dataPacketLength++;
                if (bufRAM[i] == 0x14)
                {
                    Dispatcher.Invoke((Action)delegate
                    {
                        //Encoding.ASCII.GetString(bytes);字节数组转换为ASCII字符。
                        data.lbSerialPortData2.Items.Add("时间[" + DateTime.Now.ToString() + "]  串口数据[" + Encoding.ASCII.GetString(bufRAM, i + 1 - dataPacketLength, dataPacketLength) + "]");
                    });
                    dataPacketLength = 0;
                }
            }
        }
        public void _SerialPort3_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(10);
            int length = _SerialPort3.BytesToRead;
            byte[] bufRAM = new byte[length];
            _SerialPort3.Read(bufRAM, 0, length);

            int packetCount = 0;//数据包数量
            int dataPacketLength = 0;//结束字节索引

            //每个数据包，回一个ACK
            foreach (byte data in bufRAM)
            {
                if (data == 0x14)
                {
                    packetCount++;
                    _SerialPort3.Write(ACK, 0, 1);
                }
            }
            analyseSerialPortData.SerialPortBufToRAMBuffer(bufRAM, length);

            for (int i = 0; i < bufRAM.Length; i++)
            {
                dataPacketLength++;
                if (bufRAM[i] == 0x14)
                {
                    Dispatcher.Invoke((Action)delegate
                    {
                        //Encoding.ASCII.GetString(bytes);字节数组转换为ASCII字符。
                        data.lbSerialPortData3.Items.Add("时间[" + DateTime.Now.ToString() + "]  串口数据[" + Encoding.ASCII.GetString(bufRAM, i + 1 - dataPacketLength, dataPacketLength) + "]");
                    });
                    dataPacketLength = 0;
                }
            }
        }
        public void _SerialPort4_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(10);
            int length = _SerialPort4.BytesToRead;
            byte[] bufRAM = new byte[length];
            _SerialPort4.Read(bufRAM, 0, length);

            int packetCount = 0;//数据包数量
            int dataPacketLength = 0;//结束字节索引

            //每个数据包，回一个ACK
            foreach (byte data in bufRAM)
            {
                if (data == 0x14)
                {
                    packetCount++;
                    _SerialPort4.Write(ACK, 0, 1);
                }
            }
            analyseSerialPortData.SerialPortBufToRAMBuffer(bufRAM, length);

            for (int i = 0; i < bufRAM.Length; i++)
            {
                dataPacketLength++;
                if (bufRAM[i] == 0x14)
                {
                    Dispatcher.Invoke((Action)delegate
                    {
                        //Encoding.ASCII.GetString(bytes);字节数组转换为ASCII字符。
                        data.lbSerialPortData4.Items.Add("时间[" + DateTime.Now.ToString() + "]  串口数据[" + Encoding.ASCII.GetString(bufRAM, i + 1 - dataPacketLength, dataPacketLength) + "]");
                    });
                    dataPacketLength = 0;
                }
            }
        }
        public void _SerialPort5_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(10);
            int length = _SerialPort5.BytesToRead;
            byte[] bufRAM = new byte[length];
            _SerialPort5.Read(bufRAM, 0, length);

            int packetCount = 0;//数据包数量
            int dataPacketLength = 0;//结束字节索引

            //每个数据包，回一个ACK
            foreach (byte data in bufRAM)
            {
                if (data == 0x14)
                {
                    packetCount++;
                    _SerialPort5.Write(ACK, 0, 1);
                }
            }
            analyseSerialPortData.SerialPortBufToRAMBuffer(bufRAM, length);

            for (int i = 0; i < bufRAM.Length; i++)
            {
                dataPacketLength++;
                if (bufRAM[i] == 0x14)
                {
                    Dispatcher.Invoke((Action)delegate
                    {
                        //Encoding.ASCII.GetString(bytes);字节数组转换为ASCII字符。
                        data.lbSerialPortData5.Items.Add("时间[" + DateTime.Now.ToString() + "]  串口数据[" + Encoding.ASCII.GetString(bufRAM, i + 1 - dataPacketLength, dataPacketLength) + "]");
                    });
                    dataPacketLength = 0;
                }
            }
        }
        public void _SerialPort6_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(10);
            int length = _SerialPort6.BytesToRead;
            byte[] bufRAM = new byte[length];
            _SerialPort6.Read(bufRAM, 0, length);

            int packetCount = 0;//数据包数量
            int dataPacketLength = 0;//结束字节索引

            //每个数据包，回一个ACK
            foreach (byte data in bufRAM)
            {
                if (data == 0x14)
                {
                    packetCount++;
                    _SerialPort6.Write(ACK, 0, 1);
                }
            }
            analyseSerialPortData.SerialPortBufToRAMBuffer(bufRAM, length);

            for (int i = 0; i < bufRAM.Length; i++)
            {
                dataPacketLength++;
                if (bufRAM[i] == 0x14)
                {
                    Dispatcher.Invoke((Action)delegate
                    {
                        //Encoding.ASCII.GetString(bytes);字节数组转换为ASCII字符。
                        data.lbSerialPortData6.Items.Add("时间[" + DateTime.Now.ToString() + "]  串口数据[" + Encoding.ASCII.GetString(bufRAM, i + 1 - dataPacketLength, dataPacketLength) + "]");
                    });
                    dataPacketLength = 0;
                }
            }
        }
        public void _SerialPort7_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(10);
            int length = _SerialPort7.BytesToRead;
            byte[] bufRAM = new byte[length];
            _SerialPort7.Read(bufRAM, 0, length);

            int packetCount = 0;//数据包数量
            int dataPacketLength = 0;//结束字节索引

            //每个数据包，回一个ACK
            foreach (byte data in bufRAM)
            {
                if (data == 0x14)
                {
                    packetCount++;
                    _SerialPort7.Write(ACK, 0, 1);
                }
            }
            analyseSerialPortData.SerialPortBufToRAMBuffer(bufRAM, length);

            for (int i = 0; i < bufRAM.Length; i++)
            {
                dataPacketLength++;
                if (bufRAM[i] == 0x14)
                {
                    Dispatcher.Invoke((Action)delegate
                    {
                        //Encoding.ASCII.GetString(bytes);字节数组转换为ASCII字符。
                        data.lbSerialPortData7.Items.Add("时间[" + DateTime.Now.ToString() + "]  串口数据[" + Encoding.ASCII.GetString(bufRAM, i + 1 - dataPacketLength, dataPacketLength) + "]");
                    });
                    dataPacketLength = 0;
                }
            }
        }
        public void _SerialPort8_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(10);
            int length = _SerialPort8.BytesToRead;
            byte[] bufRAM = new byte[length];
            _SerialPort8.Read(bufRAM, 0, length);

            int packetCount = 0;//数据包数量
            int dataPacketLength = 0;//结束字节索引

            //每个数据包，回一个ACK
            foreach (byte data in bufRAM)
            {
                if (data == 0x14)
                {
                    packetCount++;
                    _SerialPort8.Write(ACK, 0, 1);
                }
            }
            analyseSerialPortData.SerialPortBufToRAMBuffer(bufRAM, length);

            for (int i = 0; i < bufRAM.Length; i++)
            {
                dataPacketLength++;
                if (bufRAM[i] == 0x14)
                {
                    Dispatcher.Invoke((Action)delegate
                    {
                        //Encoding.ASCII.GetString(bytes);字节数组转换为ASCII字符。
                        data.lbSerialPortData8.Items.Add("时间[" + DateTime.Now.ToString() + "]  串口数据[" + Encoding.ASCII.GetString(bufRAM, i + 1 - dataPacketLength, dataPacketLength) + "]");
                    });
                    dataPacketLength = 0;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUserData();
            LoadEventData();
            data = new SerialPortData();
        }

        private void miSystem_Click(object sender, RoutedEventArgs e)
        {
            SystemManagerList ui = new SystemManagerList();
            ui.ShowDialog();
        }

        private void miSetting_Click(object sender, RoutedEventArgs e)
        {
            Setting ui = new Setting();
            ui.ShowDialog();
        }


        private void miSerialPortData_Click(object sender, RoutedEventArgs e)
        {
            data = new SerialPortData();
            if ((_SerialPort1.IsOpen == false) && (_SerialPort2.IsOpen == false) && (_SerialPort3.IsOpen == false) && (_SerialPort4.IsOpen == false) &&
            (_SerialPort5.IsOpen == false) && (_SerialPort6.IsOpen == false) && (_SerialPort7.IsOpen == false) && (_SerialPort8.IsOpen == false))
            {
                MessageBox.Show("没有打开任何串口！请至少打开一个串口。");
                return;
            }
            data.ShowDialog();
        }

        private void miReportFormat_Click(object sender, RoutedEventArgs e)
        {
            ReportFormatListWindow reportFormatList = new ReportFormatListWindow();
            reportFormatList.ShowDialog();
        }
        private void miSimulationTest_Click(object sender, RoutedEventArgs e)
        {
            SimulationTestWindow simulationTestWindow = new SimulationTestWindow();
            simulationTestWindow.SimulationInsertDatabase += new AlarmCenter.UI.Settings.SimulationTestWindow.InserDBEventHandler(SimulationInsertDatabase);
            simulationTestWindow.ShowDialog();
        }
        private void miUser_Click(object sender, RoutedEventArgs e)
        {
            UserList ui = new UserList();
            ui.Show();
        }

        private void miRDLCUserReport_Click(object sender, RoutedEventArgs e)
        {
            //GetDBTableDAL gdb = new GetDBTableDAL();
            //DBTableList ui = new DBTableList();
            //ui.dataTable = gdb.GetMainTable("用户资料");
            //ReportWindowRDLC.reportName = "用户资料";
            //ui.Show();
        }

        private void miAlarmReportRDLC_Click(object sender, RoutedEventArgs e)
        {
            //GetDBTableDAL gdb = new GetDBTableDAL();
            //DBTableList ui = new DBTableList();
            //ui.dataTable = gdb.GetEventTable("报警事件");
            //ReportWindowRDLC.reportName = "报警事件";
            //ui.Show();
        }

        private void miSystemReportRDLC_Click(object sender, RoutedEventArgs e)
        {
            //GetDBTableDAL gdb = new GetDBTableDAL();
            //DBTableList ui = new DBTableList();
            //ui.dataTable = gdb.GetEventTable("系统事件");
            //ReportWindowRDLC.reportName = "系统事件";
            //ui.Show();
        }

        private void miMessageReportRDLC_Click(object sender, RoutedEventArgs e)
        {
            //GetDBTableDAL gdb = new GetDBTableDAL();
            //DBTableList ui = new DBTableList();
            //ui.dataTable = gdb.GetEventTable("短信日志");
            //ReportWindowRDLC.reportName = "短信日志";
            //ui.Show();
        }

        private void miVideoReportRDLC_Click(object sender, RoutedEventArgs e)
        {
            //GetDBTableDAL gdb = new GetDBTableDAL();
            //DBTableList ui = new DBTableList();
            //ui.dataTable = gdb.GetEventTable("录像事件");
            //ReportWindowRDLC.reportName = "录像事件";
            //ui.Show();
        }

        private void miRecordReportRDLC_Click(object sender, RoutedEventArgs e)
        {
            //GetDBTableDAL gdb = new GetDBTableDAL();
            //DBTableList ui = new DBTableList();
            //ui.dataTable = gdb.GetEventTable("录音日志");
            //ReportWindowRDLC.reportName = "录音日志";
            //ui.Show();
        }

        private void miEvent_Click(object sender, RoutedEventArgs e)
        {
            EventList ui = new EventList();
            ui.Show();
        }

        private void miSystemLog_Click(object sender, RoutedEventArgs e)
        {
            SystemLogList ui = new SystemLogList();
            ui.Show();
        }

        private void miMessage_Click(object sender, RoutedEventArgs e)
        {
            MessageList ui = new MessageList();
            ui.Show();
        }

        private void miVideo_Click(object sender, RoutedEventArgs e)
        {
            VideoList ui = new VideoList();
            ui.Show();
        }

        private void miRecord_Click(object sender, RoutedEventArgs e)
        {
            RecordList ui = new RecordList();
            ui.Show();
        }

        private void miHelp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void miAbout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadUserData()
        {
            UserDAL dalUser = new UserDAL();
            gridUser.ItemsSource = dalUser.GetUsersDataTable("Main", "用户资料", pageSizeUser, pageIndexUser);
            pageCountUser = dalUser.pageCount;
            tbAllPage.Text = pageCountUser.ToString();
            tbPageSize.Text = pageSizeUser.ToString();
            tbPageIndex.Text = pageIndexUser.ToString();
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            //下一页
            if (pageIndexUser == pageCountUser)
            {
                return;
            }
            pageIndexUser++;
            LoadUserData();
        }

        private void btnUpPage_Click(object sender, RoutedEventArgs e)
        {
            if (pageIndexUser == 1)
            {
                return;
            }
            pageIndexUser--;
            LoadUserData();
        }

        private void btnGoPage_Click(object sender, RoutedEventArgs e)
        {
            if (tbGoToPage.Text == "")
            {
                return;
            }
            else if (Convert.ToInt32(tbGoToPage.Text) > pageCountUser)
            {
                MessageBox.Show("您输入的值超出范围啦！");
                tbGoToPage.Text = "";
                return;
            }
            pageIndexUser = Convert.ToInt32(tbGoToPage.Text);
            LoadUserData();
        }

        private void btnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            pageIndexUser = 1;
            LoadUserData();
        }

        private void btnEedPage_Click(object sender, RoutedEventArgs e)
        {
            pageIndexUser = pageCountUser;
            LoadUserData();
        }
        private void tbEventGoToPage_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox txt = sender as TextBox;

            //屏蔽非法按键
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = false;
                return;
            }
            else if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                e.Handled = false;
                return;
            }
            else if (e.Key == Key.Back)
            {
                e.Handled = false;
                return;
            }
            else
            {
                e.Handled = true;
                return;
            }
        }

        private void tbGoToPage_TextChanged(object sender, TextChangedEventArgs e)
        {
            //屏蔽中文输入和非法字符粘贴输入
            TextBox textBox = sender as TextBox;
            TextChange[] change = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(change, 0);

            int offset = change[0].Offset;
            if (change[0].AddedLength > 0)
            {
                double num = 0;
                if (!Double.TryParse(textBox.Text, out num))
                {
                    textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                    textBox.Select(offset, 0);
                }
            }
        }

        private void LoadEventData()
        {
            EventDAL dalEvent = new EventDAL();
            gridEvent.ItemsSource = dalEvent.GetEventsArray("Event", "报警事件");

            pageCountEvent = dalEvent.pageCount;
            tbEventAllPage.Text = pageCountEvent.ToString();
            tbEventPageSize.Text = pageSizeEvent.ToString();
            tbEventPageIndex.Text = pageIndexEvent.ToString();
        }
        /// <summary>
        /// Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEventEedPage_Click(object sender, RoutedEventArgs e)
        {
            pageIndexEvent = pageCountEvent;
            LoadEventData();
        }

        private void btnEventNextPage_Click(object sender, RoutedEventArgs e)
        {
            //下一页
            if (pageIndexEvent == pageCountEvent)
            {
                return;
            }
            pageIndexEvent++;
            LoadEventData();
        }

        private void btnEventUpPage_Click(object sender, RoutedEventArgs e)
        {
            //上一页
            if (pageIndexEvent == 1)
            {
                return;
            }
            pageIndexEvent--;
            LoadEventData();
        }

        private void btnEventFirstPage_Click(object sender, RoutedEventArgs e)
        {
            pageIndexEvent = 1;
            LoadEventData();
        }

        private void btnEventGoPage_Click(object sender, RoutedEventArgs e)
        {
            if (tbEventGoToPage.Text == "")
            {
                return;
            }
            else if (Convert.ToInt32(tbEventGoToPage.Text) > pageCountEvent)
            {
                MessageBox.Show("您输入的值超出范围啦！");
                tbGoToPage.Text = "";
                return;
            }
            pageIndexEvent = Convert.ToInt32(tbEventGoToPage.Text);
            LoadEventData();
        }
        private void tbGoToPage_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox txt = sender as TextBox;

            //屏蔽非法按键
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = false;
                return;
            }
            else if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                e.Handled = false;
                return;
            }
            else if (e.Key == Key.Back)
            {
                e.Handled = false;
                return;
            }
            else
            {
                e.Handled = true;
                return;
            }
        }

        private void tbEventGoToPage_TextChanged(object sender, TextChangedEventArgs e)
        {
            //屏蔽中文输入和非法字符粘贴输入
            TextBox textBox = sender as TextBox;
            TextChange[] change = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(change, 0);

            int offset = change[0].Offset;
            if (change[0].AddedLength > 0)
            {
                double num = 0;
                if (!Double.TryParse(textBox.Text, out num))
                {
                    textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                    textBox.Select(offset, 0);
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            UserEdit editUI = new UserEdit();
            editUI.IsInsert = true;
            if (editUI.ShowDialog() == true)
            {
                LoadUserData();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)gridUser.SelectedItem;
            if (user == null)
            {
                MessageBox.Show("请选择要编辑的行！");
                return;
            }
            UserEdit editUI = new UserEdit();
            editUI.IsInsert = false;
            editUI.EditingID = user.ID;
            if (editUI.ShowDialog() == true)
            {
                LoadUserData();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)gridUser.SelectedItem;
            if (user == null)
            {
                MessageBox.Show("请选择要删除的行！");
                return;
            }

            if (MessageBox.Show("确认删除此条数据吗？", "提醒",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                new UserDAL().DeleteByID(user.ID);

                LoadUserData();//刷新数据
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            oThread.Abort();//终止线程
            Application.Current.Shutdown();
        }


        private void gridUser_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            User user = (User)gridUser.SelectedItem;
            if (user == null)
            {
                MessageBox.Show("请选择要编辑的行！");
                return;
            }
            UserEdit editUI = new UserEdit();
            editUI.IsInsert = false;
            editUI.EditingID = user.ID;
            if (editUI.ShowDialog() == true)
            {
                LoadUserData();
            }
        }







    }
}
