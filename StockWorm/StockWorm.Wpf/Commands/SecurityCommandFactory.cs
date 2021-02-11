using System.Windows.Input;
using System.Collections.Generic;
using StockWorm.Domain;
using StockWorm.Application.Service;

namespace StockWorm.Wpf.Commands
{
    public class SecurityCommandFactory
    {
        public static ICommand BuildSecuritiesBasicInformationAcquisitionCommand()
        {
            SecurityService service = new SecurityService();
            ICommand command = new SecuritiesBasicInformationAcquisitionCommand(service);
            return command;
        }
    }
}