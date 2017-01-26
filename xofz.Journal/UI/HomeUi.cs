namespace xofz.Journal.UI
{
    using System;
    using System.Collections.Generic;
    using xofz.UI;

    public interface HomeUi : Ui
    {
        event Action NewKeyTapped;

        event Action SubmitKeyTapped;

        event Action<int> EntrySelected;

        IList<JournalEntry> Entries { get; set; }

        JournalEntry CurrentEntry { get; set; }

        string TotalTime { get; set; }

        bool ContentEditable { get; set; }
    }
}
