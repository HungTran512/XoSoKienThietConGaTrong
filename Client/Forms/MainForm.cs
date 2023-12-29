using System;
using System.Windows.Forms;
using Client.Models;
using System.Drawing;


namespace Client.Forms
{
    public partial class MainForm : Form
    {

        private readonly ApiService _apiService;
        private User _retrievedUser;

        // UI Controls
        private Label lblPhone;
        private TextBox txtPhone;
        private Button btnRetrieveUser;
        private Label lblWelcome;
        private Button btnNewAccount;
        public MainForm()
        {
            InitializeComponent();
            _apiService = new ApiService("http://localhost:3000");
            CheckFirstTimeUser();
   
        }
  
        private void SetupUI()
        {
          
            lblWelcome = new Label
            {
                Left = 10,
                Top = 40, 
                Width = 500,

            };
            this.Controls.Add(lblWelcome);

            lblPhone = new Label
            {
                Text = "Phone Number:",
                Left = 10,
                Top = 10
            };
            this.Controls.Add(lblPhone);

            txtPhone = new TextBox
            {
                Left = 120,
                Top = 10,
                Width = 150
            };
            this.Controls.Add(txtPhone);

            btnRetrieveUser = new Button
            {
                Text = "Login",
                Left = 280,
                Top = 10
            };
            btnRetrieveUser.Click += new EventHandler(this.RetrieveUser_Click);
            this.Controls.Add(btnRetrieveUser);

            btnNewAccount = new Button
            {
                Text = "New Account",
                Left = 370,
                Top = 10,
                Size = new Size(100, 23) 
            };
            btnNewAccount.Click += new EventHandler(this.btnNewAccount_Click);
            this.Controls.Add(btnNewAccount);
        }
        private void btnNewAccount_Click(object sender, EventArgs e)
        {
            // Clear the last used phone number
            Properties.Settings.Default.LastUsedPhoneNumber = string.Empty;
            Properties.Settings.Default.Save();
            ShowRegistrationForm();
        }
        private void CheckFirstTimeUser()
        {
            string lastUsedPhone = Properties.Settings.Default.LastUsedPhoneNumber;

            if (string.IsNullOrEmpty(lastUsedPhone))
            {
                ShowRegistrationForm();
            }
            else
            {
                
                SetupUI();
            }
        }

        private async void RetrieveUser_Click(object sender, EventArgs e)
        {
            string enteredPhone = txtPhone.Text;

            try
            {
                _retrievedUser = await _apiService.GetUserByPhoneNumberAsync(enteredPhone);

                if (_retrievedUser != null)
                {
                    SaveUserData(_retrievedUser);
                    MessageBox.Show($"Welcome {_retrievedUser.name}. Please continue to place your bet");
                    ShowEntryForm();
                }
                else
                {
                    MessageBox.Show("Phone number not found. Please try again or register.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPhone.Clear();
                    txtPhone.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving user data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowEntryForm()
        {   
            if (_retrievedUser != null)
            {
                EntryForm entryForm = new EntryForm(_apiService, _retrievedUser);
                entryForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("User information is not available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowRegistrationForm()
        {
            RegistrationForm registrationForm = new RegistrationForm(_apiService);
            var result = registrationForm.ShowDialog();

            if (result == DialogResult.OK && registrationForm.RegistrationSuccessful)
            {
                _retrievedUser = registrationForm.RegisteredUser;
                ShowEntryForm();
            }
     
        }
        private void SaveUserData(User user)
        {
            Properties.Settings.Default.UserName = user.name;
            Properties.Settings.Default.UserID = user._id;

            Properties.Settings.Default.Save();
        }
    }
}


