using DuckDuckGoose.Areas.Identity.Data;
using DuckDuckGoose.Models;
using DuckDuckGoose.Models.Database;
using DuckDuckGoose.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace DuckDuckGoose.Repositories;

public interface IUserRepo
{
    public IEnumerable<DuckDuckGooseUser> GetAllUsers();
    public DuckDuckGooseUser GetUserById(string id);
    public void Follow(string followerId, string followeeId);
    public void Unfollow(string followerId, string followeeId);
    public Pagination<DuckDuckGooseUser> GetUsers(GetUsersRequest request);
}

public class UserRepo : IUserRepo
{
    private DuckDuckGooseIdentityDbContext _context;

    public UserRepo(DuckDuckGooseIdentityDbContext context)
    {
        _context = context;
    }

    public void Follow(string followerId, string followeeId)
    {
        DuckDuckGooseUser follower, followee;
        try
        {
            follower = GetUserById(followerId);
        }
        catch (ArgumentOutOfRangeException e)
        {
            throw new ArgumentOutOfRangeException($"Could not find follower with id {followerId}", e);
        }

        try
        {
            followee = GetUserById(followeeId);
        }
        catch (ArgumentOutOfRangeException e)
        {
            throw new ArgumentOutOfRangeException($"Could not find user to follow with id {followeeId}", e);
        }

        followee.Followers.Append(follower);
        _context.SaveChanges();
    }

    public IEnumerable<DuckDuckGooseUser> GetAllUsers()
    {
        return _context.Users;
    }

    public DuckDuckGooseUser GetUserById(string id)
    {
        try
        {
            return _context.Users
                .Include(u => u.Followers)
                .Include(u => u.Honks)
                .Single(u => u.Id == id);
        }
        catch (InvalidOperationException e)
        {
            throw new ArgumentOutOfRangeException($"A user was not found with id {id}", e);
        }
    }

    public void Unfollow(string followerId, string followeeId)
    {
        DuckDuckGooseUser follower, followee;
        try
        {
            follower = GetUserById(followerId);
        }
        catch (ArgumentOutOfRangeException e)
        {
            throw new ArgumentOutOfRangeException($"Could not find follower with id {followerId}", e);
        }

        try
        {
            followee = GetUserById(followeeId);
        }
        catch (ArgumentOutOfRangeException e)
        {
            throw new ArgumentOutOfRangeException($"Could not find user to follow with id {followeeId}", e);
        }

        followee.Followers = followee.Followers.Where(u => u != follower);
        _context.SaveChanges();
    }

    public Pagination<DuckDuckGooseUser> GetUsers(GetUsersRequest request)
    {
        IQueryable<DuckDuckGooseUser> filteredUsers = _context.Users
            .Include(u => u.Followers)
            .Include(u => u.Follows)
            .Include(u => u.Honks);
        if (request.Search is not null)
        {
            filteredUsers = filteredUsers
                .Where(user => user.UserName.Contains(request.Search));
        }

        return Pagination<DuckDuckGooseUser>.Paginate(filteredUsers, request.Page.HasValue ? request.Page.Value : 1, 5);
    }
}
