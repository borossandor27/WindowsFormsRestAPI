using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WindowsFormsRestAPI
{
    public partial class FormLogin : Form
    {
        //-- NuGet.org-ról a Json.Net és Newtonsoft.Json csomagot telepíteni kell ehhez a megoldáshoz -----------
        //-- A RESTFul-hoz a Microsoft.AspNet.WebApi.Client csomagot 
        public FormLogin()
        {
            InitializeComponent();
        }
        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
        private void button_Submit_Click(object sender, EventArgs e)
        {
            //-- Küldi a {username: ..., password: ....} JSON-t a http://127.0.0.1:3000/login url-re
            if (String.IsNullOrEmpty(textBox_UserName.Text))
            {
                MessageBox.Show("Nincs felhasználónév megadva!");
            }
            if (String.IsNullOrEmpty(textBox_Password.Text))
            {
                MessageBox.Show("Nincs jelszó megadva!");
            }
            //User user = new User(textBox_UserName.Text, textBox_Password.Text);

            //-- new { username = textBox_UserName.Text, password = textBox_Password.Text }

            //-- Tesztelni: https://jsonlint.com/

            _ = RunAsync();
        }
                    HttpClient client = new HttpClient();

        async Task RunAsync()
        {
            object user = new { 
                username = "admin", 
                password = 123 
            };
            string loginJSON = JsonConvert.SerializeObject(user);
            MessageBox.Show(loginJSON);
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:3000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var response = await LoginUserAsync(loginJSON);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private async Task<int> LoginUserAsync(object user)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("http://localhost:3000/login", user);
            response.EnsureSuccessStatusCode(); //-- 200-299 igaz, egyébként exception

            return 200;
        }
    }
}
