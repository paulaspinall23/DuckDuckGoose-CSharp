namespace DuckDuckGoose.Models.Requests;

public class GetHonksRequest
{
    public int? Page { get; set; }
    public string? Filter { get; set; }
    public string? Search { get; set; }
}
