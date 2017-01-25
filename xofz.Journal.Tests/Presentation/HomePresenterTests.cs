namespace xofz.Journal.Tests.Presentation
{
    using System;
    using System.Linq;
    using FakeItEasy;
    using xofz.Framework;
    using xofz.Journal.Framework;
    using xofz.Journal.Presentation;
    using xofz.Journal.UI;
    using xofz.Presentation;
    using Xunit;

    public class HomePresenterTests
    {
        public class Context
        {
            protected Context()
            {
                this.ui = A.Fake<HomeUi>();
                this.web = new MethodWeb();
                this.navigator = A.Fake<Navigator>();
                this.web.RegisterDependency(this.navigator);

                this.loader = A.Fake<JournalEntryLoader>();
                this.saver = A.Fake<JournalEntrySaver>();
                this.web.RegisterDependency(this.loader);
                this.web.RegisterDependency(this.saver);
                this.presenter = new HomePresenter(this.ui, this.web);
            }

            protected readonly HomeUi ui;
            protected readonly MethodWeb web;
            protected readonly Navigator navigator;
            protected readonly JournalEntryLoader loader;
            protected readonly JournalEntrySaver saver;
            protected readonly HomePresenter presenter;
        }

        public class When_Setup_is_called : Context
        {
            [Fact]
            public void Registers_itself_with_the_navigator()
            {
                this.presenter.Setup();

                A.CallTo(() => this.navigator.RegisterPresenter(this.presenter))
                    .MustHaveHappened();
            }
        }

        public class When_Start_is_called : Context
        {
            [Fact]
            public void Calls_loader_Load()
            {
                this.presenter.Start();

                A.CallTo(() => this.loader.Load()).MustHaveHappened();
            }

            [Fact]
            public void Orders_entries_by_modified_timestamp_descending()
            {
                var entry1 = new JournalEntry { ModifiedTimestamp = DateTime.MinValue };
                var entry2 = new JournalEntry { ModifiedTimestamp = DateTime.Now };
                A.CallTo(() => this.loader.Load())
                    .Returns(
                        new[]
                        {
                            entry1,
                            entry2
                        });
                this.presenter.Start();
                Assert.Equal(entry2, this.ui.Entries.First());
            }

            [Fact]
            public void Raises_the_EntrySelected_event_on_the_UI()
            {
                var er = A.Fake<EventRaiser>();
                this.web.RegisterDependency(er);

                this.presenter.Start();

                A.CallTo(() => er.Raise(this.ui, "EntrySelected", 0)).MustHaveHappened();
            }
        }

        public class When_the_new_key_is_tapped : Context
        {
        }
    }
}
