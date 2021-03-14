using System.Windows.Forms;
using System.Drawing;
using System;
using StockWorm.Domain;
using StockWorm.Domain.Factory;
using StockWorm.Application.Service;
using StockWorm.WinForm.Error;

namespace StockWorm.WinForm
{
    public class DatabaseSettingForm:Form
    {
        public DatabaseSettingForm()
        {
            InitializeComponent();
        }

        #region sqlite 数据库配置
        private TableLayoutPanel sqliteLayoutPanel;
        private Label lblSqliteDatabaseName;
        private TextBox txtSqliteDatabaseName;
        private Label lblSqliteDatabasePath;
        private TextBox txtSqliteDatabasePath;
        private Button btnSqliteSelectDatabaseDir;
        #endregion

        #region mssql 数据库配置
        private TableLayoutPanel mssqlLayoutPanel;
        private Label lblMSSqlIP;
        private TextBox txtMSSqlIP;
        private Label lblMSSqlDatabaseName;
        private TextBox txtMSSqlDatabaseName;
        private Label lblMSSqlUserID;
        private TextBox txtMSSqlUserID;
        private Label lblMSSqlPwd;
        private TextBox txtMSSqlPwd;
        private Label lblMSSqlDatabasePath;
        private TextBox txtMSSqlDatabasePath;
        private Button btnMSSqlSelectDatabaseDir;
        #endregion
        
        #region 数据库配置框架
        private TableLayoutPanel databaseConfigLayoutPanel;
        private Label lblDatabaseType;
        private ComboBox comboDatabaseType;
        private Button btnOk;
        private Button btnCancel;
        private Panel pnlOperator;
        #endregion

