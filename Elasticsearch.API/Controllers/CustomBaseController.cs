using Elasticsearch.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Elasticsearch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(ResponseDto<T> response)
        {
            if (response.StatusCode == HttpStatusCode.NoContent) return new ObjectResult(null) { StatusCode = response.StatusCode.GetHashCode() };

            return new ObjectResult(response) { StatusCode = response.StatusCode.GetHashCode() };
        }
    }
}
