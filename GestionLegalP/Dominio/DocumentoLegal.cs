using System.ComponentModel.DataAnnotations;

namespace GestionLegalP.Dominio
{
    public class DocumentoLegal
    {
        [Key]
        public int Id_DocumentoLegal { get; set; }

        public string Codigo { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaEmision { get; set; }
        public string Formato { get; set; }
        public string ArchivoUrl { get; set; }
        public string Estado { get; set; } = "Activo";

        public List<CasoDocumento> CasoDocumentos { get; set; }
        public List<DocumentoDecision> DocumentoDecisiones { get; set; }
        public List<ConsentimientoDocumento> ConsentimientoDocumentos { get; set; }
    }
}
