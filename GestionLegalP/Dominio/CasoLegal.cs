using System.ComponentModel.DataAnnotations;

namespace GestionLegalP.Dominio
{
    public class CasoLegal
    {
        [Key]
        public int Id_CasoLegal { get; set; }

        public string Codigo { get; set; }
        public string TipoCaso { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaApertura { get; set; }
        public string EstadoCaso { get; set; }
        public string Prioridad { get; set; }
        public string Estado { get; set; } = "Activo";

        public List<CasoDocumento> CasoDocumentos { get; set; }
        public List<CasoInvolucrado> CasoInvolucrados { get; set; }
    }
}
