using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace KittyStore.Api.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    protected IActionResult Error()
    {
        return Problem();
    }
}