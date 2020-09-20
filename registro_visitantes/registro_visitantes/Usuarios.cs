using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace registro_visitantes
{
    class Usuarios
    {
        int id;
        string usuario, password, conPassword, nombre, rol;

        public string Usuario { get => usuario; set => usuario = value; }
        public string Password { get => password; set => password = value; }
        public string ConPassword { get => conPassword; set => conPassword = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Rol { get => rol; set => rol = value; }
        public int Id { get => id; set => id = value; }
    }
}
