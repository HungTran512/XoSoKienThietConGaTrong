using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Client.Models; 

namespace Client.Forms
{
    public partial class ResultForm : Form
    {
        private ListView lvResults;

        public ResultForm()
        {
            InitializeForm();
            InitializeListView();
        }

        private void InitializeForm()
        {
            this.AutoScroll = true;
            this.Size = new Size(400, 300);
            this.Text = "Bet Results";
        }

        private void InitializeListView()
        {
            lvResults = new ListView
            {
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Location = new Point(10, 10),
                Size = new Size(360, 360),
                Scrollable = true
            };

            lvResults.Columns.Add("Draw Time", 150);
            lvResults.Columns.Add("Result", 100);
            lvResults.Columns.Add("Winners", 230);
            this.Controls.Add(lvResults);
        }


        public void UpdateResults(List<LotteryResult> results, List<String> winners)
        {
            foreach (var result in results)
            {
               
                var localDrawTime = result.DrawTime.ToLocalTime();

                var listViewItem = new ListViewItem(localDrawTime.ToString("g"));
                listViewItem.SubItems.Add(result.Result.ToString());

                string winnersText = string.Join(", ", winners);

                if (string.IsNullOrEmpty(winnersText))
                {
                    winnersText = "No Winners";
                }

                listViewItem.SubItems.Add(winnersText);

                lvResults.Items.Add(listViewItem);
            }
        }

    }
}
