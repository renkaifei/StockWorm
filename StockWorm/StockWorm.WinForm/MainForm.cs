using System.ComponentModel;
using System.Windows.Forms;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using StockWorm.Application.Service;
using StockWorm.Utils;
 
namespace StockWorm.WinForm
{
    public class MainForm : Form
    {
        private Container components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(true);
        }

        private MenuStrip mainMenuStrip;
        private ToolStripMenuItem menuSH;
        private ToolStripMenuItem menuSHStartCollecting;
        private ToolStripMenuItem menuSHStopCollecting;

        private TabControl functionTab;

        #region 系统初始化面板
        private TableLayoutPanel systemTableLayoutPanel;
        private Button btnDatabaseInitialize;
        #endregion

        #region 常规面板
        private TableLayoutPanel tableLayoutPanel;
        private Button btnCalendar;
        private Button btnStartSHCollecting;
        private Button btnStopSHCollecting;
        #endregion
        
        #region 日志面板
        private Label logLabel;
        #endregion
        
        private Task syncSecurityInfoFromSSETask;
        private bool isRunning = false;
        private System.Threading.Timer myTimer;

        #region UI界面
        public void InitializeComponent()
        {
            this.Text = "股票信息采集平台";
            this.ShowIcon = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1000, 800);
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.WindowState = FormWindowState.Normal;
            this.Load += new EventHandler(MainForm_Load);

            functionTab = new TabControl();
            functionTab.Dock = DockStyle.Fill;
            this.Controls.Add(functionTab);

            #region 系统初始化
            TabPage sysInitialize = new TabPage("系统初始化");
            functionTab.TabPages.Add(sysInitialize);

            systemTableLayoutPanel = new TableLayoutPanel();
            systemTableLayoutPanel.RowCount = 3;
            systemTableLayoutPanel.ColumnCount = 3;
            systemTableLayoutPanel.Dock = DockStyle.Fill;
            btnDatabaseInitialize = new Button(){ Text = "数据库初始化" };
            btnDatabaseInitialize.Size = new Size(120,30);
            btnDatabaseInitialize.Click += new EventHandler(btnDatabaseInitialize_Click);
            systemTableLayoutPanel.Controls.Add(btnDatabaseInitialize,0,0);
            sysInitialize.Controls.Add(systemTableLayoutPanel);
            #endregion

            #region 常规面板
            TabPage regularFunctionTabPage = new TabPage("常规");
            functionTab.TabPages.Add(regularFunctionTabPage);

            tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 3;
            tableLayoutPanel.RowCount = 3;
            tableLayoutPanel.Dock = DockStyle.Fill;

            regularFunctionTabPage.Controls.Add(tableLayoutPanel);

            btnCalendar = new Button() { Text = "日历",Height = 30,TextAlign = ContentAlignment.MiddleCenter};
            btnCalendar.Click += new EventHandler(btnCalendar_Click);
            tableLayoutPanel.Controls.Add(btnCalendar,0,0);

            btnStartSHCollecting = new Button(){ Text = "开始A股采集",Width =150, Height = 30,TextAlign = ContentAlignment.MiddleCenter };
            btnStartSHCollecting.Click += new EventHandler(btnStartSHCollecting_Click);
            tableLayoutPanel.Controls.Add(btnStartSHCollecting,1,0);

            btnStopSHCollecting = new Button(){ Text = "停止A股采集",Width =150,Height = 30,TextAlign = ContentAlignment.MiddleCenter };
            btnStopSHCollecting.Click += new EventHandler(btnStopSHCollecting_Click);
            tableLayoutPanel.Controls.Add(btnStopSHCollecting,2,0);
#endregion

            #region 日志
            TabPage logTabPage = new TabPage("日志");
            functionTab.TabPages.Add(logTabPage);

            logLabel = new Label();
            logLabel.AutoSize = false;
            logLabel.Dock = DockStyle.Fill;
            logTabPage.Controls.Add(logLabel);
#endregion

        }

        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btnDatabaseInitialize_Click(object sender,EventArgs e)
        {
            DatabaseSettingForm form = new DatabaseSettingForm();
            form.Owner = this;
            form.ShowDialog();
        }
        private void btnCalendar_Click(object sender, EventArgs e)
        {
            CalendarForm calendarForm = new CalendarForm();
            calendarForm.Owner = this;
            calendarForm.ShowDialog();
        }

        private void btnStartSHCollecting_Click(object sender,EventArgs e)
        {
            if(isRunning) return;
            ShowMessage("开启股票信息采集","");
            syncSecurityInfoFromSSE();
            DateTime nextDay = DateTime.Now.AddDays(1).Date;
            long dueTime = (long)(nextDay - DateTime.Now).TotalMilliseconds;
            long period = 1000 * 60 * 60 * 24;
            myTimer = new System.Threading.Timer((obj) =>{
                if(!isRunning) syncSecurityInfoFromSSE(); 
            },null,dueTime,period);
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
                    this.Invoke(new Action<string,string>(ShowMessage),ex.Message,"Error");
                }
                finally
                {
                    isRunning = false;
                    this.Invoke(new Action<string,string>(ShowMessage),"采集停止","");
                }
            },CancelTokenSingleton.GetInstance().Token,TaskCreationOptions.LongRunning,TaskScheduler.Default);
        }

        private void btnStopSHCollecting_Click(object sender,EventArgs e)
        {
            if(!isRunning) return;
            ShowMessage("停止股票信息采集","");
            isRunning = false;
            CancelTokenSingleton.GetInstance().Cancel();
        }

        private void ShowMessage(string message,string messageType)
        {
           logLabel.Text = logLabel.Text + string.Format("{0}:{1}:{2}",
           DateTime.Now.ToString(),messageType == "Error" ? "错误信息":"信息",message) + Environment.NewLine;
        }
    }
}
