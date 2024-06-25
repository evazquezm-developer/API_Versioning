using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Api_Versioning.V1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0", Deprecated = true)]
    public class StringListController : ControllerBase
    {
        // For testing querystring.
        [HttpGet]
        public IEnumerable<string> Get() 
        {
            return Data.Summaries.Where(x => x.StartsWith("B"));
        }
    }
}
