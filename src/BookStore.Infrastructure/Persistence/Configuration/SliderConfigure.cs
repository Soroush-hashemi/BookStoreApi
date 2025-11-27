using BookStore.Domain.Entities.Slider;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Persistence.Configuration;

public class SliderConfigure : IEntityTypeConfiguration<Slider>
{
    public void Configure(EntityTypeBuilder<Slider> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Link).IsRequired();
        builder.Property(p => p.Image).IsRequired();
    }
}