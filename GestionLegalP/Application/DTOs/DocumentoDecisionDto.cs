namespace GestionLegalP.Application.DTOs
{
    public class DocumentoDecisionDto
    {
        public string Codigo { get; set; }
        public string CodigoDocumentoLegal { get; set; }
        public string TipoDecision { get; set; }
        public DateTime FechaDecision { get; set; }
        public string Observacion { get; set; }
    }
}
