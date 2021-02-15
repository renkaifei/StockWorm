using System;
using System.Collections.Generic;
using StockWorm.Repository.Net;
using StockWorm.Repository.Context;
using StockWorm.Domain;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Text;

namespace StockWorm.Repository
{
    public abstract class SecurityDayQuotationRepository
    {
        public abstract void InsertIntoDB(List<SecurityDayQuotationDomain> securityDayQuotations);
        public abstract void InsertIntoDB(SecurityDayQuotationDomain dayQuotation);
    }
}