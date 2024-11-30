using Scalar.AspNetCore;
using VerveClothingApi.Data.Repositories;
using VerveClothingApi.Interfaces;
using VerveClothingApi.Mappings;
using VerveClothingApi.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using VerveClothingApi.Validators;
using Microsoft.EntityFrameworkCore;
using VerveClothingApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add database context
builder.Services.AddDbContext<VerveClothingApi.Data.ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add Memory Cache
builder.Services.AddMemoryCache();

// Add services to the container.

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IWishlistItemRepository, WishlistItemRepository>();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
builder.Services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();

// Register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductVariantService, ProductVariantService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IWishlistItemService, WishlistItemService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IInventoryItemService, InventoryItemService>();
//

// Add FluentValidation validators
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateProductValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCategoryDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCustomExceptionHandler();

app.Run();
