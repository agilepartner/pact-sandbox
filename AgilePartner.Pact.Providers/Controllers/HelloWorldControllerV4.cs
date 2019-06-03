using Microsoft.AspNetCore.Mvc;

namespace AgilePartner.Pact.Providers.Controllers
{
    [ApiVersion("4.0")]
    [Route("api/v{version:apiVersion}/helloworld")]
    public class HelloWorldControllerV4 : Controller
    {
        [HttpGet]
        public ActionResult Get(string name)
        {
            if (string.IsNullOrEmpty(name))
                return NotFound();

            return Json(new { Message = $"Hello world v4 -> you rock {name}" });
        }
    }
}