using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using C969App.Data;

namespace C969App.Forms
{
    public partial class LoginForm : Form
    {
        private readonly UserRepository _userRepository;
        private readonly Func<Form> _mainFormFactory;
        private ResourceManager _resourceManager;

        public LoginForm(UserRepository userRepository, Func<Form> mainFormFactory)
        {
            InitializeComponent();
            _userRepository = userRepository;
            _mainFormFactory = mainFormFactory;

            // Set the culture to the current user's environment language
            SetCulture(CultureInfo.CurrentUICulture);
            //SetCulture(CultureInfo.It)
            // Set the text of the controls using the resource manager
            lblUsername.Text = _resourceManager.GetString("UsernameLabelText");
            lblPassword.Text = _resourceManager.GetString("PasswordLabelText");
            btnLogin.Text = _resourceManager.GetString("LoginButtonText");
        }

        private void SetCulture(CultureInfo culture)
        {
            // Load the appropriate resource file based on the culture
            _resourceManager = new ResourceManager("C969App.Resources.StringsIt", typeof(LoginForm).Assembly);

            // If the culture is not supported, default to English
            if (culture.TwoLetterISOLanguageName != "it")
            {
                culture = new CultureInfo("en");
                _resourceManager = new ResourceManager("C969App.Resources.Strings", typeof(LoginForm).Assembly);
            }

            Thread.CurrentThread.CurrentUICulture = culture;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool isAuthenticated = _userRepository.AuthenticateUser(txtUsername.Text, txtPassword.Text);

            if (isAuthenticated)
            {
                MessageBox.Show(_resourceManager.GetString("LoginSuccess"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide(); // Hide the login form

                var mainForm = _mainFormFactory(); // Create the MainForm instance
                mainForm.Show();
            }
            else
            {
                MessageBox.Show(_resourceManager.GetString("ErrorLoginFailed"), _resourceManager.GetString("LoginFailedTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
