using Blazored.LocalStorage;
using Motiv.Core;
using Motiv.Core.Interfaces;
using Motiv.Core.Models;
using System;
using System.Threading.Tasks;

namespace Motiv.Infrastructure.Datastores
{
    public class UserDataDatastore : IUserDataDatastore
    {
        private readonly ILocalStorageService localStorageService;

        public UserDataDatastore(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService ?? throw new ArgumentNullException(nameof(localStorageService));
        }

        public async Task<UserData> Load()
        {
            if (!await localStorageService.ContainKeyAsync(Constants.Datastore.UserDataKey))
            {
                return new();
            }

            var userData = await localStorageService.GetItemAsync<UserData>(Constants.Datastore.UserDataKey);

            UserData result = userData is not null
                ? userData
                : new();

            return result;
        }

        public async Task Save(UserData toSave)
        {
            await localStorageService.SetItemAsync(Constants.Datastore.UserDataKey, toSave);
        }

        public async Task Clear()
        {
            await localStorageService.RemoveItemAsync(Constants.Datastore.UserDataKey);
        }
    }
}
