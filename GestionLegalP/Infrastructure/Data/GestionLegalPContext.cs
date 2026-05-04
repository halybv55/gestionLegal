using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestionLegalP.Dominio;

namespace GestionLegalP.Infrastructure.Data
{
    public class GestionLegalPContext : DbContext
    {
        public GestionLegalPContext (DbContextOptions<GestionLegalPContext> options)
            : base(options)
        {
        }

        public DbSet<CasoDocumento> CasoDocumento { get; set; } = default!;
        public DbSet<CasoInvolucrado> CasoInvolucrado { get; set; } = default!;
        public DbSet<CasoLegal> CasoLegal { get; set; } = default!;
        public DbSet<Consentimiento> Consentimiento { get; set; } = default!;
        public DbSet<ConsentimientoDocumento> ConsentimientoDocumento { get; set; } = default!;
        public DbSet<DocumentoDecision> DocumentoDecision { get; set; } = default!;
        public DbSet<DocumentoLegal> DocumentoLegal { get; set; } = default!;
        public DbSet<Regla> Regla { get; set; } = default!;
        public DbSet<Solicitud> Solicitud { get; set; } = default!;
        public DbSet<SolicitudRevision> SolicitudRevision { get; set; } = default!;
    }
}
