namespace xofz.Journal.Root.Commands
{
    using xofz.Framework;
    using xofz.Journal.Presentation;
    using xofz.Journal.UI;
    using xofz.Root;

    public class SetupStatisticsCommand : Command
    {
        public SetupStatisticsCommand(
            StatisticsUi ui,
            MethodWeb web)
        {
            this.ui = ui;
            this.web = web;
        }

        public override void Execute()
        {
            this.registerDependencies();
            new StatisticsPresenter(
                    this.ui,
                    this.web)
                .Setup();
        }

        private void registerDependencies()
        {
            var w = this.web;
            w.RegisterDependency(
                new xofz.Framework.Timer(),
                "StatisticsTimer");
        }

        private readonly StatisticsUi ui;
        private readonly MethodWeb web;
    }
}
