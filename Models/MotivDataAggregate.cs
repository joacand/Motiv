using System.Collections.Generic;

namespace Motiv.Models
{
    public sealed class MotivDataAggregate
    {
        public Configuration Config { get; set; }
        public List<MotivTask> Tasks { get; set; }
        public UserData UserData { get; set; }
    }
}
