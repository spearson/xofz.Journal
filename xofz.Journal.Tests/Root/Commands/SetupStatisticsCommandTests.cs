namespace xofz.Journal.Tests.Root.Commands
{
    using FakeItEasy;
    using xofz.Framework;
    using xofz.Journal.Root.Commands;
    using xofz.Journal.UI;
    using Xunit;

    public class SetupStatisticsCommandTests
    {
        public class Context
        {
            protected Context()
            {
                this.web = A.Fake<MethodWeb>();
                this.command = new SetupStatisticsCommand(
                    A.Fake<StatisticsUi>(),
                    this.web);

            }

            protected readonly MethodWeb web;
            protected readonly SetupStatisticsCommand command;
        }

        public class When_Execute_is_called : Context
        {
            [Fact]
            public void Registers_a_statistics_timer()
            {
                this.command.Execute();

                A.CallTo(() => this.web.RegisterDependency(
                    A<xofz.Framework.Timer>.Ignored,
                    "StatisticsTimer"))
                    .MustHaveHappened();
            }
        }
    }
}
