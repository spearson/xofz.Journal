namespace xofz.Journal.Root.Commands
{
    using xofz.Framework;
    using xofz.Presentation;
    using xofz.Root;
    using xofz.UI;

    public class SetupMethodWebCommand : Command
    {
        public SetupMethodWebCommand(
            Messenger messenger)
        {
            this.messenger = messenger;
        }

        public virtual MethodWeb Web => this.web;

        public override void Execute()
        {
            this.setWeb(new MethodWeb());
            this.registerDependencies();
        }

        private void setWeb(MethodWeb web)
        {
            this.web = web;
        }

        private void registerDependencies()
        {
            var w = this.web;
            w.RegisterDependency(
                new Navigator());
            w.RegisterDependency(
                this.messenger);
        }

        private MethodWeb web;
        private readonly Messenger messenger;
    }
}
