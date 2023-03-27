using DuckDuckGoose.Areas.Identity.Data;
using DuckDuckGoose.Models;
using DuckDuckGoose.Models.Database;
using DuckDuckGoose.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace DuckDuckGoose.Repositories;

public interface IHonkRepo
{
}

public class HonkRepo : IHonkRepo
{
    private DuckDuckGooseIdentityDbContext _context;

    public HonkRepo(DuckDuckGooseIdentityDbContext context)
    {
        _context = context;
    }
}
