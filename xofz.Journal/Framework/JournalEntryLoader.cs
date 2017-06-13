namespace xofz.Journal.Framework
{
    using System.Collections.Generic;

    public interface JournalEntryLoader
    {
        IEnumerable<JournalEntry> Load();
    }
}
