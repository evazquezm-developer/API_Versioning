using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Api_Versioning.V3.Controllers
{
    [ApiController]
    [Route("api/V{version:apiVersion}/[controller]")]
    [ApiVersion("3.0")]
    public class StringListController : ControllerBase
    {
        // For testing in URL.
        [HttpGet]
        public IEnumerable<string> Get() 
        {
            return Data.Summaries.Where(x => x.StartsWith("C"));
        }
    }
}
