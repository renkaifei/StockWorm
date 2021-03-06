using System.ComponentModel;
using System.Windows.Forms;
using System;
using System.Drawing;

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

        private TabControl functionTab;
        private TableLayoutPanel tableLayoutPanel;
        private Button btnCalendar;

        #region UI界面
        public void InitializeComponent()
        {
            this.Text = "股票信息采集平台";
            this.ShowIcon = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(800, 500);
            this.WindowState = FormWindowState.Maximized;
            this.Load += new EventHandler(MainForm_Load);

            functionTab = new TabControl();
            functionTab.Dock = DockStyle.Fill;
            this.Controls.Add(functionTab);

            TabPage regularFunctionTabPage = new TabPage("常规");
            functionTab.TabPages.Add(regularFunctionTabPage);

            tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 3;
            tableLayoutPanel.RowCount = 3;

            regularFunctionTabPage.Controls.Add(tableLayoutPanel);

            btnCalendar = new Button() { Text = "日期" };
            btnCalendar.Height = 30;
            btnCalendar.Click += new EventHandler(btnCalendar_Click);
            tableLayoutPanel.Controls.Add(btnCalendar, 0, 0);
        }

        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnCalendar_Click(object sender, EventArgs e)
        {
            CalendarForm calendarForm = new CalendarForm();
            calendarForm.Owner = this;
            calendarForm.ShowDialog();
        }
    }
}
