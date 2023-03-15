namespace DuckDuckGoose.Models.Requests;

public class GetUsersRequest
{
    public int? Page { get; set; }
    public string? Filter { get; set; }
    public string? Search { get; set; }
}
