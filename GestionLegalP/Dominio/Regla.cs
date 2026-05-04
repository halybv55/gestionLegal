using System.ComponentModel.DataAnnotations;

namespace GestionLegalP.Dominio
{
    public class Regla
    {
        [Key]
        public int Id_Regla { get; set; }

        public string Codigo { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string TipoRegla { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; } = "Activo";
    }
}
