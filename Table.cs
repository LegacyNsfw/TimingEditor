using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSFW.TimingEditor
{
    public class Table : ITable
    {
        private bool isPopulated;
        private bool isReadOnly;
        private double[][] cells;
        private IList<double> rowHeaders;
        private IList<double> columnHeaders;
        public bool IsReadOnly { get { return this.isReadOnly; } set { this.isReadOnly = value; } }
        public bool IsPopulated { get { return this.isPopulated; } }
        public IList<double> RowHeaders { get { return this.rowHeaders; } }
        public IList<double> ColumnHeaders { get { return this.columnHeaders; } }

        public Table()
        {
            this.isPopulated = false;
            //this.isReadOnly = isReadOnly;
        }

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

        public void Populated()
        {
            this.isPopulated = true;
        }

        public double GetCell(int x, int y) { return this.cells[x][y]; }

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
