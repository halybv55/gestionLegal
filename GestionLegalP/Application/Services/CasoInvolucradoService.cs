using GestionLegalP.Application.DTOs;
using GestionLegalP.Application.Interfaces;
using GestionLegalP.Application.Mappers;

namespace GestionLegalP.Application.Services
{
    public class CasoInvolucradoService : ICasoInvolucradoService
    {
        private readonly ICasoInvolucradoRepository _repository;
        private readonly ICasoLegalRepository _casoLegalRepository;

        public CasoInvolucradoService(
            ICasoInvolucradoRepository repository,
            ICasoLegalRepository casoLegalRepository)
        {
            _repository = repository;
            _casoLegalRepository = casoLegalRepository;
        }

        public async Task<List<CasoInvolucradoDto>> GetActivosAsync()
        {
            var involucrados = await _repository.GetActivosAsync();
            return involucrados.Select(CasoInvolucradoMapper.ToDto).ToList();
        }

        public async Task<List<object>> GetTodosAsync()
        {
            var involucrados = await _repository.GetTodosAsync();

            return involucrados.Select(i => new
            {
                i.Codigo,
                CodigoCasoLegal = i.CasoLegal.Codigo,
                i.RolInvolucrado,
                i.DescripcionParticipacion,
                i.Estado
            }).Cast<object>().ToList();
        }

        public async Task<CasoInvolucradoDto?> GetByCodigoAsync(string codigo)
        {
            var involucrado = await _repository.GetByCodigoAsync(codigo);

            if (involucrado == null || involucrado.Estado != "Activo")
                return null;

            return CasoInvolucradoMapper.ToDto(involucrado);
        }

        public async Task<string> CrearAsync(CasoInvolucradoDto dto)
        {
            var existe = await _repository.ExisteCodigoAsync(dto.Codigo);

            if (existe)
                return "El código ya existe.";

            var casoLegal = await _casoLegalRepository.GetByCodigoAsync(dto.CodigoCasoLegal);

            if (casoLegal == null || casoLegal.Estado != "Activo")
                return "Caso legal no encontrado o inactivo.";

            var involucrado = CasoInvolucradoMapper.ToEntity(dto, casoLegal.Id_CasoLegal);

            await _repository.CrearAsync(involucrado);

            return "Involucrado del caso creado correctamente.";
        }

        public async Task<string> ActualizarAsync(string codigo, CasoInvolucradoDto dto)
        {
            var involucrado = await _repository.GetByCodigoAsync(codigo);

            if (involucrado == null || involucrado.Estado != "Activo")
                return "Involucrado no encontrado o inactivo.";

            var casoLegal = await _casoLegalRepository.GetByCodigoAsync(dto.CodigoCasoLegal);

            if (casoLegal == null || casoLegal.Estado != "Activo")
                return "Caso legal no encontrado o inactivo.";

            CasoInvolucradoMapper.UpdateEntity(involucrado, dto, casoLegal.Id_CasoLegal);

            await _repository.ActualizarAsync(involucrado);

            return "Involucrado actualizado correctamente.";
        }

        public async Task<string> DesactivarAsync(string codigo)
        {
            var involucrado = await _repository.GetByCodigoAsync(codigo);

            if (involucrado == null || involucrado.Estado != "Activo")
                return "Involucrado no encontrado o ya está inactivo.";

            involucrado.Estado = "Inactivo";

            await _repository.ActualizarAsync(involucrado);

            return "Involucrado desactivado correctamente.";
        }
    }
}