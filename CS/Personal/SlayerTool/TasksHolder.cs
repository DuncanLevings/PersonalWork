using System.Collections.Generic;

namespace RunescapeSlayerHelper
{
    public sealed class TasksHolder
    {
        //singleton implementation
        private static TasksHolder instance;

        private List<Task> taskList = new List<Task>();

        private TasksHolder() { }
        
        public static TasksHolder Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TasksHolder();
                }
                return instance;
            }
        }

        internal List<Task> TaskList { get => taskList; }
        //--------------------
    }
}
