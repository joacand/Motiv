using System;

namespace Motiv.Core.Models
{
    public record Transaction(int Balance)
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string Type => "Temp";
        public string Description => "Temp";
        public int Amount => 123;

        public static Transaction NullValue => new Transaction(0);
    }
}
