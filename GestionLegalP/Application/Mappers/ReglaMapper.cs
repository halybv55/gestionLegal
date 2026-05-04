using GestionLegalP.Application.DTOs;
using GestionLegalP.Dominio;

namespace GestionLegalP.Application.Mappers
{
    public static class ReglaMapper
    {
        public static ReglaDto ToDto(Regla regla)
        {
            return new ReglaDto
            {
                Codigo = regla.Codigo,
                Titulo = regla.Titulo,
                Descripcion = regla.Descripcion,
                TipoRegla = regla.TipoRegla,
                FechaCreacion = regla.FechaCreacion
            };
        }

        public static Regla ToEntity(ReglaDto dto)
        {
            return new Regla
            {
                Codigo = dto.Codigo,
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                TipoRegla = dto.TipoRegla,
                FechaCreacion = DateTime.SpecifyKind(dto.FechaCreacion, DateTimeKind.Utc),
                Estado = "Activo"
            };
        }

        public static void UpdateEntity(Regla regla, ReglaDto dto)
        {
            regla.Titulo = dto.Titulo;
            regla.Descripcion = dto.Descripcion;
            regla.TipoRegla = dto.TipoRegla;
            regla.FechaCreacion = DateTime.SpecifyKind(dto.FechaCreacion, DateTimeKind.Utc);
        }
    }
}