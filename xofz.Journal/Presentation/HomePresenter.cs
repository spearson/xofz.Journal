namespace xofz.Journal.Presentation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using xofz.Framework;
    using xofz.Framework.Materialization;
    using xofz.Journal.Framework;
    using xofz.Journal.UI;
    using xofz.Presentation;
    using xofz.UI;

    public sealed class HomePresenter : Presenter
    {
        public HomePresenter(
            HomeUi ui,
            MethodWeb web) 
            : base(ui, null)
        {
            this.ui = ui;
            this.web = web;
        }

        public void Setup()
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            this.ui.NewKeyTapped += this.ui_NewKeyTapped;
            this.ui.SubmitKeyTapped += this.ui_SubmitKeyTapped;
            this.ui.EntrySelected += this.ui_EntrySelected;
            var entries = this.web.Run<
                JournalEntryLoader,
                IEnumerable<JournalEntry>>(
                loader => loader.Load());
            this.setAllEntries(entries.ToList());
        }

        public override void Start()
        {
        }

        private void setAllEntries(IList<JournalEntry> allEntries)
        {
            this.allEntries = allEntries;
            UiHelpers.Write(this.ui, () => this.ui.Entries = allEntries);
        }

        private void ui_NewKeyTapped()
        {
            var w = this.web;
            var current = UiHelpers.Read(this.ui, () => this.ui.CurrentEntry);
            if (current.Content.Count > 0)
            {
                var discardChanges = w.Run<Messenger, Response>(m => UiHelpers.Read(
                    m.Subscriber,
                    () => m.Question("Discard current changes?")));
                if (discardChanges == Response.No)
                {
                    return;
                }
            }

            this.setCurrentEntry(
                new JournalEntry { CreatedTimestamp = DateTime.Now });
            UiHelpers.Write(this.ui, () =>
            {
                this.ui.CurrentEntry = this.currentEntry;
                this.ui.ContentEditable = true;
            });
        }

        private void ui_SubmitKeyTapped()
        {
            var ce = this.currentEntry;
            ce.ModifiedTimestamp = DateTime.Now;
            ce.Content = UiHelpers.Read(this.ui, () => this.ui.CurrentEntry.Content);

            var w = this.web;
            w.Run<JournalEntrySaver>(saver => saver.Save(ce));

        }

        private void ui_EntrySelected(int entryIndex)
        {
            this.setCurrentEntry(this.allEntries[entryIndex]);
        }

        private void setCurrentEntry(JournalEntry currentEntry)
        {
            if (currentEntry.CreatedTimestamp == null)
            {
                return;
            }

            this.currentEntry = currentEntry;
            var editable = currentEntry.CreatedTimestamp.Value.Date == DateTime.Today;
            UiHelpers.Write(this.ui, () =>
            {
                this.ui.CurrentEntry = currentEntry;
                this.ui.ContentEditable = editable;
            });
        }

        private int setupIf1;
        private IList<JournalEntry> allEntries;
        private JournalEntry currentEntry;
        private readonly HomeUi ui;
        private readonly MethodWeb web;
    }
}
