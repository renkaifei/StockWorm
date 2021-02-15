using StockWorm.Repository.Context;
using StockWorm.Domain;
using System.Collections.Generic;

namespace StockWorm.Repository
{
    //证券仓库
    public abstract class SecurityRepository
    {
        public abstract List<SecurityDomain> GetList();
        public abstract void InsertIntoDB(List<SecurityDomain> securities);
        public abstract void InsertIntoDB(SecurityDomain security);
        public abstract List<SecurityDomain> GetListInPage(int pageIndex,int pageSize,string exchange);
    }
}