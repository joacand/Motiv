using Motiv.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Motiv.Controllers
{
    public class TaskListController : ITaskListController
    {
        private readonly ITaskDatastore taskDatastore;

        public TaskListController(ITaskDatastore taskDatastore)
        {
            this.taskDatastore = taskDatastore ?? throw new ArgumentNullException(nameof(taskDatastore));
        }

        public async Task<List<MotivTask>> Load()
        {
            return await taskDatastore.Load();
        }

        public async Task Save(List<MotivTask> tasks)
        {
            await taskDatastore.Save(tasks);
        }
    }
}