        private CustomError customError;
        public void InitializeComponent()
        {
            #region Sqlite 配置
            this.sqliteLayoutPanel = new TableLayoutPanel();
            this.lblSqliteDatabaseName = new Label();
            this.txtSqliteDatabaseName = new TextBox();
            this.lblSqliteDatabasePath = new Label();
            this.txtSqliteDatabasePath = new TextBox();
            this.btnSqliteSelectDatabaseDir = new Button();
            #endregion 

            #region MSSql 配置
            this.mssqlLayoutPanel = new TableLayoutPanel();
            this.lblMSSqlIP = new Label();
            this.txtMSSqlIP = new TextBox();
            this.lblMSSqlDatabaseName = new Label();
            this.txtMSSqlDatabaseName = new TextBox();
            this.lblMSSqlUserID = new Label();
            this.txtMSSqlUserID = new TextBox();
            this.lblMSSqlPwd = new Label();
            this.txtMSSqlPwd = new TextBox();
            this.lblMSSqlDatabasePath = new Label();
            this.txtMSSqlDatabasePath = new TextBox();
            this.btnMSSqlSelectDatabaseDir = new Button();
            #endregion
            
            this.databaseConfigLayoutPanel = new TableLayoutPanel();
            this.lblDatabaseType = new Label();
            this.comboDatabaseType = new ComboBox();
            this.btnOk = new Button();
            this.btnCancel = new Button();
            this.pnlOperator = new Panel();

            this.pnlOperator.SuspendLayout();
            this.databaseConfigLayoutPanel.SuspendLayout();
            this.mssqlLayoutPanel.SuspendLayout();
            this.sqliteLayoutPanel.SuspendLayout();
            this.SuspendLayout();

            #region MSSql 配置
            this.lblMSSqlIP.Text = "服务器";
            this.lblMSSqlIP.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            
            this.txtMSSqlIP.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtMSSqlIP.Margin = new Padding(0,0,6,0);

            this.lblMSSqlDatabaseName.Text = "数据库名称";
            this.lblMSSqlDatabaseName.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            this.txtMSSqlDatabaseName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtMSSqlDatabaseName.Margin = new Padding(0,0,6,0);

            this.lblMSSqlUserID.Text = "用户名";
            this.lblMSSqlUserID.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            this.txtMSSqlUserID.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtMSSqlUserID.Margin = new Padding(0,0,6,0);

            this.lblMSSqlPwd.Text= "密码";
            this.lblMSSqlPwd.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            this.txtMSSqlPwd.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtMSSqlPwd.Margin = new Padding(0,0,6,0);

            this.lblMSSqlDatabasePath.Text = "路径";
            this.lblMSSqlDatabasePath.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            this.txtMSSqlDatabasePath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtMSSqlDatabasePath.ReadOnly = true;

            this.btnMSSqlSelectDatabaseDir.Text = "浏览";
            this.btnMSSqlSelectDatabaseDir.Size = new Size(50,30); 
            this.btnMSSqlSelectDatabaseDir.Margin = new Padding(0,0,6,0);
            this.btnMSSqlSelectDatabaseDir.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.btnMSSqlSelectDatabaseDir.Click += new EventHandler(btnSelectDatabaseDir_Click);

            this.mssqlLayoutPanel.RowCount = 5;
            this.mssqlLayoutPanel.ColumnCount = 3;
            this.mssqlLayoutPanel.Width = 360;
            this.mssqlLayoutPanel.Height = 200;
            this.mssqlLayoutPanel.RowStyles.Add(new RowStyle(){ SizeType = SizeType.Absolute,Height = 40 });
            this.mssqlLayoutPanel.RowStyles.Add(new RowStyle(){ SizeType = SizeType.Absolute,Height = 40 });
            this.mssqlLayoutPanel.RowStyles.Add(new RowStyle(){ SizeType = SizeType.Absolute,Height = 40 });
            this.mssqlLayoutPanel.RowStyles.Add(new RowStyle(){ SizeType = SizeType.Absolute,Height = 40 });
            this.mssqlLayoutPanel.RowStyles.Add(new RowStyle(){ SizeType = SizeType.Absolute,Height = 40 });
            this.mssqlLayoutPanel.ColumnStyles.Add(new ColumnStyle(){ SizeType = SizeType.Absolute,Width = 100 });
            this.mssqlLayoutPanel.ColumnStyles.Add(new ColumnStyle(){ SizeType = SizeType.Absolute, Width = 200 });
            this.mssqlLayoutPanel.ColumnStyles.Add(new ColumnStyle(){ SizeType = SizeType.Absolute, Width = 60 });
            this.mssqlLayoutPanel.Controls.Add(lblMSSqlIP,0,0);
            this.mssqlLayoutPanel.Controls.Add(txtMSSqlIP,1,0);
            this.mssqlLayoutPanel.SetColumnSpan(txtMSSqlIP,2);
            this.mssqlLayoutPanel.Controls.Add(lblMSSqlDatabaseName,0,1);
            this.mssqlLayoutPanel.Controls.Add(txtMSSqlDatabaseName,1,1);
            this.mssqlLayoutPanel.SetColumnSpan(txtMSSqlDatabaseName,2);
            this.mssqlLayoutPanel.Controls.Add(lblMSSqlUserID,0,2);
            this.mssqlLayoutPanel.Controls.Add(txtMSSqlUserID,1,2);
            this.mssqlLayoutPanel.SetColumnSpan(txtMSSqlUserID,2);
            this.mssqlLayoutPanel.Controls.Add(lblMSSqlPwd,0,3);
            this.mssqlLayoutPanel.Controls.Add(txtMSSqlPwd,1,3);
            this.mssqlLayoutPanel.SetColumnSpan(txtMSSqlPwd,2);
            this.mssqlLayoutPanel.Controls.Add(lblMSSqlDatabasePath,0,4);
            this.mssqlLayoutPanel.Controls.Add(txtMSSqlDatabasePath,1,4);
            this.mssqlLayoutPanel.Controls.Add(btnMSSqlSelectDatabaseDir,2,4);

            #endregion

            #region Sqlite 配置
            this.lblSqliteDatabaseName.Text = "数据库";
            this.lblSqliteDatabaseName.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            this.txtSqliteDatabaseName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtSqliteDatabaseName.Margin = new Padding(0,0,6,0);
            
            this.lblSqliteDatabasePath.Text = "路径";
            this.lblSqliteDatabasePath.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            this.txtSqliteDatabasePath.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtSqliteDatabasePath.Margin = new Padding(0,0,6,0);

            this.btnSqliteSelectDatabaseDir.Text = "浏览";
            this.btnSqliteSelectDatabaseDir.Size = new Size(50,30);
            this.btnSqliteSelectDatabaseDir.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.btnSqliteSelectDatabaseDir.Click += new EventHandler(btnSqliteSelectDatabaseDir_Click);

            this.sqliteLayoutPanel.RowCount = 2;
            this.sqliteLayoutPanel.ColumnCount = 3;
            this.sqliteLayoutPanel.Width = 360;
            this.sqliteLayoutPanel.Height = 80;
            this.sqliteLayoutPanel.RowStyles.Add(new RowStyle(){ SizeType = SizeType.Absolute,Height =40 });
            this.sqliteLayoutPanel.RowStyles.Add(new RowStyle(){ SizeType = SizeType.Absolute,Height = 40 });
            this.sqliteLayoutPanel.ColumnStyles.Add(new ColumnStyle(){ SizeType = SizeType.Absolute,Width = 100 });
            this.sqliteLayoutPanel.ColumnStyles.Add(new ColumnStyle(){ SizeType = SizeType.Absolute, Width = 200 });
            this.sqliteLayoutPanel.ColumnStyles.Add(new ColumnStyle(){ SizeType = SizeType.Absolute,Width = 60 });
            this.sqliteLayoutPanel.Controls.Add(lblSqliteDatabaseName,0,0);
            this.sqliteLayoutPanel.Controls.Add(txtSqliteDatabaseName,1,0);
            this.sqliteLayoutPanel.SetColumnSpan(txtSqliteDatabaseName,2);
            this.sqliteLayoutPanel.Controls.Add(lblSqliteDatabasePath,0,1);
            this.sqliteLayoutPanel.Controls.Add(txtSqliteDatabasePath,1,1);
            this.sqliteLayoutPanel.SetColumnSpan(txtSqliteDatabasePath,2);
            this.sqliteLayoutPanel.Controls.Add(btnSqliteSelectDatabaseDir,2,1);
            #endregion

            #region databaseConfig 布局
            this.lblDatabaseType.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.lblDatabaseType.Text = "数据库类型";

            this.comboDatabaseType.Items.Add("mssql");
            this.comboDatabaseType.Items.Add("sqlite");
            this.comboDatabaseType.SelectedIndex = 0;
            this.comboDatabaseType.SelectedIndexChanged +=  new EventHandler(comboDatabaseType_SelectedIndexChanged);
            this.comboDatabaseType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboDatabaseType.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            this.pnlOperator.Controls.Add(btnOk);
            this.pnlOperator.Controls.Add(btnCancel);
            this.pnlOperator.Margin = new Padding(0,0,3,5);
            this.pnlOperator.Dock = DockStyle.Fill;

            this.btnOk.Text = "确定";
            this.btnOk.Parent = pnlOperator;
            this.btnOk.Size = new Size(70,30);
            this.btnOk.Location = new Point(0,0);
            this.btnOk.Click += new EventHandler(btnOk_Click);

            this.btnCancel.Text = "取消";
            this.btnCancel.Parent = pnlOperator;
            this.btnCancel.Size = new Size(70,30);
            this.btnCancel.Location = new Point(76,0);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);

