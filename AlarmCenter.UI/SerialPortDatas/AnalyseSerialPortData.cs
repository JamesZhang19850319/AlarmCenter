using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using AlarmCenter.DAL;
using System.Threading;
using System.Windows;

namespace AlarmCenter.UI.SerialPortDatas
{
    public class AnalyseSerialPortData
    {
        public delegate void InserDBEventHandler(object serder, EventArgs e);//声明一个插入新[报警事件]的事件的委托
        public event InserDBEventHandler InsertDatabase;//声明插入新[报警事件]的事件

        int RAMBufferLength = 0;
        byte[] packet;
        //static List<byte> RAMBuffer = new List<byte>();
        static byte[] RAMBuffer = new byte[10240];//开辟10K字节的内存空间

        public AnalyseSerialPortData()
        {

        }
        /// <summary>
        /// 将串口缓冲区的数据加入到内存内
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="length"></param>
        public void SerialPortBufToRAMBuffer(byte[] buf, int length)
        {
            int i=0;
            for (i = 0; i < length; i++)
            {
                RAMBuffer[RAMBufferLength + i] = buf[i];
            }
            RAMBufferLength += length;
        }

        /// <summary>
        /// 检查包内是否有数据
        /// </summary>
        /// <returns></returns>
        private bool RAMBufferIsNull()
        {
            bool flag = true;
            foreach (byte i in RAMBuffer)
            {
                if (i != 0x00 && i != 0xFF)
                {
                    flag = false;
                }
            }
            return flag;
        }
        /// <summary>
        /// 分析数据
        /// </summary>
        public void AnalyseData()
        {
            while (true)
            {
                Thread.Sleep(2000);
                int packetLength = 0;

                //判断出是CID格式
                if (RAMBuffer[20] == 0x14)
                {
                    packetLength = 21;

                }
                //判断出是来电号码
                if ((RAMBuffer[28] == 0x14) && (RAMBuffer[49] == 0x14))
                {
                    packetLength = 50;
                    packet = new byte[packetLength];
                    while (RAMBufferLength >= 50)
                    {
                        //public static void Copy(Array sourceArray, Array destinationArray, int length);
                        //从第一个元素开始复制 System.Array 中的一系列元素，将它们粘贴到另一 System.Array 中（从第一个元素开始）。长度指定为32位整数。 
                        Array.Copy(RAMBuffer, packet, packetLength);
                        //数据统一左移
                        RemoveDataFromRAMBuffer(packetLength);

                        Event e = new Event();
                        e = ExecuteData(packet, packetLength);
                    }
                }

                //packet = new byte[packetLength];

                //if (RAMBufferLength >= 21)
                //{
                //        //public static void Copy(Array sourceArray, Array destinationArray, int length);
                //        //从第一个元素开始复制 System.Array 中的一系列元素，将它们粘贴到另一 System.Array 中（从第一个元素开始）。长度指定为32位整数。 
                //        Array.Copy(RAMBuffer, packet, packetLength);
                //        //数据统一左移
                //        RemoveDataFromRAMBuffer(packetLength);

                //        Event e = new Event();
                //        e = ExecuteData(packet, packetLength);
                //        EventDAL eventDal = new EventDAL();
                //        eventDal.Insert(e);
                //    //for (int i = 0; i < 1; i++)
                //    //{
                //    //    //public static void Copy(Array sourceArray, Array destinationArray, int length);
                //    //    //从第一个元素开始复制 System.Array 中的一系列元素，将它们粘贴到另一 System.Array 中（从第一个元素开始）。长度指定为32位整数。 
                //    //    Array.Copy(RAMBuffer, packet, packetLength);
                //    //    //数据统一左移
                //    //    RemoveDataFromRAMBuffer(packetLength);

                //    //    Event[] eArray = new Event[1];
                //    //    eArray[i] = ExecuteData(packet, packetLength);
                //    //    EventDAL eventDal = new EventDAL();
                //    //    eventDal.Insert(eArray);
                //    //}
                //}
            }
        }

