using DuckDuckGoose.Areas.Identity.Data;
using DuckDuckGoose.Models.Database;

namespace DuckDuckGoose.Models.ViewModels;

public class HonkViewModel
{
    public int Id { get; set; }
    public HonkViewModelUser User { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }

    public HonkViewModel(Honk honk)
    {
        Id = honk.Id;
        User = new HonkViewModelUser(honk.User);
        Content = honk.Content;
        Timestamp = honk.Timestamp;
    }
}

public class HonkViewModelUser
{
    public string Id { get; set; }
    public string UserName { get; set; }

    public HonkViewModelUser(DuckDuckGooseUser user)
    {
        Id = user.Id;
        UserName = user.UserName;
    }
}
