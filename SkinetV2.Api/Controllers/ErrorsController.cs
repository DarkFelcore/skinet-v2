using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace SkinetV2.Api.Controllers
{
    public class ErrorsController : ApiController
    {
        [Route("/error")]
        public IActionResult Error()
        {
            // Get the exception that occured during the request.
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            return Problem(title: exception?.Message);
        }
    }
}