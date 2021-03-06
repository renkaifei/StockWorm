using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

namespace StockWorm.WinForm.Component
{
    public class CalendarGrid : TableLayoutPanel
    {
        private int year;
        public int Year
        {
            get { return year; }
            set
            {
                if (year == value) return;
                year = value;
            }
        }
        private int month;
        public int Month
        {
            get { return month; }
            set
            {
                if (month == value) return;
                month = value;
            }
        }

        private int cellWidth = 60;
        public int CellWidth
        {
            get { return this.cellWidth; }
            set
            {
                if (this.cellWidth == value) return;
                this.cellWidth = value;
            }
        }
        private int cellHeight = 40;
        public int CellHeight
        {
            get { return this.cellHeight; }
            set
            {
                if (cellHeight == value) return;
                cellHeight = value;
            }
        }

        private Button btnAddYear;
        private Button btnSubYear;
        private CalendarYearCell yearCell;
        private List<CalendarMonthCell> monthCells;
        private List<CalendarDayCell> dayCells;

        private CalendarCellFactory calendarCellFactory;

        public CalendarGrid()
        {
            calendarCellFactory = new CalendarCellFactory();
        }


        private void CalculateDateCell()
        {
            year = year == 0 ? DateTime.Now.Year: year;
            month = month == 0 ? DateTime.Now.Month:month;
            DateTime tempDate = new DateTime(this.year, this.month, 1);
            DateTime endDate = new DateTime(this.year, this.month, 1).AddMonths(1).AddDays(-1);
            CalendarDayCell dayCell;
            dayCells.Clear();
            while (tempDate <= endDate)
            {
                dayCell = (CalendarDayCell)calendarCellFactory.CreateCalendarCell("year");
                dayCell.Date = tempDate;
                dayCells.Add(dayCell);
                tempDate = tempDate.AddDays(1);
            }
        }

        public void Build()
        {
            BuildCalendar();
            FillCalendar();
        }

        private void BuildCalendar()
        {
            BuildCalendarFrame();
            BuildCalendarYearPart();
            BuildCalendarMonthPart();
            BuildCalendarDayHeaderPart();
            BuildCalendarDayPart();
        }

        private void BuildCalendarFrame()
        {
            this.RowCount = 10;
            this.ColumnCount = 7;
            for (int i = 0; i < this.RowCount; i++)
            {
                this.RowStyles.Add(new RowStyle() { SizeType = SizeType.Absolute, Height = this.CellHeight });
            }
            for (int i = 0; i < this.ColumnCount; i++)
            {
                this.ColumnStyles.Add(new ColumnStyle() { SizeType = SizeType.Absolute, Width = this.CellWidth });
            }
            this.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            this.Size = new Size(this.ColumnCount * this.CellWidth + this.ColumnCount + 1, this.RowCount * this.CellHeight + this.RowCount + 1);
        }

        private void BuildCalendarYearPart()
        {
            btnSubYear = new Button() { Text = "<", Dock = DockStyle.Fill,Width = CellWidth,Height= CellHeight };
            btnSubYear.Click += new EventHandler(GoPreousYear);
            this.Controls.Add(btnSubYear, 2, 0);
            btnAddYear = new Button() { Text = ">", Dock = DockStyle.Fill,Width = CellWidth,Height= CellHeight  };
            btnAddYear.Click += new EventHandler(GoNextYear);
            this.Controls.Add(btnAddYear, 4, 0);

            yearCell = (CalendarYearCell)calendarCellFactory.CreateCalendarCell("year");
            this.Controls.Add(yearCell, 3, 0);
        }

        private void BuildCalendarMonthPart()
        {
            this.monthCells = new List<CalendarMonthCell>();
            CalendarMonthCell januaryCell = CreateMonthCell(1);
            this.monthCells.Add(januaryCell);
            this.Controls.Add(januaryCell, 0, 1);

            CalendarMonthCell februaryCell = CreateMonthCell(2);
            this.monthCells.Add(februaryCell);
            this.Controls.Add(februaryCell, 1, 1);

            CalendarMonthCell marchCell = CreateMonthCell(3);
            this.monthCells.Add(marchCell);
            this.Controls.Add(marchCell, 2, 1);

            CalendarMonthCell aprilCell = CreateMonthCell(4);
            this.monthCells.Add(aprilCell);
            this.Controls.Add(aprilCell, 3, 1);

            CalendarMonthCell mayCell = CreateMonthCell(5);
            this.monthCells.Add(mayCell);
            this.Controls.Add(mayCell, 4, 1);

            CalendarMonthCell juneCell = CreateMonthCell(6);
            this.monthCells.Add(juneCell);
            this.Controls.Add(juneCell, 5, 1);

            CalendarMonthCell julyCell = CreateMonthCell(7);
            this.monthCells.Add(julyCell);
            this.Controls.Add(julyCell, 6, 1);

            CalendarMonthCell augustCell = CreateMonthCell(8);
            this.monthCells.Add(augustCell);
            this.Controls.Add(augustCell, 1, 2);

            CalendarMonthCell septemberCell = CreateMonthCell(9);
            this.monthCells.Add(septemberCell);
            this.Controls.Add(septemberCell, 2, 2);

            CalendarMonthCell octoberCell = CreateMonthCell(10);
            this.monthCells.Add(octoberCell);
            this.Controls.Add(octoberCell, 3, 2);

            CalendarMonthCell novemberCell = CreateMonthCell(11);
            this.monthCells.Add(novemberCell);
            this.Controls.Add(novemberCell, 4, 2);

            CalendarMonthCell decemberCell = CreateMonthCell(12);
            this.monthCells.Add(decemberCell);
            this.Controls.Add(decemberCell, 5, 2);
        }

