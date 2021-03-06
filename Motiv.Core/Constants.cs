﻿using System.Drawing;

namespace Motiv.Core
{
    public static class Constants
    {
        public static class Datastore
        {
            public static string TaskListKey => "motiv.TaskListKey";
            public static string ConfigurationKey => "motiv.ConfigurationKey";
            public static string UserDataKey => "motiv.UserData";
        }

        public static class Chart
        {
            public static class ChartColors
            {
                public static readonly Color Red = Color.FromArgb(255, 99, 132);
                public static readonly Color Orange = Color.FromArgb(255, 159, 64);
                public static readonly Color Yellow = Color.FromArgb(255, 205, 86);
                public static readonly Color Green = Color.FromArgb(75, 255, 192);
                public static readonly Color Blue = Color.FromArgb(54, 162, 235);
                public static readonly Color Purple = Color.FromArgb(153, 102, 255);
                public static readonly Color Grey = Color.FromArgb(201, 203, 207);
                public static readonly Color MotivDark = Color.FromArgb(48, 50, 74);
                public static readonly Color MotivLight = Color.FromArgb(72, 80, 116);
            }
        }

        public static class CSS
        {
            public static string PositiveStyle => "taskListItemPos";
            public static string NegativeStyle => "taskListItemNeg";
        }
    }
}
