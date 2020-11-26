using Motiv.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Motiv.Interfaces
{
    public interface ITaskListController
    {
        Task<List<MotivTask>> Load();
        Task Save(List<MotivTask> tasks);
    }
}