        private CalendarMonthCell CreateMonthCell(int month)
        {
            CalendarMonthCell monthCell = (CalendarMonthCell)calendarCellFactory.CreateCalendarCell("month");
            monthCell.MonthCellMouseClick += new CalendarMonthCellMouseClickEventHandler(CalendarMonthCell_MouseClick);
            monthCell.Month = month;
            return monthCell;
        }

        private void CalendarMonthCell_MouseClick(object sender, CalendarMonthCellMouseClickEventArgs e)
        {
            foreach (CalendarMonthCell cell in monthCells)
            {
                if (cell.Month != e.Month) cell.IsSelected = false;
            }
            month = e.Month;
            FillDate();
            this.Invalidate();
        }

        private void BuildCalendarDayHeaderPart()
        {
            CalendarCell sunDayCell = calendarCellFactory.CreateCalendarCell("cell");
            sunDayCell.Text = "周日";
            sunDayCell.CellEnabled = false;
            this.Controls.Add(sunDayCell, 0, 3);

            CalendarCell monDayCell = calendarCellFactory.CreateCalendarCell("cell");
            monDayCell.Text = "周一";
            monDayCell.CellEnabled = false;
            this.Controls.Add(monDayCell, 1, 3);

            CalendarCell tuesdayCell = calendarCellFactory.CreateCalendarCell("cell");
            tuesdayCell.Text = "周二";
            tuesdayCell.CellEnabled = false;
            this.Controls.Add(tuesdayCell, 2, 3);

            CalendarCell wednesdayCell = calendarCellFactory.CreateCalendarCell("cell");
            wednesdayCell.Text = "周三";
            wednesdayCell.CellEnabled = false;
            this.Controls.Add(wednesdayCell, 3, 3);

            CalendarCell thursdayCell = calendarCellFactory.CreateCalendarCell("cell");
            thursdayCell.Text = "周四";
            thursdayCell.CellEnabled = false;
            this.Controls.Add(thursdayCell, 4, 3);

            CalendarCell fridayCell = calendarCellFactory.CreateCalendarCell("cell");
            fridayCell.Text = "周五";
            fridayCell.CellEnabled = false;
            this.Controls.Add(fridayCell, 5, 3);

            CalendarCell saturdayCell = calendarCellFactory.CreateCalendarCell("cell");
            saturdayCell.Text = "周六";
            saturdayCell.CellEnabled = false;
            this.Controls.Add(saturdayCell, 6, 3);
        }
        
        private void BuildCalendarDayPart()
        {
            this.dayCells = new List<CalendarDayCell>();
            for (int i = 4; i < 10; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    CalendarDayCell tempDayCell = (CalendarDayCell)calendarCellFactory.CreateCalendarCell("day");
                    tempDayCell.Click += new EventHandler(DayCalendarDayCell_Click);
                    dayCells.Add(tempDayCell);
                    this.Controls.Add(tempDayCell, j, i);
                }
            }
        }

        private void FillCalendar()
        {
            FillYear();
            FillMonth();
            SelectCurrentMonth();
            FillDate();
            SelectCurrentDay();
        }

        private void FillYear()
        {
            year = year == 0 ? DateTime.Now.Year : year;
            yearCell.Year = year;
        }

        private void FillMonth()
        {
            month = month == 0 ? DateTime.Now.Month : month;
        }
        private void FillDate()
        {
            DateTime tempDate = new DateTime(year, month, 1);
            int days = (int)tempDate.DayOfWeek;
            tempDate = tempDate.AddDays(-days);
            foreach (CalendarDayCell temp in dayCells)
            {
                temp.Date = tempDate;
                if (tempDate.Month != month)
                    temp.Enabled = false;
                else
                    temp.Enabled = true;
                tempDate = tempDate.AddDays(1);
            }
        }

