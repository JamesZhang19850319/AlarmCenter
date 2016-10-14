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

namespace AlarmCenter.UI.SerialPortDatas
{
    /// <summary>
    /// SerialPortData.xaml 的交互逻辑
    /// </summary>
    public partial class SerialPortData : Window
    {
        public SerialPortData()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (MainWindow._SerialPort1.IsOpen == false)
                tiSerialPortData1.Visibility = Visibility.Collapsed;
            else
            {
                tiSerialPortData1.Header = "端口" + MainWindow._SerialPort1.PortName;                
            }
            if (MainWindow._SerialPort2.IsOpen == false)
                tiSerialPortData2.Visibility = Visibility.Collapsed;
            else
                tiSerialPortData2.Header = "端口" + MainWindow._SerialPort2.PortName;
            if (MainWindow._SerialPort3.IsOpen == false)
                tiSerialPortData3.Visibility = Visibility.Collapsed;
            else
                tiSerialPortData3.Header = "端口" + MainWindow._SerialPort3.PortName;
            if (MainWindow._SerialPort4.IsOpen == false)
                tiSerialPortData4.Visibility = Visibility.Collapsed;
            else
                tiSerialPortData4.Header = "端口" + MainWindow._SerialPort4.PortName;
            if (MainWindow._SerialPort5.IsOpen == false)
                tiSerialPortData5.Visibility = Visibility.Collapsed;
            else
                tiSerialPortData5.Header = "端口" + MainWindow._SerialPort5.PortName;
            if (MainWindow._SerialPort6.IsOpen == false)
                tiSerialPortData6.Visibility = Visibility.Collapsed;
            else
                tiSerialPortData6.Header = "端口" + MainWindow._SerialPort6.PortName;
            if (MainWindow._SerialPort7.IsOpen == false)
                tiSerialPortData7.Visibility = Visibility.Collapsed;
            else
                tiSerialPortData7.Header = "端口" + MainWindow._SerialPort7.PortName;
            if (MainWindow._SerialPort8.IsOpen == false)
                tiSerialPortData8.Visibility = Visibility.Collapsed;
            else
                tiSerialPortData8.Header = "端口" + MainWindow._SerialPort8.PortName;
        }
    }
}
