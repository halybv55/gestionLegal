namespace GestionLegalP.Application.DTOs
{
    public class SolicitudDto
    {
        public string Codigo { get; set; }
        public string TipoSolicitud { get; set; }
        public string Motivo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaSolicitud { get; set; }
    }
}
