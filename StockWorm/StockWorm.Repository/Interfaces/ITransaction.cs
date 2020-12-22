
namespace StockWorm.Repository.Interfaces
{
    public interface ITransaction
    {
        void JoinTrans(SqliteTransactionRepository sqliteTransaction);
    }
}