using GestionLegalP.Application.DTOs;
using GestionLegalP.Dominio;

namespace GestionLegalP.Application.Mappers
{
    public static class SolicitudRevisionMapper
    {
        public static SolicitudRevisionDto ToDto(SolicitudRevision revision)
        {
            return new SolicitudRevisionDto
            {
                Codigo = revision.Codigo,
                CodigoSolicitud = revision.Solicitud.Codigo,
                FechaRevision = revision.FechaRevision,
                Resultado = revision.Resultado,
                Observaciones = revision.Observaciones
            };
        }

        public static SolicitudRevision ToEntity(SolicitudRevisionDto dto, int idSolicitud)
        {
            return new SolicitudRevision
            {
                Codigo = dto.Codigo,
                Id_Solicitud = idSolicitud,
                FechaRevision = DateTime.SpecifyKind(dto.FechaRevision, DateTimeKind.Utc),
                Resultado = dto.Resultado,
                Observaciones = dto.Observaciones,
                Estado = "Activo"
            };
        }

        public static void UpdateEntity(SolicitudRevision revision, SolicitudRevisionDto dto, int idSolicitud)
        {
            revision.Id_Solicitud = idSolicitud;
            revision.FechaRevision = DateTime.SpecifyKind(dto.FechaRevision, DateTimeKind.Utc);
            revision.Resultado = dto.Resultado;
            revision.Observaciones = dto.Observaciones;
        }
    }
}