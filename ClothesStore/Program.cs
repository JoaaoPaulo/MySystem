using System.Globalization;
using ClothesStore.Data;
using ClothesStore.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=store.db"));

var cultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    if (!context.Products.Any())
    {
         context.Products.AddRange(
            new Product
            {
                Name = "Camiseta Básica",
                Description = "Camiseta de algodão",
                Price = 59.90m,
                ImageUrl = "https://via.placeholder.com/200"
            },
            new Product
            {
                Name = "Calça Jeans",
                Description = "Calça jeans azul",
                Price = 129.90m,
                ImageUrl = "https://via.placeholder.com/200"
            },
            new Product
            {
                Name = "Jaqueta",
                Description = "Jaqueta de inverno",
                Price = 199.90m,
                ImageUrl = "https://via.placeholder.com/200"
            }
        );

        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
