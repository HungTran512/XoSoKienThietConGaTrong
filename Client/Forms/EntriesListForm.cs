using Client.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Client.Forms
{
    public partial class EntriesListForm : Form
    {
        private ListView lvEntries;

        public EntriesListForm()
        {
            InitializeForm();
        }
        private void InitializeForm()
        {
            this.AutoScroll = true;
            this.components = new Container();
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(360, 240);
            this.Text = "Entries List";

            // Initialize ListView
            lvEntries = new ListView
            {
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Location = new Point(10, 10),
                Size = new Size(780, 430),
                Scrollable = true
            };

            lvEntries.Columns.Add("User", 150);
            lvEntries.Columns.Add("Bet Number", 100);
            lvEntries.Columns.Add("Slot Time", 200);

            this.Controls.Add(lvEntries);
        }
        public void PopulateEntries(List<LotteryEntry> entries)
        {
            foreach (var entry in entries)
            {   
                
               var listViewItem = new ListViewItem(entry.User.name); 
                listViewItem.SubItems.Add(entry.betNumber.ToString());
                listViewItem.SubItems.Add(entry.slotTime.ToLocalTime().ToString("g"));
                System.Diagnostics.Debug.WriteLine(entry);
                lvEntries.Items.Add(listViewItem);
            }
        }


    }
}
