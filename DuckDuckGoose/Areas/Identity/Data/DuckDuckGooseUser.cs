using System.ComponentModel.DataAnnotations.Schema;
using DuckDuckGoose.Models.Database;
using Microsoft.AspNetCore.Identity;

namespace DuckDuckGoose.Areas.Identity.Data;

// Add profile data for application users by adding properties to the DuckDuckGooseUser class
public class DuckDuckGooseUser : IdentityUser
{
    [InverseProperty("Follows")]
    public IEnumerable<DuckDuckGooseUser> Followers { get; set; }
    
    [InverseProperty("Followers")]
    public IEnumerable<DuckDuckGooseUser> Follows { get; set; }
    
    public IEnumerable<Honk> Honks { get; set; }
}

