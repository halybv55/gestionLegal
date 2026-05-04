using GestionLegalP.Application.DTOs;
using GestionLegalP.Dominio;

namespace GestionLegalP.Application.Mappers
{
    public static class DocumentoDecisionMapper
    {
        public static DocumentoDecisionDto ToDto(DocumentoDecision decision)
        {
            return new DocumentoDecisionDto
            {
                Codigo = decision.Codigo,
                CodigoDocumentoLegal = decision.DocumentoLegal.Codigo,
                TipoDecision = decision.TipoDecision,
                FechaDecision = decision.FechaDecision,
                Observacion = decision.Observacion
            };
        }

        public static DocumentoDecision ToEntity(DocumentoDecisionDto dto, int idDocumentoLegal)
        {
            return new DocumentoDecision
            {
                Codigo = dto.Codigo,
                Id_DocumentoLegal = idDocumentoLegal,
                TipoDecision = dto.TipoDecision,
                FechaDecision = DateTime.SpecifyKind(dto.FechaDecision, DateTimeKind.Utc),
                Observacion = dto.Observacion,
                Estado = "Activo"
            };
        }

        public static void UpdateEntity(DocumentoDecision decision, DocumentoDecisionDto dto, int idDocumentoLegal)
        {
            decision.Id_DocumentoLegal = idDocumentoLegal;
            decision.TipoDecision = dto.TipoDecision;
            decision.FechaDecision = DateTime.SpecifyKind(dto.FechaDecision, DateTimeKind.Utc);
            decision.Observacion = dto.Observacion;
        }
    }
}