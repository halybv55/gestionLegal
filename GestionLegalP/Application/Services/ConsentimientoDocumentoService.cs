using GestionLegalP.Application.DTOs;
using GestionLegalP.Application.Interfaces;
using GestionLegalP.Application.Mappers;

namespace GestionLegalP.Application.Services
{
    public class ConsentimientoDocumentoService : IConsentimientoDocumentoService
    {
        private readonly IConsentimientoDocumentoRepository _repository;
        private readonly IConsentimientoRepository _consentimientoRepository;
        private readonly IDocumentoLegalRepository _documentoRepository;

        public ConsentimientoDocumentoService(
            IConsentimientoDocumentoRepository repository,
            IConsentimientoRepository consentimientoRepository,
            IDocumentoLegalRepository documentoRepository)
        {
            _repository = repository;
            _consentimientoRepository = consentimientoRepository;
            _documentoRepository = documentoRepository;
        }

        public async Task<List<ConsentimientoDocumentoDto>> GetActivosAsync()
        {
            var relaciones = await _repository.GetActivosAsync();
            return relaciones.Select(ConsentimientoDocumentoMapper.ToDto).ToList();
        }

        public async Task<List<object>> GetTodosAsync()
        {
            var relaciones = await _repository.GetTodosAsync();

            return relaciones.Select(cd => new
            {
                cd.Codigo,
                CodigoConsentimiento = cd.Consentimiento.Codigo,
                CodigoDocumentoLegal = cd.DocumentoLegal.Codigo,
                cd.FechaAsociacion,
                cd.Estado
            }).Cast<object>().ToList();
        }

        public async Task<ConsentimientoDocumentoDto?> GetByCodigoAsync(string codigo)
        {
            var relacion = await _repository.GetByCodigoAsync(codigo);

            if (relacion == null || relacion.Estado != "Activo")
                return null;

            return ConsentimientoDocumentoMapper.ToDto(relacion);
        }

        public async Task<string> CrearAsync(ConsentimientoDocumentoDto dto)
        {
            var existe = await _repository.ExisteCodigoAsync(dto.Codigo);

            if (existe)
                return "El código ya existe.";

            var consentimiento = await _consentimientoRepository.GetByCodigoAsync(dto.CodigoConsentimiento);
            var documento = await _documentoRepository.GetByCodigoAsync(dto.CodigoDocumentoLegal);

            if (consentimiento == null || consentimiento.Estado != "Activo")
                return "Consentimiento no encontrado o inactivo.";

            if (documento == null || documento.Estado != "Activo")
                return "Documento legal no encontrado o inactivo.";

            var relacion = ConsentimientoDocumentoMapper.ToEntity(
                dto,
                consentimiento.Id_Consentimiento,
                documento.Id_DocumentoLegal
            );

            await _repository.CrearAsync(relacion);

            return "Relación consentimiento-documento creada correctamente.";
        }

        public async Task<string> ActualizarAsync(string codigo, ConsentimientoDocumentoDto dto)
        {
            var relacion = await _repository.GetByCodigoAsync(codigo);

            if (relacion == null || relacion.Estado != "Activo")
                return "Relación no encontrada o inactiva.";

            var consentimiento = await _consentimientoRepository.GetByCodigoAsync(dto.CodigoConsentimiento);
            var documento = await _documentoRepository.GetByCodigoAsync(dto.CodigoDocumentoLegal);

            if (consentimiento == null || consentimiento.Estado != "Activo")
                return "Consentimiento no encontrado o inactivo.";

            if (documento == null || documento.Estado != "Activo")
                return "Documento legal no encontrado o inactivo.";

            ConsentimientoDocumentoMapper.UpdateEntity(
                relacion,
                dto,
                consentimiento.Id_Consentimiento,
                documento.Id_DocumentoLegal
            );

            await _repository.ActualizarAsync(relacion);

            return "Relación consentimiento-documento actualizada correctamente.";
        }

        public async Task<string> DesactivarAsync(string codigo)
        {
            var relacion = await _repository.GetByCodigoAsync(codigo);

            if (relacion == null || relacion.Estado != "Activo")
                return "Relación no encontrada o ya está inactiva.";

            relacion.Estado = "Inactivo";

            await _repository.ActualizarAsync(relacion);

            return "Relación consentimiento-documento desactivada correctamente.";
        }
    }
}