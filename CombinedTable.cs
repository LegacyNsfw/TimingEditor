using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSFW.TimingEditor
{
    /// <summary>
    /// Which operation to carry out between two tables.
    /// </summary>
    public enum Operation
    {
        Undefined = 0,
        Sum = 1,
        Difference = 2,
    }

    /// <summary>
    /// Combines two tables and presents them as one.
    /// </summary>
    public class CombinedTable : ITable
    {
        /// <summary>
        /// One of the tables to combine (see below).
        /// </summary>
        private ITable a;

        /// <summary>
        /// The other table to combine (see above).
        /// </summary>
        private ITable b;

        /// <summary>
        /// The operation to use to combine the two tables.
        /// </summary>
        private Operation operation;

        /// <summary>
        /// Indicates whether the combined table is read-only.
        /// </summary>
        public bool IsReadOnly { get { return this.a.IsReadOnly || this.b.IsReadOnly || this.operation != Operation.Sum; } set { throw new InvalidOperationException(); } }

        /// <summary>
        /// Row headers for the combined table (always just the headers from table A).
        /// </summary>
        public IList<double> RowHeaders { get { return this.a.RowHeaders; } }

        /// <summary>
        /// Column headers for the combined table (always just the headers from table A).
        /// </summary>
        public IList<double> ColumnHeaders { get { return this.a.ColumnHeaders; } }

        /// <summary>
        /// Constructor.
        /// </summary>
        public CombinedTable(ITable a, ITable b, Operation operation)
        {
            this.a = a;
            this.b = b;
            this.operation = operation;
        }

        /// <summary>
        /// Create a new table based on this one. Not implemented though. 
        /// </summary>
        /// <returns></returns>
        public ITable Clone()
        {
            throw new InvalidOperationException();
            /*CombinedTable result = new CombinedTable();
            result.a = this.a.Clone();
            result.b = this.b.Clone();
            result.operation = this.operation;
            return result;*/
        }

        /// <summary>
        /// Copy this table's data into another table. Not implemented.
        /// </summary>
        public void CopyTo(ITable other)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Indicates whether this table (actually, its two consituent tables) is fully populated.
        /// </summary>
        public bool IsPopulated
        {
            get
            {
                return this.a.IsPopulated && this.b.IsPopulated;
            }
        }

        /// <summary>
        /// Reset the table. It's a no-op for this class.
        /// </summary>
        public void Reset()
        {
        }

        /// <summary>
        /// Invoke when populated. Throws because that doesn't make sense for this table type.
        /// </summary>
        public void Populated()
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Get the value of a cell in this table. The value is computed from the underlying tables and the operation type.
        /// </summary>
        public double GetCell(int x, int y)
        {
            if (this.operation == Operation.Sum)
            {
                return this.a.GetCell(x, y) + this.b.GetCell(x, y);
            }
            else if (this.operation == Operation.Difference)
            {
                return this.b.GetCell(x, y) - this.a.GetCell(x, y);
            }
            else
            {
                throw new InvalidOperationException("Undefined CombinedTable Operation: " + this.operation.ToString());
            }
        }

        /// <summary>
        /// Set the value of a cell in this table. Actually we edit only table 'a'.
        /// </summary>
        public void SetCell(int x, int y, double value)
        {
            double oldTotalValue = this.GetCell(x, y);
            double delta = value - oldTotalValue;

            if (this.operation == Operation.Sum)
            {
                double oldValue = this.a.GetCell(x, y);
                double newValue = oldValue + delta;
                this.a.SetCell(x, y, newValue);
                return;
            }

            throw new InvalidOperationException("Cannot set the value of a difference table.");
        }
    }
}
