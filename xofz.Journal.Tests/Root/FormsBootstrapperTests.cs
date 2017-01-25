namespace xofz.Journal.Tests.Root
{
    using FakeItEasy;
    using xofz.Framework;
    using xofz.Journal.Presentation;
    using xofz.Journal.Root;
    using xofz.Journal.Root.Commands;
    using xofz.Presentation;
    using xofz.Root;
    using xofz.Root.Commands;
    using Xunit;

    public class FormsBootstrapperTests
    {
        public class Context
        {
            protected Context()
            {
                this.executor = A.Fake<CommandExecutor>();
                this.bootstrapper = new FormsBootstrapper(this.executor);
                this.web = new MethodWeb();
                var fakeCommand = A.Fake<SetupMethodWebCommand>();
                A.CallTo(() => fakeCommand.Web).Returns(this.web);
                A.CallTo(() => this.executor.Get<SetupMethodWebCommand>())
                    .Returns(fakeCommand);
                A.CallTo(() => this.executor.Execute(A<Command>.Ignored))
                    .Returns(this.executor);
            }

            protected readonly CommandExecutor executor;
            protected readonly FormsBootstrapper bootstrapper;
            protected readonly MethodWeb web;
        }

        public class When_Bootstrap_is_called : Context
        {
            [Fact]
            public void Executes_a_SetupMethodWebCommand()
            {
                this.bootstrapper.Bootstrap();

                A.CallTo(() => this.executor.Execute(A<SetupMethodWebCommand>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Executes_a_SetupMainCommand()
            {
                this.bootstrapper.Bootstrap();

                A.CallTo(() => this.executor.Execute(A<SetupMainCommand>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Executes_a_SetupShutdownCommand()
            {
                this.bootstrapper.Bootstrap();

                A.CallTo(() => this.executor.Execute(A<SetupShutdownCommand>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Executes_a_SetupHomeCommand()
            {
                this.bootstrapper.Bootstrap();

                A.CallTo(() => this.executor.Execute(A<SetupHomeCommand>.Ignored))
                    .MustHaveHappened();
            }

            [Fact]
            public void Presents_a_HomePresenter()
            {
                var w = this.web;
                var n = A.Fake<Navigator>();
                w.RegisterDependency(n);
            
                this.bootstrapper.Bootstrap();

                A.CallTo(() => n.Present<HomePresenter>()).MustHaveHappened();
            }
        }
    }
}
