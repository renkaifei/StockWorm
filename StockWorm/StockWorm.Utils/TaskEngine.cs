using System;
using System.Threading;

namespace StockWorm.Utils
{
    public class TaskEngine
    {
        private static TaskEngine instance;
        private static object obj = new object();
        private Timer timer;

        private TaskEngine()
        {
            
        }

        public TaskEngine GetInstance()
        {
            if(instance == null)
            {
                lock(obj)
                {
                    if(instance == null)
                    {
                        instance = new TaskEngine();
                    }
                }
            }
            return instance;
        }

        public void StartJob(TaskJob job)
        {
            
        }
    }
}