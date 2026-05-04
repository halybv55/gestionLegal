namespace GestionLegalP.Application.DTOs
{
    public class SolicitudRevisionDto
    {
        public string Codigo { get; set; }
        public string CodigoSolicitud { get; set; }
        public DateTime FechaRevision { get; set; }
        public string Resultado { get; set; }
        public string Observaciones { get; set; }
    }
}
