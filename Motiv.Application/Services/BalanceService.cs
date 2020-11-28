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

        public void SubtractBalance(int points)
        {
            var newBalance = UserData.CurrentBalance - points;
            UserData.CreateTransaction(newBalance);
        }

        public void AddBalance(int points)
        {
            var newBalance = UserData.CurrentBalance + points;
            UserData.CreateTransaction(newBalance);
        }

        public async Task Save()
        {
            await userDataDatastore.Save(UserData);
        }

        public int Balance => UserData?.CurrentBalance ?? 0;

        public List<Transaction> Transactions => UserData?.Transactions ?? new();

        public List<Transaction> TransactionsPerDay(int daysToFetch)
        {
            var allTransactions = Transactions;

            var perDay = allTransactions.GroupBy(x => x.Date.Date).TakeLast(daysToFetch);

            List<Transaction> result = new();

            foreach (var dateTransactions in perDay)
            {
                var latestEntry = dateTransactions.LastOrDefault();
                if (latestEntry is not null)
                {
                    result.Add(new Transaction(latestEntry.Balance)
                    {
                        Date = latestEntry.Date
                    });
                }
            }

            return result;
        }
    }
}
