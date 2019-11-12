using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace User.Api.Controllers.Comum
{
    [RequireHttps]
    [Route("docs")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DocsController : Controller
    {

        [Route(""), HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
    }
}