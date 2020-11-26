using Motiv.Interfaces;
using Motiv.Models;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Motiv.Controllers
{
    public class SettingsController : ISettingsController
    {
        private readonly ITaskDatastore taskDatastore;

        public SettingsController(ITaskDatastore taskDatastore)
        {
            this.taskDatastore = taskDatastore ?? throw new ArgumentNullException(nameof(taskDatastore));
        }

        public async Task ClearAllData()
        {
            await taskDatastore.Clear();
        }

        public Task ExportAllData()
        {
            Log.Information("Recieved ExportAllData");
            return Task.CompletedTask;
        }

        public Task ExportTaskTemplate()
        {
            Log.Information("Recieved ExportTaskTemplate");
            return Task.CompletedTask;
        }

        public Task ImportAllData()
        {
            Log.Information("Recieved ImportAllData");
            return Task.CompletedTask;
        }

        public Task ImportTaskTemplate()
        {
            Log.Information("Recieved ImportTaskTemplate");
            return Task.CompletedTask;
        }

        public Task SaveConfiguration(Configuration configuration)
        {
            Log.Information("Recieved save config with object {@configuration}", configuration);
            return Task.CompletedTask;
        }
    }
}
