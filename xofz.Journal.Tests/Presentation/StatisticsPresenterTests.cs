namespace xofz.Journal.Tests.Presentation
{
    using System;
    using FakeItEasy;
    using xofz.Framework;
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
            }

            protected readonly StatisticsUi ui;
            protected readonly MethodWeb web;
            protected readonly StatisticsPresenter presenter;
            protected readonly xofz.Framework.Timer timer;
            protected readonly JournalEntriesHolder entriesHolder;
            protected readonly Navigator navigator;
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
            // todo: implement!
        }
    }
}
