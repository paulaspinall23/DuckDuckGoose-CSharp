namespace DuckDuckGoose.Models.Requests;

public class GetHonksRequest
{
    public int? PageNumber { get; set; }
    public string? Filter { get; set; }
    public string? Search { get; set; }
}
