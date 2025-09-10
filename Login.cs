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
using static PackingApplication.POYPackingForm;
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
        public Login()
        {
            InitializeComponent();
            getYearList();

            SetButtonBorderRadius(this.signin, 8);

            YearList.SelectedIndexChanged += YearList_SelectedIndexChanged;
            email.TextChanged += Email_TextChanged;
            passwrd.TextChanged += Passwrd_TextChanged;
        }

        private static Logger Log = Logger.GetLogger();
        public string GetCallApi(string WebApiurl)
        {
            var request = (HttpWebRequest)WebRequest.Create(WebApiurl);

            request.Method = "GET";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //request.Headers.Add("Authorization", "Bearer " + SessionManager.AuthToken);
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
            var getYearResponse = GetCallApi(masterURL + "FinancialYear/GetAll?IsDropDown=" + false);
            var getYear = JsonConvert.DeserializeObject<List<FinancialYearResponse>>(getYearResponse);
            //getYear.Insert(0, new FinancialYearResponse { FinYearId = 0, FinYear = "Select Year" });
            YearList.DataSource = getYear;
            YearList.DisplayMember = "FinYear";
            YearList.ValueMember = "FinYearId";
            var currentYear = getYear.Where(x => x.FinYear == DateTime.Now.Year.ToString()).ToList();
            YearList.SelectedValue = currentYear[0].FinYearId;
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

                            AdminAccount dashboard = new AdminAccount();
                            dashboard.Show();

                            this.Hide();
                        }
                        else
                        {
                            string errorMessage = response.Content.ReadAsStringAsync().Result;

                            var userResponse = JsonConvert.DeserializeObject<ErrorResponse>(errorMessage);

                            MessageBox.Show(userResponse.message.ToString());
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

        private void SetButtonBorderRadius(System.Windows.Forms.Button button, int radius)
        {
            Log.writeMessage("SetButtonBorderRadius start");
            try
            {
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.BorderColor = Color.FromArgb(0, 92, 232); // Set to the background color of your form or panel
                button.FlatAppearance.MouseOverBackColor = button.BackColor; // To prevent color change on mouseover
                button.BackColor = Color.FromArgb(0, 92, 232);

                // Set the border radius
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                int diameter = radius * 2;
                path.AddArc(0, 0, diameter, diameter, 180, 95); // Top-left corner
                path.AddArc(button.Width - diameter, 0, diameter, diameter, 270, 95); // Top-right corner
                path.AddArc(button.Width - diameter, button.Height - diameter, diameter, diameter, 0, 95); // Bottom-right corner
                path.AddArc(0, button.Height - diameter, diameter, diameter, 90, 95); // Bottom-left corner
                path.CloseFigure();

                button.Region = new Region(path);
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"An error occurred: {ex.Message}");
                Log.writeMessage($"An error occurred: {ex.Message}");
            }
            Log.writeMessage("SetButtonBorderRadius end");
        }

        private bool ValidateForm()
        {
            bool isValid = true;

            if (YearList.SelectedIndex <= 0)
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

    }
}
