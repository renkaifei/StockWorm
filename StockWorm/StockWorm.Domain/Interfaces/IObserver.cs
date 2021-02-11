
namespace StockWorm.Domain.Interfaces
{
    public interface IObserver
    {
        void Update(IObserver observer1,string message);
    }
}