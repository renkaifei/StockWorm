using System.Windows.Forms;
using System.Drawing;
using System;
using StockWorm.Application;

namespace StockWorm.WinForm
{
    public class FormMain:Form
    {
        #region 控件
        private Button btnStartTask ;
        #endregion

        #region 服务
        private SecurityService securityService;
        #endregion

        public FormMain()
        {
            CustomInitComponents();
            CustomInitService();
        }

        private void CustomInitComponents()
        {
            this.Text = "主窗体";
            this.Size = new Size(800,600);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.btnStartTask = new Button();
            this.btnStartTask.Text = "同步A股信息";
            this.btnStartTask.AutoSize = true;
            this.btnStartTask.Click += new EventHandler(BtnStartTask_Click);
            this.Controls.Add(this.btnStartTask);
        }

        private void CustomInitService()
        {
            securityService = new SecurityService();
        }

        private void DisableControlExcept(Control[] controls)
        {
            this.btnStartTask.Enabled = false;
            
        }

        private void BtnStartTask_Click(object sender,EventArgs e)
        {
             
        }

    }

}