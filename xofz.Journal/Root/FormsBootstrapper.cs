namespace xofz.Journal.Root
{
    using System.Threading;
    using System.Windows.Forms;
    using xofz.Framework.Materialization;
    using xofz.Journal.Presentation;
    using xofz.Journal.Root.Commands;
    using xofz.Journal.UI.Forms;
    using xofz.Presentation;
    using xofz.Root;
    using xofz.Root.Commands;
    using xofz.UI.Forms;

    public class FormsBootstrapper
    {
        public FormsBootstrapper()
            : this(new CommandExecutor())
        {
        }

        public FormsBootstrapper(
            CommandExecutor executor)
        {
            this.executor = executor;
        }

        public virtual Form Shell => this.mainForm;

        public virtual void Bootstrap()
        {
            if (Interlocked.CompareExchange(ref this.bootstrappedIf1, 1, 0) == 1)
            {
                return;
            }

            this.setMainForm(
                new FormMainUi(
                    s => new LinkedListMaterializedEnumerable<string>(s)));
            var mf = this.mainForm;
            var fm = new FormsMessenger { Subscriber = mf };

            var e = this.executor;
            e.Execute(new SetupMethodWebCommand(fm));
            var w = e.Get<SetupMethodWebCommand>().Web;
            e
                .Execute(new SetupMainCommand(
                    mf,
                    w))
                .Execute(new SetupShutdownCommand(
                    mf,
                    w))
                .Execute(new SetupHomeCommand(
                    mf,
                    w));

            w.Run<Navigator>(n => n.Present<HomePresenter>());
        }


        private void setMainForm(FormMainUi mainForm)
        {
            this.mainForm = mainForm;
        }

        private int bootstrappedIf1;
        private FormMainUi mainForm;
        private readonly CommandExecutor executor;
    }
}
