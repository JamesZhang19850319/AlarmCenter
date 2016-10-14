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

namespace AlarmCenter.UI.Settings
{
    /// <summary>
    /// Reveiver.xaml 的交互逻辑
    /// </summary>
    public partial class ReceiverEdit : Window
    {
        //是新增数据还是修改数据
        public bool IsInsert { get; set; }

        public bool IsCloseWindows { get; set; }


        //如果是编辑的话被编辑行的ID
        public int EditingID { get; set; }

        Receiver receiver;

        public ReceiverEdit()
        {
            InitializeComponent();
            IsCloseWindows = false;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (tbReceiverName.Text == "")
            {
                MessageBox.Show("请填写接收机名称！");
                return;
            }
            if (cbSerialPortNum.SelectedItem == null)
            {
                MessageBox.Show("您还没有选择串口！");
                return;
            }
            if (cbReceiverType.SelectedItem == null)
            {
                MessageBox.Show("请选择接收机类型！");
                return;
            }
            if (cbBaudRate.SelectedItem == null)
            {
                MessageBox.Show("请选择波特率！");
                return;
            }
            if (cbDataBits.SelectedItem == null)
            {
                MessageBox.Show("请选择数据位！");
                return;
            }
            if (cbParity.SelectedItem == null)
            {
                MessageBox.Show("请选择校验！");
                return;
            }
            if (cbStopBits.SelectedItem == null)
            {
                MessageBox.Show("请选择停止位！");
                return;
            }
            if (IsInsert)
            {
                //grid.DataContext指向的receiver对象
                //修改过程中，数据绑定会自动把界面的修改同步到receiver对象中
                Receiver newReceiver = new Receiver();
                newReceiver.IsCheck = (bool)cbIsCheck.IsChecked;
                newReceiver.Version = (string)tbVersion.Text;
                newReceiver.ReceiverName = tbReceiverName.Text;
                newReceiver.ReceiverType = (string)cbReceiverType.SelectedItem;
                newReceiver.SerialPortNum = (string)cbSerialPortNum.SelectedItem;
                newReceiver.BaudRate = Convert.ToInt32(cbBaudRate.SelectedItem);
                newReceiver.DataBits = Convert.ToInt32(cbDataBits.SelectedItem);
                newReceiver.StopBits = Convert.ToInt32(cbStopBits.SelectedItem);
                newReceiver.FlowControl = (string)cbFlowControl.SelectedItem;
                newReceiver.Parity = cbParity.SelectedItem.ToString();
                newReceiver.ACK = Convert.ToInt32(cbACK.SelectedItem);
                newReceiver.CheckTimer = Convert.ToInt32(tbCheckTimer.Text);
                newReceiver.EndCode = Convert.ToInt32(cbEndCode.SelectedItem);
                newReceiver.Mark = tbMark.Text;

                ReceiverDAL dal = new ReceiverDAL();
                dal.Insert(newReceiver);//插入数据库
            }
            else
            {
                //先从数据库中查询旧的值，然后把界面中的值设置到旧对象上
                //Update更新
                receiver.IsCheck = (bool)cbIsCheck.IsChecked;
                receiver.Version = (string)tbVersion.Text;
                receiver.ReceiverName = tbReceiverName.Text;
                receiver.ReceiverType = (string)cbReceiverType.SelectedItem;
                receiver.SerialPortNum = (string)cbSerialPortNum.SelectedItem;
                receiver.BaudRate = Convert.ToInt32(cbBaudRate.SelectedItem);
                receiver.DataBits = Convert.ToInt32(cbDataBits.SelectedItem);
                receiver.StopBits = Convert.ToInt32(cbStopBits.SelectedItem);
                receiver.FlowControl = (string)cbFlowControl.SelectedItem;
                receiver.Parity = cbParity.SelectedItem.ToString();
                receiver.ACK = Convert.ToInt32(cbACK.SelectedItem);
                receiver.CheckTimer = Convert.ToInt32(tbCheckTimer.Text);
                receiver.EndCode = Convert.ToInt32(cbEndCode.SelectedItem);
                receiver.Mark = tbMark.Text;

                ReceiverDAL dal = new ReceiverDAL();
                dal.Update(receiver);
            }

            //ReceiverDAL dal = new ReceiverDAL();
            DialogResult = true;
        }