            this.databaseConfigLayoutPanel.RowCount = 4;
            this.databaseConfigLayoutPanel.ColumnCount = 2;
            this.databaseConfigLayoutPanel.RowStyles.Add(new RowStyle(){ SizeType = SizeType.Absolute,Height = 40 });
            this.databaseConfigLayoutPanel.RowStyles.Add(new RowStyle(){ SizeType = SizeType.Absolute ,Height = 200 });
            this.databaseConfigLayoutPanel.RowStyles.Add(new RowStyle(){ SizeType = SizeType.Absolute,Height = 0 });
            this.databaseConfigLayoutPanel.RowStyles.Add(new RowStyle(){ SizeType = SizeType.Absolute,Height = 40  });
            this.databaseConfigLayoutPanel.ColumnStyles.Add(new ColumnStyle(){ SizeType = SizeType.Absolute, Width = 100 });
            this.databaseConfigLayoutPanel.ColumnStyles.Add(new ColumnStyle(){ SizeType = SizeType.Absolute,Width = 260 });

            this.databaseConfigLayoutPanel.Controls.Add(lblDatabaseType,0,0);
            this.databaseConfigLayoutPanel.Controls.Add(comboDatabaseType,1,0);
            this.databaseConfigLayoutPanel.Controls.Add(mssqlLayoutPanel,0,1);
            this.databaseConfigLayoutPanel.SetColumnSpan(mssqlLayoutPanel,2);
            this.databaseConfigLayoutPanel.Controls.Add(sqliteLayoutPanel,0,2);
            this.databaseConfigLayoutPanel.SetColumnSpan(sqliteLayoutPanel,2);
            this.databaseConfigLayoutPanel.Controls.Add(pnlOperator,1,3);
            this.databaseConfigLayoutPanel.Location = new Point(50,20);
            this.databaseConfigLayoutPanel.Size = new Size(360,282);
            #endregion

