using StockWorm.Domain;
using System.Collections.Generic;
using StockWorm.Repository;

namespace StockWorm.Application.Service
{
    public class RealTimeStockTransactionService
    {
        public List<RealTimeStockTransaction> GetList(string stockCodes)
        {
            SecurityDayQuotationFromSinaRepository repo = new SecurityDayQuotationFromSinaRepository();
            return repo.GetList(stockCodes);
        }
    }
}