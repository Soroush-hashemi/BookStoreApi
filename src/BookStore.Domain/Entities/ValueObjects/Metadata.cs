namespace BookStore.Domain.Entities.ValueObjects;

public class Metadata
{
    public string Title { get; }
    public string Description { get; }
    public string Keyword { get; }
    public string CanonicalUrl { get; }
    public string IndexPage { get; }
    public string OgTitle { get; }
    public string OgDescription { get; }
    public string OgUrl { get; }
    public string OgImage { get; }

    public Metadata(
        string? title,
        string? description,
        string? keyword,
        string? canonicalUrl,
        string? indexPage,
        string? ogTitle,
        string? ogDescription,
        string? ogUrl,
        string? ogImage)
    {
        Title = title ?? string.Empty;
        Description = description ?? string.Empty;
        Keyword = keyword ?? string.Empty;
        CanonicalUrl = canonicalUrl ?? string.Empty;
        IndexPage = indexPage ?? string.Empty;
        OgTitle = ogTitle ?? string.Empty;
        OgDescription = ogDescription ?? string.Empty;
        OgUrl = ogUrl ?? string.Empty;
        OgImage = ogImage ?? string.Empty;
    }
}