            this.Text = "数据库初始化";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Size = new Size(460,400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ShowIcon = false;
            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
            this.Load += new EventHandler(DatabaseSettingForm_Load);
            this.Controls.Add(this.databaseConfigLayoutPanel);
            this.pnlOperator.ResumeLayout(false);
            this.pnlOperator.PerformLayout();
            this.sqliteLayoutPanel.ResumeLayout(false);
            this.sqliteLayoutPanel.PerformLayout();
            this.mssqlLayoutPanel.ResumeLayout(false);
            this.mssqlLayoutPanel.PerformLayout();
            this.databaseConfigLayoutPanel.ResumeLayout(false);
            this.databaseConfigLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void DatabaseSettingForm_Load(object sender,EventArgs e)
        {
            customError = new CustomError();
        }

        private void comboDatabaseType_SelectedIndexChanged(object sender,EventArgs e)
        {
            this.databaseConfigLayoutPanel.RowStyles.Clear();
            ComboBox comboBox= (ComboBox)sender;
            if(comboBox.SelectedIndex == 0)
            {
                this.databaseConfigLayoutPanel.RowStyles.Add(new RowStyle(){ SizeType = SizeType.Absolute,Height = 40 });
                this.databaseConfigLayoutPanel.RowStyles.Add(new RowStyle(){ SizeType = SizeType.Absolute,Height = 200 });
                this.databaseConfigLayoutPanel.RowStyles.Add(new RowStyle(){ SizeType = SizeType.Absolute,Height = 0 });
                this.databaseConfigLayoutPanel.RowStyles.Add(new RowStyle(){ SizeType = SizeType.Absolute, Height = 40 });
                this.databaseConfigLayoutPanel.Size = new Size(360,282);
            }
            else
            {
                this.databaseConfigLayoutPanel.RowStyles.Add(new RowStyle(){ SizeType = SizeType.Absolute,Height = 40 });
                this.databaseConfigLayoutPanel.RowStyles.Add(new RowStyle(){ SizeType = SizeType.Absolute,Height = 0 });
                this.databaseConfigLayoutPanel.RowStyles.Add(new RowStyle(){ SizeType = SizeType.Absolute,Height = 80 });
                this.databaseConfigLayoutPanel.RowStyles.Add(new RowStyle(){ SizeType = SizeType.Absolute, Height = 40 });
                this.databaseConfigLayoutPanel.Size = new Size(360,162);
            }
        }

        private void btnSelectDatabaseDir_Click(object sender,EventArgs e)
        {
            string tempPath = txtMSSqlDatabasePath.Text.Trim();
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = tempPath;
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                txtMSSqlDatabasePath.Text = dialog.SelectedPath;
            }
        }

        private void btnSqliteSelectDatabaseDir_Click(object sender,EventArgs e)
        {
            string tempPath = txtSqliteDatabasePath.Text.Trim();
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = tempPath;
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                txtSqliteDatabasePath.Text = dialog.SelectedPath;
            }
        }

        private void btnOk_Click(object sender,EventArgs e)
        {
            string databaseType = comboDatabaseType.Text;
            DatabaseConfigFactory configFactory = new DatabaseConfigFactory();
            DatabaseConfig config = configFactory.Create(databaseType);
            SecurityDatabaseService databaseService = new SecurityDatabaseService();
            if(databaseType == "mssql")
            {
                MSSqlDatabaseConfig tempConfig = (MSSqlDatabaseConfig)config;
                tempConfig.DataSource = txtMSSqlIP.Text.Trim();
                tempConfig.InitialCatelog = txtMSSqlDatabaseName.Text.Trim();
                tempConfig.UserID = txtMSSqlUserID.Text.Trim();
                tempConfig.Password = txtMSSqlPwd.Text.Trim();
                tempConfig.DatabasePath = txtMSSqlDatabasePath.Text.Trim();
                try
                {
                    databaseService.CreateDatabase(tempConfig);
                }
                catch(Exception ex)
                {
                    customError.ShowError(ex.Message);
                }
                
            }
            else
            {
                SqliteDatabaseConfig tempConfig = (SqliteDatabaseConfig)config;
                tempConfig.DataSource = txtSqliteDatabaseName.Text.Trim();
                try
                {
                    databaseService.CreateDatabase(tempConfig);
                }
                catch(Exception ex)
                {
                    customError.ShowError(ex.Message);
                }
                
            }
        }

        private void btnCancel_Click(object sender,EventArgs e)
        {
            this.Close();
        }
    }
}