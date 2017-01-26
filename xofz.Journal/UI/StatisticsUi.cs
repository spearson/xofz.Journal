namespace xofz.Journal.UI
{
    using xofz.UI;
    public interface StatisticsUi : Ui
    {
        string TotalCount { get; set; }

        string CountThisMonth { get; set; }

        string CountThisYear { get; set; }

        string AvgTime { get; set; }

        string AvgTimeThisMonth { get; set; }

        string AvgTimeThisYear { get; set; }
    }
}
