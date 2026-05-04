namespace GestionLegalP.Application.DTOs
{
    public class CasoLegalDto
    {
        public string Codigo { get; set; }
        public string TipoCaso { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaApertura { get; set; }
        public string EstadoCaso { get; set; }
        public string Prioridad { get; set; }
    }
}
