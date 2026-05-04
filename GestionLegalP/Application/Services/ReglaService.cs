using GestionLegalP.Application.DTOs;
using GestionLegalP.Application.Interfaces;
using GestionLegalP.Application.Mappers;

namespace GestionLegalP.Application.Services
{
    public class ReglaService : IReglaService
    {
        private readonly IReglaRepository _repository;

        public ReglaService(IReglaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ReglaDto>> GetActivasAsync()
        {
            var reglas = await _repository.GetActivasAsync();
            return reglas.Select(ReglaMapper.ToDto).ToList();
        }

        public async Task<List<object>> GetTodasAsync()
        {
            var reglas = await _repository.GetTodasAsync();

            return reglas.Select(r => new
            {
                r.Codigo,
                r.Titulo,
                r.Descripcion,
                r.TipoRegla,
                r.FechaCreacion,
                r.Estado
            }).Cast<object>().ToList();
        }

        public async Task<ReglaDto?> GetByCodigoAsync(string codigo)
        {
            var regla = await _repository.GetByCodigoAsync(codigo);

            if (regla == null || regla.Estado != "Activo")
                return null;

            return ReglaMapper.ToDto(regla);
        }

        public async Task<string> CrearAsync(ReglaDto dto)
        {
            var existe = await _repository.ExisteCodigoAsync(dto.Codigo);

            if (existe)
                return "El código ya existe.";

            var regla = ReglaMapper.ToEntity(dto);

            await _repository.CrearAsync(regla);

            return "Regla creada correctamente.";
        }

        public async Task<string> ActualizarAsync(string codigo, ReglaDto dto)
        {
            var regla = await _repository.GetByCodigoAsync(codigo);

            if (regla == null || regla.Estado != "Activo")
                return "Regla no encontrada o inactiva.";

            ReglaMapper.UpdateEntity(regla, dto);

            await _repository.ActualizarAsync(regla);

            return "Regla actualizada correctamente.";
        }

        public async Task<string> DesactivarAsync(string codigo)
        {
            var regla = await _repository.GetByCodigoAsync(codigo);

            if (regla == null || regla.Estado != "Activo")
                return "Regla no encontrada o ya está inactiva.";

            regla.Estado = "Inactivo";

            await _repository.ActualizarAsync(regla);

            return "Regla desactivada correctamente.";
        }
    }
}