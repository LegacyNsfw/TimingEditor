using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NSFW.TimingEditor
{
    public partial class LogOverlay : Form
    {
        public LogOverlay()
        {
            InitializeComponent();
        }

        public String[] LogParameters
        {
            get
            {
                if (this.headerListBox.SelectedItems.Count > 0)
                {
                    int i = 0;
                    String[] s = new String[this.headerListBox.CheckedItems.Count];
                    foreach (Object o in this.headerListBox.CheckedItems)
                        s[i++] = o.ToString();
                    return s;
                }
                else
                    return new String[0];
            }
            set
            {
                if (value.Length > 0)
                {
                    this.headerListBox.Items.Clear();
                    this.xAxisComboBox.Items.Clear();
                    this.yAxisComboBox.Items.Clear();
                    String engLoad = null;
                    String engSpeed = null;
                    foreach (String s in value)
                    {
                        this.headerListBox.Items.Add(s);
                        this.xAxisComboBox.Items.Add(s);
                        this.yAxisComboBox.Items.Add(s);
                        if (Regex.IsMatch(s, ".*\\bengine[_\\s]load\\b.*", RegexOptions.IgnoreCase))
                            engLoad = s;
                        else if (Regex.IsMatch(s, ".*\\b(engine[_\\s]speed|rpm)\\b.*", RegexOptions.IgnoreCase))
                            engSpeed = s;
                    }
                    if (engLoad != null)
                        this.xAxisComboBox.SelectedItem = engLoad;
                    else
                        this.xAxisComboBox.SelectedIndex = 0;
                    if (engSpeed != null)
                        this.yAxisComboBox.SelectedItem = engSpeed;
                    else
                        this.yAxisComboBox.SelectedIndex = 0;
                }
            }
        }

        public String XAxis
        {
            get { return this.xAxisComboBox.SelectedItem.ToString(); }
        }

        public String YAxis
        {
            get { return this.yAxisComboBox.SelectedItem.ToString(); }
        }
    }
}