        /// <summary>
        /// 校验接收到的数据
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        private bool CRCCheck(byte[] packet, int number)
        {
            bool flag = false;
            if (packet[number - 1] == 0x14)
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// 打包数据后将数组元素左移
        /// </summary>
        /// <param name="length"></param>
        private void RemoveDataFromRAMBuffer(int length)
        {
            //public static void ConstrainedCopy(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length);
            //从指定的源索引开始，复制 System.Array 中的一系列元素，将它们粘贴到另一 System.Array 中（从指定的目标索引开始）。
            //保证在复制未成功完成的情况下撤消所有更改。
            byte[] bufferArray = new byte[10240];
            Array.ConstrainedCopy(RAMBuffer, length, bufferArray, 0, RAMBufferLength - length);
            RAMBuffer = bufferArray;
            RAMBufferLength -= length;
        }

        /// <summary>
        /// 读出缓存池的第一个数据包
        /// </summary>
        /// <param name="number"></param>
        private void ReadRAMBufferFirstDataPacket(int length)
        {
            packet = new byte[length];
            //public static void Copy(Array sourceArray, Array destinationArray, int length);
            //从第一个元素开始复制 System.Array 中的一系列元素，将它们粘贴到另一 System.Array 中（从第一个元素开始）。长度指定为32位整数。
            Array.Copy(RAMBuffer, packet, length);
        }

        /// <summary>
        /// 分析收到的命令，并响应
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="length"></param>
        public Event ExecuteData(byte[] buffer, int length)
        {
            string _isNewEvent="R";
            string _cid="100";
            Event _event = new Event();

            //事件代码
            if (length == 21)
            {
                _event.Account = Encoding.ASCII.GetString(buffer, 7, 4);//主机编号
                _event.PartitionNumber = Encoding.ASCII.GetString(buffer, 15, 2);//分区编号
                _event.ZoneNumber = Encoding.ASCII.GetString(buffer, 17, 3);//防区编号
                _isNewEvent=Encoding.ASCII.GetString(buffer, 11, 1);
                _cid=Encoding.ASCII.GetString(buffer, 12, 3);
            }

            //来电代码
            if (length == 50)
            {
                _event.Account = Encoding.ASCII.GetString(buffer, 36, 4);//主机编号
                _event.PartitionNumber = Encoding.ASCII.GetString(buffer, 44, 2);//分区编号
                _event.ZoneNumber = Encoding.ASCII.GetString(buffer, 46, 3);//防区编号
                _isNewEvent = Encoding.ASCII.GetString(buffer, 40, 1);
                _cid = Encoding.ASCII.GetString(buffer, 41, 3);
                _event.TellNum = Encoding.ASCII.GetString(buffer, 10, 3);//来电号码e.TellNum = "";
            }

            //public virtual string GetString(byte[] bytes, int index, int count);
            //在派生类中重写时，将指定字节数组中的一个字节序列解码为一个字符串。

            _event.ID = Guid.NewGuid().ToString();
            _event.AlarmTime = DateTime.Now;

            //获取事件信息
            CIDDAL cidDAL = new CIDDAL();
            CID cid = new CID();
            cid = cidDAL.GetEventInformation(_isNewEvent, _cid);
            _event.EventTpye = cid.EventTpye;//获取事件类型
            _event.EventInfomation = cid.EventInformation; ;//获取辅助信息

            //获取报警事件相关的用户资料
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
            _event.ZoneType = (string)zone.ZoneType;
            _event.InstallSide = (string)zone.InstallSide;
            _event.DetectorType = (string)zone.DetectorType;

            _event.Classify = "待查";//归类处理e.Classify
            _event.DataCode = Encoding.ASCII.GetString(buffer);//通讯代码e.DataCode = "";

            StrategiesDAL strategiesDAL = new StrategiesDAL();
            Strategies strategies = new Strategies();
            strategies = strategiesDAL.GetStrategiesByStrategiesName(cid.StrategiesName);
            _event.EventFontColor = strategies.EventFontColor;//事件字体颜色
            _event.EventBackgroundColor = strategies.EventBackgroundColor;//事件背景颜色

            _event.Operator = "admin";//当前操作员e.Operator = "";
            _event.MarkEvent = "无";//处理内容e.Mark = "";
            _event.Side = "00";//站点编号e.Side = "";

            _event.TowLeverSide = "00";//二级站点编号e= "";

            //弹出消息提示窗口
            if (strategies.NoticeType == "声音和窗口提示")
            {
                MainWindow.isNoMarkEvents.Add(_event);
                ShowWindow(_event);
            }

            EventDAL eventDal = new EventDAL();
            eventDal.Insert(_event);

            //插入新[报警事件]的事件
            if (InsertDatabase != null)
                InsertDatabase(this, new EventArgs());
            return _event;
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
    }
}

//通常C#自定义事件有下面的几个步骤： 

//1、声明一个delegate: (用于事件的类型的定义） 如：
//public delegate void 事件名称EventHandler(object serder, EventArgs e);   
//事件名称用你的自己的来代替，随后的EventHandler是C#的建议命名规范，当然如果你不想遵守，可以使用任何字符甚至可以不要。 
//如果你想自定义事件的参数EventArgs,你可以从这个类派生你自己的事件参数类，然后在delegate的声明中，用你的参数类替换EventArgs 
//注：要全面了解自定义事件的原理，你需要学习有关delegate的知识。

//2、在你的类中声明一个事件，并且使用步骤1的delegate作为事件的类型： 
//public event 事件名称EventHandler 事件名称；  

//3、在你的类中需要触发事件的方法中，添加事件触发代码： 
//事件名称(this, new EventArgs());  或者： 
//if(事件名称!=null)   事件名称(this, new EventArgs());  
//如果使用你自己的事件参数类，你可以用你的参数类事例替换new EventArgs(), 同时在你的参数类中保存你需要传递的数据。 

//4、C#自定义事件注册： 
//事件注册和普通的事件注册没有不同，也就是说如果一个外部的对象在你的事件被触发的时候需要作出响应，那么你可以在外部了构造器中（或者适当的地方）对事件进行注册 
//带有事件的类实例.事件名称+= new 事件名称EventHandler( 事件处理方法名称);  

//5、编写事件处理方法： 
//public void 事件处理方法名称（object sender, EventArgs e)   {   //添加你的代码   }  
//注：如果你在类中处理自己的触发事件，你可以选择C#自定义事件步骤4和5的方式，也就是注册自己，也可以在触发事件代码中直接调用事件处理方法。

