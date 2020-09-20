using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Spreadsheet;
using MySql.Data.MySqlClient;
using SpreadsheetLight;

namespace registro_visitantes
{
    public partial class frmHome : Form
    {
        frmRegister wRegistrarse;
        bool newPersona = false;
        public frmHome()
        {
            InitializeComponent();
            customizeDesign();
            lblNombreUser.Text = Session.usuario;
            lblTipoUsuario.Text = Session.rol;
            txtDni.Focus();
            
        }
        
        public void customizeDesign()
        {
            btnGenerar.Visible = false;
            btnCrearUsuario.Visible = false;
            if (Session.rol == "Admin")
            {
                btnGenerar.Visible=true;
                btnCrearUsuario.Visible = true;
            }
        }

        
        private Login mainScreen;

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                string dni = txtDni.Text;
                DateTime fecha = DateTime.Today;
                DateTime hora = DateTime.Now;
                txtFecha.Text = fecha.ToShortDateString();
                txtHoraIngreso.Text = hora.ToShortTimeString();
                Modelo modelo = new Modelo();
                Persona nomPersona = null;
                nomPersona = modelo.getNombresPersona(dni);
                if (nomPersona == null)
                {
                    MessageBox.Show("Usuario no encontrado");
                    newPersona = true;
                }
                else
                {
                    txtNombre.Text = nomPersona.NombreP;
                    txtApellidoPa.Text = nomPersona.Apellido_pat;
                    txtApellidoMa.Text = nomPersona.Apellido_mat;
                    newPersona = false;
                    //txtApellido.Focus();
                }
                
            }
            
        }

        private void bRegistrarEntrada_Click(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Today;
            DateTime hora = DateTime.Now;
            Visitantes persona = new Visitantes();

            if (txtHoraIngreso.Text == "" || txtFecha.Text =="")
            {
                txtFecha.Text = fecha.ToShortDateString();
                txtHoraIngreso.Text = hora.ToShortTimeString();
            }
            persona.HoraIng = txtHoraIngreso.Text;
            persona.Dni = txtDni.Text;
            persona.Nombre = txtNombre.Text;
            persona.ApellidoPat = txtApellidoPa.Text;
            persona.ApellidoMat = txtApellidoMa.Text;
            persona.EmpresaVisitada = cmbEmpresas.Text;
            persona.EmpresaVisitante = cmbEmVisitante.Text;
            persona.MotivoVisita = txtMotivo.Text;
            persona.PersonaVisita = txtPersona.Text;
            persona.Piso = cmbPiso.Text;
            persona.Fecha = txtFecha.Text;

            if(txtOficina.Text != "")
            {
                persona.Oficina = int.Parse(txtOficina.Text);
            }
            else
            {
                persona.Oficina = 0;
            }

            if (newPersona == true)
            {
                Persona nueva = new Persona();
                nueva.DniP = txtDni.Text;
                nueva.Apellido_pat = txtApellidoPa.Text;
                nueva.Apellido_mat = txtApellidoMa.Text;
                nueva.NombreP = txtNombre.Text;

                Modelo nmodelo = new Modelo();
                nmodelo.guardarNuevaPersona(nueva);               
            }
            try
            {
                Controlador controlador = new Controlador();
                string respuesta = controlador.ctrlRegistrarVisita(persona);
                if (respuesta.Length > 0)
                {
                    MessageBox.Show(respuesta);
                }
                else
                {
                    MessageBox.Show("Registrado correctamente");
                    controlador.BorrarCampos(this);
                    txtDni.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            SLDocument sl = new SLDocument();
            int celdaTitle = 4;
            SLStyle estiloHeader = sl.CreateStyle();
            estiloHeader.Font.Bold = true;
            estiloHeader.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Aquamarine, System.Drawing.Color.Aquamarine);

            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Registro visitas");
            sl.SetCellValue("A"+celdaTitle,"HORA DE INGRESO");
            sl.SetCellValue("B" + celdaTitle, "DNI");
            sl.SetCellValue("C"+celdaTitle,"NOMBRE");
            sl.SetCellValue("D"+celdaTitle,"APELLIDO PATERNO");
            sl.SetCellValue("E"+celdaTitle,"APELLIDO MATERNO");
            sl.SetCellValue("F"+celdaTitle,"EMPRESA QUE VISITA");
            sl.SetCellValue("G"+celdaTitle,"EMPRESA DEL VISITANTE");
            sl.SetCellValue("H"+celdaTitle,"MOTIVO DE VISITA");
            sl.SetCellValue("I"+celdaTitle,"PERSONA QUE VISITA");
            sl.SetCellValue("J"+celdaTitle,"PISO");
            sl.SetCellValue("K"+celdaTitle,"OFICINA");
            sl.SetCellValue("L"+celdaTitle,"FECHA");
            sl.SetCellValue("M"+celdaTitle,"HORA DE SALIDA");

            sl.SetCellStyle("A" + celdaTitle, "M" + celdaTitle, estiloHeader);
            sl.AutoFitColumn("A", "M");
            Modelo modelo = new Modelo();
            sl = modelo.generarRegistro(celdaTitle, sl);
            sl.SaveAs("Registo_visitas.xlsx");
            Process process = Process.Start("Registo_visitas.xlsx");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            mainScreen = new Login();
            mainScreen.Show();
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            
        }

        private void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            wRegistrarse = new frmRegister();
            wRegistrarse.Show();
        }

        private void cmbToUpper(object sender, KeyPressEventArgs e)
        {
            if (char.IsLower(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }

        private void bRegistroSalida_Click(object sender, EventArgs e)
        {
            DateTime fechaActual = DateTime.Today;
            
            if (txtFecha.Text == "")
            {
                txtFecha.Text = fechaActual.ToShortDateString();
            }

            Visitantes persona = new Visitantes();
            persona.Dni = txtDni.Text;
            persona.Nombre = txtNombre.Text;
            persona.ApellidoPat = txtApellidoPa.Text;
            persona.ApellidoMat = txtApellidoMa.Text;
            persona.EmpresaVisitada = cmbEmpresas.Text;
            persona.EmpresaVisitante = cmbEmVisitante.Text;
            persona.MotivoVisita = txtMotivo.Text;
            persona.PersonaVisita = txtPersona.Text;
            persona.Piso = cmbPiso.Text;
            persona.Fecha = txtFecha.Text;
            persona.HoraSalida = txtHoraSalida.Text;

            Controlador ctr = new Controlador();
            string resp = ctr.registrarSalida(persona);
            if (resp.Length > 0)
            {
                MessageBox.Show(resp);
            }
            else
            {
                DateTime horaSalida = DateTime.Now;
                txtHoraSalida.Text = horaSalida.ToShortTimeString();
                MessageBox.Show("Salida registrada");
                ctr.BorrarCampos(this);
                txtDni.Focus();
            }
        }
    }
}
