using Motiv.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Motiv.Interfaces
{
    public interface ITaskListController
    {
        Task Init();
        Task<List<MotivTask>> Load();
        Task Save(List<MotivTask> tasks);
        Task CompleteTask(MotivTask taskCompleted, bool completeState, List<MotivTask> allTasks);
        int Balance { get; }
    }
}
