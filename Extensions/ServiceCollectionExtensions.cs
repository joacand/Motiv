using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection;
using Motiv.Controllers;
using Motiv.Datastores;
using Motiv.Interfaces;
using Motiv.Services;
using System;
using System.Net.Http;

namespace Motiv.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services, string baseAddress)
        {
            services.AddHttpClient(baseAddress);
            services.AddLocalStorage();
            services.AddDatastores();
            services.AddControllers();
            services.AddApplicationServices();
        }

        private static void AddHttpClient(this IServiceCollection services, string baseAddress)
        {
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
        }

        private static void AddLocalStorage(this IServiceCollection services)
        {
            services.AddBlazoredLocalStorage(config =>
                config.JsonSerializerOptions.WriteIndented = true);
        }

        private static void AddDatastores(this IServiceCollection services)
        {
            services.AddScoped<ITaskDatastore, TaskDatastore>();
            services.AddScoped<IConfigurationDatastore, ConfigurationDatastore>();
        }

        private static void AddControllers(this IServiceCollection services)
        {
            services.AddScoped<ISettingsController, SettingsController>();
            services.AddScoped<ITaskListController, TaskListController>();
            services.AddScoped<IUserDataDatastore, UserDataDatastore>();
        }

        private static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBalanceService, BalanceService>();
        }
    }
}
