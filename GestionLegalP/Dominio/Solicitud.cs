using System.ComponentModel.DataAnnotations;

namespace GestionLegalP.Dominio
{
    public class Solicitud
    {
        [Key]
        public int Id_Solicitud { get; set; }

        public string Codigo { get; set; }
        public string TipoSolicitud { get; set; }
        public string Motivo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Estado { get; set; } = "Activo";

        public List<SolicitudRevision> SolicitudRevisiones { get; set; }
    }
}
