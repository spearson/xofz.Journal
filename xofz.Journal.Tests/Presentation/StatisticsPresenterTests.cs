namespace xofz.Journal.Tests.Presentation
{
    using System;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Materialization;
    using xofz.Journal.Framework;
    using xofz.Journal.Presentation;
    using xofz.Journal.UI;
    using xofz.Presentation;
    using Xunit;

    public class StatisticsPresenterTests
    {
        public class Context
        {
            protected Context()
            {
                this.ui = A.Fake<StatisticsUi>();
                this.web = new MethodWeb();
                this.presenter = new StatisticsPresenter(
                    this.ui,
                    this.web);
                this.timer = A.Fake<xofz.Framework.Timer>();
                this.entriesHolder = A.Fake<JournalEntriesHolder>();
                this.navigator = A.Fake<Navigator>();

                this.web.RegisterDependency(this.timer, "StatisticsTimer");
                this.web.RegisterDependency(this.entriesHolder);
                this.web.RegisterDependency(this.navigator);

                this.fixture = new Fixture();
            }

            protected readonly StatisticsUi ui;
            protected readonly MethodWeb web;
            protected readonly StatisticsPresenter presenter;
            protected readonly xofz.Framework.Timer timer;
            protected readonly JournalEntriesHolder entriesHolder;
            protected readonly Navigator navigator;
            protected readonly Fixture fixture;
        }

        public class When_Setup_is_called : Context
        {
            [Fact]
            public void Subscribes_to_the_timer_elapsed_event()
            {
                var w = A.Fake<MethodWeb>();
                var p = new StatisticsPresenter(A.Fake<StatisticsUi>(), w);

                p.Setup();

                A.CallTo(() => w.Subscribe<xofz.Framework.Timer>(
                    "Elapsed",
                    A<Action>.Ignored,
                    "StatisticsTimer")).MustHaveHappened();
            }

            [Fact]
            public void Registers_itself_with_the_navigator()
            {
                this.presenter.Setup();

                A.CallTo(() =>
                        this.navigator.RegisterPresenter(
                            this.presenter))
                    .MustHaveHappened();
            }
        }

        public class When_Start_is_called : Context
        {
            [Fact]
            public void Starts_the_timer()
            {
                this.presenter.Start();

                A.CallTo(() => this.timer.Start(A<int>.Ignored)).MustHaveHappened();
            }

            [Fact]
            public void Starts_the_timer_with_a_1000_millisecond_interval()
            {
                this.presenter.Start();

                A.CallTo(() => this.timer.Start(1000)).MustHaveHappened();
            }
        }

        public class When_Stop_is_called : Context
        {
            [Fact]
            public void Stops_the_timer()
            {
                this.presenter.Stop();

                A.CallTo(() => this.timer.Stop()).MustHaveHappened();
            }
        }

        public class When_the_timer_elapses : Context
        {
            [Fact]
            public void Accesses_entries_from_entries_holder()
            {
                this.presenter.Setup();

                this.timer.Elapsed += Raise.With<Action>();

                A.CallTo(() => this.entriesHolder.Entries).MustHaveHappened();
            }

            [Fact]
            public void Sets_the_total_count()
            {
                this.presenter.Setup();
                A.CallTo(() => this.entriesHolder.Entries).Returns(
                    new LinkedListMaterializedEnumerable<JournalEntry>(
                        new[]
                        {
                            new JournalEntry(),
                            new JournalEntry(),
                            new JournalEntry()
                        }));

                this.timer.Elapsed += Raise.With<Action>();

                Assert.Equal("3", this.ui.TotalCount);
            }

            [Fact]
            public void Sets_count_this_month()
            {
                this.presenter.Setup();
                A.CallTo(() => this.entriesHolder.Entries).Returns(
                    new LinkedListMaterializedEnumerable<JournalEntry>(
                        new[]
                        {
                            new JournalEntry { ModifiedTimestamp = DateTime.Now },
                            new JournalEntry { ModifiedTimestamp = DateTime.Now.AddMonths(-1) },
                            new JournalEntry { ModifiedTimestamp = DateTime.Now },
                            new JournalEntry { ModifiedTimestamp = DateTime.MinValue }
                        }));

                this.timer.Elapsed += Raise.With<Action>();

                Assert.Equal("2", this.ui.CountThisMonth);
            }

            [Fact]
            public void Sets_count_this_year()
            {
                this.presenter.Setup();
                A.CallTo(() => this.entriesHolder.Entries).Returns(
                    new LinkedListMaterializedEnumerable<JournalEntry>(
                        new[]
                        {
                            new JournalEntry { ModifiedTimestamp = DateTime.Now },
                            new JournalEntry { ModifiedTimestamp = DateTime.Now.AddYears(-1) },
                            new JournalEntry { ModifiedTimestamp = DateTime.Now },
                            new JournalEntry { ModifiedTimestamp = DateTime.MinValue },
                            new JournalEntry { ModifiedTimestamp = DateTime.Now }
                        }));

                this.timer.Elapsed += Raise.With<Action>();

                Assert.Equal("3", this.ui.CountThisYear);
            }

            [Fact]
            public void If_total_count_is_zero_then_zeros_out_average_time()
            {
                this.presenter.Setup();
                A.CallTo(() => this.entriesHolder.Entries).Returns(
                    new LinkedListMaterializedEnumerable<JournalEntry>());
                this.ui.AvgTime = this.fixture.Create<string>();

                this.timer.Elapsed += Raise.With<Action>();

                Assert.Equal("0h 0m 0s", this.ui.AvgTime);
            }

            [Fact]
            public void If_total_count_is_zero_then_zeros_out_average_time_this_month()
            {
                this.presenter.Setup();
                A.CallTo(() => this.entriesHolder.Entries).Returns(
                    new LinkedListMaterializedEnumerable<JournalEntry>());
                this.ui.AvgTimeThisMonth = this.fixture.Create<string>();

                this.timer.Elapsed += Raise.With<Action>();

                Assert.Equal("0h 0m 0s", this.ui.AvgTimeThisMonth);
            }

            [Fact]
            public void If_total_count_is_zero_then_zeros_out_average_time_this_year()
            {
                this.presenter.Setup();
                A.CallTo(() => this.entriesHolder.Entries).Returns(
                    new LinkedListMaterializedEnumerable<JournalEntry>());
                this.ui.AvgTimeThisYear = this.fixture.Create<string>();

                this.timer.Elapsed += Raise.With<Action>();

                Assert.Equal("0h 0m 0s", this.ui.AvgTimeThisYear);
            }
        }
    }
}
