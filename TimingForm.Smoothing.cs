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
            this.Smooth(this.dataGrid.SelectedCells, true);
        }

        private void smoothTableButton_Click(object sender, EventArgs e)
        {
            // create 2d array same size as table
            // smooth across rows
            // smooth across columns
            // apply deltas
        }

        private bool CanSmooth
        {
            get
            {
                return this.Smooth(this.dataGrid.SelectedCells, false);
            }
        }

        private bool Smooth(DataGridViewSelectedCellCollection selectedCells, bool forReal)
        {
            if (this.SelectedColumn(selectedCells))
            {
                if (forReal)
                {
                    IList<DataGridViewCell> cells = this.SortColumn(selectedCells);
                    this.Smooth(cells);
                }
                return true;
            }
            else if (this.SelectedRow(selectedCells))
            {
                if (forReal)
                {
                    IList<DataGridViewCell> cells = this.SortRow(selectedCells);
                    this.Smooth(cells);
                }
                return true;
            }
            return false;
        }

        private bool SelectedColumn(System.Collections.ICollection selectedCells)
        {
            int column = -1;
            int total = 0;
            foreach (DataGridViewCell cell in selectedCells)
            {
                total++;
                if (column == -1)
                {
                    column = cell.ColumnIndex;
                }
                else
                {
                    if (column != cell.ColumnIndex)
                    {
                        return false;
                    }
                }
            }

            if ((column == -1) || (total <= 2))
            {
                return false;
            }

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
                {
                    row = cell.RowIndex;
                }
                else
                {
                    if (row != cell.RowIndex)
                    {
                        return false;
                    }
                }
            }

            if ((row == -1) || (total <= 2))
            {
                return false;
            }

            return true;
        }

        private void Smooth(IList<DataGridViewCell> cells)
        {
            try
            {
                double cellMinValue = cells[0].ValueAsDouble();
                double cellMaxValue = cells[cells.Count - 1].ValueAsDouble();
                double step = (cellMaxValue - cellMinValue) / (cells.Count - 1);
                double min, max;

                if (cellMinValue < cellMaxValue)
                {
                    min = cellMinValue;
                    max = cellMaxValue;
                    for (int i = 0; i < cells.Count; i++)
                    {
                        double value = min + (step * i);
                        cells[i].Value = value.ToString(Util.DoubleFormat);
                    }
                }
                else
                {
                    min = cellMaxValue;
                    max = cellMinValue;
                    for (int i = 0; i < cells.Count; i++)
                    {
                        double value = max + (step * i);
                        cells[i].Value = value.ToString(Util.DoubleFormat);
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

        private List<DataGridViewCell> SortColumn(DataGridViewSelectedCellCollection input)
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
                {
                    return -1;
                }
                if (a.RowIndex > b.RowIndex)
                {
                    return 1;
                }
                return 0;
            });
            return result;
        }

        private List<DataGridViewCell> SortRow(DataGridViewSelectedCellCollection input)
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
                {
                    return -1;
                }
                if (a.ColumnIndex > b.ColumnIndex)
                {
                    return 1;
                }
                return 0;
            });
            return result;
        }
    }
}
