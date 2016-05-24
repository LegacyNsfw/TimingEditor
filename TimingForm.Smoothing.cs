using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSFW.TimingEditor
{
    public partial class TimingForm : Form
    {
        private void smoothButton_Click(object sender, EventArgs e)
        {
            disposeCellPopup();
            this.Smooth(this.dataGrid.SelectedCells, true);
        }

        private bool Smooth(DataGridViewSelectedCellCollection selectedCells, bool forReal)
        {
            bool ret = false;
            if (this.smoothComboBox.SelectedIndex == 0 || this.smoothComboBox.SelectedIndex == 1)
            {
                if (this.SelectedRow(selectedCells))
                {
                    if (forReal)
                    {
                        IList<DataGridViewCell> cells = this.SortCellsByRow(selectedCells);
                        this.SmoothHorizontal(cells);
                    }
                    ret = true;
                }
            }
            if (this.smoothComboBox.SelectedIndex == 0 || this.smoothComboBox.SelectedIndex == 2)
            {
                if (this.SelectedColumn(selectedCells))
                {
                    if (forReal)
                    {
                        IList<DataGridViewCell> cells = this.SortCellsByColumn(selectedCells);
                        this.SmoothVertical(cells);
                    }
                    ret = true;
                }
            }
            return ret;
        }

        private bool SelectedColumn(System.Collections.ICollection selectedCells)
        {
            int column = -1;
            int total = 0;
            foreach (DataGridViewCell cell in selectedCells)
            {
                total++;
                if (column == -1)
                    column = cell.ColumnIndex;
                else
                {
                    if (column != cell.ColumnIndex)
                        total--;
                }
            }

            if ((column == -1) || (total <= 2))
                return false;

            return true;
        }

        private bool SelectedRow(System.Collections.ICollection selectedCells)
        {
            int row = -1;
            int total = 0;
            foreach (DataGridViewCell cell in selectedCells)
            {
                total++;
                if (row == -1)
                    row = cell.RowIndex;
                else
                {
                    if (row != cell.RowIndex)
                        total--;
                }
            }

            if ((row == -1) || (total <= 2))
                return false;

            return true;
        }

        private void SmoothHorizontal(IList<DataGridViewCell> cells)
        {
            try
            {
                double x, x1, x2, y1, y2;
                for (int start = 0, end = 0; end < cells.Count; )
                {
                    x1 = dataGrid.Columns[cells[start].ColumnIndex].HeaderCell.ValueAsDouble();
                    y1 = cells[start].ValueAsDouble();
                    for (end = start; end < cells.Count && cells[start].RowIndex == cells[end].RowIndex; ++end)
                        continue;
                    x2 = dataGrid.Columns[cells[end - 1].ColumnIndex].HeaderCell.ValueAsDouble();
                    y2 = cells[end - 1].ValueAsDouble();
                    for (; start < end; ++start)
                    {
                        x = dataGrid.Columns[cells[start].ColumnIndex].HeaderCell.ValueAsDouble();
                        double value = Util.LinearInterpolation(x, x1, x2, y1, y2);
                        cells[start].Value = value.ToString(Util.DoubleFormat);
                    }
                }
            }
            catch (FormatException e)
            {
                statusStrip1.Items[0].Text = e.Message;
            }
            catch (ArgumentNullException)
            {
            }
        }

        private void SmoothVertical(IList<DataGridViewCell> cells)
        {
            try
            {
                double x, x1, x2, y1, y2;
                for (int start = 0, end = 0; end < cells.Count; )
                {
                    x1 = dataGrid.Rows[cells[start].RowIndex].HeaderCell.ValueAsDouble();
                    y1 = cells[start].ValueAsDouble();
                    for (end = start; end < cells.Count && cells[start].ColumnIndex == cells[end].ColumnIndex; ++end)
                        continue;
                    x2 = dataGrid.Rows[cells[end - 1].RowIndex].HeaderCell.ValueAsDouble();
                    y2 = cells[end - 1].ValueAsDouble();
                    for (; start < end; ++start)
                    {
                        x = dataGrid.Rows[cells[start].RowIndex].HeaderCell.ValueAsDouble();
                        double value = Util.LinearInterpolation(x, x1, x2, y1, y2);
                        cells[start].Value = value.ToString(Util.DoubleFormat);
                    }
                }
            }
            catch (FormatException e)
            {
                statusStrip1.Items[0].Text = e.Message;
            }
            catch (ArgumentNullException)
            {
            }
        }

        private List<DataGridViewCell> SortCellsByRow(DataGridViewSelectedCellCollection input)
        {
            List<DataGridViewCell> result = new List<DataGridViewCell>();
            foreach (DataGridViewCell cell in input)
            {
                if (cell == null)
                {
                    continue;
                }
                result.Add(cell);
            }
            result.Sort(delegate(DataGridViewCell a, DataGridViewCell b)
            {
                if (a.RowIndex < b.RowIndex)
                    return -1;
                else if (a.RowIndex == b.RowIndex)
                {
                    if (a.ColumnIndex < b.ColumnIndex)
                        return -1;
                    else if (a.ColumnIndex == b.ColumnIndex)
                        return 0;
                }
                return 1;
            });
            return result;
        }

        private List<DataGridViewCell> SortCellsByColumn(DataGridViewSelectedCellCollection input)
        {
            List<DataGridViewCell> result = new List<DataGridViewCell>();
            foreach (DataGridViewCell cell in input)
            {
                if (cell == null)
                {
                    continue;
                }
                result.Add(cell);
            }
            result.Sort(delegate(DataGridViewCell a, DataGridViewCell b)
            {
                if (a.ColumnIndex < b.ColumnIndex)
                    return -1;
                else if (a.ColumnIndex == b.ColumnIndex)
                {
                    if (a.RowIndex < b.RowIndex)
                        return -1;
                    else if (a.RowIndex == b.RowIndex)
                        return 0;
                }
                return 1;
            });
            return result;
        }
    }
}
