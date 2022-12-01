using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models.Repositories;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PizzaDbContextConnection");builder.Services.AddDbContext<PizzaDbContext>(options =>
    options.UseSqlServer(connectionString));builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<PizzaDbContext>();
// Add services to the container.

//builder.Services.AddDbContext<PizzaDbContext>();
builder.Services.AddScoped<IDbPizzaRepository, DbPizzaRepository>();
builder.Services.AddScoped<IDbCategoryRepository, DbCategoryRepository>();
builder.Services.AddScoped<IDbCommentRepository, DbCommentRepository>();
builder.Services.AddScoped<IDbIngredientRepository, DbIngredientRepository>();
builder.Services.AddScoped<IDbMessageRepository, DbMessageRepository>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Guest}/{action=Index}/{id?}");

app.Run();
