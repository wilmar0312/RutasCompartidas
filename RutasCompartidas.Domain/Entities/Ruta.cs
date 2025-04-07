using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RutasCompartidas.Domain.Entities
{
    public class Ruta
    {
        public int Id { get; set; }
        public string Origen { get; set; } = string.Empty;
        public string Destino { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty; // Información adicional
        public DateTime FechaHora { get; set; }
        public int ConductorId { get; set; } // ID del conductor
        public Usuario? Conductor { get; set; } // Relación con el usuario
        public int CantidadPasajeros { get; set; } = 0;

    }
}