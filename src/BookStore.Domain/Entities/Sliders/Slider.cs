using BookStore.Domain.Common;
using BookStore.Domain.Exceptions;

namespace BookStore.Domain.Entities.Slider;

public class Slider : BaseEntity
{
    public string Link { get; set; }
    public string Image { get; set; }
    public bool IsActive { get; set; }

    public Slider(string link, string image, bool isActive)
    {
        Link = link ?? throw new NullPropertyException("Link is null");
        Image = image ?? throw new NullPropertyException("Image is null");
        IsActive = isActive;
    }

    public void Active()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}