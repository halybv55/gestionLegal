using GestionLegalP.Application.DTOs;
using GestionLegalP.Dominio;

namespace GestionLegalP.Application.Mappers
{
    public static class CasoLegalMapper
    {
        public static CasoLegalDto ToDto(CasoLegal caso)
        {
            return new CasoLegalDto
            {
                Codigo = caso.Codigo,
                TipoCaso = caso.TipoCaso,
                Descripcion = caso.Descripcion,
                FechaApertura = caso.FechaApertura,
                EstadoCaso = caso.EstadoCaso,
                Prioridad = caso.Prioridad
            };
        }

        public static CasoLegal ToEntity(CasoLegalDto dto)
        {
            return new CasoLegal
            {
                Codigo = dto.Codigo,
                TipoCaso = dto.TipoCaso,
                Descripcion = dto.Descripcion,
                FechaApertura = DateTime.SpecifyKind(dto.FechaApertura, DateTimeKind.Utc),
                EstadoCaso = dto.EstadoCaso,
                Prioridad = dto.Prioridad,
                Estado = "Activo"
            };
        }

        public static void UpdateEntity(CasoLegal caso, CasoLegalDto dto)
        {
            caso.TipoCaso = dto.TipoCaso;
            caso.Descripcion = dto.Descripcion;
            caso.FechaApertura = DateTime.SpecifyKind(dto.FechaApertura, DateTimeKind.Utc);
            caso.EstadoCaso = dto.EstadoCaso;
            caso.Prioridad = dto.Prioridad;
        }
    }
}