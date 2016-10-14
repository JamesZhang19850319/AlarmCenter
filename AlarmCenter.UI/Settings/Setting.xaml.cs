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
using System.IO.Ports;
using System.Windows.Threading;

namespace AlarmCenter.UI.Settings
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class Setting : Window
    {
        public int pageIndex = 1;
        public const int pageSize = 10;
        DispatcherTimer dt = new DispatcherTimer();

        public Setting()
        {
            InitializeComponent();
            dt.Interval = TimeSpan.FromSeconds(0);
            dt.Tick += new EventHandler(dt_Tick);//调用函数
        }
        public void LoadReceiverData()
        {
            ReceiverDAL dal = new ReceiverDAL();
            dgReceiver.ItemsSource = dal.ListAll();

            dt.Start();
        }
        public void LoadStrategiesData()
        {
            StrategiesDAL dal = new StrategiesDAL();
            dgStrategies.ItemsSource = dal.GetStrategiesArray();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadReceiverData();
            LoadStrategiesData();
        }
        private void btnStrategiesAdd_Click(object sender, RoutedEventArgs e)
        {
            StrategiesEditWindow strategiesEditWindow = new StrategiesEditWindow();
            strategiesEditWindow.IsInsert = true;
            if (strategiesEditWindow.ShowDialog() == true)
            {
                LoadStrategiesData();
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ReceiverEdit receiverEdit = new ReceiverEdit();
            receiverEdit.IsInsert = true;
            if (receiverEdit.ShowDialog() == true)
            {
                LoadReceiverData();
            }
        }


        private void btnStrategiesEdit_Click(object sender, RoutedEventArgs e)
        {
            Strategies strategies = (Strategies)dgStrategies.SelectedItem;

            //dgStrategies.SelectedItem
            if (strategies == null)
            {
                MessageBox.Show("请选择要编辑的行！");
                return;
            }

            StrategiesEditWindow strategiesEditWindow = new StrategiesEditWindow();
            strategiesEditWindow.IsInsert = false;
            strategiesEditWindow.strategiesID = strategies.ID;
            if (strategiesEditWindow.ShowDialog() == true)
            {
                LoadStrategiesData();
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Receiver receiver = (Receiver)dgReceiver.SelectedItem;
            //dgReceiver.SelectedItem
            if (receiver == null)
            {
                MessageBox.Show("请选择要编辑的行！");
                return;
            }
            if ((dgReceiver.Columns[0].GetCellContent(dgReceiver.SelectedItem) as TextBlock).Text == "打开")
            {
                MessageBox.Show("请先关闭该串口！");
                return;
            }
            ReceiverEdit receiverEdit = new ReceiverEdit();
            receiverEdit.IsInsert = false;
            receiverEdit.EditingID = receiver.ID;
            if (receiverEdit.ShowDialog() == true)
            {
                LoadReceiverData();
            }
        }

        private void btnStrategiesDelete_Click(object sender, RoutedEventArgs e)
        {
            Strategies strategies = (Strategies)dgStrategies.SelectedItem;
            if (strategies == null)
            {
                MessageBox.Show("请选择要删除的行！");
                return;
            }
            if (MessageBox.Show("确认删除此条数据吗？", "提醒",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                new StrategiesDAL().DeleteByID(strategies.ID);

                LoadStrategiesData();//刷新数据
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Receiver receiver = (Receiver)dgReceiver.SelectedItem;
            if (receiver == null)
            {
                MessageBox.Show("请选择要删除的行！");
                return;
            }
            if ((dgReceiver.Columns[0].GetCellContent(dgReceiver.SelectedItem) as TextBlock).Text == "打开")
            {
                MessageBox.Show("请先关闭该串口！");
                return;
            }
            if (MessageBox.Show("确认删除此条数据吗？", "提醒",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                new ReceiverDAL().DeleteByID(receiver.ID);

                LoadReceiverData();//刷新数据
            }
        }


        private void dgStrategies_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Strategies strategies = (Strategies)dgStrategies.SelectedItem;

            //dgStrategies.SelectedItem
            if (strategies == null)
            {
                MessageBox.Show("请选择要编辑的行！");
                return;
            }

            StrategiesEditWindow strategiesEditWindow = new StrategiesEditWindow();
            strategiesEditWindow.IsInsert = false;
            strategiesEditWindow.strategiesID = strategies.ID;
            if (strategiesEditWindow.ShowDialog() == true)
            {
                LoadStrategiesData();
            }
        }
        private void dgReceiver_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Receiver receiver = (Receiver)dgReceiver.SelectedItem;
            //dgReceiver.SelectedItem
            if (receiver == null)
            {
                MessageBox.Show("请选择要编辑的行！");
                return;
            }
            if ((dgReceiver.Columns[0].GetCellContent(dgReceiver.SelectedItem) as TextBlock).Text == "打开")
            {
                MessageBox.Show("请先关闭该串口！");
                return;
            }
            ReceiverEdit receiverEdit = new ReceiverEdit();
            receiverEdit.IsInsert = false;
            receiverEdit.EditingID = receiver.ID;
            if (receiverEdit.ShowDialog() == true)
            {
                LoadReceiverData();
            }
        }

        private void btnOpenSerialPort_Click(object sender, RoutedEventArgs e)
        {
            Receiver receiver = (Receiver)dgReceiver.SelectedItem;
            if (receiver == null)
            {
                MessageBox.Show("请选择一项！");
                return;
            }
            if ((receiver.SerialPortNum == MainWindow._SerialPort1.PortName) && (MainWindow._SerialPort1.IsOpen == true))
            {
                MessageBox.Show(receiver.SerialPortNum + "已经被打开了！");
                return;
            }
            if ((receiver.SerialPortNum == MainWindow._SerialPort2.PortName) && (MainWindow._SerialPort2.IsOpen == true))
            {
                MessageBox.Show(receiver.SerialPortNum + "已经被打开了！");
                return;
            }
            if ((receiver.SerialPortNum == MainWindow._SerialPort3.PortName) && (MainWindow._SerialPort3.IsOpen == true))
            {
                MessageBox.Show(receiver.SerialPortNum + "已经被打开了！");
                return;
            }
            if ((receiver.SerialPortNum == MainWindow._SerialPort4.PortName) && (MainWindow._SerialPort4.IsOpen == true))
            {
                MessageBox.Show(receiver.SerialPortNum + "已经被打开了！");
                return;
            }
            if ((receiver.SerialPortNum == MainWindow._SerialPort5.PortName) && (MainWindow._SerialPort5.IsOpen == true))
            {
                MessageBox.Show(receiver.SerialPortNum + "已经被打开了！");
                return;
            }
            if ((receiver.SerialPortNum == MainWindow._SerialPort6.PortName) && (MainWindow._SerialPort6.IsOpen == true))
            {
                MessageBox.Show(receiver.SerialPortNum + "已经被打开了！");
                return;
            }
            if ((receiver.SerialPortNum == MainWindow._SerialPort7.PortName) && (MainWindow._SerialPort7.IsOpen == true))
            {
                MessageBox.Show(receiver.SerialPortNum + "已经被打开了！");
                return;
            }
            if ((receiver.SerialPortNum == MainWindow._SerialPort8.PortName) && (MainWindow._SerialPort8.IsOpen == true))
            {
                MessageBox.Show(receiver.SerialPortNum + "已经被打开了！");
                return;
            }

            if (MainWindow._SerialPort1.IsOpen == false)
            {
                MainWindow._SerialPort1.PortName = receiver.SerialPortNum;
                MainWindow._SerialPort1.BaudRate = receiver.BaudRate;
                MainWindow._SerialPort1.DataBits = receiver.DataBits;
                MainWindow._SerialPort1.StopBits = (StopBits)receiver.StopBits;
                MainWindow._SerialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), receiver.Parity);

                MainWindow._SerialPort1ComNum = receiver.SerialPortNum;

                try { MainWindow._SerialPort1.Open(); }
                catch { MessageBox.Show(MainWindow._SerialPort1ComNum + "已经被其他程序占用或者无此端口!"); return; }

                MessageBox.Show(MainWindow._SerialPort1ComNum + "被打开！");
                (dgReceiver.Columns[0].GetCellContent(dgReceiver.SelectedItem) as TextBlock).Text = "打开";

                var row = dgReceiver.ItemContainerGenerator.ContainerFromItem(dgReceiver.SelectedItem) as DataGridRow;
                row.Background = new SolidColorBrush(Colors.GreenYellow);

                return;
            }
            if (MainWindow._SerialPort2.IsOpen == false)
            {
                MainWindow._SerialPort2.PortName = receiver.SerialPortNum;
                MainWindow._SerialPort2.BaudRate = receiver.BaudRate;
                MainWindow._SerialPort2.DataBits = receiver.DataBits;
                MainWindow._SerialPort2.StopBits = (StopBits)receiver.StopBits;
                MainWindow._SerialPort2.Parity = (Parity)Enum.Parse(typeof(Parity), receiver.Parity);

                MainWindow._SerialPort2ComNum = receiver.SerialPortNum;

                try { MainWindow._SerialPort2.Open(); }
                catch { MessageBox.Show(MainWindow._SerialPort2ComNum + "已经被其他程序占用或者无此端口！"); return; }

                MessageBox.Show(MainWindow._SerialPort2ComNum + "被打开！");
                (dgReceiver.Columns[0].GetCellContent(dgReceiver.SelectedItem) as TextBlock).Text = "打开";

                var row = dgReceiver.ItemContainerGenerator.ContainerFromItem(dgReceiver.SelectedItem) as DataGridRow;
                row.Background = new SolidColorBrush(Colors.GreenYellow);

                return;
            }
            if (MainWindow._SerialPort3.IsOpen == false)
            {
                MainWindow._SerialPort3.PortName = receiver.SerialPortNum;
                MainWindow._SerialPort3.BaudRate = receiver.BaudRate;
                MainWindow._SerialPort3.DataBits = receiver.DataBits;
                MainWindow._SerialPort3.StopBits = (StopBits)receiver.StopBits;
                MainWindow._SerialPort3.Parity = (Parity)Enum.Parse(typeof(Parity), receiver.Parity);

                MainWindow._SerialPort3ComNum = receiver.SerialPortNum;

                try { MainWindow._SerialPort3.Open(); }
                catch { MessageBox.Show(MainWindow._SerialPort3ComNum + "已经被其他程序占用或者无此端口！"); return; }

                MessageBox.Show(MainWindow._SerialPort3ComNum + "被打开！");
                (dgReceiver.Columns[0].GetCellContent(dgReceiver.SelectedItem) as TextBlock).Text = "打开";

                var row = dgReceiver.ItemContainerGenerator.ContainerFromItem(dgReceiver.SelectedItem) as DataGridRow;
                row.Background = new SolidColorBrush(Colors.GreenYellow);

                return;
            }
            if (MainWindow._SerialPort4.IsOpen == false)
            {
                MainWindow._SerialPort4.PortName = receiver.SerialPortNum;
                MainWindow._SerialPort4.BaudRate = receiver.BaudRate;
                MainWindow._SerialPort4.DataBits = receiver.DataBits;
                MainWindow._SerialPort4.StopBits = (StopBits)receiver.StopBits;
                MainWindow._SerialPort4.Parity = (Parity)Enum.Parse(typeof(Parity), receiver.Parity);

                MainWindow._SerialPort4ComNum = receiver.SerialPortNum;

                try { MainWindow._SerialPort4.Open(); }
                catch { MessageBox.Show(MainWindow._SerialPort4ComNum + "已经被其他程序占用或者无此端口！"); return; }

                MessageBox.Show(MainWindow._SerialPort4ComNum + "被打开！");
                (dgReceiver.Columns[0].GetCellContent(dgReceiver.SelectedItem) as TextBlock).Text = "打开";

                var row = dgReceiver.ItemContainerGenerator.ContainerFromItem(dgReceiver.SelectedItem) as DataGridRow;
                row.Background = new SolidColorBrush(Colors.GreenYellow);

                return;
            }
            if (MainWindow._SerialPort5.IsOpen == false)
            {
                MainWindow._SerialPort5.PortName = receiver.SerialPortNum;
                MainWindow._SerialPort5.BaudRate = receiver.BaudRate;
                MainWindow._SerialPort5.DataBits = receiver.DataBits;
                MainWindow._SerialPort5.StopBits = (StopBits)receiver.StopBits;
                MainWindow._SerialPort5.Parity = (Parity)Enum.Parse(typeof(Parity), receiver.Parity);

                MainWindow._SerialPort5ComNum = receiver.SerialPortNum;

                try { MainWindow._SerialPort5.Open(); }
                catch { MessageBox.Show(MainWindow._SerialPort5ComNum + "已经被其他程序占用或者无此端口！"); return; }

                MessageBox.Show(MainWindow._SerialPort5ComNum + "被打开！");
                (dgReceiver.Columns[0].GetCellContent(dgReceiver.SelectedItem) as TextBlock).Text = "打开";

                var row = dgReceiver.ItemContainerGenerator.ContainerFromItem(dgReceiver.SelectedItem) as DataGridRow;
                row.Background = new SolidColorBrush(Colors.GreenYellow);

                return;
            }
            if (MainWindow._SerialPort6.IsOpen == false)
            {
                MainWindow._SerialPort6.PortName = receiver.SerialPortNum;
                MainWindow._SerialPort6.BaudRate = receiver.BaudRate;
                MainWindow._SerialPort6.DataBits = receiver.DataBits;
                MainWindow._SerialPort6.StopBits = (StopBits)receiver.StopBits;
                MainWindow._SerialPort6.Parity = (Parity)Enum.Parse(typeof(Parity), receiver.Parity);

                MainWindow._SerialPort6ComNum = receiver.SerialPortNum;

                try { MainWindow._SerialPort6.Open(); }
                catch { MessageBox.Show(MainWindow._SerialPort6ComNum + "已经被其他程序占用或者无此端口！"); return; }

                MessageBox.Show(MainWindow._SerialPort6ComNum + "被打开！");
                (dgReceiver.Columns[0].GetCellContent(dgReceiver.SelectedItem) as TextBlock).Text = "打开";

                var row = dgReceiver.ItemContainerGenerator.ContainerFromItem(dgReceiver.SelectedItem) as DataGridRow;
                row.Background = new SolidColorBrush(Colors.GreenYellow);

                return;
            }
            if (MainWindow._SerialPort7.IsOpen == false)
            {
                MainWindow._SerialPort7.PortName = receiver.SerialPortNum;
                MainWindow._SerialPort7.BaudRate = receiver.BaudRate;
                MainWindow._SerialPort7.DataBits = receiver.DataBits;
                MainWindow._SerialPort7.StopBits = (StopBits)receiver.StopBits;
                MainWindow._SerialPort7.Parity = (Parity)Enum.Parse(typeof(Parity), receiver.Parity);

                MainWindow._SerialPort7ComNum = receiver.SerialPortNum;

                try { MainWindow._SerialPort7.Open(); }
                catch { MessageBox.Show(MainWindow._SerialPort7ComNum + "已经被其他程序占用或者无此端口！"); return; }

                MessageBox.Show(MainWindow._SerialPort7ComNum + "被打开！");
                (dgReceiver.Columns[0].GetCellContent(dgReceiver.SelectedItem) as TextBlock).Text = "打开";

                var row = dgReceiver.ItemContainerGenerator.ContainerFromItem(dgReceiver.SelectedItem) as DataGridRow;
                row.Background = new SolidColorBrush(Colors.GreenYellow);

                return;
            }
            if (MainWindow._SerialPort8.IsOpen == false)
            {
                MainWindow._SerialPort8.PortName = receiver.SerialPortNum;
                MainWindow._SerialPort8.BaudRate = receiver.BaudRate;
                MainWindow._SerialPort8.DataBits = receiver.DataBits;
                MainWindow._SerialPort8.StopBits = (StopBits)receiver.StopBits;
                MainWindow._SerialPort8.Parity = (Parity)Enum.Parse(typeof(Parity), receiver.Parity);

                MainWindow._SerialPort8ComNum = receiver.SerialPortNum;

                try { MainWindow._SerialPort8.Open(); }
                catch { MessageBox.Show(MainWindow._SerialPort8ComNum + "已经被其他程序占用或者无此端口！"); return; }

                MessageBox.Show(MainWindow._SerialPort8ComNum + "被打开！");
                (dgReceiver.Columns[0].GetCellContent(dgReceiver.SelectedItem) as TextBlock).Text = "打开";

                var row = dgReceiver.ItemContainerGenerator.ContainerFromItem(dgReceiver.SelectedItem) as DataGridRow;
                row.Background = new SolidColorBrush(Colors.GreenYellow);

                return;
            }

        }

        private void btnCloseSerialPort_Click(object sender, RoutedEventArgs e)
        {
            Receiver receiver = (Receiver)dgReceiver.SelectedItem;
            if (receiver == null)
            {
                MessageBox.Show("请选择要关闭串口的接警机！");
                return;
            }
            if ((receiver.SerialPortNum == MainWindow._SerialPort1ComNum) && (MainWindow._SerialPort1.IsOpen == true))
            {
                MainWindow._SerialPort1.Close();
                MessageBox.Show(MainWindow._SerialPort1ComNum + "被关闭！");
                (dgReceiver.Columns[0].GetCellContent(dgReceiver.SelectedItem) as TextBlock).Text = "关闭";

                var row = dgReceiver.ItemContainerGenerator.ContainerFromItem(dgReceiver.SelectedItem) as DataGridRow;
                row.Background = new SolidColorBrush(Colors.Orange);

                return;
            }
            if ((receiver.SerialPortNum == MainWindow._SerialPort2ComNum) && (MainWindow._SerialPort2.IsOpen == true))
            {
                MainWindow._SerialPort2.Close();
                MessageBox.Show(MainWindow._SerialPort2ComNum + "被关闭！");
                (dgReceiver.Columns[0].GetCellContent(dgReceiver.SelectedItem) as TextBlock).Text = "关闭";

                var row = dgReceiver.ItemContainerGenerator.ContainerFromItem(dgReceiver.SelectedItem) as DataGridRow;
                row.Background = new SolidColorBrush(Colors.Orange);

                return;
            }
            if ((receiver.SerialPortNum == MainWindow._SerialPort3ComNum) && (MainWindow._SerialPort3.IsOpen == true))
            {
                MainWindow._SerialPort3.Close();
                MessageBox.Show(MainWindow._SerialPort3ComNum + "被关闭！");
                (dgReceiver.Columns[0].GetCellContent(dgReceiver.SelectedItem) as TextBlock).Text = "关闭";

                var row = dgReceiver.ItemContainerGenerator.ContainerFromItem(dgReceiver.SelectedItem) as DataGridRow;
                row.Background = new SolidColorBrush(Colors.Orange);

                return;
            }
            if ((receiver.SerialPortNum == MainWindow._SerialPort4ComNum) && (MainWindow._SerialPort4.IsOpen == true))
            {
                MainWindow._SerialPort4.Close();
                MessageBox.Show(MainWindow._SerialPort4ComNum + "被关闭！");
                (dgReceiver.Columns[0].GetCellContent(dgReceiver.SelectedItem) as TextBlock).Text = "关闭";

                var row = dgReceiver.ItemContainerGenerator.ContainerFromItem(dgReceiver.SelectedItem) as DataGridRow;
                row.Background = new SolidColorBrush(Colors.Orange);

                return;
            }
            if ((receiver.SerialPortNum == MainWindow._SerialPort5ComNum) && (MainWindow._SerialPort5.IsOpen == true))
            {
                MainWindow._SerialPort5.Close();
                MessageBox.Show(MainWindow._SerialPort5ComNum + "被关闭！");
                (dgReceiver.Columns[0].GetCellContent(dgReceiver.SelectedItem) as TextBlock).Text = "关闭";

                var row = dgReceiver.ItemContainerGenerator.ContainerFromItem(dgReceiver.SelectedItem) as DataGridRow;
                row.Background = new SolidColorBrush(Colors.Orange);

                return;
            }
            if ((receiver.SerialPortNum == MainWindow._SerialPort6ComNum) && (MainWindow._SerialPort6.IsOpen == true))
            {
                MainWindow._SerialPort6.Close();
                MessageBox.Show(MainWindow._SerialPort6ComNum + "被关闭！");
                (dgReceiver.Columns[0].GetCellContent(dgReceiver.SelectedItem) as TextBlock).Text = "关闭";

                var row = dgReceiver.ItemContainerGenerator.ContainerFromItem(dgReceiver.SelectedItem) as DataGridRow;
                row.Background = new SolidColorBrush(Colors.Orange);

                return;
            }
            if ((receiver.SerialPortNum == MainWindow._SerialPort7ComNum) && (MainWindow._SerialPort7.IsOpen == true))
            {
                MainWindow._SerialPort7.Close();
                MessageBox.Show(MainWindow._SerialPort7ComNum + "被关闭！");
                (dgReceiver.Columns[0].GetCellContent(dgReceiver.SelectedItem) as TextBlock).Text = "关闭";

                var row = dgReceiver.ItemContainerGenerator.ContainerFromItem(dgReceiver.SelectedItem) as DataGridRow;
                row.Background = new SolidColorBrush(Colors.Orange);

                return;
            }
            if ((receiver.SerialPortNum == MainWindow._SerialPort8ComNum) && (MainWindow._SerialPort8.IsOpen == true))
            {
                MainWindow._SerialPort8.Close();
                MessageBox.Show(MainWindow._SerialPort8ComNum + "被关闭！");
                (dgReceiver.Columns[0].GetCellContent(dgReceiver.SelectedItem) as TextBlock).Text = "关闭";

                var row = dgReceiver.ItemContainerGenerator.ContainerFromItem(dgReceiver.SelectedItem) as DataGridRow;
                row.Background = new SolidColorBrush(Colors.Orange);

                return;
            }

        }


        private void dt_Tick(object sender, EventArgs e)
        {
            dt.Stop();
            ReceiverDAL dal = new ReceiverDAL();
            Receiver[] receivers = new Receiver[dal.ListAll().Length];
            receivers = dal.ListAll();
            for (int i = 0; i < receivers.Length; i++)
            {
                if (((MainWindow._SerialPort1.IsOpen == true) && (MainWindow._SerialPort1.PortName == receivers[i].SerialPortNum))
                    || (MainWindow._SerialPort2.IsOpen == true) && ((MainWindow._SerialPort2.PortName == receivers[i].SerialPortNum))
                    || (MainWindow._SerialPort3.IsOpen == true) && ((MainWindow._SerialPort3.PortName == receivers[i].SerialPortNum))
                    || (MainWindow._SerialPort4.IsOpen == true) && ((MainWindow._SerialPort4.PortName == receivers[i].SerialPortNum))
                    || (MainWindow._SerialPort5.IsOpen == true) && ((MainWindow._SerialPort5.PortName == receivers[i].SerialPortNum))
                    || (MainWindow._SerialPort6.IsOpen == true) && ((MainWindow._SerialPort6.PortName == receivers[i].SerialPortNum))
                    || (MainWindow._SerialPort7.IsOpen == true) && ((MainWindow._SerialPort7.PortName == receivers[i].SerialPortNum))
                    || (MainWindow._SerialPort8.IsOpen == true) && ((MainWindow._SerialPort8.PortName == receivers[i].SerialPortNum)))
                {
                    (dgReceiver.Columns[0].GetCellContent(dgReceiver.Items[i]) as TextBlock).Text = "打开";

                    var row = dgReceiver.ItemContainerGenerator.ContainerFromItem(dgReceiver.Items[i]) as DataGridRow;
                    row.Background = new SolidColorBrush(Colors.GreenYellow);
                }
                else
                {
                    (dgReceiver.Columns[0].GetCellContent(dgReceiver.Items[i]) as TextBlock).Text = "关闭";

                    var row = dgReceiver.ItemContainerGenerator.ContainerFromItem(dgReceiver.Items[i]) as DataGridRow;
                    row.Background = new SolidColorBrush(Colors.Orange);
                }
            }
        }


    }
}
