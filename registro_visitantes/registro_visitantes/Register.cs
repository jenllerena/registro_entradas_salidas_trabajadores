using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace registro_visitantes
{
    public partial class frmRegister : Form
    {
        string rolUser;
        public frmRegister()
        {
            InitializeComponent();
        }
        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cmbRol.SelectedIndex;
            rolUser = cmbRol.Items[index].ToString();
        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            Usuarios usuario = new Usuarios();
            usuario.Usuario = txtUsuario.Text;
            usuario.Password = txtPassword.Text;
            usuario.ConPassword = txtConPassword.Text;
            usuario.Nombre = txtNombre.Text;
            usuario.Rol = rolUser;

            try
            {
                Controlador controlador = new Controlador();
                string respuesta = controlador.ctrlRegistro(usuario);
                if(respuesta.Length > 0)
                {
                    MessageBox.Show(respuesta);
                }
                else
                {
                    MessageBox.Show("Usuario registrado");
                    this.Close();
                    this.Dispose();
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
