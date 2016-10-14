using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//定义ViewModel类。定义了一个命令属性"CopyCmd"，聚合了一个Model对象"model"。这里的关键是，给CopyCmd命令指定响应命令的方法是model对象的“Copy”方法。
namespace MVVMDemo
{
    class ViewModel
    {
        public DelegateCommand CopyCmd { get; set; }        
        public Model model { get; set; }

        public ViewModel()
        {
            this.model = new Model();
            this.CopyCmd = new DelegateCommand();
            this.CopyCmd.ExecuteCommand = new Action<object>(this.model.Copy);
        }
    }
}