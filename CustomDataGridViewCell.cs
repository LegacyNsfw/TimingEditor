using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSFW.TimingEditor
{
    class CustomDataGridViewCell : DataGridViewTextBoxCell
    {
        private DataGridViewAdvancedBorderStyle _style;

        public CustomDataGridViewCell() : base()
        {

            _style = new DataGridViewAdvancedBorderStyle();

            _style.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            _style.Top = DataGridViewAdvancedCellBorderStyle.None;
            _style.Left = DataGridViewAdvancedCellBorderStyle.None;
            _style.Right = DataGridViewAdvancedCellBorderStyle.None;

        }

        public DataGridViewAdvancedBorderStyle AdvancedBorderStyle
        {
            get { return _style; }
            set
            {
                _style.Bottom = value.Bottom;
                _style.Top = value.Top;
                _style.Left = value.Left;
                _style.Right = value.Right;
            }
        }

        protected override void PaintBorder(Graphics graphics, Rectangle clipBounds, Rectangle bounds, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle)
        {
            base.PaintBorder(graphics, clipBounds, bounds, cellStyle, _style);
            using (Pen p = new Pen(Color.Navy, 5))
            {
                Rectangle rect = bounds;
                rect.X = rect.X + 1;
                rect.Y = rect.Y + 1;
                rect.Width -= 4;
                rect.Height -= 4;
                graphics.DrawRectangle(p, rect);
            }
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, _style, paintParts);
            using (Pen p = new Pen(Color.Navy, 2))
            {
                Rectangle rect = cellBounds;
                rect.Width -= 1;
                rect.Height -= 1;
                graphics.DrawRectangle(p, rect);
            }
        }
    }
}
