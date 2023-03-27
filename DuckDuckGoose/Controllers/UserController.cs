using DuckDuckGoose.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DuckDuckGoose.Controllers;

[Route("users")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepo _users;

    public UserController
    (
        ILogger<UserController> logger,
        IUserRepo users
    )
    {
        _logger = logger;
        _users = users;
    }
}
