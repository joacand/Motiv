using System;

namespace Motiv.Core.Models
{
    public record Transaction(int Balance, int Amount, string Description)
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string Type { get; set; } = Amount < 0
            ? "Loss"
            : "Profit";
    }
}
