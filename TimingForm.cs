using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSFW.TimingEditor
{
    /// <summary>
    /// The main form for the application.
    /// </summary>
    public partial class TimingForm : Form
    {
        /// <summary>
        /// Collection of timing tables.
        /// </summary>
        private TimingTables tables = new TimingTables();

        /// <summary>
        /// Indicates whether we're changing tables.
        /// </summary>
        private bool changingTables;

        /// <summary>
        /// When the mouse is in a cell, we need to suppress value-changed messages.
        /// </summary>
        private bool inCellMouseEnter;

        /// <summary>
        /// Indicates which column is selected.
        /// </summary>
        private int selectedColumn;

        /// <summary>
        /// Indicates which row is selected.
        /// </summary>
        private int selectedRow;

        /// <summary>
        /// Since the advance table is often smaller than the base table, this indicates
        /// how many columns had to be faked in order to line up the table contents.
        /// </summary>
        private int advancePadding;

        /// <summary>
        /// Indicates whether we're subscribed for key-down events.
        /// It's a reminder to unsubscribe later.
        /// </summary>
        private bool editControlKeyDownSubscribed;

        /// <summary>
        /// Indicates whether we're in single-table mode.
        /// </summary>
        private bool singleTableMode;

        /// <summary>
        /// Constructor.
        /// </summary>
        public TimingForm(bool singleTableMode)
        {
            this.singleTableMode = singleTableMode;
            
            InitializeComponent();
        }

        /// <summary>
        /// Update the undo and redo buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void CommandHistory_UpdateButtons(object sender, EventArgs args)
        {
            this.undoButton.Enabled = CommandHistory.Instance.CanUndo;
            this.redoButton.Enabled = CommandHistory.Instance.CanRedo;
        }

        /// <summary>
        /// Prepare the form contents before displaying.
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            CommandHistory.Instance.UpdateCommandHistoryButtons += this.CommandHistory_UpdateButtons;
            this.CommandHistory_UpdateButtons(null, null);

            this.tableList.Items.Add(new TableListEntry("Initial base timing", this.tables.InitialBaseTiming, true,
                "When you paste into this table, the modified base timing table will also be initialized with the same data."));
            this.tableList.Items.Add(new TableListEntry("Initial advance timing", this.tables.InitialAdvanceTiming, true,
                "When you paste into this table, the modified advance timing table will also be initialized with the same data."));
            this.tableList.Items.Add(new TableListEntry("Initial total timing", this.tables.InitialTotalTiming, false,
                "You cannot edit this table."));
            this.tableList.Items.Add(new TableListEntry("Modified base timing", this.tables.ModifiedBaseTiming, true,
                ""));
            this.tableList.Items.Add(new TableListEntry("Modified advance timing", this.tables.ModifiedAdvanceTiming, true,
                "The base timing will be adjusted when you change cells in this table, so that the total timing does not change."));
            this.tableList.Items.Add(new TableListEntry("Modified total timing", this.tables.ModifiedTotalTiming, false,
                "When you edit cells in this table, the changes are actually made to the base timing table."));
            this.tableList.Items.Add(new TableListEntry("Delta total timing", this.tables.DeltaTotalTiming, false,
                "This table shows the difference between the initial total timing and the modified total timing."));

            if (this.singleTableMode)
            {
                this.tableList.Visible = false;
            }

            if (Program.Debug)
            {
                try
                {
                    using (FileStream file = new FileStream("..\\..\\TimingBase.txt", FileMode.Open))
                    {
                        StreamReader reader = new StreamReader(file);
                        string content = reader.ReadToEnd();
                        Util.LoadTable(content, this.tables.InitialBaseTiming);
                        this.tables.InitialBaseTiming.IsReadOnly = true;
                        Util.LoadTable(content, this.tables.ModifiedBaseTiming);
                    }
                    using (FileStream file = new FileStream("..\\..\\TimingAdvance.txt", FileMode.Open))
                    {
                        StreamReader reader = new StreamReader(file);
                        string content = reader.ReadToEnd();
                        Util.LoadTable(content, this.tables.InitialAdvanceTiming);
                        this.tables.InitialAdvanceTiming.IsReadOnly = true;
                        Util.LoadTable(content, this.tables.ModifiedAdvanceTiming);
                    }

                    tableList_SelectedIndexChanged(null, null);
                }
                catch (IOException)
                {
                }
                catch (ApplicationException)
                {
                }
                catch (ArgumentOutOfRangeException)
                {
                }
            }

            this.tableList.SelectedIndex = 0;
        }
        
        /// <summary>
        /// Invoked when the selected-index of the table list has changed.
        /// </summary>
        private void tableList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tableList.SelectedItem == null)
            {
                return;
            }

            TableListEntry entry = this.tableList.SelectedItem as TableListEntry;
            string title;

            if (this.singleTableMode)
            {
                this.statusStrip1.Items[0].Text = "This is a reduced-functionality version of Timing Editor, for use with a single table.";
                title = "Table Editor {0}";
            }
            else
            {
                title = "Timing Editor {0}: " + entry.Description;
                this.pasteButton.Enabled = entry.AllowPaste;
                this.statusStrip1.Items[0].Text = entry.StatusText;
            }

            this.Text = string.Format(title, "v14");

            if (entry.Table.IsPopulated)
            {
                try
                {
                    List<int[]> selectedIndices = new List<int[]>(this.dataGrid.SelectedCells.Count);
                    DataGridViewSelectedCellCollection selected = this.dataGrid.SelectedCells;
                    foreach (DataGridViewCell cell in selected)
                    {
                        selectedIndices.Add(new int[2] { cell.ColumnIndex, cell.RowIndex });
                    }

                    this.dataGrid.ReadOnly = entry.Table.IsReadOnly;
                    this.changingTables = true;
                    Util.ShowTable(this, entry.Table, this.dataGrid);
                    this.DrawSideViews(this.selectedColumn, this.selectedRow);
                    Util.ColorTable(this.dataGrid, entry.Table, this.selectedColumn, this.selectedRow);
                    this.dataGrid.ClearSelection();
                    this.changingTables = false;
                    foreach (int[] pair in selectedIndices)
                    {
                        dataGrid.Rows[pair[1]].Cells[pair[0]].Selected = true;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    this.dataGrid.ClearSelection();
                    dataGrid.Rows.Clear();
                    this.statusStrip1.Items[0].Text = "This only works if the base and advance tables are the same size";
                }
            }
            else
            {
                this.changingTables = true;
                this.dataGrid.Columns.Clear();
                this.changingTables = false;
            }

            if (entry.Table.IsReadOnly)
            {
                this.smoothButton.Enabled = false;
            }

/*            DataGridViewCell cell;
            if (this.dataGrid.SelectedCells.Count == 1)
            {
                cell = this.dataGrid.SelectedCells[0];
                this.DrawSideViews(cell.ColumnIndex, cell.RowIndex);
            }            */
        }

        /// <summary>
        /// Invoked when the Copy button is clicked.
        /// </summary>
        private void copyButton_Click(object sender, EventArgs e)
        {
            TableListEntry entry = this.tableList.SelectedItem as TableListEntry;
            if (entry == null)
            {
                return;
            }

            ITable copyFrom = entry.Table;
            if ((entry.Table == this.tables.InitialAdvanceTiming) || (entry.Table == this.tables.ModifiedAdvanceTiming))
            {
                if (this.advancePadding > 0)
                {
                    string message = string.Format("The advance table will have the leftmost {0} columns removed.", this.advancePadding);
                    MessageBox.Show(this, message, "Timing Editor", MessageBoxButtons.OK);
                    copyFrom = Util.TrimLeft(copyFrom, this.advancePadding);
                }
            }

            string text = Util.CopyTable(copyFrom);
            Clipboard.SetData(DataFormats.Text, text);
        }

        /// <summary>
        /// Invoked when the paste button is clicked.
        /// </summary>
        private void pasteButton_Click(object sender, EventArgs e)
        {
            TableListEntry entry = this.tableList.SelectedItem as TableListEntry;
            if (entry == null)
            {
                return;
            }

            try
            {
                string tableText = Clipboard.GetData(System.Windows.Forms.DataFormats.Text) as string;
                if (string.IsNullOrEmpty(tableText))
                {
                    throw new ApplicationException("Doesn't contain text.");
                }

                this.changingTables = true;
                bool wasReadOnly = entry.Table.IsReadOnly;
                if (wasReadOnly)
                {
                    entry.Table.IsReadOnly = false;
                }

                Table temporaryTable = new Table();
                Util.LoadTable(tableText, temporaryTable);

                if ((entry.Table == this.tables.InitialAdvanceTiming) || (entry.Table == this.tables.ModifiedAdvanceTiming))
                {
                    if (temporaryTable.ColumnHeaders.Count < this.tables.InitialBaseTiming.ColumnHeaders.Count)
                    {
                        MessageBox.Show(this, "The advance table will have values added to the left side, to make it align with the base timing table.", "Timing Editor", MessageBoxButtons.OK);
                        this.advancePadding = this.tables.InitialBaseTiming.ColumnHeaders.Count - temporaryTable.ColumnHeaders.Count;
                        temporaryTable = Util.PadLeft(temporaryTable, this.tables.InitialBaseTiming.ColumnHeaders.Count);
                    }
                    else
                    {
                        this.advancePadding = 0;
                    }
                }
                
                temporaryTable.CopyTo(entry.Table);
                
                if (wasReadOnly)
                {
                    entry.Table.IsReadOnly = true;
                }
                Util.ShowTable(this, entry.Table, this.dataGrid);
                this.changingTables = false;

                if (entry.Table == this.tables.InitialBaseTiming)
                {
                    temporaryTable.CopyTo(this.tables.ModifiedBaseTiming);
                    //Util.LoadTable(tableText, this.tables.ModifiedBaseTiming);
                }

                if (entry.Table == this.tables.InitialAdvanceTiming)
                {
                    temporaryTable.CopyTo(this.tables.ModifiedAdvanceTiming);
                    //Util.LoadTable(tableText, this.tables.ModifiedAdvanceTiming);
                }
            }
            catch (ApplicationException ex)
            {
                string errorMessageFormat = "Clipboard does not contain valid table data.\r\n{0}";
                MessageBox.Show(string.Format(errorMessageFormat, ex.Message));
            }
        }

        /// <summary>
        /// Invoked when the value of a data grid cell has changed.
        /// </summary>
        private void dataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.changingTables || this.inCellMouseEnter)
            {
                return;
            }

            TableListEntry entry = this.tableList.SelectedItem as TableListEntry;
            if (entry == null)
            {
                return;
            }

            ITable table = entry.Table;
            if (table == null)
            {
                return;
            }

            if (table.IsReadOnly)
            {
                return;
            }

            object cellValue = this.dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            string newStringValue = cellValue as string;
            
            double value;
            if (double.TryParse(newStringValue, out value))
            {
                EditCell edit = new EditCell(table, e.ColumnIndex, e.RowIndex, value);
                CommandHistory.Instance.Execute(edit);

                // The "smooth" button stops working if this code is enabled...
/*                foreach (DataGridViewCell cell in this.dataGrid.SelectedCells)
                {
                    // TODO: create an "EditSelectedCells" command, execute that instead, for better undo/redo
                    EditCell edit = new EditCell(table, cell.ColumnIndex, cell.RowIndex, value);
                    CommandHistory.Instance.Execute(edit);
                    cell.Value = value;
                }                
 */ 
            }

            this.DrawSideViews(e.ColumnIndex, e.RowIndex);
        }

        /// <summary>
        /// Invoked when the state of a data-grid cell has changed.
        /// </summary>
        private void dataGrid_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            DataGridViewSelectedCellCollection selectedCells = this.dataGrid.SelectedCells;

            TableListEntry entry = this.tableList.SelectedItem as TableListEntry;
            if (entry.Table.IsReadOnly)
            {
                this.smoothButton.Enabled = false;
            }
            else
            {
/*                DataGridViewCellStyle style = Util.DefaultStyle;
                style.BackColor = Color.Black;
                style.ForeColor = Color.White;

                foreach (DataGridViewCell cell in this.dataGrid.SelectedCells)
                {
                    cell.Style = style;
                }
*/
                if (this.Smooth(selectedCells, false))
                {
                    this.smoothButton.Enabled = true;
                }
                else
                {
                    this.smoothButton.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Invoked when a data-grid cell is validating new data.
        /// </summary>
        private void dataGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
        }

        /// <summary>
        /// Invoked when a data-grid cell is exiting edit mode.
        /// </summary>
        private void dataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        }

        /// <summary>
        /// Invoked when the mouse is entering a data-grid cell.
        /// </summary>
        private void dataGrid_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.inCellMouseEnter = true;
            this.selectedColumn = e.ColumnIndex;
            this.selectedRow = e.RowIndex;

            TableListEntry entry = this.tableList.SelectedItem as TableListEntry;
            
            this.DrawSideViews(this.selectedColumn, this.selectedRow);
            Util.ColorTable(this.dataGrid, entry.Table, this.selectedColumn, this.selectedRow);
            this.inCellMouseEnter = false;
        }

        /// <summary>
        /// Invoked when a key is pressed and a data-grid cell has focus.
        /// </summary>
        private void dataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.Z)
                {
                    this.undoButton_Click(this, e);
                }

                if (e.KeyCode == Keys.Y)
                {
                    this.redoButton_Click(this, e);
                }
            }
        }
        
        /// <summary>
        /// Invoked when the editing control is shown for a data-grid cell.
        /// </summary>
        private void dataGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (!this.editControlKeyDownSubscribed)
            {
                e.Control.KeyDown += dataGridEditControl_KeyDown;
                this.editControlKeyDownSubscribed = true;
            }
        }

        /// <summary>
        /// Invoked when a key is pressed and a data-grid cell's editor is showing.
        /// </summary>
        private void dataGridEditControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 187)
            {
                this.Delta(+0.35);
                e.Handled = true;
                this.dataGrid.CancelEdit();
                this.dataGrid.EndEdit();
            }

            if (e.KeyValue == 189)
            {
                this.Delta(-0.35);
                e.Handled = true;
                this.dataGrid.CancelEdit();
                this.dataGrid.EndEdit();
            }
        }

        /// <summary>
        /// Adjust the currently-selected cells by the given amount.
        /// </summary>
        private void Delta(double delta)
        {
            foreach (DataGridViewCell cell in this.dataGrid.SelectedCells)
            {
                double value;
                if (double.TryParse(cell.Value.ToString(), out value))
                {
                    value += delta;
                    cell.Value = value.ToString();
                }
            }
        }

        /// <summary>
        /// Try to give the value for a single cell.
        /// </summary>
        private bool TryGetValue(int x, int y, out double value)
        {
            value = 0;

            object o = this.dataGrid.Rows[y].Cells[x].Value;
            if (o == null)
            {
                return false;
            }

            return double.TryParse(o.ToString(), out value);
        }

        /// <summary>
        /// Invoked when the redo button is clicked.
        /// </summary>
        private void redoButton_Click(object sender, EventArgs e)
        {
            CommandHistory.Instance.Redo();
            
            this.changingTables = true;
            TableListEntry entry = this.tableList.SelectedItem as TableListEntry;
            Util.ShowTable(this, entry.Table, this.dataGrid);
            Util.ColorTable(this.dataGrid, entry.Table, this.selectedColumn, this.selectedRow);
            this.changingTables = false;
            this.DrawSideViews(this.selectedColumn, this.selectedRow);
        }

        /// <summary>
        /// Invoked when the undo button is clicked.
        /// </summary>
        private void undoButton_Click(object sender, EventArgs e)
        {
            Command command = CommandHistory.Instance.Undo();
            
            this.changingTables = true;
            TableListEntry entry = this.tableList.SelectedItem as TableListEntry;
            Util.ShowTable(this, entry.Table, this.dataGrid);
            Util.ColorTable(this.dataGrid, entry.Table, this.selectedColumn, this.selectedRow);
            this.changingTables = false;
            this.DrawSideViews(this.selectedColumn, this.selectedRow);
        }
    }
}
