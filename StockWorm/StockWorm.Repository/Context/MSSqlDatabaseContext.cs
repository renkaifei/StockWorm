using System.Data.SqlClient;
using System.Data;
using StockWorm.Utils;
using System;
using System.Data.Common;
using StockWorm.Domain;

namespace StockWorm.Repository.Context
{
    public class SqlDatabaseContext:DatabaseContext
    {
        public SqlDatabaseContext()
        {
            connectionString = AppSetting.GetInstance().GetMSSqlConnectionString();
            if(string.IsNullOrEmpty(connectionString)) throw new ArgumentException("数据库连接不能为空");
        }

        protected override void OpenConnection()
        {
            if(connection == null)
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
        }

        protected override DbParameter CreateDbParameter(string name,DbType dbType,object value)
        {
            return new SqlParameter(){ ParameterName=name,DbType = dbType,Value = value };
        }
    
        public override void CreateDatabase(DatabaseConfig config)
        {
            MSSqlDatabaseConfig mssqlConfig = config as MSSqlDatabaseConfig;
            connectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};",
                                    mssqlConfig.DataSource,"master",mssqlConfig.UserID,mssqlConfig.Password);
            string sqlDatabaseCheckSql = "select 1 From master.dbo.sysdatabases where name=@DatabaseName";
            SqlParameter prmDatabaseName = new SqlParameter("@DatabaseName",SqlDbType.NVarChar,128){ Value = mssqlConfig.InitialCatelog };
            bool existsDb = false;
            ExecuteDataReader(reader =>{
                existsDb = reader.HasRows;
            },sqlDatabaseCheckSql,prmDatabaseName);
            if(existsDb) throw new ArgumentException(string.Format("数据库[{0}]已经存在",mssqlConfig.InitialCatelog ));
            
            string databaseCreateSql = string.Format(@"CREATE DATABASE {0} ON PRIMARY
                                        (
                                            NAME= {1} ,
                                            FILENAME= '{2}',
                                            SIZE=100MB,
                                            filegrowth=50MB
                                        )
                                        LOG ON
                                        (
                                            name= {3},
                                            filename= '{4}',
                                            SIZE=100MB,
                                            filegrowth=50MB
                                        )",mssqlConfig.InitialCatelog ,string.Format("{0}_data",mssqlConfig.InitialCatelog ),mssqlConfig.DatabasePath + "\\" + string.Format("{0}_data",mssqlConfig.InitialCatelog) + ".mdf",
                                        string.Format("{0}_log",mssqlConfig.InitialCatelog),mssqlConfig.DatabasePath + "\\" + string.Format("{0}_log",mssqlConfig.InitialCatelog) + ".ldf");
            ExecuteNoQuery(databaseCreateSql);
            connectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};",
                                    mssqlConfig.DataSource,mssqlConfig.InitialCatelog,mssqlConfig.UserID,mssqlConfig.Password);
            string securityTableSql = @"create table Security(
                                SecurityID integer not null primary key identity(1,1),
                                SecurityCode varchar(10) not null,
                                SecurityAbbr varchar(128) not null,
                                CompanyCode varchar(128) not null,
                                CompanyAbbr varchar(128) not null,
                                ListingDate DateTime not null,
                                ExchangeMarket varchar(10) not null
                            )";
            string SecurityTaskTableSql = @"create table SecurityTask(
                            TaskID integer not null primary key identity(1,1),
                            SecurityCode varchar(10) not null,
                            ExchangeMarket varchar(10) not null,
                            BeginDate DateTime not null,
                            EndDate DateTime not null,
                            IsFinished int not null
                        );";
            string SecurityDayQuotationTableSql = @"create table SecurityDayQuotation
                                                    (
                                                        id integer not null primary key Identity(1,1),
                                                        SecurityCode varchar(10) not null,
                                                        TxDate DateTime not null,
                                                        ClosePrice float not null,
                                                        HighPrice float not null,
                                                        LowPrice float not null,
                                                        OpenPrice float not null,
                                                        LastClosePrice float not null,
                                                        PriceChange float not null,
                                                        Change float not null,
                                                        TurnOver float not null,
                                                        VolumeTurnOver float not null,
                                                        PriceTurnOver float not null,
                                                        MarketValue float not null,
                                                        NegoValue float not null
                                                    )";

            ExecuteNoQuery(securityTableSql);
            ExecuteNoQuery(SecurityTaskTableSql);
            ExecuteNoQuery(SecurityDayQuotationTableSql);
        }
    }
}