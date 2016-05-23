using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSFW.TimingEditor
{
    public interface ITable
    {
        bool IsReadOnly { get; set; }
        bool IsPopulated { get; }
        IList<double> RowHeaders { get; }
        IList<double> ColumnHeaders { get; }
        ITable Clone();
        void CopyTo(ITable destination);
        double GetCell(int x, int y);
        void SetCell(int x, int y, double value);
        void Reset();
        void Populated();
    }
}
