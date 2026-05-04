namespace GestionLegalP.Application.DTOs
{
    public class ConsentimientoDto
    {
        public string Codigo { get; set; }
        public string TipoTratamiento { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public bool Firmado { get; set; }
        public string NombreFirmante { get; set; }
        public string ParentescoFirmante { get; set; }
    }
}