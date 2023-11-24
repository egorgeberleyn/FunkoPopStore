using Microsoft.AspNetCore.Mvc;

namespace FunkoPopStore.Api.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    protected IActionResult Error()
    {
        return Problem();
    }
}