using Microsoft.AspNetCore.Mvc;
using DuckDuckGoose.Repositories;

namespace DuckDuckGoose.Controllers;

[Route("honks")]
public class HonkController : Controller
{
    private readonly ILogger<HonkController> _logger;
    private readonly IHonkRepo _honks;

    public HonkController
    (
        ILogger<HonkController> logger,
        IHonkRepo honks
    )
    {
        _logger = logger;
        _honks = honks;
    }
}
