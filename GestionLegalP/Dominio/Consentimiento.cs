using System.ComponentModel.DataAnnotations;

namespace GestionLegalP.Dominio
{
    public class Consentimiento
    {
        [Key]
        public int Id_Consentimiento { get; set; }

        public string Codigo { get; set; }
        public string TipoTratamiento { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }

        public bool Firmado { get; set; }
        public string NombreFirmante { get; set; }
        public string ParentescoFirmante { get; set; }
        public string Estado { get; set; } = "Activo";

        public List<ConsentimientoDocumento> ConsentimientoDocumentos { get; set; }
    }
}
