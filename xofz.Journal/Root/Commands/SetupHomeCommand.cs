namespace xofz.Journal.Root.Commands
{
    using xofz.Framework;
    using xofz.Journal.Framework;
    using xofz.Journal.Framework.Implementation;
    using xofz.Journal.Presentation;
    using xofz.Journal.UI;
    using xofz.Root;
    public class SetupHomeCommand : Command
    {
        public SetupHomeCommand(
            HomeUi ui,
            MethodWeb web)
        {
            this.ui = ui;
            this.web = web;
        }

        public override void Execute()
        {
            this.registerDependencies();

            new HomePresenter(
                    this.ui,
                    this.web)
                .Setup();
        }

        private void registerDependencies()
        {
            var w = this.web;
            w.RegisterDependency(
                new JournalEntryManager());
            w.RegisterDependency(
                new JournalEntriesHolder());
            w.RegisterDependency(
                new xofz.Framework.Timer(),
                "HomeTimer");
        }

        private readonly HomeUi ui;
        private readonly MethodWeb web;
    }
}
