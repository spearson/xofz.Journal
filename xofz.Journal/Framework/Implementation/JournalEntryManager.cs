namespace xofz.Journal.Framework.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using xofz.Framework.Materialization;

    public sealed class JournalEntryManager 
        : JournalEntryLoader, 
        JournalEntrySaver
    {
        public IEnumerable<JournalEntry> Load()
        {
            if (!Directory.Exists("Data"))
            {
                yield break;
            }

            var monthDirectories = Directory.GetDirectories("Data");
            foreach (var monthDirectory in monthDirectories)
            {
                foreach (var filePath in Directory.GetFiles(monthDirectory))
                {
                    var lines = File.ReadAllLines(filePath);
                    if (lines.Length == 0)
                    {
                        continue;
                    }

                    yield return new JournalEntry
                    {
                        CreatedTimestamp = new DateTime(
                            long.Parse(
                                Path.GetFileName(filePath))),
                        ModifiedTimestamp = new DateTime(long.Parse(lines[0])),
                        Content = new LinkedListMaterializedEnumerable<string>(
                            lines.Skip(1))
                    };
                }
            }
        }

        public void Save(JournalEntry entry)
        {
            if (!Directory.Exists("Data"))
            {
                Directory.CreateDirectory("Data");
            }

            var saveDirectory = @"Data\"
                                + DateTime.Now.Year
                                + DateTime.Now.Month.ToString().PadLeft(2, '0');
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            DateTime createdTimestamp = DateTime.Now;
            if (entry.CreatedTimestamp != null)
            {
                createdTimestamp = (DateTime)entry.CreatedTimestamp;
            }

            var lines = new LinkedList<string>(entry.Content);
            lines.AddFirst(entry.ModifiedTimestamp?.Ticks.ToString());
            File.WriteAllLines(
                Path.Combine(saveDirectory, createdTimestamp.Ticks.ToString()),
                lines);
        }
    }
}
