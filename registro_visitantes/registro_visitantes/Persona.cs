using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace registro_visitantes
{
    class Persona
    {
        int IdP;
        string nombreP,dniP, apellido_pat, apellido_mat;

        public string NombreP { get => nombreP; set => nombreP = value; }
        public string Apellido_pat { get => apellido_pat; set => apellido_pat = value; }
        public string Apellido_mat { get => apellido_mat; set => apellido_mat = value; }
        public int IdP1 { get => IdP; set => IdP = value; }
        public string DniP { get => dniP; set => dniP = value; }
    }
}
