using DuckDuckGoose.Areas.Identity.Data;

namespace DuckDuckGoose.Models.Database;

public class Honk
{
    public int Id { get; set; }
    public DuckDuckGooseUser User { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
}
