using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GestionLegalP.Dominio
{
    public class DocumentoDecision
    {
        [Key]
        public int Id_DocumentoDecision { get; set; }

        public string Codigo { get; set; }
        public int Id_DocumentoLegal { get; set; }

        public string TipoDecision { get; set; }
        public DateTime FechaDecision { get; set; }
        public string Observacion { get; set; }
        public string Estado { get; set; } = "Activo";

        [ForeignKey("Id_DocumentoLegal")]
        [JsonIgnore]
        public DocumentoLegal DocumentoLegal { get; set; }
    }
}
