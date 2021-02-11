using System;

namespace StockWorm.Domain.Interfaces
{
    public interface ISubject
    {
        void RegisterObserver();
        void RemoveObserver();
        void Notify();
    }
}