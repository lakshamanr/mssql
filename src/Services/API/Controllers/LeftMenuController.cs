using API.Repository.LeftMenu;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LeftMenuController : ControllerBase
    {
        private readonly LeftMenuRepository _leftMenuRepository;
        public LeftMenuController(LeftMenuRepository leftMenuRepository)
        {
            _leftMenuRepository = leftMenuRepository;
        }
        [HttpGet("left-menu")]
        public async Task<string> GetLeftMenu()
        {
            return await _leftMenuRepository.GetLeftMenuAsync();
        }

    }
}
