namespace DuckDuckGoose.Models.ViewModels;

public class UsersViewModel
{
    public Pagination<UserViewModel>? Users { get; set; }
    public string? Filter { get; set; }
    public string? Search { get; set; }
}
