using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

        private void button_Submit_Click(object sender, EventArgs e)
        {
            //-- Küldi a {username: ..., password: ....} JSON-t a http://127.0.0.1/login url-re
            if (String.IsNullOrEmpty(textBox_UserName.Text))
            {
                MessageBox.Show("Nincs felhasználónév megadva!");
            }
            if (String.IsNullOrEmpty(textBox_Password.Text))
            {
                MessageBox.Show("Nincs jelszó megadva!");
            }
            //User user = new User(textBox_UserName.Text, textBox_Password.Text);
            string loginJSON = JsonConvert.SerializeObject(new { username = textBox_UserName.Text, password = textBox_Password.Text });
            //-- Tesztelni: https://jsonlint.com/



        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
