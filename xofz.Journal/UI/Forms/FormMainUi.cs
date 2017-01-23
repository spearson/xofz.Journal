namespace xofz.Journal.UI.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;
    using xofz.UI;
    using xofz.UI.Forms;

    public partial class FormMainUi : FormUi, MainUi, HomeUi
    {
        public FormMainUi(
            Func<IEnumerable<string>, MaterializedEnumerable<string>> materializeStrings)
        {
            this.materializeStrings = materializeStrings;
            this.InitializeComponent();
        }

        public event Action ShutdownRequested;

        public event Action NewKeyTapped;

        public event Action SubmitKeyTapped;

        public event Action<int> EntrySelected;

        private void this_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            new Thread(() => this.ShutdownRequested?.Invoke()).Start();
        }

        public JournalEntry CurrentEntry
        {
            get
            {
                return new JournalEntry
                {
                    Content = this.materializeStrings(this.contentTextBox.Lines)
                };
            }

            set
            {
                this.createdTextBox.Text = value.CreatedTimestamp?
                    .ToString("yyyy/MM/dd hh:mm:ss tt");
                this.modifiedTextBox.Text = value.ModifiedTimestamp?
                    .ToString("yyyy/MM/dd hh:mm:ss tt");
                this.contentTextBox.Lines = value.Content?.ToArray();
            }
        }

        bool HomeUi.ContentEditable
        {
            get { return this.contentTextBox.ReadOnly; }

            set { this.contentTextBox.ReadOnly = !value; }
        }

        private void newKey_Click(object sender, EventArgs e)
        {
            new Thread(() => this.NewKeyTapped?.Invoke()).Start();
        }

        private readonly Func<IEnumerable<string>, MaterializedEnumerable<string>> materializeStrings;

        private void submitKey_Click(object sender, EventArgs e)
        {
            new Thread(() => this.SubmitKeyTapped?.Invoke()).Start();
        }

        private void searchResultsViewer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            new Thread(() => this.EntrySelected?.Invoke(e.RowIndex)).Start();
        }

        public IList<JournalEntry> Entries
        {
            get { return new List<JournalEntry>(); }

            set
            {
                this.entriesGrid.Rows.Clear();
                foreach (var entry in value)
                {
                    var summary = entry.Content.FirstOrDefault();
                    summary = summary?.Substring(
                        0,
                        summary.Length > 50
                            ? 50
                            : summary.Length);

                    this.entriesGrid.Rows.Add(
                        entry.CreatedTimestamp?.ToString("yyyy/MM/dd hh:mm:ss tt"),
                        entry.ModifiedTimestamp?.ToString("yyyy/MM/dd hh:mm:ss tt"),
                        summary);
                }
            }
        }
    }
}
