using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace registro_visitantes
{
    class Visitantes
    {
        int idV,oficina;
        string horaIng, dni, nombre, apellidoPat, apellidoMat, empresaVisitada, empresaVisitante, motivoVisita,personaVisita, piso,fecha,horaSalida;

        public string HoraIng { get => horaIng; set => horaIng = value; }
        public string Dni { get => dni; set => dni = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string ApellidoPat { get => apellidoPat; set => apellidoPat = value; }
        public string ApellidoMat { get => apellidoMat; set => apellidoMat = value; }
        public string EmpresaVisitada { get => empresaVisitada; set => empresaVisitada = value; }
        public string EmpresaVisitante { get => empresaVisitante; set => empresaVisitante = value; }
        public string MotivoVisita { get => motivoVisita; set => motivoVisita = value; }
        public string PersonaVisita { get => personaVisita; set => personaVisita = value; }
        public string Piso { get => piso; set => piso = value; }
        public int Oficina { get => oficina; set => oficina = value; }
        public int IdV { get => idV; set => idV = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public string HoraSalida { get => horaSalida; set => horaSalida = value; }
    }
}
