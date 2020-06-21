using Microsoft.AspNetCore.Mvc;

namespace Orders.Api.Controllers
{
    /// <summary>
    /// Api контроллер документации.
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("")]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// Вход на страницу документации.
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}
