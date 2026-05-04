using GestionLegalP.Application.DTOs;
using GestionLegalP.Dominio;

namespace GestionLegalP.Application.Mappers
{
    public static class DocumentoLegalMapper
    {
        public static DocumentoLegalDto ToDto(DocumentoLegal doc)
        {
            return new DocumentoLegalDto
            {
                Codigo = doc.Codigo,
                Titulo = doc.Titulo,
                Tipo = doc.Tipo,
                Descripcion = doc.Descripcion,
                FechaEmision = doc.FechaEmision,
                Formato = doc.Formato,
                ArchivoUrl = doc.ArchivoUrl
            };
        }

        public static DocumentoLegal ToEntity(DocumentoLegalDto dto)
        {
            return new DocumentoLegal
            {
                Codigo = dto.Codigo,
                Titulo = dto.Titulo,
                Tipo = dto.Tipo,
                Descripcion = dto.Descripcion,
                FechaEmision = DateTime.SpecifyKind(dto.FechaEmision, DateTimeKind.Utc),
                Formato = dto.Formato,
                ArchivoUrl = dto.ArchivoUrl,
                Estado = "Activo"
            };
        }

        public static void UpdateEntity(DocumentoLegal doc, DocumentoLegalDto dto)
        {
            doc.Titulo = dto.Titulo;
            doc.Tipo = dto.Tipo;
            doc.Descripcion = dto.Descripcion;
            doc.FechaEmision = DateTime.SpecifyKind(dto.FechaEmision, DateTimeKind.Utc);
            doc.Formato = dto.Formato;
            doc.ArchivoUrl = dto.ArchivoUrl;
        }
    }
}