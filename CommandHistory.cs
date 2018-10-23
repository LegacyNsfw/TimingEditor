using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSFW.TimingEditor
{
    /// <summary>
    /// Base class for a command.
    /// </summary>
    public abstract class Command
    {
        public abstract void Execute();
        public abstract void Undo();
    }

    /// <summary>
    /// Change the value of a cell.
    /// </summary>
    public class EditCell : Command
    {
        private ITable table;
        private double oldValue;
        private double newValue;
        private int x;
        private int y;

        public ITable Table { get { return this.table; } }
        public int Y { get { return this.y; } }
        public int X { get { return this.x; } }
        public double OldValue { get { return this.oldValue; } }
        public double NewValue { get { return this.newValue; } }

        public EditCell(ITable table, int x, int y, double newValue)
        {
            this.table = table;
            this.x = x;
            this.y = y;
            this.newValue = newValue;
            this.oldValue = this.table.GetCell(this.x, this.y);
        }

        public override void Execute()
        {
            this.table.SetCell(this.x, this.y, this.newValue);            
        }

        public override void Undo()
        {
            this.table.SetCell(this.x, this.y, this.oldValue);
        }
    }

/*    public class EditMultipleCells : Command
    {
        private IList<EditCell> cells;
        public EditMultipleCells(IList<EditCell> cells)
        {
            this.cells = cells;
        }

        public override void Execute()
        {
            foreach (EditCell cell in this.cells)
            {
                cell.Execute();
            }
        }

        public override void Undo()
        {
            foreach (EditCell cell in this.cells)
            {
                cell.Undo();
            }
        }
    }

    public class Paste : Command
    {
        private ITable source;
        private ITable destination;
        private ITable backup;

        public Paste(ITable source, ITable destination)
        {
            this.source = source;
            this.destination = destination;
            this.backup = destination.Clone();
        }

        public override void Execute()
        {
            this.source.CopyTo(destination);
        }

        public override void Undo()
        {
            this.backup.CopyTo(this.destination);
        }
    }

    public class DoublePaste : Command
    {
        private Paste initial;
        private Paste modified;

        public DoublePaste(Paste initial, Paste modified)
        {
            this.initial = initial;
            this.modified = modified;
        }
   
        public override void Execute()
        {
            this.initial.Execute();
            this.modified.Execute();
        }

        public override void Undo()
        {
            this.initial.Undo();
            this.modified.Undo();
        }
    }
*/

    /// <summary>
    /// Allows the undo/redo buttons to be updated appropriately.
    /// </summary>
    public delegate void UpdateCommandHistoryButtons(object sender, EventArgs args);

    /// <summary>
    /// Implements the CommandHistory patter from the Gang-Of-Four book.
    /// </summary>
    public class CommandHistory
    {
        /// <summary>
        /// Singleton.
        /// </summary>
        private static CommandHistory instance = new CommandHistory();

        /// <summary>
        /// List of commands that can be undone.
        /// </summary>
        private List<Command> commands;

        /// <summary>
        /// List of commands that have been undone - and therefore can be redone.
        /// </summary>
        private List<Command> undone;

        /// <summary>
        /// Allows updating the undo/redo buttons.
        /// </summary>
        public event UpdateCommandHistoryButtons UpdateCommandHistoryButtons;

        /// <summary>
        /// Constructor.
        /// </summary>
        private CommandHistory()
        {
            this.commands = new List<Command>();
            this.undone = new List<Command>();
        }

        /// <summary>
        /// Singleton accessor.
        /// </summary>
        public static CommandHistory Instance
        {
            [System.Diagnostics.DebuggerStepThrough]
            get 
            {
                return instance;
            }
        }

        /// <summary>
        /// Indicates whether we're in a state where a command can be undone.
        /// </summary>
        public bool CanUndo { get { return this.commands.Count > 0; } }

        /// <summary>
        /// Indicates whether we're in a state when a command can be redone.
        /// </summary>
        public bool CanRedo { get { return this.undone.Count > 0; } }

        /// <summary>
        /// Execute a new command.
        /// </summary>
        public void Execute(Command command)
        {
            command.Execute();
            this.commands.Add(command);
            this.undone.Clear();
            this.UpdateButtons();
        }

        /// <summary>
        /// Undo a recent command.
        /// </summary>
        public Command Undo()
        {
            if (this.commands.Count == 0)
            {
                return null;
            }

            int lastIndex = this.commands.Count - 1;
            Command command = this.commands[lastIndex];
            this.commands.RemoveAt(lastIndex);

            command.Undo();

            this.undone.Add(command);
            this.UpdateButtons();

            return command;
        }

        /// <summary>
        /// Redo a recently undone command.
        /// </summary>
        public Command Redo()
        {
            if (this.undone.Count == 0)
            {
                return null;
            }

            int lastIndex = this.undone.Count - 1;
            Command command = this.undone[lastIndex];
            this.undone.RemoveAt(lastIndex);

            command.Execute();

            this.commands.Add(command);
            this.UpdateButtons();

            return command;
        }

        /// <summary>
        /// Update the undo/redo buttons.
        /// </summary>
        private void UpdateButtons()
        {
            if (this.UpdateCommandHistoryButtons != null)
            {
                this.UpdateCommandHistoryButtons(this, new EventArgs());
            }
        }
    }
}
