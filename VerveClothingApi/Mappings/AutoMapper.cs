using AutoMapper;
using VerveClothingApi.DTOs;
using VerveClothingApi.Entities;

namespace VerveClothingApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Product mappings
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Generic PagedResult mapping
            CreateMap(typeof(PagedResult<>), typeof(PagedResult<>));

            // Category mappings with recursive child categories
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.ChildCategories, opt => opt.MapFrom(src => src.ChildCategories));
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ProductVariant mappings
            CreateMap<ProductVariant, ProductVariantDto>();
            CreateMap<CreateProductVariantDto, ProductVariant>();
            CreateMap<UpdateProductVariantDto, ProductVariant>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Order mappings
            CreateMap<Order, OrderDto>();
            CreateMap<CreateOrderDto, Order>();
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<CreateOrderItemDto, OrderItem>();

            // Review mappings
            CreateMap<Review, ReviewDto>();
            CreateMap<CreateReviewDto, Review>();
            CreateMap<UpdateReviewDto, Review>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // WishlistItem mappings
            CreateMap<WishlistItem, WishlistItemDto>();
            CreateMap<CreateWishlistItemDto, WishlistItem>();

            // Coupon mappings
            CreateMap<Coupon, CouponDto>();
            CreateMap<CreateCouponDto, Coupon>();
            CreateMap<UpdateCouponDto, Coupon>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // InventoryItem mappings
            CreateMap<InventoryItem, InventoryItemDto>();
            CreateMap<CreateInventoryItemDto, InventoryItem>();
            CreateMap<UpdateInventoryItemDto, InventoryItem>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
