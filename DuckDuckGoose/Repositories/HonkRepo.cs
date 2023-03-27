using DuckDuckGoose.Areas.Identity.Data;
using DuckDuckGoose.Models.Database;

namespace DuckDuckGoose.Repositories;

public interface IHonkRepo
{
    public Honk CreateHonk(string content, string userId);
}

public class HonkRepo : IHonkRepo
{
    private DuckDuckGooseIdentityDbContext _context;

    public HonkRepo(DuckDuckGooseIdentityDbContext context)
    {
        _context = context;
    }

    public Honk CreateHonk(string content, string userId)
    {
        DuckDuckGooseUser user;

        try
        {
            user = _context.Users
                .Single(u => u.Id == userId);
        }
        catch (Exception e)
        {
            throw new ArgumentException($"Cannot find user with id {userId}", e);
        }

        var newHonk = new Honk
        {
            Content = content,
            Timestamp = DateTime.Now.ToUniversalTime(),
            User = user,
        };

        var addedEntity = _context.Honks.Add(newHonk);

        return addedEntity.Entity;
    }
}
