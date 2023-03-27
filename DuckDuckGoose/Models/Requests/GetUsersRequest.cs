namespace DuckDuckGoose.Models.Requests;

public class GetUsersRequest
{
    public int? PageNumber { get; set; }
    public string? Filter { get; set; }
    public string? Search { get; set; }
}
