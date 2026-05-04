using GestionLegalP.Application.DTOs;
using GestionLegalP.Dominio;

namespace GestionLegalP.Application.Mappers
{
    public static class SolicitudMapper
    {
        public static SolicitudDto ToDto(Solicitud solicitud)
        {
            return new SolicitudDto
            {
                Codigo = solicitud.Codigo,
                TipoSolicitud = solicitud.TipoSolicitud,
                Motivo = solicitud.Motivo,
                Descripcion = solicitud.Descripcion,
                FechaSolicitud = solicitud.FechaSolicitud
            };
        }

        public static Solicitud ToEntity(SolicitudDto dto)
        {
            return new Solicitud
            {
                Codigo = dto.Codigo,
                TipoSolicitud = dto.TipoSolicitud,
                Motivo = dto.Motivo,
                Descripcion = dto.Descripcion,
                FechaSolicitud = DateTime.SpecifyKind(dto.FechaSolicitud, DateTimeKind.Utc),
                Estado = "Activo"
            };
        }

        public static void UpdateEntity(Solicitud solicitud, SolicitudDto dto)
        {
            solicitud.TipoSolicitud = dto.TipoSolicitud;
            solicitud.Motivo = dto.Motivo;
            solicitud.Descripcion = dto.Descripcion;
            solicitud.FechaSolicitud = DateTime.SpecifyKind(dto.FechaSolicitud, DateTimeKind.Utc);
        }
    }
}