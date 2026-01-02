using Newtonsoft.Json;
using PackingApplication.Models.RequestEntities;
using PackingApplication.Models.ResponseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PackingApplication.Helper;
using PackingApplication.Models.CommonEntities;
using System.Configuration;

namespace PackingApplication
{
    public partial class Login: Form
    {
        string userURL = ConfigurationManager.AppSettings["userURL"];
        string masterURL = ConfigurationManager.AppSettings["masterURL"];
        private bool isPasswordVisible = false;
        private int finYearId = 0;
        CommonMethod _cmethod = new CommonMethod();
        public Login()
        {
            InitializeComponent();
            getYearList();
            ApplyFonts();

            _cmethod.SetButtonBorderRadius(this.signin, 8);

            YearList.SelectedIndexChanged += YearList_SelectedIndexChanged;
            email.TextChanged += Email_TextChanged;
            passwrd.TextChanged += Passwrd_TextChanged;

            email.Text = "kirti.shinde@cyberscriptit.com";
            passwrd.Text = "Kirti@123";
            //email.Text = "sanket.bankar@cyberscriptit.com";
            //passwrd.Text = "Sanket@123";
        }

        private void ApplyFonts()
        {
            this.emailid.Font = FontManager.GetFont(9F, FontStyle.Regular);
            this.email.Font = FontManager.GetFont(9F, FontStyle.Regular);
            this.password.Font = FontManager.GetFont(9F, FontStyle.Regular);
            this.passwrd.Font = FontManager.GetFont(9F, FontStyle.Regular);
            this.year.Font = FontManager.GetFont(9F, FontStyle.Regular);
            this.YearList.Font = FontManager.GetFont(9F, FontStyle.Regular);
            this.rememberme.Font = FontManager.GetFont(9F, FontStyle.Regular);
            this.signin.Font = FontManager.GetFont(10F, FontStyle.Bold);
            this.welcome.Font = FontManager.GetFont(14F, FontStyle.Bold);
            this.subtitle.Font = FontManager.GetFont(10F, FontStyle.Regular);
            this.subtitle1.Font = FontManager.GetFont(10F, FontStyle.Regular);
            this.req1.Font = FontManager.GetFont(10F, FontStyle.Bold);
            this.req2.Font = FontManager.GetFont(10F, FontStyle.Bold);
            this.req3.Font = FontManager.GetFont(10F, FontStyle.Bold);
            this.label2.Font = FontManager.GetFont(10F, FontStyle.Bold);
            this.label1.Font = FontManager.GetFont(10F, FontStyle.Bold);
            this.yearerror.Font = FontManager.GetFont(9F, FontStyle.Regular);
            this.passworderror.Font = FontManager.GetFont(9F, FontStyle.Regular);
            this.emailerror.Font = FontManager.GetFont(9F, FontStyle.Regular);
        }

        private static Logger Log = Logger.GetLogger();
        public string GetCallApi(string WebApiurl)
        {
            var request = (HttpWebRequest)WebRequest.Create(WebApiurl);

            request.Method = "GET";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var content = string.Empty;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }

            return content;
        }

        private void getYearList()
        {
            var getYearResponse = GetCallApi(masterURL + "FinancialYear/GetAll?IsDropDown=" + true);
            var getYear = JsonConvert.DeserializeObject<List<FinancialYearResponse>>(getYearResponse);
            YearList.DataSource = getYear;
            YearList.DisplayMember = "FinYear";
            YearList.ValueMember = "FinYearId";
            var currentYear = getYear.FirstOrDefault(x => x.FinYear == DateTime.Now.Year.ToString());
            if (currentYear != null)
            {
                YearList.SelectedValue = currentYear.FinYearId;
                finYearId = currentYear.FinYearId;
                label1.Text = "ALL RIGHT RESERVED © " + currentYear.FinYear.ToString();
            }
        }

