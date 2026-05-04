using GestionLegalP.Application.DTOs;
using GestionLegalP.Dominio;

namespace GestionLegalP.Application.Mappers
{
    public static class CasoDocumentoMapper
    {
        public static CasoDocumentoDto ToDto(CasoDocumento cd)
        {
            return new CasoDocumentoDto
            {
                Codigo = cd.Codigo,
                CodigoCasoLegal = cd.CasoLegal.Codigo,
                CodigoDocumentoLegal = cd.DocumentoLegal.Codigo,
                TipoRelacion = cd.TipoRelacion,
                FechaAdjunta = cd.FechaAdjunta
            };
        }

        public static CasoDocumento ToEntity(CasoDocumentoDto dto, int idCaso, int idDoc)
        {
            return new CasoDocumento
            {
                Codigo = dto.Codigo,
                Id_CasoLegal = idCaso,
                Id_DocumentoLegal = idDoc,
                TipoRelacion = dto.TipoRelacion,
                FechaAdjunta = DateTime.SpecifyKind(dto.FechaAdjunta, DateTimeKind.Utc),
                Estado = "Activo"
            };
        }

        public static void UpdateEntity(CasoDocumento cd, CasoDocumentoDto dto, int idCaso, int idDoc)
        {
            cd.Id_CasoLegal = idCaso;
            cd.Id_DocumentoLegal = idDoc;
            cd.TipoRelacion = dto.TipoRelacion;
            cd.FechaAdjunta = DateTime.SpecifyKind(dto.FechaAdjunta, DateTimeKind.Utc);
        }
    }
}
