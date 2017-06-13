namespace xofz.Journal.UI.Forms
{
    using xofz.UI.Forms;

    public partial class UserControlStatisticsUi : UserControlUi, StatisticsUi
    {
        public UserControlStatisticsUi()
        {
            this.InitializeComponent();
        }

        string StatisticsUi.TotalCount
        {
            get => this.countTotalLabel.Text;

            set => this.countTotalLabel.Text = value;
        }

        string StatisticsUi.CountThisMonth
        {
            get => this.countThisMonthLabel.Text;

            set => this.countThisMonthLabel.Text = value;
        }

        string StatisticsUi.CountThisYear
        {
            get => this.countThisYearLabel.Text;

            set => this.countThisYearLabel.Text = value;
        }

        string StatisticsUi.AvgTime
        {
            get => this.avgTimeLabel.Text;

            set => this.avgTimeLabel.Text = value;
        }

        string StatisticsUi.AvgTimeThisMonth
        {
            get => this.avgTimeMonthLabel.Text;

            set => this.avgTimeMonthLabel.Text = value;
        }

        string StatisticsUi.AvgTimeThisYear
        {
            get => this.avgTimeYearLabel.Text;

            set => this.avgTimeYearLabel.Text = value;
        }
    }
}
