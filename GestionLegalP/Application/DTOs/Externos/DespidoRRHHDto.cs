using System.Text.Json.Serialization;

namespace GestionLegalP.Application.DTOs.Externos
{
    public class DespidoRRHHDto
    {
        [JsonPropertyName("empleadoId")]
        public int EmpleadoId { get; set; }

        [JsonPropertyName("motivo")]
        public string Motivo { get; set; }

        [JsonPropertyName("fechaDespido")]
        public DateTime FechaDespido { get; set; }

        [JsonPropertyName("registradoPor")]
        public string RegistradoPor { get; set; }
    }
}
