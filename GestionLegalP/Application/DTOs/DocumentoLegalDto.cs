namespace GestionLegalP.Application.DTOs
{
    public class DocumentoLegalDto
    {
        public string Codigo { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaEmision { get; set; }
        public string Formato { get; set; }
        public string ArchivoUrl { get; set; }
    }
}
