using ChartJs.Blazor.Common;
using ChartJs.Blazor.Common.Axes;
using ChartJs.Blazor.Common.Axes.Ticks;
using ChartJs.Blazor.Common.Enums;
using ChartJs.Blazor.LineChart;
using ChartJs.Blazor.Util;
using Motiv.Core.Interfaces;
using Motiv.Core.Models;
using Motiv.Extensions;
using Motiv.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using static Motiv.Core.Constants.Chart;

namespace Motiv.Controllers
{
    public class ReportsController : IReportsController
    {
        private readonly IBalanceService balanceService;

        public ReportsController(IBalanceService balanceService)
        {
            this.balanceService = balanceService ?? throw new ArgumentNullException(nameof(balanceService));
        }

        public LineConfig GenerateLineConfig()
        {
            return new LineConfig
            {
                Options = new LineOptions
                {
                    Responsive = true,
                    Title = new OptionsTitle
                    {
                        Display = false
                    },
                    Tooltips = new Tooltips
                    {
                        Mode = InteractionMode.Nearest,
                        Intersect = true
                    },
                    Hover = new Hover
                    {
                        Mode = InteractionMode.Nearest,
                        Intersect = true
                    },
                    Scales = new Scales
                    {
                        XAxes = new List<CartesianAxis>
                        {
                            new CategoryAxis
                            {
                                ScaleLabel = new ScaleLabel
                                {
                                    LabelString = "Date",
                                    Display = true
                                }
                            }
                        },
                        YAxes = new List<CartesianAxis>
                        {
                            new LinearCartesianAxis
                            {
                                ScaleLabel = new ScaleLabel
                                {
                                    LabelString = "Points",
                                    Display = true
                                },
                                Ticks = new LinearCartesianTicks
                                {
                                    BeginAtZero=true
                                }
                            }
                        }
                    }
                }
            };
        }

        public List<Transaction> GetTransactionHistory(int days)
        {
            return balanceService.GetTransactions(days);
        }

        public List<Transaction> GetTransactionHistoryPerDay(int days)
        {
            return balanceService.GetTransactionsPerDay(days);
        }

        public void UpdateConfig(LineConfig config, int daysToPlot)
        {
            config.Data.Datasets.Clear();
            config.Data.Labels.Clear();

            IDataset<int> dataSet = new LineDataset<int>(balanceService.GetTransactionsPerDay(daysToPlot).Select(x => x.Balance))
            {
                Label = "Balance history",
                BackgroundColor = ColorUtil.FromDrawingColor(ChartColors.MotivLight),
                BorderColor = ColorUtil.FromDrawingColor(ChartColors.MotivDark),
                Fill = FillingMode.Origin
            };

            config.Data.Labels.AddRange(balanceService.GetTransactionsPerDay(daysToPlot));
            config.Data.Datasets.Add(dataSet);
        }
    }
}
