using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
//首先定义NotificationObject类。目的是绑定数据属性。这个类的作用是实现了INotifyPropertyChanged接口。WPF中类要实现这个接口，其属性成员才具备通知UI的能力，数据绑定的知识，后面详细讨论。
namespace MVVMDemo
{
    class NotificationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}