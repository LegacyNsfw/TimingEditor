using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSFW.TimingEditor
{
    /// <summary>
    /// Implements a table that passes edits through to another table.
    /// </summary>
    public class PassThroughTable : ITable
    {
        /// <summary>
        /// The base timing table.
        /// </summary>
        private ITable baseTable;

        /// <summary>
        /// The dynamic timing advance table.
        /// </summary>
        private ITable advanceTable;

        /// <summary>
        /// Indicates whether the table is fully populated.
        /// </summary>
        private bool populated;

        /// <summary>
        /// Indicates whether the table is read-only.
        /// </summary>
        public bool IsReadOnly { get { return false; } set { throw new InvalidOperationException(); } }

        /// <summary>
        /// Row headers for the table.
        /// </summary>
        public IList<double> RowHeaders { get { return this.advanceTable.RowHeaders; } }

        /// <summary>
        /// Column headers for the table.
        /// </summary>
        public IList<double> ColumnHeaders { get { return this.advanceTable.ColumnHeaders; } }

        /// <summary>
        /// Constructor - takes a reference to the base timing table.
        /// </summary>
        public PassThroughTable(ITable baseTable)
        {
            this.baseTable = baseTable;
            this.advanceTable = new Table();
        }

        /// <summary>
        /// Clone this table.
        /// </summary>
        public ITable Clone()
        {
            PassThroughTable result = new PassThroughTable(this.baseTable);
            result.advanceTable = this.advanceTable.Clone();
            result.populated = this.populated;
            return result;
        }

        /// <summary>
        /// Copy the contents of this table into another table.
        /// </summary>
        public void CopyTo(ITable other)
        {
            other.Reset();

            for (int i = 0; i < this.baseTable.RowHeaders.Count; i++)
            {
                other.RowHeaders.Add(this.baseTable.RowHeaders[i]);
            }

            for (int i = 0; i < this.baseTable.ColumnHeaders.Count; i++)
            {
                other.ColumnHeaders.Add(this.baseTable.ColumnHeaders[i]);
            }

            for (int x = 0; x < this.baseTable.ColumnHeaders.Count; x++)
            {
                for (int y = 0; y < this.baseTable.RowHeaders.Count; y++)
                {
                    other.SetCell(x, y, this.GetCell(x, y));
                }
            }
            other.Populated();
        }

        /// <summary>
        /// Indicates whether this table is fully populated.
        /// </summary>
        public bool IsPopulated
        {
            get
            {
                return this.baseTable.IsPopulated && this.advanceTable.IsPopulated;
            }
        }

        /// <summary>
        /// Reset this table.
        /// </summary>
        public void Reset()
        {
            this.populated = false;
            this.advanceTable.Reset();
        }

        /// <summary>
        /// Invoke when fully populated to mark this table and the advance table as populated.
        /// </summary>
        public void Populated()
        {
            this.populated = true;
            this.advanceTable.Populated();
        }

        /// <summary>
        /// Get the contents of a cell.
        /// </summary>
        public double GetCell(int x, int y)
        {
            return this.advanceTable.GetCell(x, y);
        }

        /// <summary>
        /// Set the contents of a cell.
        /// </summary>
        public void SetCell(int x, int y, double value)
        {
            if (this.populated)
            {
                double oldTotalValue = this.advanceTable.GetCell(x, y);
                double delta = value - oldTotalValue;

                double oldBaseValue = this.baseTable.GetCell(x, y);
                this.baseTable.SetCell(x, y, oldBaseValue - delta);
                this.advanceTable.SetCell(x, y, oldTotalValue + delta);
            }
            else
            {
                this.advanceTable.SetCell(x, y, value);
            }
        }
    }
}
