using DuckDuckGoose.Areas.Identity.Data;
using DuckDuckGoose.Models;
using DuckDuckGoose.Models.Database;
using DuckDuckGoose.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace DuckDuckGoose.Repositories;

public interface IUserRepo
{
}

public class UserRepo : IUserRepo
{
    private DuckDuckGooseIdentityDbContext _context;

    public UserRepo(DuckDuckGooseIdentityDbContext context)
    {
        _context = context;
    }
}
