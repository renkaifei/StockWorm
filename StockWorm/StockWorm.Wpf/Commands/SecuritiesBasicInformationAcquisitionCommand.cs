using System.Windows.Input;
using System;
using System.Collections.Generic;
using StockWorm.Domain;
using StockWorm.Application.Service;

namespace StockWorm.Wpf.Commands
{
    public class SecuritiesBasicInformationAcquisitionCommand:ICommand
    {
        private SecurityService service;
        private EventHandler canExecuteChanged;
        public event EventHandler CanExecuteChanged
        {
             add { canExecuteChanged += value; }
             remove{ canExecuteChanged -= value;}
        }

        public SecuritiesBasicInformationAcquisitionCommand(SecurityService service)
        {
            this.service = service;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //service.SaveSecuritiesFromSSEToSqlite();
        }
    }
}