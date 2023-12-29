using Client.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Client.Forms
{
    public partial class RegistrationForm : Form
    {
        public bool RegistrationSuccessful { get; private set; }
        private Label lblName;
        private TextBox nameTextBox;
        private Label lblDob;
        private DateTimePicker dobDateTimePicker;
        private Label lblPhone;
        private TextBox phoneTextBox;
        private Button registerButton;

        private readonly ApiService _apiService;
        public User RegisteredUser { get; private set; }
        public RegistrationForm(ApiService apiService)
        {
            InitializeComponent();
            SetUpUI();
            _apiService = apiService;
        }
        private void SetUpUI()
        {
            this.lblName = new Label();
            this.nameTextBox = new TextBox();
            this.lblDob = new Label();
            this.dobDateTimePicker = new DateTimePicker();
            this.lblPhone = new Label();
            this.phoneTextBox = new TextBox();
            this.registerButton = new Button();

            this.lblName.AutoSize = true;
            this.lblName.Location = new Point(20, 20);
            this.lblName.Text = "Name:";


            this.nameTextBox.Location = new Point(100, 20);
            this.nameTextBox.Size = new Size(200, 20);


            this.lblDob.AutoSize = true;
            this.lblDob.Location = new Point(20, 60);
            this.lblDob.Text = "Date of Birth:";


            this.dobDateTimePicker.Location = new Point(100, 60);
            this.dobDateTimePicker.Size = new Size(200, 20);

            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new Point(20, 100);
            this.lblPhone.Text = "Phone:";


            this.phoneTextBox.Location = new Point(100, 100);
            this.phoneTextBox.Size = new Size(200, 20);


            this.registerButton.Text = "Register";
            this.registerButton.Location = new Point(100, 140);
            this.registerButton.Click += new EventHandler(this.registerButton_Click);


            this.Controls.Add(this.lblName);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.lblDob);
            this.Controls.Add(this.dobDateTimePicker);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.phoneTextBox);
            this.Controls.Add(this.registerButton);

            this.Size = new Size(350, 220);
            this.Text = "Registration";
        }

        private async void registerButton_Click(object sender, EventArgs e)
        {
            User newUser = new User
            {
                name = nameTextBox.Text,
                DateOfBirth = dobDateTimePicker.Value,
                phoneNumber = phoneTextBox.Text
            };

            try
            {
                User registeredUser = await _apiService.RegisterUserAsync(newUser);
                MessageBox.Show($"User Registered: {registeredUser.name}");
              
                SaveUserData(registeredUser);
                this.RegisteredUser = registeredUser;
                this.DialogResult = DialogResult.OK;
                RegistrationSuccessful = true;
                this.Close();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Registration failed: {ex.Message}");
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void SaveUserData(User user)
        {
            Properties.Settings.Default.UserName = user.name;
            Properties.Settings.Default.UserID = user._id;
            Properties.Settings.Default.LastUsedPhoneNumber = user.phoneNumber;
            Properties.Settings.Default.Save();
        }
    }


}
