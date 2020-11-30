using ChartJs.Blazor.LineChart;
using Motiv.Core.Models;
using System.Collections.Generic;

namespace Motiv.Interfaces
{
    public interface IReportsController
    {
        LineConfig GenerateLineConfig();
        void UpdateConfig(LineConfig config, int daysToPlot);
        List<Transaction> GetTransactionHistoryPerDay(int days);
        List<Transaction> GetTransactionHistory(int days);
    }
}