        public void SelectCurrentMonth()
        {
            foreach (CalendarMonthCell cell in monthCells)
            {
                if (cell.Month == month)
                {
                    cell.IsSelected = true;
                    break;
                }
            }
        }

        public void SelectCurrentDay()
        {
            foreach (CalendarDayCell cell in dayCells)
            {
                if(cell.Date == DateTime.Now.Date) 
                    cell.IsSelected = true;
                else
                    cell.IsSelected = false;
            }
            
        }
        
        private void GoPreousYear(object sender, EventArgs e)
        {
            year = year - 1;
            yearCell.Year = year;
            FillDate();
        }

        private void GoNextYear(object sender, EventArgs e)
        {
            year = year + 1;
            yearCell.Year = year;
            FillDate();
        }

        private void DayCalendarDayCell_Click(object sender, EventArgs e)
        {
            foreach (CalendarDayCell item in dayCells)
            {
                item.IsSelected = false;
            }
            ((CalendarDayCell)sender).IsSelected = true;
        }
    }

    public class CalendarCell : Label
    {
        private bool isSelected = false;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected == value) return;
                isSelected = value;
                UpdateLabelUI();
            }
        }
        private bool cellEnabled = true;
        public bool CellEnabled 
        {
            get { return cellEnabled; }
            set
            {
                if(cellEnabled == value) return;
                cellEnabled = value;
            }
        }
        private void UpdateLabelUI()
        {
            if(!cellEnabled) return;
            if (isSelected)
            {
                this.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                this.BorderStyle = BorderStyle.None;
            }
        }

        public override ContentAlignment TextAlign
        {
            get { return ContentAlignment.MiddleCenter; }
        }

        protected override Size DefaultSize
        {
            get { return new Size(60, 40); }
        }

        protected override Padding DefaultMargin
        {
            get { return new Padding(0, 0, 0, 0); }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            IsSelected = true;
        }
    }

    public class CalendarYearCell : CalendarCell
    {
        private int year ;
        public int Year
        {
            get { return year;}
            set
            {
                if(year == value) return;
                year = value;
                Text = year.ToString();
            }
        }
    }

    public class CalendarMonthCell : CalendarCell
    {
        private int month = 0;
        public int Month
        {
            get { return month; }
            set
            {
                if (month == value) return;
                month = value;
                string result = "";
                switch (month)
                {
                    case 1:
                        result = "一月";
                        break;
                    case 2:
                        result = "二月";
                        break;
                    case 3:
                        result = "三月";
                        break;
                    case 4:
                        result = "四月";
                        break;
                    case 5:
                        result = "五月";
                        break;
                    case 6:
                        result = "六月";
                        break;
                    case 7:
                        result = "七月";
                        break;
                    case 8:
                        result = "八月";
                        break;
                    case 9:
                        result = "九月";
                        break;
                    case 10:
                        result = "十月";
                        break;
                    case 11:
                        result = "十一月";
                        break;
                    case 12:
                        result = "十二月";
                        break;
                    default:
                        throw new ArgumentException("月份参数不正确");
                }
                Text = result;
            }
        }

        public CalendarMonthCellMouseClickEventHandler MonthCellMouseClick;

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            CalendarMonthCellMouseClickEventArgs eventArgs = new CalendarMonthCellMouseClickEventArgs() { Month = month };
            if (MonthCellMouseClick != null) MonthCellMouseClick(this, eventArgs);
        }
    }

    public delegate void CalendarMonthCellMouseClickEventHandler(object sender, CalendarMonthCellMouseClickEventArgs e);
    public class CalendarMonthCellMouseClickEventArgs : EventArgs
    {
        private int month;
        public int Month
        {
            get { return month; }
            set
            {
                if (month == value) return;
                month = value;
            }
        }
    }

    public class CalendarDayCell : CalendarCell
    {
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                if (date == value) return;
                date = value;
                Text = date.Day.ToString();
            }
        }
    }

    public class CalendarCellFactory
    {
        public CalendarCell CreateCalendarCell(string cellType)
        {
            if (cellType == "year")
            {
                return new CalendarYearCell();
            }
            if (cellType == "month")
            {
                return new CalendarMonthCell();
            }
            else if (cellType == "day")
            {
                return new CalendarDayCell();
            }
            else if (cellType == "cell")
            {
                return new CalendarCell();
            }
            else
            {
                throw new ArgumentException(string.Format("单元格类型[{0}]不存在。", cellType));
            }
        }
    }
}
