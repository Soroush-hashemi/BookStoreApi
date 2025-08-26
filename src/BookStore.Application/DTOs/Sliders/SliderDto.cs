using System;

namespace BookStore.Application.DTOs.Sliders;

public class SliderDto
{
    public SliderDto(string link, string image, bool isActive)
    {
        Link = link;
        Image = image;
        IsActive = isActive;
    }

    public string Link { get; set; }
    public string Image { get; set; }
    public bool IsActive { get; set; }
}