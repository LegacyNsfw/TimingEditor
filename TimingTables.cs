using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSFW.TimingEditor
{
    /// <summary>
    /// Represents a single entry in the list of tables.
    /// </summary>
    public class TableListEntry
    {
        private string description;
        private ITable table;
        private bool allowPaste;
        private bool hasData;
        private string statusText;

        public string Description { get { return this.description; } }
        public ITable Table { get { return this.table; } }
        public bool AllowPaste { get { return this.allowPaste; } }
        public bool HasData { get { return this.hasData; } }
        public string StatusText { get { return this.statusText; } }

        public TableListEntry(string description, ITable table, bool allowPaste, string statusText)
        {
            this.description = description;
            this.table = table;
            this.allowPaste = allowPaste;
            this.hasData = false;
            this.statusText = statusText;
        }

        public override string ToString()
        {
            return this.description;
        }
    }

    /// <summary>
    /// Represents the set of tables (including virtual / synthetic tables) that the application can render.
    /// </summary>
    public class TimingTables
    {
        private ITable initialBaseTiming;
        private ITable initialAdvanceTiming;
        private ITable initialTotalTiming;
        private ITable modifiedBaseTiming;
        private ITable modifiedAdvanceTiming;
        private ITable modifiedTotalTiming;
        private ITable deltaTotalTiming;

        public ITable InitialBaseTiming { get { return this.initialBaseTiming; } }
        public ITable InitialAdvanceTiming { get { return this.initialAdvanceTiming; } }
        public ITable InitialTotalTiming { get { return this.initialTotalTiming; } }
        public ITable ModifiedBaseTiming { get { return this.modifiedBaseTiming; } }
        public ITable ModifiedAdvanceTiming { get { return this.modifiedAdvanceTiming; } }
        public ITable ModifiedTotalTiming { get { return this.modifiedTotalTiming; } }
        public ITable DeltaTotalTiming { get { return this.deltaTotalTiming; } }

        public TimingTables()
        {
            this.initialBaseTiming = new Table();
            this.initialAdvanceTiming = new Table();
            this.initialTotalTiming = new CombinedTable(this.initialBaseTiming, this.initialAdvanceTiming, Operation.Sum);
            this.modifiedBaseTiming = new Table();
            this.modifiedAdvanceTiming = new PassThroughTable(this.modifiedBaseTiming);
            this.modifiedTotalTiming = new CombinedTable(this.modifiedBaseTiming, this.modifiedAdvanceTiming, Operation.Sum);
            this.deltaTotalTiming = new CombinedTable(this.initialTotalTiming, this.modifiedTotalTiming, Operation.Difference);
        }
    }
}
