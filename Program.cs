using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Spark.Data;
using Spark.Logic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredUniqueChars = 3;
    options.Password.RequiredLength = 10;
    options.Password.RequireNonAlphanumeric = true;
}
).AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("User", policy => policy.RequireClaim("User"));
    options.AddPolicy("Admin", policy => policy.RequireClaim("Admin"));
    //options.AddPolicy("Staff", policy => policy.RequireClaim("Staff"));
});//For cliams
builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddSession();//Adding sessions functionality
builder.Services.AddScoped<CartActions>();
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
app.UseAuthentication();
app.UseRouting();
app.UseSession();//Adding session functionality
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
