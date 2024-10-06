using API.Repository.LeftMenu;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LeftMenuController : BaseController
    {
        private readonly LeftMenuRepository _leftMenuRepository;
        public LeftMenuController(LeftMenuRepository leftMenuRepository)
        {
            _leftMenuRepository = leftMenuRepository;
        }
        [HttpGet("left-menu")]
        public async Task<string> GetLeftMenuAsync(CancellationToken cancellationToken)
        {
            return await _leftMenuRepository.GetLeftMenuAsync().ConfigureAwait(false);
        }

    }
}
