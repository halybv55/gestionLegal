using GestionLegalP.Application.DTOs;
using GestionLegalP.Application.Interfaces;
using GestionLegalP.Application.Mappers;

namespace GestionLegalP.Application.Services
{
    public class SolicitudRevisionService : ISolicitudRevisionService
    {
        private readonly ISolicitudRevisionRepository _repository;
        private readonly ISolicitudRepository _solicitudRepository;

        public SolicitudRevisionService(
            ISolicitudRevisionRepository repository,
            ISolicitudRepository solicitudRepository)
        {
            _repository = repository;
            _solicitudRepository = solicitudRepository;
        }

        public async Task<List<SolicitudRevisionDto>> GetActivasAsync()
        {
            var revisiones = await _repository.GetActivasAsync();
            return revisiones.Select(SolicitudRevisionMapper.ToDto).ToList();
        }

        public async Task<List<object>> GetTodasAsync()
        {
            var revisiones = await _repository.GetTodasAsync();

            return revisiones.Select(r => new
            {
                r.Codigo,
                CodigoSolicitud = r.Solicitud.Codigo,
                r.FechaRevision,
                r.Resultado,
                r.Observaciones,
                r.Estado
            }).Cast<object>().ToList();
        }

        public async Task<SolicitudRevisionDto?> GetByCodigoAsync(string codigo)
        {
            var revision = await _repository.GetByCodigoAsync(codigo);

            if (revision == null || revision.Estado != "Activo")
                return null;

            return SolicitudRevisionMapper.ToDto(revision);
        }

        public async Task<string> CrearAsync(SolicitudRevisionDto dto)
        {
            var existe = await _repository.ExisteCodigoAsync(dto.Codigo);

            if (existe)
                return "El código ya existe.";

            var solicitud = await _solicitudRepository.GetByCodigoAsync(dto.CodigoSolicitud);

            if (solicitud == null || solicitud.Estado != "Activo")
                return "Solicitud no encontrada o inactiva.";

            var revision = SolicitudRevisionMapper.ToEntity(dto, solicitud.Id_Solicitud);

            await _repository.CrearAsync(revision);

            return "Revisión de solicitud creada correctamente.";
        }

        public async Task<string> ActualizarAsync(string codigo, SolicitudRevisionDto dto)
        {
            var revision = await _repository.GetByCodigoAsync(codigo);

            if (revision == null || revision.Estado != "Activo")
                return "Revisión no encontrada o inactiva.";

            var solicitud = await _solicitudRepository.GetByCodigoAsync(dto.CodigoSolicitud);

            if (solicitud == null || solicitud.Estado != "Activo")
                return "Solicitud no encontrada o inactiva.";

            SolicitudRevisionMapper.UpdateEntity(revision, dto, solicitud.Id_Solicitud);

            await _repository.ActualizarAsync(revision);

            return "Revisión de solicitud actualizada correctamente.";
        }

        public async Task<string> DesactivarAsync(string codigo)
        {
            var revision = await _repository.GetByCodigoAsync(codigo);

            if (revision == null || revision.Estado != "Activo")
                return "Revisión no encontrada o ya está inactiva.";

            revision.Estado = "Inactivo";

            await _repository.ActualizarAsync(revision);

            return "Revisión de solicitud desactivada correctamente.";
        }
    }
}