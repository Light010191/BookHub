using BooksHub.Models;
using Microsoft.EntityFrameworkCore;
using BooksHub.Models.Identity;
using Microsoft.AspNetCore.Identity;
using BooksHub.Services;
using BooksHub.Filtres;
using BooksHub.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserDbContext>(options =>
{
    var connString = builder.Configuration.GetConnectionString("UserBookHub");
    options.UseSqlServer(connString);
});

builder.Services.AddDbContext<BookDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("BookHub"));
});



builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<UserDbContext>().AddDefaultTokenProviders();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<AuthorizeFilter>();
builder.Services.AddScoped<MailSenderService>();
builder.Services.ConfigureApplicationCookie(c =>
{
    //  c.LoginPath = "/Home/Privacy";

});


builder.Services.AddControllersWithViews();

var app = builder.Build();

//BookDbInitializer.seed(app);


if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseStaticFiles();

app.UseMiddleware<KeyMiddleware>();
app.UseMiddleware<AuthMiddleware>();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "admin",
	pattern: "{area}/{controller=Home}/{action=Index}");

app.MapControllerRoute(
    name: "user",
    pattern: "{area}/{controller=Home}/{action=Index}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
