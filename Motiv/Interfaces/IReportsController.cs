using ChartJs.Blazor.LineChart;

namespace Motiv.Interfaces
{
    public interface IReportsController
    {
        LineConfig GenerateLineConfig();
        void UpdateConfig(LineConfig config, int daysToPlot);
    }
}
