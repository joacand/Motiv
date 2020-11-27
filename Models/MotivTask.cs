namespace Motiv.Models
{
    public record MotivTask(string TaskName, int Points)
    {
        public bool Completed { get; set; }

        public MotivTask NoStateClone()
        {
            return new MotivTask(TaskName, Points);
        }
    }
}
