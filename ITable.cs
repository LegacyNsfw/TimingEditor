using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSFW.TimingEditor
{
    /// <summary>
    /// Represents a table in the ECU.
    /// </summary>
    public interface ITable
    {
        /// <summary>
        /// Indicates whether the table is read-only in the UI.
        /// </summary>
        bool IsReadOnly { get; set; }

        /// <summary>
        /// Indicates whether the table is fully populated.
        /// </summary>
        bool IsPopulated { get; }

        /// <summary>
        /// Gets the row header values.
        /// </summary>
        IList<double> RowHeaders { get; }

        /// <summary>
        /// Gets the column header values.
        /// </summary>
        IList<double> ColumnHeaders { get; }

        /// <summary>
        /// Clones the table.
        /// </summary>
        ITable Clone();

        /// <summary>
        /// Copies the table contents into another table.
        /// </summary>
        void CopyTo(ITable destination);

        /// <summary>
        /// Gets the value of a cell in the table.
        /// </summary>
        double GetCell(int x, int y);

        /// <summary>
        /// Sets the value of a cell in the table.
        /// </summary>
        void SetCell(int x, int y, double value);

        /// <summary>
        /// Resets the contents of a table.
        /// </summary>
        void Reset();

        /// <summary>
        /// Invoke this when the table is fully populated.
        /// </summary>
        void Populated();
    }
}
