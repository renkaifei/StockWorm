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
using StockWorm.Domain;
using StockWorm.Wpf.Component;
using StockWorm.Utils;

namespace StockWorm.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Task syncSecurityInfoFromSSETask;
        private bool isRunning = false;
        private System.Threading.Timer myTimer;
        public MainWindow()
        {
            this.Loaded += new RoutedEventHandler(MainWindow_Load);
        }

        private void MainWindow_Load(object sender, RoutedEventArgs e)
        {
            SecurityDataTable securityDataTable = new SecurityDataTable();
        }

        private void syncSecurityInfoFromSSE()
        {
            isRunning = true;
            syncSecurityInfoFromSSETask = Task.Factory.StartNew(() =>{
                try
                {
                    SecurityService securityService = new SecurityService();
                    securityService.SyncSecuritiesFromSSE();
                    SecurityDayQuotationService dayQuotationService = new SecurityDayQuotationService();
                    dayQuotationService.SyncSSEDayQuotationFromWangYI();
                    
                }
                catch(OperationCanceledException)
                {
                    CancelTokenSingleton.GetInstance().BuildNewToken();
                }
                catch(Exception ex)
                {
                    this.Dispatcher.Invoke(new Action<string,string>(ShowMessage),ex.Message,"Error");
                }
                finally
                {
                    isRunning = false;
                    this.Dispatcher.Invoke(new Action<string,string>(ShowMessage),"采集停止","");
                }
            },CancelTokenSingleton.GetInstance().Token,TaskCreationOptions.LongRunning,TaskScheduler.Default);
        }

        private void BtnStartCollecting_Click(object sender,RoutedEventArgs e)
        {
            if(isRunning) return;
            ShowMessage("开启股票信息采集","");
            syncSecurityInfoFromSSE();
            DateTime nextDay = DateTime.Now.AddDays(1).Date;
            long dueTime = (long)(nextDay - DateTime.Now).TotalMilliseconds;
            long period = 1000 * 60 * 60 * 24;
            myTimer = new Timer((obj) =>{
                if(!isRunning) syncSecurityInfoFromSSE(); 
            },null,dueTime,period);
        }

        private void BtnStopCollecting_Click(object sender,RoutedEventArgs e)
        {
            if(!isRunning) return;
            ShowMessage("停止股票信息采集","");
            isRunning = false;
            CancelTokenSingleton.GetInstance().Cancel();
        }

        private void BtnSSESecurityInfo_Click(object sender,RoutedEventArgs e)
        {
            SecurityService securityService = new SecurityService();
            List<SecurityDomain> securities = securityService.GetListInPage(1,50,"SSE");
        }

        private void ShowMessage(string message,string messageType)
        {
           txtErrorMessage.Text = txtErrorMessage.Text + string.Format("{0}:{1}:{2}",
           DateTime.Now.ToString(),messageType == "Error" ? "错误信息":"信息",message) + Environment.NewLine;
        }
        
    }
}
