using System;

namespace Motiv.Core.Models
{
    public record Transaction(int Balance)
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public static Transaction NullValue => new Transaction(0);
    }
}
