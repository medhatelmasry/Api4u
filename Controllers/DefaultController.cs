using Microsoft.AspNetCore.Mvc;

namespace Api4u.Controllers
{

    public class DefaultController : Controller
    {
        [Route(""), HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public RedirectResult RedirectToSwaggerUi()
        {
            return Redirect("/swagger/index.html");
        }
    }

}