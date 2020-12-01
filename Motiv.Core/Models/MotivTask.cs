using System;

namespace Motiv.Core.Models
{
    public record MotivTask(string TaskName, int Points, TimeSpan ResetTime, int DaysUntilReset)
    {
        private bool completed;

        public bool NotCompletable { get; set; } = false;

        public DateTime TimeCompleted { get; set; } = DateTime.MinValue;

        public bool Completed {
            get {
                return
                    completed &&
                    NotResetByDays() &&
                    NotResetByResetTime();
            }
            set {
                completed = value;
                TimeCompleted = DateTime.UtcNow;
            }
        }

        private bool NotResetByResetTime()
        {
            return DateTime.UtcNow.Subtract(TimeCompleted) < ResetTime;
        }

        private bool NotResetByDays()
        {
            return (DateTime.UtcNow.Date - TimeCompleted.Date).Days < DaysUntilReset;
        }

        public MotivTask NoStateClone()
        {
            return new MotivTask(TaskName, Points, ResetTime, DaysUntilReset);
        }

        public static MotivTask OneDayTask(string taskName, int points) =>
            new MotivTask(taskName, points, TimeSpan.MaxValue, DaysUntilReset: 1);

        public string Style()
        {
            return Points > 0
                ? Constants.CSS.PositiveStyle
                : Constants.CSS.NegativeStyle;
        }
    }
}