        private void signin_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                try
                {                 
                    Log.writeMessage("CheckLogin start");
                    LoginRequest login = new LoginRequest();
                    login.Email = email.Text;
                    login.PasswordHash = passwrd.Text;
                    login.IsRemember = rememberme.Checked;

                    var path = userURL + "OTP/CheckLogin";
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri(path);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var loginResponse = "";
                    using (var content = new StringContent(JsonConvert.SerializeObject(login), System.Text.Encoding.UTF8, "application/json"))
                    {
                        var response = client.PostAsync(path, content).Result;
                        //use await it has moved in some context on .core 6.0
                        if (response.IsSuccessStatusCode == true)
                        {
                            loginResponse = response.Content.ReadAsStringAsync().Result;

                            var userResponse = JsonConvert.DeserializeObject<UserResponse>(loginResponse);

                            SessionManager.AuthToken = userResponse.AccessToken;
                            SessionManager.UserName = userResponse.FullName;
                            SessionManager.Role = userResponse.Role;
                            SessionManager.FinYearId = finYearId;

                            AdminAccount dashboard = new AdminAccount();
                            dashboard.Show();

                            this.Hide();
                        }
                        else
                        {
                            string errorMessage = response.Content.ReadAsStringAsync().Result;

                            var userResponse = JsonConvert.DeserializeObject<ErrorResponse>(errorMessage);

                            MessageBox.Show(userResponse.message.ToString(),"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    Log.writeMessage("CheckLogin stop");
                }
                catch (Exception ex)
                {
                    Log.writeMessage("An error occurred: {ex.Message}");
                }
            }
            
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool ValidateForm()
        {
            bool isValid = true;

            if (YearList.SelectedValue == null || Convert.ToInt32(YearList.SelectedValue) <= 0)
            {
                yearerror.Text = "Please select valid year.";
                yearerror.Visible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(email.Text) || !IsValidEmail(email.Text))
            {
                emailerror.Text = "Please enter valid email id.";
                emailerror.Visible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(passwrd.Text))
            {
                passworderror.Text = "Please enter valid password.";
                passworderror.Visible = true;
                isValid = false;
            }

            return isValid;
        }

        private void YearList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (YearList.SelectedIndex > 0)
            {
                yearerror.Text = "";
                yearerror.Visible = false;
            }
        }

        // Hide Email error when user types valid email
        private void Email_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(email.Text) && IsValidEmail(email.Text))
            {
                emailerror.Text = "";
                emailerror.Visible = false;
            }
        }

        // Hide Password error when user types something
        private void Passwrd_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(passwrd.Text))
            {
                passworderror.Text = "";
                passworderror.Visible = false;
            }
        }

        private void eyeIcon_Click(object sender, EventArgs e)
        {
            if (isPasswordVisible)
            {
                this.passwrd.UseSystemPasswordChar = true;
                this.eyeicon.Image = Properties.Resources.icons8_hide_24;  // set closed-eye icon
                isPasswordVisible = false;
            }
            else
            {
                this.passwrd.UseSystemPasswordChar = false;
                this.eyeicon.Image = Properties.Resources.icons8_eye_24;    // set open-eye icon
                isPasswordVisible = true;
            }
        }

        private void Control_EnterKeyMoveNext(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;           // Mark as handled
                e.SuppressKeyPress = true;

                Control current = (Control)sender;

                if (current == rememberme)
                {
                    signin.Focus();
                }
                else
                {
                    this.SelectNextControl(current, true, true, true, true);
                }
                if (this.ActiveControl is CheckBox cb)
                {
                    cb.Invalidate(); // triggers paint to show focus border
                }
                if (this.ActiveControl is Button btn)
                {
                    // This makes Windows draw the dotted focus rectangle
                    btn.Focus();
                    btn.FlatStyle = FlatStyle.Standard; // ensures focus rectangle is visible
                }
            }
        }

        private void CheckBox_DrawFocusBorder(object sender, PaintEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            if (cb.Focused)
            {
                _cmethod.DrawRoundedDashedBorder((CheckBox)sender, e, 1, Color.Black, 1);
            }
        }

    }
}
