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
        public Login()
        {
            InitializeComponent();
            getYearList();
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
            try
            {
                if (string.IsNullOrWhiteSpace(email.Text))
                {
                    email.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(passwrd.Text))
                {
                    passwrd.Focus();
                    return;
                }
                if (!IsValidEmail(email.Text))
                {
                    email.Focus();
                    return;
                }
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

                        POYPackingForm dashboard = new POYPackingForm();
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
    }
}
