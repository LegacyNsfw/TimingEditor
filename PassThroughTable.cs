using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSFW.TimingEditor
{
    public class PassThroughTable : ITable
    {
        private ITable baseTable;
        private ITable advanceTable;
        private bool populated;

        public bool IsReadOnly { get { return false; } set { throw new InvalidOperationException(); } }
        public IList<double> RowHeaders { get { return this.advanceTable.RowHeaders; } }
        public IList<double> ColumnHeaders { get { return this.advanceTable.ColumnHeaders; } }

        public PassThroughTable(ITable baseTable)
        {
            this.baseTable = baseTable;
            this.advanceTable = new Table();
        }

        public ITable Clone()
        {
            PassThroughTable result = new PassThroughTable(this.baseTable);
            result.advanceTable = this.advanceTable.Clone();
            result.populated = this.populated;
            return result;
        }

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

        public bool IsPopulated
        {
            get
            {
                return this.baseTable.IsPopulated && this.advanceTable.IsPopulated;
            }
        }

        public void Reset()
        {
            this.populated = false;
            this.advanceTable.Reset();
        }

        public void Populated()
        {
            this.populated = true;
            this.advanceTable.Populated();
        }

        public double GetCell(int x, int y)
        {
            return this.advanceTable.GetCell(x, y);
        }

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
