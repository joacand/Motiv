using Motiv.Core.Models;
using System.Threading.Tasks;

namespace Motiv.Interfaces
{
    public interface ISettingsController
    {
        Task ClearAllData();
        Task SaveConfiguration(Configuration configuration);
        Task<Configuration> LoadConfiguration();
        Task<string> ExportAllData();
        Task ImportAllData(string jsonData);
        Task<string> ExportTaskTemplate();
        Task ImportTaskTemplate(string jsonData);
    }
}
