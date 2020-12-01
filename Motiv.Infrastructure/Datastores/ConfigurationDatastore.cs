using Blazored.LocalStorage;
using Motiv.Core;
using Motiv.Core.Interfaces;
using Motiv.Core.Models;
using System;
using System.Threading.Tasks;

namespace Motiv.Infrastructure.Datastores
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
            if (!await localStorageService.ContainKeyAsync(Constants.Datastore.ConfigurationKey))
            {
                return new();
            }

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
