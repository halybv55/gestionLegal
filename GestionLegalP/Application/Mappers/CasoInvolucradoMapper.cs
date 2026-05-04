using GestionLegalP.Application.DTOs;
using GestionLegalP.Dominio;

namespace GestionLegalP.Application.Mappers
{
    public static class CasoInvolucradoMapper
    {
        public static CasoInvolucradoDto ToDto(CasoInvolucrado involucrado)
        {
            return new CasoInvolucradoDto
            {
                Codigo = involucrado.Codigo,
                CodigoCasoLegal = involucrado.CasoLegal.Codigo,
                RolInvolucrado = involucrado.RolInvolucrado,
                DescripcionParticipacion = involucrado.DescripcionParticipacion
            };
        }

        public static CasoInvolucrado ToEntity(CasoInvolucradoDto dto, int idCasoLegal)
        {
            return new CasoInvolucrado
            {
                Codigo = dto.Codigo,
                Id_CasoLegal = idCasoLegal,
                RolInvolucrado = dto.RolInvolucrado,
                DescripcionParticipacion = dto.DescripcionParticipacion,
                Estado = "Activo"
            };
        }

        public static void UpdateEntity(CasoInvolucrado involucrado, CasoInvolucradoDto dto, int idCasoLegal)
        {
            involucrado.Id_CasoLegal = idCasoLegal;
            involucrado.RolInvolucrado = dto.RolInvolucrado;
            involucrado.DescripcionParticipacion = dto.DescripcionParticipacion;
        }
    }
}