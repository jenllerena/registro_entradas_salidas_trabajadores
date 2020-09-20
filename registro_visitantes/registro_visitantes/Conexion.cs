using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace registro_visitantes
{
    class Conexion
    {
        public static MySqlConnection getConexion()
        {
            string servidor = "localhost";
            string puerto = "3306";
            string usuario = "root";
            string password = "password";
            string bd = "bdusers";
            string cConexion = "server=" + servidor + "; port=" + puerto + "; user id=" + usuario + "; password=" + password + "; database=" + bd;
            MySqlConnection conexion = new MySqlConnection(cConexion);
            return conexion; 
        }
    }
}
