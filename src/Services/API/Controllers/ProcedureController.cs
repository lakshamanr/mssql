using API.Model.Procedure;
using API.Repository.Procedure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedureController : ControllerBase
    {
        private readonly ProcedureRepository _procedureRepository;
        public ProcedureController(ProcedureRepository procedureRepository)
        {
            _procedureRepository = procedureRepository;
        }
        [HttpGet("procedure-meta-data")]
        public async Task<ProcedureMetadata> LoadProcedureMetData(string procedurename, CancellationToken cancellationToken)
        {
            return await _procedureRepository.LoadProcedureMetaData(procedurename);
        }

    }
}
