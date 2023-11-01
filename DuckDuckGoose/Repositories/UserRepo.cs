using DuckDuckGoose.Areas.Identity.Data;
using DuckDuckGoose.Models;
using DuckDuckGoose.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace DuckDuckGoose.Repositories;

public interface IUserRepo
{
    public Pagination<DuckDuckGooseUser> GetUsers(GetUsersRequest request);
    public DuckDuckGooseUser GetUserById(string id);
}

public class UserRepo : IUserRepo
{
    private DuckDuckGooseIdentityDbContext _context;

    public UserRepo(DuckDuckGooseIdentityDbContext context)
    {
        _context = context;
    }

    public Pagination<DuckDuckGooseUser> GetUsers(GetUsersRequest request)
    {
        IQueryable<DuckDuckGooseUser> filteredUsers = _context.Users
            .OrderBy(u => u.UserName)
            .Include(u => u.Honks);
        if (request.Search is not null)
        {
            filteredUsers = filteredUsers
                .Where(user => user.UserName.Contains(request.Search));
        }
    

        return Pagination<DuckDuckGooseUser>.Paginate(filteredUsers, request.PageNumber.HasValue ? request.PageNumber.Value : 1, 5);
    }

    public DuckDuckGooseUser GetUserById(string id)
    {
        try
        {
            return _context.Users
                .Include(u => u.Honks)
                .Single(u => u.Id == id);
        }
        catch (InvalidOperationException e)
        {
            throw new ArgumentOutOfRangeException($"A user was not found with id {id}", e);
        }
    }
}
