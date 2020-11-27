namespace Motiv.Models
{
    public record MotivTask(string TaskName, int Points)
    {
        public bool Completed { get; set; }
        public bool NotCompletable { get; set; } = false;

        public MotivTask NoStateClone()
        {
            return new MotivTask(TaskName, Points);
        }
    }
}
