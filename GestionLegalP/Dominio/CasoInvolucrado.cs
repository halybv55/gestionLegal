using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GestionLegalP.Dominio
{
    public class CasoInvolucrado
    {
        [Key]
        public int Id_CasoInvolucrado { get; set; }

        public string Codigo { get; set; }
        public int Id_CasoLegal { get; set; }

        public string RolInvolucrado { get; set; }
        public string DescripcionParticipacion { get; set; }
        public string Estado { get; set; } = "Activo";

        [ForeignKey("Id_CasoLegal")]
        [JsonIgnore]
        public CasoLegal CasoLegal { get; set; }
    }
}
