namespace xofz.Journal.Framework
{
    using System.Collections;
    using System.Collections.Generic;

    public interface JournalEntryLoader
    {
        IEnumerable<JournalEntry> Load();
    }
}
