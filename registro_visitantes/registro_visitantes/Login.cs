using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace registro_visitantes
{
    public partial class Login : Form
    {
        frmHome login;
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUser.Text;
            string password = txtPassword.Text;
            try
            {
                Controlador ctrl = new Controlador();
                string res = ctrl.ctrlLogin(usuario, password);
                if(res.Length > 0)
                {
                    MessageBox.Show(res);
                }
                else
                {
                    login = new frmHome();
                    login.Show();
                    this.Hide();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void picturebtnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
        }

        private void ReleaseCapture()
        {
            throw new NotImplementedException();
        }
    }
}
