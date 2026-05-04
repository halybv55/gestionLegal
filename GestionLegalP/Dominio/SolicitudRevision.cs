using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GestionLegalP.Dominio
{
    public class SolicitudRevision
    {
        [Key]
        public int Id_SolicitudRevision { get; set; }

        public string Codigo { get; set; }
        public int Id_Solicitud { get; set; }

        public DateTime FechaRevision { get; set; }
        public string Resultado { get; set; }
        public string Observaciones { get; set; }
        public string Estado { get; set; } = "Activo";

        [ForeignKey("Id_Solicitud")]
        [JsonIgnore]
        public Solicitud Solicitud { get; set; }
    }
}
