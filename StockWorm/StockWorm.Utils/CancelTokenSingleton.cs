using System;
using System.Threading;

namespace StockWorm.Utils
{
    public class CancelTokenSingleton
    {
        private static CancelTokenSingleton instance;
        private static object obj = new object();
        private CancellationTokenSource source;
        private CancelTokenSingleton()
        {
            source = new CancellationTokenSource();
        }

        public static CancelTokenSingleton GetInstance()
        {
            if(instance == null)
            {
                lock(obj)
                {
                    if(instance == null)
                    {
                        instance = new CancelTokenSingleton();
                    }
                }
            }
            return instance;
        }

        public CancellationToken Token
        {
            get { return source.Token;  }
        }

        public void Cancel()
        {
            source.Cancel();
            source = new CancellationTokenSource();
        }
    }
}
