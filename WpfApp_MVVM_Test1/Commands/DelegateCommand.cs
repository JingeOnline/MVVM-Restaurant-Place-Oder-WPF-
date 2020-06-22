using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp_MVVM_Test1.Commands
{
    class DelegateCommand : ICommand
    {
        //当CanExecute状态改变的时候，通知命令的调用者
        public event EventHandler CanExecuteChanged;

        //帮助命令的呼叫者判断该命令是否允许执行
        public bool CanExecute(object parameter)
        {
            //如果委托没有指向任何方法，返回true，可以执行
            if (this.CanExecuteFunc == null)
            {
                return true;
            }
            //把要执行的命令委托给CanExecuteFunc所指向的方法
            return this.CanExecuteFunc(parameter);
        }
        //所执行的命令
        public void Execute(object parameter)
        {
            //如果委托没有指向任何方法，直接返回
            if (this.ExecuteAction == null)
            {
                return;
            }
            //把要执行的命令委托给EcecuteAction所指向的方法
            this.ExecuteAction(parameter);
        }

        //声明一个委托属性
        public Action<object> ExecuteAction { get; set; }

        //声明一个有返回值的委托属性
        public Func<object, bool> CanExecuteFunc { get; set; }
    }
}
