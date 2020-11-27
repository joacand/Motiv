using Blazored.LocalStorage;
using Motiv.Interfaces;
using Motiv.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Motiv.Datastores
{
    public class ConfigurationDatastore : IConfigurationDatastore
    {
        private readonly ILocalStorageService localStorageService;

        public ConfigurationDatastore(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService ?? throw new ArgumentNullException(nameof(localStorageService));
        }

        public async Task<Configuration> Load()
        {
            var configuration = await localStorageService.GetItemAsStringAsync(Constants.Datastore.ConfigurationKey);

            Configuration result = !string.IsNullOrWhiteSpace(configuration)
                ? JsonConvert.DeserializeObject<Configuration>(configuration)
                : new();

            return result;
        }

        public async Task Save(Configuration toSave)
        {
            var jsonSerialized = JsonConvert.SerializeObject(toSave);

            await localStorageService.SetItemAsync(Constants.Datastore.ConfigurationKey, jsonSerialized);
        }

        public async Task Clear()
        {
            await localStorageService.RemoveItemAsync(Constants.Datastore.ConfigurationKey);
        }
    }
}
