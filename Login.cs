using Newtonsoft.Json;
using PackingApplication.Helper;
using PackingApplication.Models.CommonEntities;
using PackingApplication.Models.RequestEntities;
using PackingApplication.Models.ResponseEntities;
using PackingApplication.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackingApplication
{
    public partial class Login: Form
    {
        string userURL = ConfigurationManager.AppSettings["userURL"];
        string masterURL = ConfigurationManager.AppSettings["masterURL"];
        private bool isPasswordVisible = false;
        private int finYearId = 0;
        CommonMethod _cmethod = new CommonMethod();
        private static Logger Log = Logger.GetLogger();
        public Login()
        {
            Log.writeMessage("Login - Start : " + DateTime.Now);

            InitializeComponent();
            getYearList();
            ApplyFonts();

            _cmethod.SetButtonBorderRadius(this.signin, 8);

            YearList.SelectedIndexChanged += YearList_SelectedIndexChanged;
            email.TextChanged += Email_TextChanged;
            passwrd.TextChanged += Passwrd_TextChanged;

            email.Text = "kirti.shinde@cyberscriptit.com";
            passwrd.Text = "Kirti@123";
            //email.Text = "satish@beekaylon.com";
            //passwrd.Text = "Test@123";

            Log.writeMessage("Login - End : " + DateTime.Now);
        }

        private void ApplyFonts()
        {
            Log.writeMessage("Login ApplyFonts - Start : " + DateTime.Now);

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

            Log.writeMessage("Login ApplyFonts - End : " + DateTime.Now);
        }

        public string GetCallApi(string WebApiurl)
        {
            Log.writeMessage("Login GetCallApi - Start : " + DateTime.Now);

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
            Log.writeMessage("Login GetCallApi - End : " + DateTime.Now);

            return content;
        }

        private void getYearList()
        {
            Log.writeMessage("Login getYearList - Start : " + DateTime.Now);

            DateTime today = DateTime.Today;
            var getYearResponse = GetCallApi(masterURL + "FinancialYear/GetAll?IsDropDown=" + true);
            var getYear = JsonConvert.DeserializeObject<List<FinancialYearResponse>>(getYearResponse);
            YearList.DataSource = getYear;
            YearList.DisplayMember = "FinYear";
            YearList.ValueMember = "FinYearId";
            var currentYear = getYear.FirstOrDefault(x => today >= x.StartDate.Date && today <= x.EndDate.Date);
            if (currentYear != null)
            {
                YearList.SelectedValue = currentYear.FinYearId;
                finYearId = currentYear.FinYearId;
                label1.Text = "ALL RIGHT RESERVED © " + currentYear.FinYear.ToString();
            }

            Log.writeMessage("Login getYearList - End : " + DateTime.Now);
        }

        private void signin_Click(object sender, EventArgs e)
        {
            Log.writeMessage("Login signin_Click - Start : " + DateTime.Now);

            SignIn();

            Log.writeMessage("Login signin_Click - End : " + DateTime.Now);
        }

        private void SignIn()
        {
            Log.writeMessage("Login SignIn - Start : " + DateTime.Now);

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

                            MessageBox.Show(userResponse.message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    Log.writeMessage("CheckLogin stop");
                }
                catch (Exception ex)
                {
                    Log.writeMessage("An error occurred: {ex.Message}");
                }
            }

            Log.writeMessage("Login SignIn - End : " + DateTime.Now);
        }

        private bool IsValidEmail(string email)
        {
            Log.writeMessage("Login IsValidEmail - Start : " + DateTime.Now);

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
            Log.writeMessage("Login ValidateForm - Start : " + DateTime.Now);

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

            Log.writeMessage("Login ValidateForm - End : " + DateTime.Now);

            return isValid;
        }

        private void YearList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.writeMessage("Login YearList_SelectedIndexChanged - Start : " + DateTime.Now);

            if (YearList.SelectedIndex > 0)
            {
                yearerror.Text = "";
                yearerror.Visible = false;
            }

            Log.writeMessage("Login YearList_SelectedIndexChanged - End : " + DateTime.Now);
        }

        // Hide Email error when user types valid email
        private void Email_TextChanged(object sender, EventArgs e)
        {
            Log.writeMessage("Login Email_TextChanged - Start : " + DateTime.Now);

            if (!string.IsNullOrWhiteSpace(email.Text) && IsValidEmail(email.Text))
            {
                emailerror.Text = "";
                emailerror.Visible = false;
            }

            Log.writeMessage("Login Email_TextChanged - End : " + DateTime.Now);
        }

        // Hide Password error when user types something
        private void Passwrd_TextChanged(object sender, EventArgs e)
        {
            Log.writeMessage("Login Passwrd_TextChanged - Start : " + DateTime.Now);

            if (!string.IsNullOrWhiteSpace(passwrd.Text))
            {
                passworderror.Text = "";
                passworderror.Visible = false;
            }

            Log.writeMessage("Login Passwrd_TextChanged - End : " + DateTime.Now);
        }

        private void eyeIcon_Click(object sender, EventArgs e)
        {
            Log.writeMessage("Login eyeIcon_Click - Start : " + DateTime.Now);

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

            Log.writeMessage("Login eyeIcon_Click - End : " + DateTime.Now);
        }

        private void Control_EnterKeyMoveNext(object sender, KeyEventArgs e)
        {
            Log.writeMessage("Login Control_EnterKeyMoveNext - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;           // Mark as handled
                e.SuppressKeyPress = true;

                SignIn();
                //Control current = (Control)sender;

                //if (current == rememberme)
                //{
                //    signin.Focus();
                //}
                //else
                //{
                //    this.SelectNextControl(current, true, true, true, true);
                //}
                //if (this.ActiveControl is CheckBox cb)
                //{
                //    cb.Invalidate(); // triggers paint to show focus border
                //}
                //if (this.ActiveControl is Button btn)
                //{
                //    // This makes Windows draw the dotted focus rectangle
                //    btn.Focus();
                //    btn.FlatStyle = FlatStyle.Standard; // ensures focus rectangle is visible
                //}
            }

            Log.writeMessage("Login Control_EnterKeyMoveNext - End : " + DateTime.Now);
        }

        private void CheckBox_DrawFocusBorder(object sender, PaintEventArgs e)
        {
            Log.writeMessage("Login CheckBox_DrawFocusBorder - Start : " + DateTime.Now);

            CheckBox cb = (CheckBox)sender;

            if (cb.Focused)
            {
                _cmethod.DrawRoundedDashedBorder((CheckBox)sender, e, 1, Color.Black, 1);
            }

            Log.writeMessage("Login CheckBox_DrawFocusBorder - End : " + DateTime.Now);
        }

        private void YearList_KeyDown(object sender, KeyEventArgs e)
        {
            Log.writeMessage("Login YearList_KeyDown - Start : " + DateTime.Now);

            if (e.KeyCode == Keys.ShiftKey) // Detect Shift key
            {
                YearList.DroppedDown = true; // Open the dropdown list
                e.SuppressKeyPress = true;    // Prevent any side effect
            }
            if (e.KeyCode == Keys.Escape)
            {
                YearList.DroppedDown = false;
            }

            Log.writeMessage("Login YearList_KeyDown - End : " + DateTime.Now);
        }
    }
}
