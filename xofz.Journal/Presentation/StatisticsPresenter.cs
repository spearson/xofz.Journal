namespace xofz.Journal.Presentation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using xofz.Framework;
    using xofz.Journal.Framework;
    using xofz.Journal.UI;
    using xofz.Presentation;
    using xofz.UI;

    public sealed class StatisticsPresenter : Presenter
    {
        public StatisticsPresenter(
            StatisticsUi ui,
            MethodWeb web) :
            base(ui, null)
        {
            this.ui = ui;
            this.web = web;
        }

        public void Setup()
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            var w = this.web;
            w.Subscribe<xofz.Framework.Timer>(
                "Elapsed",
                this.timer_Elapsed,
                "StatisticsTimer");
            w.Run<Navigator>(n => n.RegisterPresenter(this));
        }

        public override void Start()
        {
            this.web.Run<xofz.Framework.Timer>(
                t => t.Start(1000), 
                "StatisticsTimer");
        }

        public override void Stop()
        {
            this.web.Run<xofz.Framework.Timer>(
                t => t.Stop(),
                "StatisticsTimer");
        }

        private void timer_Elapsed()
        {
            var w = this.web;
            var entries = w.Run<JournalEntriesHolder,
                MaterializedEnumerable<JournalEntry>>(holder => holder.Entries);

            var totalCount = entries.Count;
            UiHelpers.Write(this.ui, () => this.ui.TotalCount = totalCount.ToString());

            var countThisMonth = entries.Count(e =>
                e.ModifiedTimestamp >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
            UiHelpers.Write(this.ui, () => this.ui.CountThisMonth = countThisMonth.ToString());

            var countThisYear = entries.Count(e =>
                e.ModifiedTimestamp >= new DateTime(DateTime.Now.Year, 1, 1));
            UiHelpers.Write(this.ui, () => this.ui.CountThisYear = countThisYear.ToString());

            if (totalCount == 0)
            {
                UiHelpers.Write(this.ui, () =>
                {
                    this.ui.AvgTime = "0h, 0m, 0s";
                    this.ui.AvgTimeThisMonth = "0h, 0m, 0s";
                    this.ui.AvgTimeThisYear = "0h, 0m, 0s";
                });
                return;
            }

            var totalTime = this.getTotalTime(entries);
            var avgTime = new TimeSpan(totalTime.Ticks / totalCount);
            UiHelpers.Write(this.ui, () => this.ui.AvgTime
                = this.formatTimeSpan(avgTime));

            var entriesThisMonth = new LinkedList<JournalEntry>(
                entries.Where(
                    e => e.ModifiedTimestamp >= new DateTime(
                             DateTime.Now.Year,
                             DateTime.Now.Month,
                             1)));
            var timeThisMonth = this.getTotalTime(entriesThisMonth);
            var avgTimeThisMonth = new TimeSpan(timeThisMonth.Ticks / entriesThisMonth.Count);
            UiHelpers.Write(this.ui, () => this.ui.AvgTimeThisMonth
                = this.formatTimeSpan(avgTimeThisMonth));

            var entriesThisYear = new LinkedList<JournalEntry>(
                entries.Where(
                    e => e.ModifiedTimestamp >= new DateTime(
                             DateTime.Now.Year,
                             1,
                             1)));
            var timeThisYear = this.getTotalTime(entriesThisYear);
            var avgTimeThisYear = new TimeSpan(timeThisYear.Ticks / entriesThisYear.Count);
            UiHelpers.Write(this.ui, () => this.ui.AvgTimeThisYear = this.formatTimeSpan(avgTimeThisYear));
        }

        private TimeSpan getTotalTime(IEnumerable<JournalEntry> entries)
        {
            return entries.Aggregate(
                TimeSpan.Zero,
                (current, e) =>
                {
                    if (e.ModifiedTimestamp == null || e.CreatedTimestamp == null)
                    {
                        return current.Add(TimeSpan.Zero);
                    }

                    return current.Add(
                        e.ModifiedTimestamp.Value
                        - e.CreatedTimestamp.Value);
                });
        }

        private string formatTimeSpan(TimeSpan timeSpan)
        {
            return timeSpan.Days + "d, " 
                + timeSpan.Hours + "h, " 
                + timeSpan.Minutes + "m, " 
                + timeSpan.Seconds + "s";
        }

        private int setupIf1;
        private readonly StatisticsUi ui;
        private readonly MethodWeb web;
    }
}
