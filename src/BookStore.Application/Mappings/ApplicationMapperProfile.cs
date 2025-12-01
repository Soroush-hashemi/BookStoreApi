using AutoMapper;
using BookStore.Application.DTOs.Books;
using BookStore.Application.DTOs.Categories;
using BookStore.Application.DTOs.Comments;
using BookStore.Application.DTOs.Sliders;
using BookStore.Application.DTOs.ValueObjects;
using BookStore.Application.DTOs.Carts;
using BookStore.Domain.Entities.Books;
using BookStore.Domain.Entities.Category;
using BookStore.Domain.Entities.Comments;
using BookStore.Domain.Entities.Slider;
using BookStore.Domain.Entities.ValueObjects;
using BookStore.Domain.Entities.Carts;

namespace BookStore.Application.Mappings;

public class ApplicationMapperProfile : Profile
{
    public ApplicationMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<Book, BookUpdateDto>();
        CreateMap<Metadata, MetadataDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategoryUpdateDto>();
        CreateMap<Comment, CommentDto>();
        CreateMap<Slider, SliderDto>();
        CreateMap<CartItem, CartItemDto>();
        CreateMap<Cart, CartDto>();
    }
}