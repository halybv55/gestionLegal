using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GestionLegalP.Dominio
{
    public class CasoDocumento
    {
        [Key]
        public int Id_CasoDocumento { get; set; }

        public string Codigo { get; set; }
        public int Id_CasoLegal { get; set; }
        public int Id_DocumentoLegal { get; set; }

        public string TipoRelacion { get; set; }
        public DateTime FechaAdjunta { get; set; }
        public string Estado { get; set; } = "Activo";

        [ForeignKey("Id_CasoLegal")]
        [JsonIgnore]
        public CasoLegal CasoLegal { get; set; }

        [ForeignKey("Id_DocumentoLegal")]
        [JsonIgnore]
        public DocumentoLegal DocumentoLegal { get; set; }
    }
}
