using Motiv.Core.Interfaces;
using Motiv.Core.Models;
using Motiv.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motiv.Controllers
{
    public class TaskListController : ITaskListController
    {
        private readonly ITaskDatastore taskDatastore;
        private readonly IBalanceService balanceService;

        public async Task<int> Balance()
        {
            return await balanceService.SpendableBalance();
        }

        public TaskListController(
            ITaskDatastore taskDatastore,
            IBalanceService balanceService)
        {
            this.taskDatastore = taskDatastore ?? throw new ArgumentNullException(nameof(taskDatastore));
            this.balanceService = balanceService ?? throw new ArgumentNullException(nameof(balanceService));
        }

        public async Task Init()
        {
            await balanceService.Init();
        }

        public async Task CompleteTask(MotivTask taskCompleted, bool completeState, List<MotivTask> allTasks)
        {
            var taskToEdit = allTasks.FirstOrDefault(x => x.Equals(taskCompleted));

            if (taskToEdit is not null)
            {
                taskToEdit.SetCompleted(completeState);

                if (completeState)
                {
                    balanceService.AddBalance(taskToEdit);
                }
                else
                {
                    balanceService.SubtractBalance(taskToEdit);
                }

                ChangeCompletableState(allTasks);
                await Save(allTasks);
                await balanceService.Save();
            }
        }

        public async Task<List<MotivTask>> Load()
        {
            return await taskDatastore.Load();
        }

        public async Task Save(List<MotivTask> tasks)
        {
            await taskDatastore.Save(tasks);
        }

        private void ChangeCompletableState(List<MotivTask> allTasks)
        {
            foreach (var task in allTasks)
            {
                task.NotCompletable = !task.Completed &&
                    balanceService.Balance < (-1 * task.Points);
            }
        }
    }
}
