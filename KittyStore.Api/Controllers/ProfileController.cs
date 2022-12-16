using Microsoft.AspNetCore.Mvc;

namespace KittyStore.Api.Controllers;

[Route("profile")]
public class ProfileController : ApiController
{
    [HttpPut]
    public IActionResult ReplenishmentBalance()
    {
        return Ok();
    }
}