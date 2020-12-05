using Motiv.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Motiv.Core.Interfaces
{
    public interface IBalanceService
    {
        int Balance { get; }
        Task<int> SpendableBalance();
        Task Init();
        void SubtractBalance(MotivTask task);
        void AddBalance(MotivTask task);
        Task Save();
        List<Transaction> Transactions { get; }
        List<Transaction> GetTransactionsPerDay(int daysToFetch);
        List<Transaction> GetTransactions(int daysToFetch);
    }
}
