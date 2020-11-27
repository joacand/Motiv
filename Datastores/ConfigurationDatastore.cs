using Blazored.LocalStorage;
using Motiv.Interfaces;
using Motiv.Models;
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
            var configuration = await localStorageService.GetItemAsync<Configuration>(Constants.Datastore.ConfigurationKey);

            Configuration result = configuration is not null
                ? configuration
                : new();

            return result;
        }

        public async Task Save(Configuration toSave)
        {
            await localStorageService.SetItemAsync(Constants.Datastore.ConfigurationKey, toSave);
        }

        public async Task Clear()
        {
            await localStorageService.RemoveItemAsync(Constants.Datastore.ConfigurationKey);
        }
    }
}
