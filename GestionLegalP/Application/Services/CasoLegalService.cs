using GestionLegalP.Application.Interfaces;
using GestionLegalP.Application.Mappers;
using GestionLegalP.Application.DTOs;

namespace GestionLegalP.Application.Services
{
    public class CasoLegalService : ICasoLegalService
    {
        private readonly ICasoLegalRepository _repository;

        public CasoLegalService(ICasoLegalRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CasoLegalDto>> GetActivosAsync()
        {
            var casos = await _repository.GetActivosAsync();
            return casos.Select(CasoLegalMapper.ToDto).ToList();
        }

        public async Task<List<object>> GetTodosAsync()
        {
            var casos = await _repository.GetTodosAsync();

            return casos.Select(c => new
            {
                c.Codigo,
                c.TipoCaso,
                c.Descripcion,
                c.FechaApertura,
                c.EstadoCaso,
                c.Prioridad,
                c.Estado
            }).Cast<object>().ToList();
        }

        public async Task<CasoLegalDto?> GetByCodigoAsync(string codigo)
        {
            var caso = await _repository.GetByCodigoAsync(codigo);

            if (caso == null || caso.Estado != "Activo")
                return null;

            return CasoLegalMapper.ToDto(caso);
        }

        public async Task<string> CrearAsync(CasoLegalDto dto)
        {
            var existe = await _repository.ExisteCodigoAsync(dto.Codigo);

            if (existe)
                return "El código ya existe.";

            var caso = CasoLegalMapper.ToEntity(dto);

            await _repository.CrearAsync(caso);

            return "Caso legal creado correctamente.";
        }

        public async Task<string> ActualizarAsync(string codigo, CasoLegalDto dto)
        {
            var caso = await _repository.GetByCodigoAsync(codigo);

            if (caso == null || caso.Estado != "Activo")
                return "Caso legal no encontrado o inactivo.";

            CasoLegalMapper.UpdateEntity(caso, dto);

            await _repository.ActualizarAsync(caso);

            return "Caso legal actualizado correctamente.";
        }

        public async Task<string> DesactivarAsync(string codigo)
        {
            var caso = await _repository.GetByCodigoAsync(codigo);

            if (caso == null || caso.Estado != "Activo")
                return "Caso legal no encontrado o ya está inactivo.";

            caso.Estado = "Inactivo";

            await _repository.ActualizarAsync(caso);

            return "Caso legal desactivado correctamente.";
        }
    }
}