        public bool CheckSerialPortIsOpen(string newSerialPortName)
        {
            SerialPort newSerialPort = new SerialPort();
            newSerialPort.PortName = newSerialPortName;
            if (newSerialPort.IsOpen)
            {
                return true;
            }
            return false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// 加载串口combobox选择项
        /// </summary>
        private void cb_Loaded()
        {
            string[] ports = SerialPort.GetPortNames();
            ReceiverDAL dal = new ReceiverDAL();
            //cbSerialPortNum.Items.Clear();
            foreach (string port in ports)
            {
                if (dal.SerialPortNumIsUsed(port, EditingID) == false)
                {
                    cbSerialPortNum.Items.Add(port);
                }
            }

            cbReceiverType.Items.Add("Sementic DRC");
            cbReceiverType.Items.Add("Sementic BUR");
            cbReceiverType.Items.Add("Sementic DRR");
            cbReceiverType.Items.Add("IPR512-Surgard MLR2-DG");
            cbReceiverType.Items.Add("IPR512-Radionics 6500");
            cbReceiverType.Items.Add("Ademco 685");
            cbReceiverType.Items.Add("Sur-Gard MLR2000");
            cbReceiverType.Items.Add("Sur-Gard SYSTEM_III");
            cbReceiverType.Items.Add("Sur-Gard MLR");
            cbReceiverType.Items.Add("Sur-Gard SLR");
            cbReceiverType.Items.Add("CFSKIII");

            cbBaudRate.Items.Add("1200");
            cbBaudRate.Items.Add("2400");
            cbBaudRate.Items.Add("4800");
            cbBaudRate.Items.Add("9600");
            cbBaudRate.Items.Add("14400");
            cbBaudRate.Items.Add("19200");
            cbBaudRate.Items.Add("28800");
            cbBaudRate.Items.Add("28400");
            cbBaudRate.Items.Add("57600");

            cbDataBits.Items.Add("5");
            cbDataBits.Items.Add("6");
            cbDataBits.Items.Add("7");
            cbDataBits.Items.Add("8");

            cbStopBits.Items.Add("1");
            cbStopBits.Items.Add("2");

            cbFlowControl.Items.Add("无");
            cbFlowControl.Items.Add("comXOnXOff");
            cbFlowControl.Items.Add("comRTS");
            cbFlowControl.Items.Add("comTRSXOnOff");

            cbParity.Items.Add("None");
            cbParity.Items.Add("Odd");
            cbParity.Items.Add("Even");
            cbParity.Items.Add("Mark");
            cbParity.Items.Add("Space");

            cbEndCode.Items.Add("20");
            cbEndCode.Items.Add("13");

            cbACK.Items.Add("6");

            tbCheckTimer.Text = "30";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cb_Loaded();

            if (!IsInsert)
            {
                receiver = new ReceiverDAL().GetByID(EditingID);

                tbReceiverName.Text = receiver.ReceiverName;
                cbReceiverType.SelectedItem = receiver.ReceiverType;
                cbSerialPortNum.SelectedItem = receiver.SerialPortNum;
                cbBaudRate.SelectedItem = receiver.BaudRate.ToString();
                cbDataBits.SelectedItem = receiver.DataBits.ToString();
                cbStopBits.SelectedItem = receiver.StopBits.ToString();
                cbParity.SelectedItem = receiver.Parity;
                cbFlowControl.SelectedItem = receiver.FlowControl;
                cbEndCode.SelectedItem = receiver.EndCode.ToString();
                cbACK.SelectedItem = receiver.ACK.ToString();
                tbCheckTimer.Text = receiver.CheckTimer.ToString();
                cbIsCheck.IsChecked = receiver.IsCheck;
                tbVersion.Text = receiver.Version;
                tbMark.Text = receiver.Mark;
            }

            //连接PC设置选项是否显示
            CheckTimerWriteStatus();

            Receiver newReceiver = new Receiver();

        }

        private void CheckTimerWriteStatus()
        {
            //连接测试打开或者关闭，限制设置时间
            if (cbIsCheck.IsChecked == true)
            {
                tbCheckTimer.Visibility = Visibility.Visible;
                tbKCheckTimer.Visibility = Visibility.Visible;
            }
            else
            {
                tbCheckTimer.Visibility = Visibility.Hidden;
                tbKCheckTimer.Visibility = Visibility.Hidden;
            }
        }

        private void ReceiverDefaultSetting()
        {

            switch (cbReceiverType.SelectedItem.ToString())
            {
                case "Sementic DRC":
                    cbBaudRate.SelectedIndex = 3;
                    cbDataBits.SelectedIndex = 2;
                    cbParity.SelectedIndex = 2;
                    cbStopBits.SelectedIndex = 0;
                    cbACK.SelectedIndex = 0;
                    cbFlowControl.SelectedIndex = 0;
                    cbEndCode.SelectedIndex = 0;
                    break;
                case "Sementic BUR":
                    cbBaudRate.SelectedIndex = 3;
                    cbDataBits.SelectedIndex = 3;
                    cbParity.SelectedIndex = 0;
                    cbStopBits.SelectedIndex = 0;
                    cbACK.SelectedIndex = 0;
                    cbFlowControl.SelectedIndex = 0;
                    cbEndCode.SelectedIndex = 0;
                    break;
                case "Sementic DRR":
                    cbBaudRate.SelectedIndex = 3;
                    cbDataBits.SelectedIndex = 3;
                    cbParity.SelectedIndex = 0;
                    cbStopBits.SelectedIndex = 0;
                    cbACK.SelectedIndex = 0;
                    cbFlowControl.SelectedIndex = 0;
                    cbEndCode.SelectedIndex = 0;
                    break;
                case "IPR512-Surgard MLR2-DG":
                    cbBaudRate.SelectedIndex = 3;
                    cbDataBits.SelectedIndex = 3;
                    cbParity.SelectedIndex = 0;
                    cbStopBits.SelectedIndex = 0;
                    cbACK.SelectedIndex = 0;
                    cbFlowControl.SelectedIndex = 0;
                    cbEndCode.SelectedIndex = 0;
                    break;
                case "IPR512-Radionics 6500":
                    cbBaudRate.SelectedIndex = 3;
                    cbDataBits.SelectedIndex = 3;
                    cbParity.SelectedIndex = 0;
                    cbStopBits.SelectedIndex = 0;
                    cbACK.SelectedIndex = 0;
                    cbFlowControl.SelectedIndex = 0;
                    cbEndCode.SelectedIndex = 0;
                    break;
                case "Ademco 685":
                    cbBaudRate.SelectedIndex = 3;
                    cbDataBits.SelectedIndex = 3;
                    cbParity.SelectedIndex = 0;
                    cbStopBits.SelectedIndex = 0;
                    cbACK.SelectedIndex = 0;
                    cbFlowControl.SelectedIndex = 0;
                    cbEndCode.SelectedIndex = 1;
                    break;
                case "Sur-Gard MLR2000":
                    cbBaudRate.SelectedIndex = 3;
                    cbDataBits.SelectedIndex = 3;
                    cbParity.SelectedIndex = 0;
                    cbStopBits.SelectedIndex = 1;
                    cbACK.SelectedIndex = 0;
                    cbFlowControl.SelectedIndex = 0;
                    cbEndCode.SelectedIndex = 0;
                    break;
                case "Sur-Gard SYSTEM_III":
                    cbBaudRate.SelectedIndex = 3;
                    cbDataBits.SelectedIndex = 3;
                    cbParity.SelectedIndex = 0;
                    cbStopBits.SelectedIndex = 1;
                    cbACK.SelectedIndex = 0;
                    cbFlowControl.SelectedIndex = 0;
                    cbEndCode.SelectedIndex = 0;
                    break;
                case "Sur-Gard MLR":
                    cbBaudRate.SelectedIndex = 0;
                    cbDataBits.SelectedIndex = 2;
                    cbParity.SelectedIndex = 1;
                    cbStopBits.SelectedIndex = 0;
                    cbACK.SelectedIndex = 0;
                    cbFlowControl.SelectedIndex = 0;
                    cbEndCode.SelectedIndex = 0;
                    break;
                case "Sur-Gard SLR":
                    cbBaudRate.SelectedIndex = 0;
                    cbDataBits.SelectedIndex = 2;
                    cbParity.SelectedIndex = 1;
                    cbStopBits.SelectedIndex = 0;
                    cbACK.SelectedIndex = 0;
                    cbFlowControl.SelectedIndex = 0;
                    cbEndCode.SelectedIndex = 0;
                    break;
                case "CFSKIII":
                    cbBaudRate.SelectedIndex = 0;
                    cbDataBits.SelectedIndex = 2;
                    cbParity.SelectedIndex = 1;
                    cbStopBits.SelectedIndex = 0;
                    cbACK.SelectedIndex = 0;
                    cbFlowControl.SelectedIndex = 0;
                    cbEndCode.SelectedIndex = 0;
                    break;
            }
        }

        private void cbReceiverType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReceiverDefaultSetting();
        }

        private void btnDefaultSetting_Click(object sender, RoutedEventArgs e)
        {
            ReceiverDefaultSetting();
        }

        private void cbIsCheck_Click(object sender, RoutedEventArgs e)
        {
            CheckTimerWriteStatus();
        }
    }
}
