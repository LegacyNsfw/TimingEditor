using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSFW.TimingEditor
{
    /// <summary>
    /// Painting code for the application's main window.
    /// </summary>
	public partial class TimingForm : Form
	{
        /// <summary>
        /// Draw the side views that help visual=ize the table shape.
        /// </summary>
        private void DrawSideViews(int activeColumn, int activeRow)
        {
            Bitmap horizontalPanelBitmap = new Bitmap(this.horizontalPanel.Width, this.horizontalPanel.Height);
            Graphics horizontalPanelBackBuffer = Graphics.FromImage(horizontalPanelBitmap);
            //Graphics horizontalPanelBackBuffer = horizontalPanel.CreateGraphics();

            Bitmap verticalPanelBitmap = new Bitmap(this.verticalPanel.Width, this.verticalPanel.Height);
            Graphics verticalPanelBackBuffer = Graphics.FromImage(verticalPanelBitmap);
            //Graphics verticalPanelBackBuffer = verticalPanel.CreateGraphics();

            horizontalPanelBackBuffer.FillRectangle(Brushes.White, this.horizontalPanel.ClientRectangle);
            verticalPanelBackBuffer.FillRectangle(Brushes.White, this.verticalPanel.ClientRectangle);
                        
            double min;
            double max;
            this.GetMinMax(out min, out max);

            Pen pen = Pens.Gray;
            for (int row = 0; row < this.dataGrid.Rows.Count; row++)
            {
                this.DrawRow(horizontalPanelBackBuffer, pen, row, min, max);
            }

            for (int column = 0; column < this.dataGrid.Columns.Count; column++)
            {
                this.DrawColumn(verticalPanelBackBuffer, pen, column, min, max);
            }

            if ((activeColumn >= 0) && (activeColumn < this.dataGrid.Columns.Count) &&
                (activeRow >= 0) && (activeRow < this.dataGrid.Rows.Count))
            {
                using (Pen heavyPen = new Pen(Color.Black, 3))
                {
                    this.DrawRow(horizontalPanelBackBuffer, heavyPen, activeRow, min, max);
                    this.DrawColumn(verticalPanelBackBuffer, heavyPen, activeColumn, min, max);
                }

                using (Pen lightPen = new Pen(Color.Gray, 2))
                {
                    int x = this.GetRowX(activeColumn);
                    horizontalPanelBackBuffer.DrawLine(lightPen, x, 0, x, horizontalPanel.Height);

                    int y = this.GetColumnY(activeRow);
                    verticalPanelBackBuffer.DrawLine(lightPen, 0, y, verticalPanel.Width, y);
                }
            }
            
            SmoothInfo si = this.GetSmoothInfo(min, max);
            if (si != null)
            {
                if (si.A.RowIndex == si.B.RowIndex)
                {
                    this.DrawRowSmooth(horizontalPanelBackBuffer, si);
                }
                else
                {
                    this.DrawColumnSmooth(verticalPanelBackBuffer, si);
                }
            }

            Graphics graphics = this.horizontalPanel.CreateGraphics();
            graphics.DrawImage(horizontalPanelBitmap, 0, 0);
            graphics = this.verticalPanel.CreateGraphics();
            graphics.DrawImage(verticalPanelBitmap, 0, 0);
        }

        /// <summary>
        /// Get the min and max values for the whole data-grid.
        /// </summary>
        private void GetMinMax(out double min, out double max)
        {
            min = double.MaxValue;
            max = double.MinValue;
            double value;
            for (int row = 0; row < this.dataGrid.Rows.Count; row++)
            {
                for (int column = 0; column < this.dataGrid.Columns.Count; column++)
                {
                    if (!this.TryGetValue(column, row, out value))
                    {
                        return;
                    }

                    min = Math.Min(min, value);
                    max = Math.Max(max, value);
                }
            }
        }

        /// <summary>
        /// Min and Max data about a range of cells.
        /// </summary>
        private class SmoothInfo
        {
            public DataGridViewCell A;
            public DataGridViewCell B;
            public double MinValue;
            public double MaxValue;
        }

        /// <summary>
        /// Calculate the 'SmoothInfo' for a selected set of cells.
        /// </summary>
        private SmoothInfo GetSmoothInfo(double min, double max)
        {
            DataGridViewSelectedCellCollection selected = this.dataGrid.SelectedCells;
            if (this.SelectedColumn(selected))
            {
                SmoothInfo result = new SmoothInfo();
                result.MinValue = min;
                result.MaxValue = max;
                IEnumerable<DataGridViewCell> cells = selected.Cast<DataGridViewCell>();
                int minY = cells.Min(cell => cell.RowIndex);
                int maxY = cells.Max(cell => cell.RowIndex);
                result.A = cells.Where(cell => cell.RowIndex == minY).First();
                result.B = cells.Where(cell => cell.RowIndex == maxY).First();
                return result;
            }

            if (this.SelectedRow(this.dataGrid.SelectedCells))
            {
                SmoothInfo result = new SmoothInfo();
                result.MinValue = min;
                result.MaxValue = max;
                IEnumerable<DataGridViewCell> cells = selected.Cast<DataGridViewCell>();
                int minX = cells.Min(cell => cell.ColumnIndex);
                int maxX = cells.Max(cell => cell.ColumnIndex);
                result.A = cells.Where(cell => cell.ColumnIndex == minX).First();
                result.B = cells.Where(cell => cell.ColumnIndex == maxX).First();
                return result;
            }

            return null;
        }

        /// <summary>
        /// Draw a row of data.
        /// </summary>
        private void DrawRowSmooth(Graphics graphics, SmoothInfo si)
        {
            double valueA, valueB;
            if (!this.TryGetValue(si.A.ColumnIndex, si.A.RowIndex, out valueA))
            {
                return;
            }

            if (!this.TryGetValue(si.B.ColumnIndex, si.B.RowIndex, out valueB))
            {
                return;
            }

            float x1 = this.GetRowX(si.A.ColumnIndex);
            float y1 = this.GetRowY(si.MinValue, si.MaxValue, valueA);
            float x2 = this.GetRowX(si.B.ColumnIndex);
            float y2 = this.GetRowY(si.MinValue, si.MaxValue, valueB);

            using (Pen pen = new Pen(Color.Blue, 3))
            {
                graphics.DrawLine(
                    pen,
                    x1,
                    y1,
                    x2,
                    y2);
            }
        }

        /// <summary>
        /// Draw a column of data.
        /// </summary>
        private void DrawColumnSmooth(Graphics graphics, SmoothInfo si)
        {
            double valueA, valueB;
            if (!this.TryGetValue(si.A.ColumnIndex, si.A.RowIndex, out valueA))
            {
                return;
            }

            if (!this.TryGetValue(si.B.ColumnIndex, si.B.RowIndex, out valueB))
            {
                return;
            }

            using (Pen pen = new Pen(Color.Blue, 3))
            {
                graphics.DrawLine(
                    pen,
                    this.GetColumnX(si.MinValue, si.MaxValue, valueA),
                    this.GetColumnY(si.A.RowIndex),
                    this.GetColumnX(si.MinValue, si.MaxValue, valueB),
                    this.GetColumnY(si.B.RowIndex));
            }
        }

        /// <summary>
        /// Draw a row of data.
        /// </summary>
        private void DrawRow(Graphics graphics, Pen pen, int row, double min, double max)
        {
            double value;
            if (!this.TryGetValue(0, row, out value))
            {
                return;
            }

            int lastX = this.dataGrid.RowHeadersWidth;
            int lastY = this.GetRowY(min, max, value);

            int nextX;
            int nextY;
            for (int i = 0; i < this.dataGrid.Columns.Count; i++)
            {
                if (!this.TryGetValue(i, row, out value))
                {
                    return;
                }

                nextX = this.GetRowX(i);
                nextY = this.GetRowY(min, max, value);

                if (i != 0)
                {
                    graphics.DrawLine(pen, lastX, lastY, nextX, nextY);
                }

                lastX = nextX;
                lastY = nextY;
            }

            //nextX = this.horizontalPanel.Width;
            //nextY = lastY;
            //graphics.DrawLine(pen, lastX, lastY, nextX, nextY);
        }

        /// <summary>
        /// Draw a column of data.
        /// </summary>
        private void DrawColumn(Graphics graphics, Pen pen, int column, double min, double max)
        {
            double value;
            if (!this.TryGetValue(column, 0, out value))
            {
                return;
            }

            int lastX = this.GetColumnX(min, max, value);
            int lastY = this.dataGrid.ColumnHeadersHeight;
            int nextX;
            int nextY;
            for (int i = 0; i < this.dataGrid.Rows.Count; i++)
            {
                if (!this.TryGetValue(column, i, out value))
                {
                    return;
                }

                nextX = this.GetColumnX(min, max, value);
                nextY = this.GetColumnY(i);

                if (i != 0)
                {
                    graphics.DrawLine(pen, lastX, lastY, nextX, nextY);
                }

                lastX = nextX;
                lastY = nextY;
            }

            //nextX = lastX;
            //nextY = this.verticalPanel.Height;
            //graphics.DrawLine(pen, lastX, lastY, nextX, nextY);
        }

        private int GetRowX(int i)
        {
            int width = this.horizontalPanel.Width - this.dataGrid.RowHeadersWidth;
            int offset = (width / (this.dataGrid.Columns.Count * 2)) + this.dataGrid.RowHeadersWidth;

            return ((width * i) / (this.dataGrid.Columns.Count)) + offset;
        }

        private int GetRowY(double min, double max, double value)
        {
            double difference = max - min;
            double magnitude = difference == 0 ? 0.5 : (max - value) / difference;
            double result = magnitude * (this.horizontalPanel.Height * 0.8);
            return (int)result + (int)(this.horizontalPanel.Height * 0.1);
        }

        private int GetColumnX(double min, double max, double value)
        {
            double difference = max - min;
            double magnitude = difference == 0 ? 0.5 : (max - value) / difference;
            double result = magnitude * (this.verticalPanel.Width * 0.8);
            return (int)result + (int)(this.verticalPanel.Width * 0.1);
        }

        private int GetColumnY(int i)
        {
            int height = this.verticalPanel.Height - this.dataGrid.ColumnHeadersHeight;
            int offset = (height / (this.dataGrid.Columns.Count * 2)) + this.dataGrid.ColumnHeadersHeight;
            return ((height * i) / (this.dataGrid.Rows.Count)) + offset;
        }
    }
}
