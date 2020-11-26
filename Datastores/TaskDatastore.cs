﻿using Blazored.LocalStorage;
using Motiv.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Motiv.Datastores
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
            var taskList = await localStorageService.GetItemAsync<string>(Constants.Datastore.TaskListKey);

            List<MotivTask> result = taskList is not null
                ? JsonConvert.DeserializeObject<List<MotivTask>>(taskList)
                : new();

            return result;
        }

        public async Task Save(List<MotivTask> toSave)
        {
            var jsonSerialized = JsonConvert.SerializeObject(toSave);

            await localStorageService.SetItemAsync(Constants.Datastore.TaskListKey, jsonSerialized);
        }

        public async Task Clear()
        {
            await localStorageService.RemoveItemAsync(Constants.Datastore.TaskListKey);
        }
    }
}