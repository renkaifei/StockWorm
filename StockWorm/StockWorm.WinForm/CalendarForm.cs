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
        private CalendarGrid calendarGrid;
        private Button btn1;
        #endregion

        private void InitializeComponent()
        {
            this.Text = "日历";
            this.Size = new Size(400, 400);
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowIcon = false;
            this.Load += new EventHandler(CalendarForm_Load);


            calendarGrid = new CalendarGrid();
            this.Controls.Add(calendarGrid);

            btn1 = new Button(){ Text = "aaa",Location = new Point(200,200) };
            btn1.Click += new EventHandler(btn1_Click);
            this.Controls.Add(btn1);
        }

        public CalendarForm()
        {
            InitializeComponent();
        }

        private void CalendarForm_Load(object sender,EventArgs e)
        {
            this.Size = new Size(600,800);
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
