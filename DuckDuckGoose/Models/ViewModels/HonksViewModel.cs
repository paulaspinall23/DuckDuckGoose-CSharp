namespace DuckDuckGoose.Models.ViewModels;

public class HonksViewModel
{
    public Pagination<HonkViewModel>? Honks { get; set; }
    public string? Filter { get; set; }
    public string? Search { get; set; }
}
