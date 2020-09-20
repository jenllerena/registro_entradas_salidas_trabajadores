using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace registro_visitantes
{
    class Controlador
    {
        public string ctrlRegistro(Usuarios usuario)
        {
            Modelo modelo = new Modelo();
            string respuesta = "";
            if(string.IsNullOrEmpty(usuario.Usuario) || string.IsNullOrEmpty(usuario.Password)
                || string.IsNullOrEmpty(usuario.ConPassword) || string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrEmpty(usuario.Rol))
            {
                respuesta = "Debe llenar todos los campos";
            }
            else
            {
                if(usuario.Password == usuario.ConPassword)
                {
                    if (modelo.existeUsuario(usuario.Usuario))
                    {
                        respuesta = "El usuario ya existe";
                    }
                    else
                    {
                        //usuario.Password = generarSHA1(usuario.Password);
                        modelo.registro(usuario);
                    }
                }
                else
                {
                    respuesta = "Contraseñas no coinciden";
                }
            }
            return respuesta;
        }

        private string generarSHA1(string cadena)
        {
            UTF8Encoding enc = new UTF8Encoding();
            byte[] data = enc.GetBytes(cadena);
            byte[] result;
            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            result = sha.ComputeHash(data);
            StringBuilder sb = new StringBuilder();
            for(int i = 0;i < result.Length; i++)
            {
                if (result[i] < 16)
                {
                    sb.Append("0");
                }
                sb.Append(result[i].ToString("x"));
            }
            return sb.ToString();
        }

        public string ctrlLogin(string usuario,string password)
        {
            Modelo modelo = new Modelo();
            string res = "";
            Usuarios datosUser = null;

            if(string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                res = "Debe llenar todos los campos.";
            }
            else
            {
                datosUser = modelo.eachUsuario(usuario);
                if(datosUser == null)
                {
                    res = "El usuario no existe";
                }
                else
                {
                    if(datosUser.Password != password)
                    {
                        res = "El usuario y/o contraseña no coinciden";
                    }
                    else
                    {
                        Session.id = datosUser.Id;
                        Session.usuario = usuario;
                        Session.rol = datosUser.Rol;
                    }
                }
            }
            return res;
        }

        public string ctrlRegistrarVisita(Visitantes visita)
        {
            Modelo modelo = new Modelo();
            string respuesta = "";
            if (string.IsNullOrEmpty(visita.Dni)  || string.IsNullOrEmpty(visita.Nombre)|| string.IsNullOrEmpty(visita.ApellidoPat) || string.IsNullOrEmpty(visita.ApellidoMat) ||
                string.IsNullOrEmpty(visita.EmpresaVisitada) || string.IsNullOrEmpty(visita.EmpresaVisitante) || string.IsNullOrEmpty(visita.Piso) || (visita.Oficina == 0))
            {
                respuesta = "Debe llenar todos los campos";
            }
            else
            {
                modelo.registroVisita(visita);
            }
            return respuesta;
        }
        public string registrarSalida(Visitantes registro)
        {
            Modelo mod = new Modelo();
            string res = "";
            Visitantes registroPrevio = null;

            if (string.IsNullOrEmpty(registro.Dni) || string.IsNullOrEmpty(registro.Nombre) || string.IsNullOrEmpty(registro.ApellidoPat) || string.IsNullOrEmpty(registro.ApellidoMat) ||
                string.IsNullOrEmpty(registro.EmpresaVisitada) || string.IsNullOrEmpty(registro.EmpresaVisitante))
            {
                res = "Campos no deben de estar vacios";
            }
            else
            {
                registroPrevio = mod.existeRegistroEntrada(registro.Fecha,registro.Dni);
                if(registroPrevio == null)
                {
                    res = "Persona no se encuentra en el edificio";
                }
                else
                {
                    DateTime horaSalida = DateTime.Now;
                    string hora = horaSalida.ToShortTimeString();
                    mod.actualizarHoraSalida(registroPrevio.IdV, hora);
                }
            }
            return res;
        }
        public void BorrarCampos(Control control)
        {
            foreach (var txt in control.Controls)
            {
                if (txt is TextBox)
                {
                    ((TextBox)txt).Clear();
                }
                else
                {
                    if (txt is ComboBox)
                    {
                        ((ComboBox)txt).Text = string.Empty;
                    }
                }
            }
        }
    }
}
