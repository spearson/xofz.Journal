namespace xofz.Journal.Tests.Presentation
{
    using System;
    using System.Linq;
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Framework;
    using xofz.Framework.Materialization;
    using xofz.Journal.Framework;
    using xofz.Journal.Presentation;
    using xofz.Journal.UI;
    using xofz.Presentation;
    using xofz.UI;
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
                this.loader = A.Fake<JournalEntryLoader>();
                this.saver = A.Fake<JournalEntrySaver>();
                this.eventRaiser = A.Fake<EventRaiser>();
                this.messenger = A.Fake<Messenger>();

                this.web.RegisterDependency(this.navigator);
                this.web.RegisterDependency(this.loader);
                this.web.RegisterDependency(this.saver);
                this.web.RegisterDependency(this.eventRaiser);
                this.web.RegisterDependency(this.messenger);

                this.presenter = new HomePresenter(this.ui, this.web);
                this.fixture = new Fixture();
            }

            protected readonly HomeUi ui;
            protected readonly MethodWeb web;
            protected readonly Navigator navigator;
            protected readonly JournalEntryLoader loader;
            protected readonly JournalEntrySaver saver;
            protected readonly EventRaiser eventRaiser;
            protected readonly Messenger messenger;
            protected readonly HomePresenter presenter;
            protected readonly Fixture fixture;
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
            public void Raises_the_EntrySelected_event_on_the_first_entry()
            {
                this.presenter.Start();

                A.CallTo(() => this.eventRaiser.Raise(
                    this.ui,
                    "EntrySelected",
                    0)).MustHaveHappened();
            }
        }

        public class When_the_new_key_is_tapped : Context
        {
            [Fact]
            public void If_content_is_editable_and_there_is_some_content_prompts_to_discard_changes()
            {
                this.presenter.Setup();
                this.ui.ContentEditable = true;
                this.ui.CurrentEntry = new JournalEntry
                {
                    Content= new LinkedListMaterializedEnumerable<string>(
                        new []
                        {
                            this.fixture.Create<string>()
                        })
                };

                this.ui.NewKeyTapped += Raise.With<Action>();

                A.CallTo(() => this.messenger.Question(
                    A<string>.That.Matches(s => s.ToLower().Contains("discard"))))
                    .MustHaveHappened();
            }

            [Fact]
            public void Makes_the_content_editable()
            {
                this.presenter.Setup();
                this.ui.ContentEditable = false;

                this.ui.NewKeyTapped += Raise.With<Action>();

                Assert.True(this.ui.ContentEditable);
            }
        }

        public class When_the_submit_key_is_tapped : Context
        {
            [Fact]
            public void Changes_the_modified_timestamp()
            {
                this.presenter.Setup();
                this.ui.NewKeyTapped += Raise.With<Action>();

                var oldModified = DateTime.Now.AddSeconds(-1);
                this.ui.CurrentEntry.ModifiedTimestamp = oldModified;

                this.ui.SubmitKeyTapped += Raise.With<Action>();

                Assert.True(this.ui.CurrentEntry.ModifiedTimestamp > oldModified);
            }

            [Fact]
            public void Calls_saver_Save()
            {
                this.presenter.Setup();
                this.ui.NewKeyTapped += Raise.With<Action>();

                this.ui.SubmitKeyTapped += Raise.With<Action>();

                A.CallTo(() => this.saver.Save(A<JournalEntry>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Reloads_the_entries()
            {
                this.presenter.Setup();
                this.ui.NewKeyTapped += Raise.With<Action>();

                this.ui.SubmitKeyTapped += Raise.With<Action>();

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
                this.presenter.Setup();
                this.ui.NewKeyTapped += Raise.With<Action>();

                this.ui.SubmitKeyTapped += Raise.With<Action>();

                Assert.Equal(entry2, this.ui.Entries.First());
            }

            [Fact]
            public void Raises_the_EntrySelected_event_on_the_first_entry()
            {
                this.presenter.Setup();
                this.ui.NewKeyTapped += Raise.With<Action>();

                this.ui.SubmitKeyTapped += Raise.With<Action>();

                A.CallTo(() => this.eventRaiser.Raise(
                    this.ui,
                    "EntrySelected",
                    0)).MustHaveHappened();
            }
        }
    }
}
