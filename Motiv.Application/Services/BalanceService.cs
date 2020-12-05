using Motiv.Core.Interfaces;
using Motiv.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motiv.Application.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly IUserDataDatastore userDataDatastore;
        private readonly IConfigurationDatastore configurationDatastore;

        private UserData UserData { get; set; }

        public BalanceService(IUserDataDatastore userDataDatastore, IConfigurationDatastore configurationDatastore)
        {
            this.userDataDatastore = userDataDatastore ?? throw new ArgumentNullException(nameof(userDataDatastore));
            this.configurationDatastore = configurationDatastore ?? throw new ArgumentNullException(nameof(configurationDatastore));
        }

        public async Task Init()
        {
            if (UserData is null)
            {
                UserData = await userDataDatastore.Load();
            }
        }

        public void SubtractBalance(MotivTask task)
        {
            var newBalance = UserData.CurrentBalance - task.Points;
            UserData.CreateTransaction(newBalance, task, true);
        }

        public void AddBalance(MotivTask task)
        {
            var newBalance = UserData.CurrentBalance + task.Points;
            UserData.CreateTransaction(newBalance, task, false);
        }

        public async Task Save()
        {
            await userDataDatastore.Save(UserData);
        }

        public int Balance => UserData?.CurrentBalance ?? 0;

        public async Task<int> SpendableBalance()
        {
            var useOrLoseValue = (await configurationDatastore.Load()).UseOrLose.Value;
            var useOrLoseTransactions = Transactions
                .Reverse<Transaction>()
                .TakeWhile(x => (x.Date.Date - DateTime.UtcNow.AddDays(-1 * useOrLoseValue)).Days >= 0)
                .ToList();

            var spendableBalance = 0;

            foreach (var transaction in useOrLoseTransactions)
            {
                spendableBalance += transaction.Removal
                    ? -transaction.Amount
                    : transaction.Amount;
            }

            return spendableBalance > 0 ? spendableBalance : 0;
        }

        public List<Transaction> Transactions => UserData?.Transactions ?? new();

        public List<Transaction> GetTransactionsPerDay(int daysToFetch)
        {
            var perDay = Transactions.GroupBy(x => x.Date.Date).TakeLast(daysToFetch);

            List<Transaction> result = new();

            foreach (var dateTransactions in perDay)
            {
                var latestEntry = dateTransactions.LastOrDefault();
                if (latestEntry is not null)
                {
                    result.Add(latestEntry with
                    {
                        Date = latestEntry.Date
                    });
                }
            }

            return result;
        }

        public List<Transaction> GetTransactions(int daysToFetch)
        {
            List<Transaction> result = new();

            var perDay = Transactions.GroupBy(x => x.Date.Date).TakeLast(daysToFetch);

            foreach (var dateTransactions in perDay)
            {
                foreach (var transaction in dateTransactions)
                {
                    result.Add(transaction with
                    {
                        Date = transaction.Date
                    });
                }
            }

            return result;
        }
    }
}
