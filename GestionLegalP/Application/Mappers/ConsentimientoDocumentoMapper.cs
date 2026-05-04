using GestionLegalP.Application.DTOs;
using GestionLegalP.Dominio;

namespace GestionLegalP.Application.Mappers
{
    public static class ConsentimientoDocumentoMapper
    {
        public static ConsentimientoDocumentoDto ToDto(ConsentimientoDocumento cd)
        {
            return new ConsentimientoDocumentoDto
            {
                Codigo = cd.Codigo,
                CodigoConsentimiento = cd.Consentimiento.Codigo,
                CodigoDocumentoLegal = cd.DocumentoLegal.Codigo,
                FechaAsociacion = cd.FechaAsociacion
            };
        }

        public static ConsentimientoDocumento ToEntity(ConsentimientoDocumentoDto dto, int idConsentimiento, int idDocumentoLegal)
        {
            return new ConsentimientoDocumento
            {
                Codigo = dto.Codigo,
                Id_Consentimiento = idConsentimiento,
                Id_DocumentoLegal = idDocumentoLegal,
                FechaAsociacion = DateTime.SpecifyKind(dto.FechaAsociacion, DateTimeKind.Utc),
                Estado = "Activo"
            };
        }

        public static void UpdateEntity(ConsentimientoDocumento cd, ConsentimientoDocumentoDto dto, int idConsentimiento, int idDocumentoLegal)
        {
            cd.Id_Consentimiento = idConsentimiento;
            cd.Id_DocumentoLegal = idDocumentoLegal;
            cd.FechaAsociacion = DateTime.SpecifyKind(dto.FechaAsociacion, DateTimeKind.Utc);
        }
    }
}