using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SoodamaniHospital.Commands
{
    public class RelayCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public Action<T> commandHandler;

        #region Ctor
        /// <summary>
        /// Intialize the new instance for the RelayCommand
        /// </summary>
        public RelayCommand(Action<T> handler)
        {
            this.commandHandler = handler;
        }

        #endregion

        #region ICommand Implementations
        /// <summary>
        ///  
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            bool canexecute;
            if(parameter !=null)
            {
                canexecute = parameter is T;
            }
            else
            {
                canexecute = false;
            }
            return canexecute;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
           if(parameter !=null && this.commandHandler !=null && parameter is T)
            {
                this.commandHandler((T)parameter);
            }
        }

        #endregion
    }
}
