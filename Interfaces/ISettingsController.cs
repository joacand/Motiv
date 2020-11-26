using Motiv.Models;
using System.Threading.Tasks;

namespace Motiv.Interfaces
{
    public interface ISettingsController
    {
        Task ClearAllData();
        Task SaveConfiguration(Configuration configuration);
    }
}
