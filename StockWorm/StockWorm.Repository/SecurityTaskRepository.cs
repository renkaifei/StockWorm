using StockWorm.Domain;
using StockWorm.Repository.Context;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;
using StockWorm.Domain.Factory;
using System;

namespace StockWorm.Repository
{
    public abstract class SecurityTaskRepository
    {
        public abstract SecurityTaskDomain InsertIntoDB(SecurityTaskDomain securityTask);
        public abstract List<SecurityTaskDomain> GetListUnFinished(int count,DateTime date);
        public abstract void UpdateTaskStatus(SecurityTaskDomain securityTask);
    }
}
