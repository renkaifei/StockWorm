using System.ComponentModel;
using System;
using StockWorm.Domain.Interfaces;

namespace StockWorm.Domain
{
    public class StockTask
    {
        private int taskID = 0;
        [Description("任务ID")]
        public int TaskID 
        {
            get { return taskID; }
            set
            {
                if(taskID == 0) return;
                taskID = value;
            }
        }

        private string taskName = "";
        [Description("任务名称")]
        public string TaskName
        {
            get { return taskName; }
            set
            {
                if(taskName == value) return;
                taskName = value;
            }
        }

        private string taskDescription = "";
        [Description("任务描述")]
        public string TaskDescription
        {
            get { return taskDescription; }
            set
            {
                if(taskDescription == value) return;
                taskDescription = value;
            }
        }

        public string taskStatus = "";
        [Description("任务状态")]
        public string TaskStatus
        {
            get { return taskStatus; }
            set
            {
                if(taskStatus == value) return;
                taskStatus =value;
            }
        }

        private string taskResult = "";
        [Description("任务结果")]
        public string TaskResult
        {
            get{ return taskResult; }
            set
            {
                if(taskResult == value) return;
                taskResult = value;
            }
        }
    
        private DateTime createTime;
        [Description("创建时间")]
        public DateTime CreateTime
        {
            get { return createTime; }
            set{
                if(createTime == value) return;
                createTime = value;
            }
        }

        private DateTime executeTime;
        [Description("任务执行时间")]
        public DateTime ExecuteTime
        {
            get { return executeTime; }
            set
            {
                if(executeTime == value) return;
                executeTime = value;
            }
        }

        private DateTime finishTime;
        [Description("完成时间")]
        public DateTime FinishTime
        {
            get{ return finishTime; }
            set
            {
                if(finishTime == value) return;
                finishTime = value;
            }
        }
    
        private ICreateDataBehavior createDataBehavior;

        public void RegisterCreateDataBehavior(ICreateDataBehavior createDataBehavior)
        {
            this.createDataBehavior = createDataBehavior;
        }


    }
}