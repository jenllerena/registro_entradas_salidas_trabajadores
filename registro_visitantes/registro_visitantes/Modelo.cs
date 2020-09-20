using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SpreadsheetLight;

namespace registro_visitantes
{
    class Modelo
    {
        public int registro(Usuarios usuario)
        {
            MySqlConnection conexion = Conexion.getConexion();
            conexion.Open();

            string sql = "INSERT INTO usuarios (usuario,password,nombre,rol) VALUES(@usuario,@password,@nombre,@rol)";
            MySqlCommand comando = new MySqlCommand(sql,conexion);
            comando.Parameters.AddWithValue("@usuario", usuario.Usuario);
            comando.Parameters.AddWithValue("@password", usuario.Password);
            comando.Parameters.AddWithValue("@nombre", usuario.Nombre);
            comando.Parameters.AddWithValue("@rol", usuario.Rol);

            int resultado = comando.ExecuteNonQuery();
            return resultado;
        }
        public bool existeUsuario(string usuario)
        {
            MySqlDataReader reader;
            MySqlConnection conexion = Conexion.getConexion();
            conexion.Open();

            string sql = "SELECT id FROM usuarios WHERE usuario LIKE @usuario";
            MySqlCommand comando = new MySqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@usuario", usuario);

            reader = comando.ExecuteReader();
            if (reader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Usuarios eachUsuario(string usuario)
        {
            MySqlDataReader reader;
            MySqlConnection conexion = Conexion.getConexion();
            conexion.Open();

            string sql = "SELECT id, password, nombre, rol FROM usuarios WHERE usuario LIKE @usuario";
            MySqlCommand comando = new MySqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@usuario", usuario);

            reader = comando.ExecuteReader();
            Usuarios usr = null;
            while (reader.Read())
            {
                usr = new Usuarios();
                usr.Id = int.Parse( reader["id"].ToString());
                usr.Password = reader["password"].ToString();
                usr.Nombre = reader["nombre"].ToString();
                usr.Rol = reader["rol"].ToString();
            }
            return usr;
        }
        public Persona getNombresPersona (string dni)
        {
            MySqlDataReader reader;
            MySqlConnection conexion = Conexion.getConexion();
            conexion.Open();

            string sql = "SELECT nombre, ape_pat, ape_mat FROM personas WHERE dni LIKE @dni";
            MySqlCommand comando = new MySqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@dni", dni);
            Persona persona = null;
            reader = comando.ExecuteReader();
            
                while (reader.Read())
                {
                    persona = new Persona();
                    string nombre = reader["nombre"].ToString();
                    string ap_pat = reader["ape_pat"].ToString();
                    string ap_mat = reader["ape_mat"].ToString();
                    persona.NombreP = nombre;
                    persona.Apellido_pat = ap_pat;
                    persona.Apellido_mat = ap_mat;
                }
            
            return persona;
        }
        public int guardarNuevaPersona(Persona persona)
        {
            MySqlConnection conexion = Conexion.getConexion();
            conexion.Open();

            string sql = "INSERT INTO personas (dni, ape_pat, ape_mat, nombre) VALUES (@dni,@ape_pat,@ape_mat,@nombre)";
            MySqlCommand comando = new MySqlCommand(sql, conexion);
            
            comando.Parameters.AddWithValue("@dni", persona.DniP);
            comando.Parameters.AddWithValue("@ape_pat", persona.Apellido_pat);
            comando.Parameters.AddWithValue("@ape_mat", persona.Apellido_mat);
            comando.Parameters.AddWithValue("@nombre", persona.NombreP);
            int resultado = comando.ExecuteNonQuery();
            return resultado;
        }

        public int registroVisita(Visitantes persona)
        {
            MySqlConnection conexion = Conexion.getConexion();
            conexion.Open();

            string sql = "INSERT INTO visitantes (hora_ing,dni, nombres, apellido_pat,apellido_mat, empresaVisitada, " +
                "empresaVisitante, motivoVisita,persona, piso, oficina,fecha) VALUES(@horaIng,@dni,@nombres,@apellido_pat,@apellido_mat,@empresaVisitada," +
                "@empresaVisitante,@motivoVisita,@persona,@piso,@oficina,@fecha)";

            MySqlCommand comando = new MySqlCommand(sql, conexion);

            comando.Parameters.AddWithValue("@horaIng", persona.HoraIng);
            comando.Parameters.AddWithValue("@dni", persona.Dni);
            comando.Parameters.AddWithValue("@nombres", persona.Nombre);
            comando.Parameters.AddWithValue("@apellido_pat", persona.ApellidoPat);
            comando.Parameters.AddWithValue("@apellido_mat", persona.ApellidoMat);
            comando.Parameters.AddWithValue("@empresaVisitada", persona.EmpresaVisitada);
            comando.Parameters.AddWithValue("@empresaVisitante", persona.EmpresaVisitante);
            comando.Parameters.AddWithValue("@motivoVisita", persona.MotivoVisita);
            comando.Parameters.AddWithValue("@persona", persona.PersonaVisita);
            comando.Parameters.AddWithValue("@piso", persona.Piso);
            comando.Parameters.AddWithValue("@oficina", persona.Oficina);
            comando.Parameters.AddWithValue("@fecha", persona.Fecha);

            int resultado = comando.ExecuteNonQuery();
            //conexion.Close();
            return resultado;
        }
        public SLDocument generarRegistro(int celdaTitle, SLDocument sl)
        {
            MySqlConnection conexion = Conexion.getConexion();
            conexion.Open();

            string sql = "SELECT hora_ing,dni,nombres,apellido_pat,apellido_mat,empresaVisitada,empresaVisitante,motivoVisita,persona,piso,oficina,fecha,hora_salida FROM visitantes";

            MySqlCommand comando = new MySqlCommand(sql, conexion);
            MySqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                celdaTitle++;
                sl.SetCellValue("A" + celdaTitle, reader["hora_ing"].ToString());
                sl.SetCellValue("B" + celdaTitle, reader["dni"].ToString());
                sl.SetCellValue("C" + celdaTitle, reader["nombres"].ToString());
                sl.SetCellValue("D" + celdaTitle, reader["apellido_pat"].ToString());
                sl.SetCellValue("E" + celdaTitle, reader["apellido_mat"].ToString());
                sl.SetCellValue("F" + celdaTitle, reader["empresaVisitada"].ToString());
                sl.SetCellValue("G" + celdaTitle, reader["empresaVisitante"].ToString());
                sl.SetCellValue("H" + celdaTitle, reader["motivoVisita"].ToString());
                sl.SetCellValue("I" + celdaTitle, reader["persona"].ToString());
                sl.SetCellValue("J" + celdaTitle, reader["piso"].ToString());
                sl.SetCellValue("K" + celdaTitle, reader["oficina"].ToString());
                sl.SetCellValue("L" + celdaTitle, reader["fecha"].ToString());
                sl.SetCellValue("M" + celdaTitle, reader["hora_salida"].ToString());
            }

            return sl;
        }

        public Visitantes existeRegistroEntrada(string fecha, string dni)
        {
            MySqlDataReader reader;
            MySqlConnection conexion = Conexion.getConexion();
            conexion.Open();

            string sql = "SELECT * FROM visitantes WHERE fecha LIKE @fecha AND dni LIKE @dni";
            MySqlCommand comando = new MySqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@fecha", fecha);
            comando.Parameters.AddWithValue("@dni", dni);
            reader = comando.ExecuteReader();
            Visitantes registro = null;
            while (reader.Read())
            {
                if (reader["hora_salida"].ToString() == "")
                {
                    registro = new Visitantes();
                    registro.IdV = int.Parse(reader["id"].ToString());
                    registro.Dni = reader["dni"].ToString();
                    registro.Nombre = reader["nombres"].ToString();
                    registro.ApellidoPat = reader["apellido_pat"].ToString();
                    registro.ApellidoMat = reader["apellido_mat"].ToString();
                    registro.EmpresaVisitada = reader["empresaVisitada"].ToString();
                    registro.EmpresaVisitante = reader["empresaVisitante"].ToString();
                    registro.MotivoVisita = reader["motivoVisita"].ToString();
                    registro.PersonaVisita = reader["persona"].ToString();
                    registro.Piso = reader["piso"].ToString();
                    registro.Oficina = int.Parse(reader["oficina"].ToString());
                    registro.Fecha = reader["fecha"].ToString();
                    registro.HoraSalida = reader["hora_salida"].ToString();
                }

            }
            return registro;
        }
        public int actualizarHoraSalida(int id,string hora)
        {
            MySqlConnection conexion = Conexion.getConexion();
            conexion.Open();
            string sql = "UPDATE visitantes SET hora_salida=@hora_salida WHERE id=@id";
            MySqlCommand comando = new MySqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@hora_salida", hora);
            comando.Parameters.AddWithValue("@id", id);

            int resultado = comando.ExecuteNonQuery();
            return resultado;
        }
    }
}
