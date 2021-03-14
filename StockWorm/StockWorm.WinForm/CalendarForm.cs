using System.Windows.Forms;
using System.ComponentModel;
using StockWorm.WinForm.Component;
using System.Drawing;
using System;
using System.Text;

namespace StockWorm.WinForm
{
    public class CalendarForm : Form
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region UI控件
        private TableLayoutPanel layoutPanel;
        private CalendarGrid calendarGrid;
        private TableLayoutPanel settingLayoutPanel;
        private Button btnBuildCalendar;
        private Button btnSetHoliday;
        
        #endregion

        private void InitializeComponent()
        {
            this.layoutPanel = new TableLayoutPanel();
            this.calendarGrid = new CalendarGrid();
            this.settingLayoutPanel = new TableLayoutPanel();
            this.btnBuildCalendar = new Button();
            this.btnSetHoliday = new Button();
            this.settingLayoutPanel.SuspendLayout();
            this.layoutPanel.SuspendLayout();
            this.SuspendLayout();

            settingLayoutPanel.RowCount = 5;
            settingLayoutPanel.ColumnCount = 2;
            settingLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single ;
            settingLayoutPanel.Dock = DockStyle.Fill;
            settingLayoutPanel.Controls.Add(btnBuildCalendar,0,0);
            settingLayoutPanel.Controls.Add(btnSetHoliday,1,0);

            btnBuildCalendar.Text = "插入日历";
            btnBuildCalendar.Size = new Size(100,30);

            btnSetHoliday.Text = "设置假期";
            btnSetHoliday.Size = new Size(100,30);

            this.layoutPanel.RowCount = 1;
            this.layoutPanel.ColumnCount = 2;
            this.layoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            this.layoutPanel.Dock = DockStyle.Fill;
            this.layoutPanel.Controls.Add(this.calendarGrid,0,0);
            this.layoutPanel.Controls.Add(settingLayoutPanel,1,0);

            this.Text = "日历";
            this.Size = new Size(800, 500);
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowIcon = false;
            this.Load += new EventHandler(CalendarForm_Load);
            this.Controls.Add(this.layoutPanel);
            this.settingLayoutPanel.ResumeLayout(false);
            this.settingLayoutPanel.PerformLayout();
            this.layoutPanel.ResumeLayout(false);
            this.layoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public CalendarForm()
        {
            InitializeComponent();
        }

        private void CalendarForm_Load(object sender,EventArgs e)
        {
            calendarGrid.Build();
        }

        private void btn1_Click(object sender,EventArgs e)
        {
            DateTime beginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            int rowCount = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) / 7;
            if (beginDate.DayOfWeek <= endDate.DayOfWeek)
            {
                rowCount = rowCount + 1;
            }
        }
    }
}
