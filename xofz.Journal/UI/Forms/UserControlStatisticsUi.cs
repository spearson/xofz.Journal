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
            get { return this.countTotalLabel.Text; }

            set { this.countTotalLabel.Text = value; }
        }

        string StatisticsUi.CountThisMonth
        {
            get { return this.countThisMonthLabel.Text; }

            set { this.countThisMonthLabel.Text = value; }
        }

        string StatisticsUi.CountThisYear
        {
            get { return this.countThisYearLabel.Text; }

            set { this.countThisYearLabel.Text = value; }
        }

        string StatisticsUi.AvgTime
        {
            get { return this.avgTimeLabel.Text; }

            set { this.avgTimeLabel.Text = value; }
        }

        string StatisticsUi.AvgTimeThisMonth
        {
            get { return this.avgTimeMonthLabel.Text; }

            set { this.avgTimeMonthLabel.Text = value; }
        }

        string StatisticsUi.AvgTimeThisYear
        {
            get { return this.avgTimeYearLabel.Text; }

            set { this.avgTimeYearLabel.Text = value; }
        }
    }
}
