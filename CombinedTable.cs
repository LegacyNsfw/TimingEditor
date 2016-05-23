using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSFW.TimingEditor
{
    public enum Operation
    {
        Undefined = 0,
        Sum = 1,
        Difference = 2,
    }

    public class CombinedTable : ITable
    {
        private ITable a;
        private ITable b;
        private Operation operation;

        public bool IsReadOnly { get { return this.a.IsReadOnly || this.b.IsReadOnly || this.operation != Operation.Sum; } set { throw new InvalidOperationException(); } }
        public IList<double> RowHeaders { get { return this.a.RowHeaders; } }
        public IList<double> ColumnHeaders { get { return this.a.ColumnHeaders; } }

        public CombinedTable(ITable a, ITable b, Operation operation)
        {
            this.a = a;
            this.b = b;
            this.operation = operation;
        }

        public ITable Clone()
        {
            throw new InvalidOperationException();
            /*CombinedTable result = new CombinedTable();
            result.a = this.a.Clone();
            result.b = this.b.Clone();
            result.operation = this.operation;
            return result;*/
        }

        public void CopyTo(ITable other)
        {
            throw new InvalidOperationException();
        }

        public bool IsPopulated
        {
            get
            {
                return this.a.IsPopulated && this.b.IsPopulated;
            }
        }

        public void Reset()
        {
        }

        public void Populated()
        {
            throw new InvalidOperationException();
        }

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
