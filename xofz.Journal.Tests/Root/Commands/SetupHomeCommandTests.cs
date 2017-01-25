namespace xofz.Journal.Tests.Root.Commands
{
    using FakeItEasy;
    using xofz.Framework;
    using xofz.Journal.Framework;
    using xofz.Journal.Root.Commands;
    using xofz.Journal.UI;
    using Xunit;

    public class SetupHomeCommandTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = A.Fake<MethodWeb>();
                this.command = new SetupHomeCommand(
                    A.Fake<HomeUi>(), 
                    this.web);
            }
            protected readonly MethodWeb web;
            protected readonly SetupHomeCommand command;
        }

        public class When_Execute_is_called : Context
        {
            [Fact]
            public void Registers_a_JournalEntryManager()
            {
                this.command.Execute();

                A.CallTo(() => this.web.RegisterDependency(
                    A<JournalEntryManager>.Ignored,
                    null))
                    .MustHaveHappened();
            }
        }
    }
}
