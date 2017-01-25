namespace xofz.Journal.Tests.Root.Commands
{
    using FakeItEasy;
    using Ploeh.AutoFixture;
    using xofz.Journal.Root.Commands;
    using xofz.Presentation;
    using xofz.UI;
    using Xunit;

    public class SetupMethodWebCommandTests
    {
        public class Context
        {
            protected Context()
            {
                this.messenger = A.Fake<Messenger>();
                this.command = new SetupMethodWebCommand(this.messenger);
                this.fixture = new Fixture();
            }

            protected readonly Messenger messenger;
            protected readonly SetupMethodWebCommand command;
            protected readonly Fixture fixture;
        }

        public class When_Execute_is_called : Context
        {
            [Fact]
            public void Registers_a_navigator()
            {
                var c = this.command;
                var registered = false;
                c.Execute();

                c.Web.Run<Navigator>(n => registered = true);

                Assert.True(registered);
            }

            [Fact]
            public void Registers_a_messenger()
            {
                var c = this.command;
                var registered = false;
                c.Execute();

                c.Web.Run<Messenger>(m => registered = true);

                Assert.True(registered);
            }

            [Fact]
            public void Registers_the_messenger_passed_in()
            {
                var c = this.command;
                c.Execute();
                var s = this.fixture.Create<string>();

                c.Web.Run<Messenger>(m => m.Inform(s));

                A.CallTo(() => this.messenger.Inform(s)).MustHaveHappened();
            }
        }
    }
}
