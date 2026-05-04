using GestionLegalP.Application.DTOs;
using GestionLegalP.Application.Interfaces;
using GestionLegalP.Application.Mappers;

namespace GestionLegalP.Application.Services
{
    public class ConsentimientoService : IConsentimientoService
    {
        private readonly IConsentimientoRepository _repository;

        public ConsentimientoService(IConsentimientoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ConsentimientoDto>> GetActivosAsync()
        {
            var consentimientos = await _repository.GetActivosAsync();
            return consentimientos.Select(ConsentimientoMapper.ToDto).ToList();
        }

        public async Task<List<object>> GetTodosAsync()
        {
            var consentimientos = await _repository.GetTodosAsync();

            return consentimientos.Select(c => new
            {
                c.Codigo,
                c.TipoTratamiento,
                c.Descripcion,
                c.Fecha,
                c.Firmado,
                c.NombreFirmante,
                c.ParentescoFirmante,
                c.Estado
            }).Cast<object>().ToList();
        }

        public async Task<ConsentimientoDto?> GetByCodigoAsync(string codigo)
        {
            var consentimiento = await _repository.GetByCodigoAsync(codigo);

            if (consentimiento == null || consentimiento.Estado != "Activo")
                return null;

            return ConsentimientoMapper.ToDto(consentimiento);
        }

        public async Task<string> CrearAsync(ConsentimientoDto dto)
        {
            var existe = await _repository.ExisteCodigoAsync(dto.Codigo);

            if (existe)
                return "El código ya existe.";

            var consentimiento = ConsentimientoMapper.ToEntity(dto);

            await _repository.CrearAsync(consentimiento);

            return "Consentimiento creado correctamente.";
        }

        public async Task<string> ActualizarAsync(string codigo, ConsentimientoDto dto)
        {
            var consentimiento = await _repository.GetByCodigoAsync(codigo);

            if (consentimiento == null || consentimiento.Estado != "Activo")
                return "Consentimiento no encontrado o inactivo.";

            ConsentimientoMapper.UpdateEntity(consentimiento, dto);

            await _repository.ActualizarAsync(consentimiento);

            return "Consentimiento actualizado correctamente.";
        }

        public async Task<string> DesactivarAsync(string codigo)
        {
            var consentimiento = await _repository.GetByCodigoAsync(codigo);

            if (consentimiento == null || consentimiento.Estado != "Activo")
                return "Consentimiento no encontrado o ya está inactivo.";

            consentimiento.Estado = "Inactivo";

            await _repository.ActualizarAsync(consentimiento);

            return "Consentimiento desactivado correctamente.";
        }
    }
}