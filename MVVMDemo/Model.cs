using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
//开始定义Model类。一个属性成员"WPF",它就是数据属性，的有通知功能，它改变后，会知道通知UI更新。一个方法“Copy”,用来改变属性“WPF”的值，它通过命令的方式相应UI事件。
namespace MVVMDemo
{
    class Model : NotificationObject
    {
        private string _wpf = "WPF";

        public string WPF
        {
            get { return _wpf; }
            set
            {
                _wpf = value;
                this.RaisePropertyChanged("WPF");
            }
        }        

        public void Copy(object obj)
        {
            this.WPF += " WPF";
        }
        
    }
}