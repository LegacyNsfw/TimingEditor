using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSFW.TimingEditor
{
    /// <summary>
    /// Represents a single table direct from the ECU image.
    /// </summary>
    public class Table : ITable
    {
        /// <summary>
        /// Indicates whether the table is fully populated.
        /// </summary>
        private bool isPopulated;

        /// <summary>
        /// Indicates whether the table is read-only.
        /// </summary>
        private bool isReadOnly;

        /// <summary>
        /// Contains the raw values of the table cells.
        /// </summary>
        private double[][] cells;

        /// <summary>
        /// Row header values.
        /// </summary>
        private IList<double> rowHeaders;

        /// <summary>
        /// Column header values.
        /// </summary>
        private IList<double> columnHeaders;

        /// <summary>
        /// Indicates whether the table is read-only.
        /// </summary>
        public bool IsReadOnly { get { return this.isReadOnly; } set { this.isReadOnly = value; } }

        /// <summary>
        /// Indicates whether the table is fully populated.
        /// </summary>
        public bool IsPopulated { get { return this.isPopulated; } }

        /// <summary>
        /// Gets the row header values.
        /// </summary>
        public IList<double> RowHeaders { get { return this.rowHeaders; } }

        /// <summary>
        /// Gets the column header values.
        /// </summary>
        public IList<double> ColumnHeaders { get { return this.columnHeaders; } }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Table()
        {
            this.isPopulated = false;
            //this.isReadOnly = isReadOnly;
        }

        /// <summary>
        /// Clone the table.
        /// </summary>
        public ITable Clone()
        {
            Table result = new Table();

            result.isPopulated = this.isPopulated;
            result.isReadOnly = this.isReadOnly;

            result.cells = new double[cells.Length][];
            for (int x = 0; x < this.cells.Length; x++)
            {
                result.cells[x] = new double[this.cells[0].Length];
                for (int y = 0; y < this.cells.Length; y++)
                {
                    result.cells[x][y] = this.cells[x][y];
                }
            }

            result.rowHeaders = new List<double>(this.rowHeaders.Count);
            for (int i = 0; i < this.rowHeaders.Count; i++)
            {
                result.rowHeaders[i] = this.rowHeaders[i];
            }

            result.columnHeaders = new List<double>(this.columnHeaders.Count);
            for (int i = 0; i < this.columnHeaders.Count; i++)
            {
                result.columnHeaders[i] = this.columnHeaders[i];
            }

            return result;
        }

        /// <summary>
        /// Copy the table contents into another table.
        /// </summary>
        public void CopyTo(ITable other)
        {
            other.Reset();
            bool wasReadOnly = other.IsReadOnly;
            if (wasReadOnly)
            {
                other.IsReadOnly = false;
            }

            for (int i = 0; i < this.rowHeaders.Count; i++)
            {
                other.RowHeaders.Add(this.rowHeaders[i]);
            }

            for (int i = 0; i < this.columnHeaders.Count; i++)
            {
                other.ColumnHeaders.Add(this.columnHeaders[i]);
            }

            for (int x = 0; x < this.cells.Length; x++)
            {
                for (int y = 0; y < this.cells[0].Length; y++)
                {
                    other.SetCell(x, y, this.cells[x][y]);
                }
            }

            other.Populated();

            if (wasReadOnly)
            {
                other.IsReadOnly = true;
            }
        }

        /// <summary>
        /// Reset the table contents.
        /// </summary>
        public void Reset()
        {
            this.cells = null;
            if (this.rowHeaders == null)
            {
                this.rowHeaders = new List<double>();
            }
            else
            {
                this.rowHeaders.Clear();
            }

            if (this.columnHeaders == null)
            {
                this.columnHeaders = new List<double>();
            }
            else
            {
                this.columnHeaders.Clear();
            }
        }

        /// <summary>
        /// Indicates whether the table is populated.
        /// </summary>
        public void Populated()
        {
            this.isPopulated = true;
        }

        /// <summary>
        /// Get the value of a single cell.
        /// </summary>
        public double GetCell(int x, int y) { return this.cells[x][y]; }

        /// <summary>
        /// Set the value of a single cell.
        /// </summary>
        public void SetCell(int x, int y, double value)
        {
            if (this.isReadOnly)
            {
                throw new InvalidOperationException("This table is read-only");
            }

            if (this.cells == null)
            {
                this.cells = new double[this.columnHeaders.Count][];
                for (int i = 0; i < this.cells.Length; i++)
                {
                    this.cells[i] = new double[this.rowHeaders.Count];
                }
            }
            double[] column = this.cells[x];
            column[y] = value;
        }
    }
}
