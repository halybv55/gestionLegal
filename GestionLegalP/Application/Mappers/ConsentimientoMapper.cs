using GestionLegalP.Application.DTOs;
using GestionLegalP.Dominio;

namespace GestionLegalP.Application.Mappers
{
    public static class ConsentimientoMapper
    {
        public static ConsentimientoDto ToDto(Consentimiento consentimiento)
        {
            return new ConsentimientoDto
            {
                Codigo = consentimiento.Codigo,
                TipoTratamiento = consentimiento.TipoTratamiento,
                Descripcion = consentimiento.Descripcion,
                Fecha = consentimiento.Fecha,
                Firmado = consentimiento.Firmado,
                NombreFirmante = consentimiento.NombreFirmante,
                ParentescoFirmante = consentimiento.ParentescoFirmante
            };
        }

        public static Consentimiento ToEntity(ConsentimientoDto dto)
        {
            return new Consentimiento
            {
                Codigo = dto.Codigo,
                TipoTratamiento = dto.TipoTratamiento,
                Descripcion = dto.Descripcion,
                Fecha = DateTime.SpecifyKind(dto.Fecha, DateTimeKind.Utc),
                Firmado = dto.Firmado,
                NombreFirmante = dto.NombreFirmante,
                ParentescoFirmante = dto.ParentescoFirmante,
                Estado = "Activo"
            };
        }

        public static void UpdateEntity(Consentimiento consentimiento, ConsentimientoDto dto)
        {
            consentimiento.TipoTratamiento = dto.TipoTratamiento;
            consentimiento.Descripcion = dto.Descripcion;
            consentimiento.Fecha = DateTime.SpecifyKind(dto.Fecha, DateTimeKind.Utc);
            consentimiento.Firmado = dto.Firmado;
            consentimiento.NombreFirmante = dto.NombreFirmante;
            consentimiento.ParentescoFirmante = dto.ParentescoFirmante;
        }
    }
}