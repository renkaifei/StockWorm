using System.Collections.Generic;
using StockWorm.Domain;
using StockWorm.Repository;
using System;

namespace StockWorm.Application
{
    public class SecurityTaskEngine
    {
        private Queue<SecurityTaskDomain> queue;

        public SecurityTaskEngine()
        {
            queue = new Queue<SecurityTaskDomain>();
        }

        public SecurityTaskDomain Pop()
        {
            if(queue.Count == 0) 
            {
                GetSecurityTasksInDB();
            }
            if(queue.Count == 0)
                return new SecurityTaskDomain();
            else
                return queue.Dequeue();
        }

        private void GetSecurityTasksInDB()
        {
            SecurityTaskRepository repo = new SecurityTaskRepository();
            List<SecurityTaskDomain> lst = repo.GetListUnFinished(200,DateTime.Now.Date);
            foreach(SecurityTaskDomain securityTask in lst)
            {
                queue.Enqueue(securityTask);
            }
        }
    }
}