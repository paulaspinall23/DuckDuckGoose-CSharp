using DuckDuckGoose.Areas.Identity.Data;
using DuckDuckGoose.Models;
using DuckDuckGoose.Models.Requests;
using DuckDuckGoose.Models.ViewModels;
using DuckDuckGoose.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DuckDuckGoose.Controllers;

[Route("users")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepo _users;
    private readonly UserManager<DuckDuckGooseUser> _userManager;
    private readonly IHttpContextAccessor _httpContext;

    public UserController
    (
        ILogger<UserController> logger,
        IUserRepo users,
        UserManager<DuckDuckGooseUser> userManager,
        IHttpContextAccessor httpContext
    )
    {
        _logger = logger;
        _users = users;
        _userManager = userManager;
        _httpContext = httpContext;
    }

    public IActionResult Index(
        [FromQuery] GetUsersRequest request
    )
    {
        ViewData["IsAuthenticated"] = false;
        var users = _users.GetUsers(request);
        UsersViewModel viewModel = new UsersViewModel
        {
            Users = new Pagination<UserViewModel>
            {
                Page = users.Page,
                PerPage = users.PerPage,
                Items = users.Items.Select(user => new UserViewModel(user, 1, 5)),
                Total = users.Total,
            },
            Filter = request.Filter,
            Search = request.Search,
        };
        return View(viewModel);
    }

    [HttpGet("{userId}")]
    public IActionResult UserPage([FromRoute] string userId, [FromQuery] int? page)
    {
        UserViewModel user = new UserViewModel(_users.GetUserById(userId), page.HasValue ? page.Value : 1, 5);
        return View(user);
    }

    [HttpGet("current")]
    public IActionResult Current()
    {
        ViewData["IsAuthenticated"] = false;
        return NotFound();
    }

    [HttpPost("{userId}/follow")]
    public async Task<IActionResult> Follow([FromRoute] string userId)
    {
        var follower = await _userManager.GetUserAsync(_httpContext.HttpContext?.User);
        string followerId = follower.Id;

        try
        {
            _users.Follow(followerId, userId);
        }
        catch (ArgumentOutOfRangeException)
        {
            return NotFound($"Could not find user with ID {userId}");
        }

        return RedirectToAction("UserPage", new { userId });
    }
    
    [HttpPost("{userId}/unfollow")]
    public async Task<IActionResult> Unfollow([FromRoute] string userId)
    {
        var follower = await _userManager.GetUserAsync(_httpContext.HttpContext?.User);
        string followerId = follower.Id;

        try
        {
            _users.Unfollow(followerId, userId);
        }
        catch (ArgumentOutOfRangeException)
        {
            return NotFound($"Could not find user with ID {userId}");
        }

        return RedirectToAction("UserPage", new { userId });
    }
}
