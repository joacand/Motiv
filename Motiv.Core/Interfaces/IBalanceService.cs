using Motiv.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Motiv.Core.Interfaces
{
    public interface IBalanceService
    {
        int Balance { get; }
        Task Init();
        void SubtractBalance(int points);
        void AddBalance(int points);
        Task Save();
        List<Transaction> Transactions { get; }
        List<Transaction> GetTransactionsPerDay(int daysToFetch);
        List<Transaction> GetTransactions(int daysToFetch);
    }
}
