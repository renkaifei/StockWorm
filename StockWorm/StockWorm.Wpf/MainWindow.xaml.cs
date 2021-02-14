using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using StockWorm.Application.Service;

namespace StockWorm.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Task syncSecurityInfoFromSSETask;
        private CancellationTokenSource syncSecurityInfoFromSSETaskSource;
        private bool isRunning = false;
        private System.Threading.Timer myTimer;
        public MainWindow()
        {
            this.Loaded += new RoutedEventHandler(MainWindow_Load);
        }

        private void MainWindow_Load(object sender, RoutedEventArgs e)
        {
            syncSecurityInfoFromSSE();
            DateTime nextDay = DateTime.Now.AddDays(1).Date;
            long dueTime = (long)(nextDay - DateTime.Now).TotalMilliseconds;
            long period = 1000 * 60 * 60 * 24;
            myTimer = new Timer((obj) =>{
                if(!isRunning) syncSecurityInfoFromSSE(); 
            },null,dueTime,period);
        }

         private void syncSecurityInfoFromSSE()
        {
            syncSecurityInfoFromSSETaskSource = new CancellationTokenSource();
            syncSecurityInfoFromSSETask = Task.Factory.StartNew(() =>{
                try
                {
                    SecurityService securityService = new SecurityService();
                    securityService.SyncSecuritiesFromSSE();
                    SecurityDayQuotationService dayQuotationService = new SecurityDayQuotationService();
                    dayQuotationService.SyncSSEDayQuotationFromWangYI();
                }
                catch(Exception ex)
                {
                    this.Dispatcher.Invoke(new Action<string>(ShowErrorMessage),ex.StackTrace);
                }
                finally
                {
                    isRunning = false;
                    syncSecurityInfoFromSSETaskSource.Cancel();
                }
            },syncSecurityInfoFromSSETaskSource.Token,TaskCreationOptions.LongRunning,TaskScheduler.Default);
            isRunning = true;
        }

        private void BtnBasicSecurityInfoAcquisition_Click(object sender,RoutedEventArgs e)
        {
            
        }
    
        private void ShowErrorMessage(string message)
        {
           lblErrorMessage.Content = message;
        }
    
        private void BtnStockDayQuotation_Click(object sender,RoutedEventArgs e)
        {
            
        }
    }
}
