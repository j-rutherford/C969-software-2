using System;
using System.Windows.Forms;
using C969App.Data;

namespace C969App.Forms
{
    public partial class LoginForm : Form
    {
        private readonly UserRepository _userRepository;
        private readonly Func<Form> _mainFormFactory;

        public LoginForm(UserRepository userRepository, Func<Form> mainFormFactory)
        {
            InitializeComponent();
            _userRepository = userRepository;
            _mainFormFactory = mainFormFactory;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool isAuthenticated = _userRepository.AuthenticateUser(txtUsername.Text, txtPassword.Text);

            if (isAuthenticated)
            {
                MessageBox.Show("Login successful!");
                this.Hide(); // Hide the login form

                var mainForm = _mainFormFactory(); // Create the MainForm instance
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
