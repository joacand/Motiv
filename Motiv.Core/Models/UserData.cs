using System.Collections.Generic;
using System.Linq;

namespace Motiv.Core.Models
{
    public record UserData
    {
        public int CurrentBalance => Transactions.LastOrDefault()?.Balance ?? default;
        public List<Transaction> Transactions { get; set; } = new();

        public void CreateTransaction(int newBalance)
        {
            Transactions.Add(new Transaction(newBalance));
        }
    }
}
