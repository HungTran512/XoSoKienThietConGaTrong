using System;
using System.Drawing;
using System.Windows.Forms;
using Client.Models;
using System.Collections.Generic;
using System.Linq;
namespace Client.Forms
{
    public partial class EntryForm : Form
    {

        private readonly ApiService _apiService;
        private readonly User _currentUser;
        private System.Collections.Generic.List<LotteryEntry> _entries;

        private Label lblUpcomingSlot;
        private Label lblBetNumber;
        private TextBox txtBetNumber;
        private Button btnPlaceBet;


        public EntryForm(ApiService apiService, User currentUser)
        {
            _apiService = apiService;
            _currentUser = currentUser;
            InitializeForm();
            InitializeUpcomingSlot();
        }

        private void InitializeForm()
        {
            this.lblUpcomingSlot = new Label();
            this.lblBetNumber = new Label();
            this.txtBetNumber = new TextBox();
            this.btnPlaceBet = new Button();

            // Label for Upcoming Slot Time
            this.lblUpcomingSlot.AutoSize = true;
            this.lblUpcomingSlot.Location = new Point(20, 110);
            this.lblUpcomingSlot.Size = new Size(260, 20);
            this.lblUpcomingSlot.Text = "Next Slot Time: Calculating...";

            // Label for Bet Number
            this.lblBetNumber.AutoSize = true;
            this.lblBetNumber.Location = new Point(20, 20);
            this.lblBetNumber.Text = "Enter your bet number (0-9):";

            // TextBox for Bet Number
            this.txtBetNumber.Location = new Point(20, 50);
            this.txtBetNumber.Size = new Size(100, 20);

            // Place Bet Button
            this.btnPlaceBet.Text = "Place Bet";
            this.btnPlaceBet.Location = new Point(20, 80);
            this.btnPlaceBet.Click += new EventHandler(this.btnPlaceBet_Click);

            // Add controls to the form
            this.Controls.Add(this.lblUpcomingSlot);
            this.Controls.Add(this.lblBetNumber);
            this.Controls.Add(this.txtBetNumber);
            this.Controls.Add(this.btnPlaceBet);

            // Set the size and title of the form
            this.Size = new Size(300, 200);
            this.Text = "Place Your Bet";
        }

        private void InitializeUpcomingSlot()
        {
            DateTime nextSlotTime = CalculateNextSlotTime();
            lblUpcomingSlot.Text = $"Next Slot Time: {nextSlotTime:hh:mm tt}";
        }

        private DateTime CalculateNextSlotTime()
        {
            DateTime currentTime = DateTime.Now;

            // If it's exactly on the hour, set the next slot to the next hour
            if (currentTime.Minute == 0 && currentTime.Second == 0)
            {
                return currentTime.AddHours(1);
            }
            else
            {
                // Otherwise, set the next slot to the start of the next hour
                return new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, currentTime.Hour, 0, 0).AddHours(1);
            }
        }

        private async void ShowEntriesListForm()
        {
            var entries = await _apiService.GetAllLotteryEntriesAsync();

            this._entries = entries;

            if (entries != null && entries.Count > 0)
            {
                EntriesListForm entriesListForm = new EntriesListForm();
                entriesListForm.PopulateEntries(entries); 
                entriesListForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No entries available.");
            }
        }
        private async void btnPlaceBet_Click(object sender, EventArgs e)
        {
            int betNumber;
            if (int.TryParse(txtBetNumber.Text, out betNumber) && betNumber >= 0 && betNumber <= 9)
            {
                // Valid bet number, proceed with placing the bet
                DateTime nextSlotTime = CalculateNextSlotTime();
                DateTime currentTime = DateTime.Now;
               
                if (currentTime >= nextSlotTime.AddMinutes(-60) && currentTime < nextSlotTime)
                {
                    // Place the bet
                    LotteryEntry entry = new LotteryEntry { betNumber = betNumber, User = _currentUser, slotTime = nextSlotTime };

                    var result = await _apiService.SubmitEntryAsync(entry);
                    if (result != null)
                    {
                        // Handle success - update UI or notify the user
                        MessageBox.Show("Bet placed successfully.");
                        ShowEntriesListForm();

                        var results = await _apiService.GetLotteryResultsForTodayAsync();

                        if (results != null && results.Count > 0)
                        {
                            //find the winner list
                            List<string> winners = new List<string>();

                            if(_entries != null)
                            {
                                foreach (var res in results)
                            {
                                var winningEntries = _entries.Where(lotteryEntry => lotteryEntry.betNumber == res.Result
                                                                         && lotteryEntry.slotTime == res.DrawTime)
                                                             .ToList();

                                foreach (var en in winningEntries)
                                {
                                    // Check if the user or user's name is not null before adding
                                    if (en.User != null && !string.IsNullOrEmpty(en.User.name))
                                    {
                                        winners.Add(en.User.name);
                                    }
                                }
                            }
                            }

                            ResultForm resultForm = new ResultForm();
                            resultForm.UpdateResults(results, winners);
                            resultForm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("No results available for today.");
                        }

                    }
                    else
                    {
                        // Handle failure - update UI or notify the user
                        MessageBox.Show("Failed to place the bet. Please try again.");
                    }
                }

                InitializeUpcomingSlot();
            }
            else
            {
                MessageBox.Show("Please enter a valid number between 0 and 9.");
            }
        }
    }
}
