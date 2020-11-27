using Blazored.LocalStorage;
using Motiv.Core;
using Motiv.Core.Interfaces;
using Motiv.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Motiv.Infrastructure.Datastores
{
    public class TaskDatastore : ITaskDatastore
    {
        private readonly ILocalStorageService localStorageService;

        public TaskDatastore(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService ?? throw new ArgumentNullException(nameof(localStorageService));
        }

        public async Task<List<MotivTask>> Load()
        {
            var taskList = await localStorageService.GetItemAsync<List<MotivTask>>(Constants.Datastore.TaskListKey);

            List<MotivTask> result = taskList is not null
                ? taskList
                : new();

            return result;
        }

        public async Task Save(List<MotivTask> toSave)
        {
            await localStorageService.SetItemAsync(Constants.Datastore.TaskListKey, toSave);
        }

        public async Task Clear()
        {
            await localStorageService.RemoveItemAsync(Constants.Datastore.TaskListKey);
        }
    }
}
