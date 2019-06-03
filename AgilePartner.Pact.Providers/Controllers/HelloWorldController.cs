using Microsoft.AspNetCore.Mvc;

namespace AgilePartner.Pact.Providers.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/helloworld")]
    public class HelloWorldController : Controller
    {
        [HttpGet]
        public ActionResult Get() => Json(new { Message = "Hello world v1" });

        [HttpGet, MapToApiVersion("2.0")]
        public ActionResult GetV2() => Json(new { Message = "Hello world v2" });

        [HttpGet, MapToApiVersion("3.0")]
        public ActionResult GetV3() => Json(new { Message = "Hello world v3" });
    }
}