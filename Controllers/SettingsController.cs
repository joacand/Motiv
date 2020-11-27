using Motiv.Interfaces;
using Motiv.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motiv.Controllers
{
    public class SettingsController : ISettingsController
    {
        private readonly ITaskDatastore taskDatastore;
        private readonly IConfigurationDatastore configurationDatastore;
        private readonly IUserDataDatastore userDataDatastore;

        public SettingsController(
            ITaskDatastore taskDatastore,
            IConfigurationDatastore configurationDatastore,
            IUserDataDatastore userDataDatastore)
        {
            this.taskDatastore = taskDatastore ?? throw new ArgumentNullException(nameof(taskDatastore));
            this.configurationDatastore = configurationDatastore ?? throw new ArgumentNullException(nameof(configurationDatastore));
            this.userDataDatastore = userDataDatastore ?? throw new ArgumentNullException(nameof(userDataDatastore));
        }

        public async Task ClearAllData()
        {
            await taskDatastore.Clear();
            await configurationDatastore.Clear();
            await userDataDatastore.Clear();
        }

        public async Task<string> ExportAllData()
        {
            MotivDataAggregate dataAggregate = new()
            {
                Config = await configurationDatastore.Load(),
                Tasks = await taskDatastore.Load(),
                UserData = await userDataDatastore.Load()
            };
            return JsonConvert.SerializeObject(dataAggregate);
        }

        public async Task ImportAllData(string jsonData)
        {
            var dataAggregate = JsonConvert.DeserializeObject<MotivDataAggregate>(jsonData);
            await taskDatastore.Save(dataAggregate.Tasks);
            await configurationDatastore.Save(dataAggregate.Config);
            await userDataDatastore.Save(dataAggregate.UserData);
        }

        public async Task<string> ExportTaskTemplate()
        {
            var taskList = (await taskDatastore.Load())
                .Select(x => x.NoStateClone());
            return JsonConvert.SerializeObject(taskList);
        }

        public async Task ImportTaskTemplate(string jsonData)
        {
            var taskList = JsonConvert.DeserializeObject<List<MotivTask>>(jsonData);
            await taskDatastore.Save(taskList);
        }

        public async Task<Configuration> LoadConfiguration()
        {
            return await configurationDatastore.Load();
        }

        public async Task SaveConfiguration(Configuration configuration)
        {
            await configurationDatastore.Save(configuration);
        }
    }
}
