using Microsoft.Data.Sqlite;
using System;
using StockWorm.Utils;
using System.Data;
using System.Data.Common;
using System.IO;
using StockWorm.Domain;

namespace StockWorm.Repository.Context
{
    public class SqliteDatabaseContext:DatabaseContext
    {
        public SqliteDatabaseContext()
        {
            connectionString = AppSetting.GetInstance().GetSqliteConnectionString();
            if(string.IsNullOrEmpty(connectionString)) throw new ArgumentException("数据库连接不能为空");
        }
        protected override void OpenConnection()
        {
            if(connection == null)
            {
                connection = new SqliteConnection(connectionString);
                connection.Open();
            }
        }
        protected override DbParameter CreateDbParameter(string name,DbType dbType,object value)
        {
            return new SqliteParameter(){ ParameterName=name,DbType = dbType,Value = value };
        }

        public override void CreateDatabase(DatabaseConfig config)
        {
            SqliteDatabaseConfig sqliteConfig = config as SqliteDatabaseConfig;
            FileInfo sqliteFile = new FileInfo(sqliteConfig.DataSource);
            if(File.Exists(sqliteConfig.DataSource)) 
            {
                throw new Exception("指定的数据库已经存在");
            }
            connectionString = string.Format("Data Source={0};Cache=Shared;",
                                    sqliteConfig.DataSource);
            string securityTableSql = @"create table if not exists Security
                                        (
                                            SecurityID integer not null primary key AUTOINCREMENT,
                                            SecurityCode varchar(10) not null,
                                            SecurityAbbr varchar(128) not null,
                                            CompanyCode varchar(128) not null,
                                            CompanyAbbr varchar(128) not null,
                                            ListingDate DateTime not null,
                                            ExchangeMarket varchar(10) not null
                                        )";
            string SecurityTaskTableSql = @"create table if not exists SecurityTask(
                                            TaskID integer not null primary key AUTOINCREMENT,
                                            SecurityCode varchar(10) not null,
                                            ExchangeMarket varchar(10) not null,
                                            BeginDate DateTime not null,
                                            EndDate DateTime not null,
                                            IsFinished int not null
                                        )";
            string SecurityDayQuotationTableSql = @"create table if not exists SecurityDayQuotation
                                            (
                                                id integer not null primary key AUTOINCREMENT,
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