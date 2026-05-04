using GestionLegalP.Application.DTOs;
using GestionLegalP.Application.Interfaces;
using GestionLegalP.Application.Mappers;

namespace GestionLegalP.Application.Services
{
    public class CasoDocumentoService : ICasoDocumentoService
    {
        private readonly ICasoDocumentoRepository _repository;
        private readonly ICasoLegalRepository _casoRepo;
        private readonly IDocumentoLegalRepository _docRepo;

        public CasoDocumentoService(
            ICasoDocumentoRepository repository,
            ICasoLegalRepository casoRepo,
            IDocumentoLegalRepository docRepo)
        {
            _repository = repository;
            _casoRepo = casoRepo;
            _docRepo = docRepo;
        }

        public async Task<List<CasoDocumentoDto>> GetActivosAsync()
        {
            var list = await _repository.GetActivosAsync();
            return list.Select(CasoDocumentoMapper.ToDto).ToList();
        }

        public async Task<List<object>> GetTodosAsync()
        {
            var list = await _repository.GetTodosAsync();

            return list.Select(cd => new
            {
                cd.Codigo,
                Caso = cd.CasoLegal.Codigo,
                Documento = cd.DocumentoLegal.Codigo,
                cd.TipoRelacion,
                cd.FechaAdjunta,
                cd.Estado
            }).Cast<object>().ToList();
        }

        public async Task<CasoDocumentoDto?> GetByCodigoAsync(string codigo)
        {
            var cd = await _repository.GetByCodigoAsync(codigo);

            if (cd == null || cd.Estado != "Activo")
                return null;

            return CasoDocumentoMapper.ToDto(cd);
        }

        public async Task<string> CrearAsync(CasoDocumentoDto dto)
        {
            if (await _repository.ExisteCodigoAsync(dto.Codigo))
                return "El código ya existe.";

            var caso = await _casoRepo.GetByCodigoAsync(dto.CodigoCasoLegal);
            var doc = await _docRepo.GetByCodigoAsync(dto.CodigoDocumentoLegal);

            if (caso == null || caso.Estado != "Activo")
                return "Caso inválido.";

            if (doc == null || doc.Estado != "Activo")
                return "Documento inválido.";

            var entity = CasoDocumentoMapper.ToEntity(dto, caso.Id_CasoLegal, doc.Id_DocumentoLegal);

            await _repository.CrearAsync(entity);

            return "Relación creada correctamente.";
        }

        public async Task<string> ActualizarAsync(string codigo, CasoDocumentoDto dto)
        {
            var cd = await _repository.GetByCodigoAsync(codigo);

            if (cd == null || cd.Estado != "Activo")
                return "Relación no encontrada.";

            var caso = await _casoRepo.GetByCodigoAsync(dto.CodigoCasoLegal);
            var doc = await _docRepo.GetByCodigoAsync(dto.CodigoDocumentoLegal);

            if (caso == null || doc == null)
                return "Datos inválidos.";

            CasoDocumentoMapper.UpdateEntity(cd, dto, caso.Id_CasoLegal, doc.Id_DocumentoLegal);

            await _repository.ActualizarAsync(cd);

            return "Actualizado correctamente.";
        }

        public async Task<string> DesactivarAsync(string codigo)
        {
            var cd = await _repository.GetByCodigoAsync(codigo);

            if (cd == null || cd.Estado != "Activo")
                return "No encontrado.";

            cd.Estado = "Inactivo";

            await _repository.ActualizarAsync(cd);

            return "Desactivado.";
        }
    }
}