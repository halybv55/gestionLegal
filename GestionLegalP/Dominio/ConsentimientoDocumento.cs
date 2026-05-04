using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GestionLegalP.Dominio
{
    public class ConsentimientoDocumento
    {
        [Key]
        public int Id_ConsentimientoDocumento { get; set; }

        public string Codigo { get; set; }
        public int Id_Consentimiento { get; set; }
        public int Id_DocumentoLegal { get; set; }

        public DateTime FechaAsociacion { get; set; }
        public string Estado { get; set; } = "Activo";

        [ForeignKey("Id_Consentimiento")]
        [JsonIgnore]
        public Consentimiento Consentimiento { get; set; }

        [ForeignKey("Id_DocumentoLegal")]
        [JsonIgnore]
        public DocumentoLegal DocumentoLegal { get; set; }
    }
}
