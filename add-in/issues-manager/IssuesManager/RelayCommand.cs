﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CodeStack.SwEx.AddIn.Examples.IssuesManager
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private readonly Action<object> m_Execute;
        private readonly Func<object, bool> m_CanExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            m_Execute = execute;
            m_CanExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return m_CanExecute != null ? m_CanExecute.Invoke(parameter) : true;
        }

        public void Execute(object parameter)
        {
            m_Execute.Invoke(parameter);
        }
    }
}
