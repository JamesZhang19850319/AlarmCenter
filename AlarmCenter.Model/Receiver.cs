using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Windows.Controls;

namespace AlarmCenter.DAL
{
    public class Receiver
    {
        public SerialPort newSerialPort{ get; set; }

        public int ID { get; set; }
        public string ReceiverName { get; set; }
        public string SerialPortNum { get; set; }
        public string ReceiverType { get; set; }
        public string Version { get; set; }
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public int StopBits { get; set; }
        public string FlowControl { get; set; }
        public string Parity { get; set; }
        public int EndCode { get; set; }
        public int ACK { get; set; }
        public int CheckTimer { get; set; }
        public bool IsCheck { get; set; }
        public string Mark { get; set; }

        byte[] RAMBuffer = new byte[1024];//开辟1K字节的内存空间
        int RAMBufferLength = 0;
        byte[] packet;

        public Receiver()
        {
            newSerialPort = new SerialPort();
            newSerialPort.DataReceived += new SerialDataReceivedEventHandler(newSerialPort_DataReceived);
        }
        public void newSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //if (!CheckSerialPortOpen())
            //{
            //    MessageBox.Show("串口连接错误！");
            //    return;
            //}
            Thread.Sleep(200);
            int length = newSerialPort.BytesToRead;
            byte[] bufRAM = new byte[length];
            newSerialPort.Read(bufRAM, 0, length);
            if (length == 37 && CRCCheck(bufRAM, length))
            {
                newSerialPort.Write(bufRAM, 0, 37);
                //listBoxSend.Items.Add(ToHexString(bufRAM));
            }
            SerialPortBufToRAMBuffer(bufRAM, length);
        }

        public bool CheckSerialPortIsOpen(string cbSerialPortNum)
        {   
            try
            {
                SerialPort newSerialPort = new SerialPort(cbSerialPortNum);
                newSerialPort.Open();
                newSerialPort.Close();
                return false;
            }
            catch
            {            
                return true;
            }
        }

        /// <summary>
        /// 将字节数据转换为字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToHexString(byte[] bytes) // 0xae00cf => "AE 00 CF "
        {
            //Encoding.ASCII.GetString(bytes);字节数组转换为ASCII字符。
            string hexString = string.Empty;
            if (bytes != null)
            {
                StringBuilder strB = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    strB.Append(bytes[i].ToString("X2") + " ");
                }
                hexString = strB.ToString();
            }
            return hexString;
        }


        /// <summary>
        /// 校验接收到的数据
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        private bool CRCCheck(byte[] packet, int number)
        {
            bool bTrue = false;
            uint sum = 0;
            for (int i = 0; i < number - 1; i++)
            {
                sum += packet[i];
                if (sum >= 256)
                {
                    sum -= 256;
                }
            }
            if (sum == packet[number - 1])
            {
                bTrue = true;
            }
            return bTrue;
        }

        /// <summary>
        /// 将串口缓冲区的数据加入到内存内
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="length"></param>
        private void SerialPortBufToRAMBuffer(byte[] buf, int length)
        {
            int i;
            for (i = 0; i < length; i++)
            {
                RAMBuffer[RAMBufferLength + i] = buf[i];
            }
            RAMBufferLength += length;
        }

        /// <summary>
        /// 将内存中的数据打包处理
        /// </summary>
        /// <param name="number"></param>
        private void ReadRAMBufferFirstDataPacket(int length)
        {
            packet = new byte[length];
            for (int i = 0; i < length; i++)
            {
                packet[i] = RAMBuffer[i];
            }
        }

        /// <summary>
        /// 打包数据后将数组元素左移
        /// </summary>
        /// <param name="length"></param>
        private void MoveRAMBuffer(int length)
        {
            int i;
            for (i = 0; i < RAMBufferLength - length; i++)
            {
                RAMBuffer[i] = RAMBuffer[i + length];
            }
            for (i = RAMBufferLength - length; i < RAMBufferLength; i++)
            {
                RAMBuffer[i] = 0x00;
            }
            RAMBufferLength -= length;
        }

        /// <summary>
        /// 检查包内是否有数据
        /// </summary>
        /// <returns></returns>
        private bool CheckRAMBuffer()
        {
            bool flag = false;
            for (int i = 0; i < 1024; i++)
            {
                if (RAMBuffer[i] > 0x00 && RAMBuffer[i] <= 0xFF)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

    }
}        
//    public void CreateSerialPort(string newSerialPortName)
//        {
//            if (newSerialPort.IsOpen == false)
//            {
//                newSerialPort.PortName = newSerialPortName;
//                newSerialPort.BaudRate = this.BaudRate;
//                newSerialPort.DataBits = this.DataBits;
//                newSerialPort.StopBits = (StopBits)this.StopBits;
//                newSerialPort.Parity = (Parity)Enum.Parse(typeof(Parity), this.Parity);
//            }
//        }

//        public bool OpenSerialPort(string newSerialPortName)
//        {
//            CreateSerialPort(newSerialPortName);
//            try 
//            { 
//                this.newSerialPort.Open(); 
//                MessageBox.Show("串口打开成功!");
//                return true;
//            }
//            catch
//            {   
//                //MessageBox.Show(e.ToString());
//                MessageBox.Show("串口打开失败!");
//                return false;
//            }
//        }
//        public bool SerialPortClose(string newSerialPortName)
//        {
//            //CreateSerialPort(newSerialPortName);
//            try
//            {
//                this.newSerialPort.Close();
//                MessageBox.Show("串口关闭成功!");
//                return true;
//            }
//            catch(Exception e)
//            {
//                MessageBox.Show(e.ToString());
//                MessageBox.Show("串口关闭失败!");
//                return false;
//            }

//            //if (this.newSerialPort.PortName == newSerialPortName)
//            //{
//            //    if (this.newSerialPort.IsOpen == true)
//            //        this.newSerialPort.Close();
//            //    else
//            //        MessageBox.Show("串口已经关闭!");
//            //}
//        }

//SerialPort newSerialPort = new SerialPort(cbSerialPortNum.SelectedItem.ToString());
//try
//{
//    newSerialPort.Open();
//}
//catch (Exception ex)
//{
//    MessageBox.Show("该端口无法使用,请选择其他串口！" + ex.Message);
//}
//finally
//{
//    newSerialPort.Close();
//}
//newSerialPort.BaudRate = Convert.ToInt32(cbBaudRate.SelectedItem);
//newSerialPort.DataBits = Convert.ToInt32(cbDataBits.SelectedItem);
//newSerialPort.StopBits = (StopBits)Convert.ToInt32(cbStopBits.SelectedItem);
//newSerialPort.Parity = (Parity)Enum.Parse(typeof(Parity),cbParity.SelectedItem.ToString());  //校验位
////newSerialPort.Open();