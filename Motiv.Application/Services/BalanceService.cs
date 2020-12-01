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

        private UserData UserData { get; set; }

        public BalanceService(IUserDataDatastore userDataDatastore)
        {
            this.userDataDatastore = userDataDatastore ?? throw new ArgumentNullException(nameof(userDataDatastore));
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
            UserData.CreateTransaction(newBalance, task);
        }

        public void AddBalance(MotivTask task)
        {
            var newBalance = UserData.CurrentBalance + task.Points;
            UserData.CreateTransaction(newBalance, task);
        }

        public async Task Save()
        {
            await userDataDatastore.Save(UserData);
        }

        public int Balance => UserData?.CurrentBalance ?? 0;

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
