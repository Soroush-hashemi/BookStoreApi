namespace BookStore.Domain.ValueObjects;

public class Metadata
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Keyword { get; set; }
    public string? CanonicalUrl { get; set; }
    public string? IndexPage { get; set; }
    public string? OgTitle { get; set; }
    public string? OgDescription { get; set; }
    public string? OgUrl { get; set; }
    public string? OgImage { get; set; }
}