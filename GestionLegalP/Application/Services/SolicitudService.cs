using GestionLegalP.Application.DTOs;
using GestionLegalP.Application.Interfaces;
using GestionLegalP.Application.Mappers;

namespace GestionLegalP.Application.Services
{
    public class SolicitudService : ISolicitudService
    {
        private readonly ISolicitudRepository _repository;

        public SolicitudService(ISolicitudRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SolicitudDto>> GetActivasAsync()
        {
            var solicitudes = await _repository.GetActivasAsync();
            return solicitudes.Select(SolicitudMapper.ToDto).ToList();
        }

        public async Task<List<object>> GetTodasAsync()
        {
            var solicitudes = await _repository.GetTodasAsync();

            return solicitudes.Select(s => new
            {
                s.Codigo,
                s.TipoSolicitud,
                s.Motivo,
                s.Descripcion,
                s.FechaSolicitud,
                s.Estado
            }).Cast<object>().ToList();
        }

        public async Task<SolicitudDto?> GetByCodigoAsync(string codigo)
        {
            var solicitud = await _repository.GetByCodigoAsync(codigo);

            if (solicitud == null || solicitud.Estado != "Activo")
                return null;

            return SolicitudMapper.ToDto(solicitud);
        }

        public async Task<string> CrearAsync(SolicitudDto dto)
        {
            var existe = await _repository.ExisteCodigoAsync(dto.Codigo);

            if (existe)
                return "El código ya existe.";

            var solicitud = SolicitudMapper.ToEntity(dto);

            await _repository.CrearAsync(solicitud);

            return "Solicitud creada correctamente.";
        }

        public async Task<string> ActualizarAsync(string codigo, SolicitudDto dto)
        {
            var solicitud = await _repository.GetByCodigoAsync(codigo);

            if (solicitud == null || solicitud.Estado != "Activo")
                return "Solicitud no encontrada o inactiva.";

            SolicitudMapper.UpdateEntity(solicitud, dto);

            await _repository.ActualizarAsync(solicitud);

            return "Solicitud actualizada correctamente.";
        }

        public async Task<string> DesactivarAsync(string codigo)
        {
            var solicitud = await _repository.GetByCodigoAsync(codigo);

            if (solicitud == null || solicitud.Estado != "Activo")
                return "Solicitud no encontrada o ya está inactiva.";

            solicitud.Estado = "Inactivo";

            await _repository.ActualizarAsync(solicitud);

            return "Solicitud desactivada correctamente.";
        }
    }
}