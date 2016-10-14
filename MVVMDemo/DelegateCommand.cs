using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
//定义DelegateCommand类。目的是绑定命令属性。这个类的作用是实现了ICommand接口，WPF中实现了ICommand接口的类，才能作为命令绑定到UI。命令的知识，后面详细讨论。
namespace MVVMDemo
{
    class DelegateCommand : ICommand
    {
        //A method prototype without return value.
        public Action<object> ExecuteCommand = null;
        //A method prototype return a bool type.
        public Func<object, bool> CanExecuteCommand = null;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (CanExecuteCommand != null)
            {
                return this.CanExecuteCommand(parameter);
            }
            else
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            if (this.ExecuteCommand != null) this.ExecuteCommand(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}