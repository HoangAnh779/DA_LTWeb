
using Bai3.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var connectionString =
builder.Configuration.GetConnectionString("WebsiteBanHangConnection");
builder.Services.AddDbContext<WebsiteBanHangContext>(options => options.UseSqlServer(connectionString));
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
AddCookie(options =>
{
    options.Cookie.Name = "PetStoreCookie";
    options.LoginPath = "/User/Login";
});

builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "trang-chu",
        pattern: "trang-chu",
        defaults: new { controller = "Home", action = "Index" });
    endpoints.MapControllerRoute(
        name: "ve-chung-toi",
        pattern: "ve-chung-toi",
        defaults: new { controller = "Blog", action = "Index" });
    endpoints.MapControllerRoute(
         name: "san-pham",
         pattern: "san-pham",
         defaults: new { controller = "Product", action = "Index2" });
    endpoints.MapControllerRoute(
         name: "thuc-uong",
         pattern: "thuc-uong",
         defaults: new { controller = "Product", action = "Index" });
    endpoints.MapControllerRoute(
        name: "lien-he",
        pattern: "lien-he",
        defaults: new { controller = "Contact", action = "Index" });
    endpoints.MapControllerRoute(
         name: "khuyen-mai",
         pattern: "khuyen-mai",
         defaults: new { controller = "Promotion", action = "Index" });
    endpoints.MapControllerRoute(
         name: "mon-moi",
         pattern: "mon-moi",
         defaults: new { controller = "New", action = "Index" });
    endpoints.MapControllerRoute(
        name: "dang-ky",
        pattern: "dang-ky",
        defaults: new { controller = "User", action = "Register" });
    endpoints.MapControllerRoute(
        name: "dang-nhap",
        pattern: "dang-nhap",
        defaults: new { controller = "User", action = "Login" });
    endpoints.MapControllerRoute(
        name: "thong-tin",
        pattern: "thong-tin",
        defaults: new { controller = "User", action = "Info" });
    endpoints.MapControllerRoute(
        name: "dang-xuat",
        pattern: "dang-xuat",
        defaults: new { controller = "User", action = "Logout" });
    endpoints.MapControllerRoute(
        name: "gio-hang",
        pattern: "gio-hang",
        defaults: new { controller = "Cart", action = "Index" });
    endpoints.MapControllerRoute(
         name: "up-date",
         pattern: "up-date",
         defaults: new { controller = "User", action = "Update" });
    endpoints.MapControllerRoute(
        name: "them-gio-hang",
        pattern: "them-gio-hang",
        defaults: new { controller = "Cart", action = "AddItem" });
    endpoints.MapControllerRoute(
        name: "thanh-toan",
        pattern: "thanh-toan",
        defaults: new { controller = "Cart", action = "Payment" });
    endpoints.MapControllerRoute(
        name: "hoan-thanh",
        pattern: "hoan-thanh",
        defaults: new { controller = "Cart", action = "Success" });
    endpoints.MapControllerRoute(
         name: "the-loai-san-pham",
         pattern: "{slug}-{id}",
         defaults: new { controller = "Product", action = "CateProd" });
    endpoints.MapControllerRoute(
         name: "chi-tiet-san-pham",
         pattern: "san-pham/{slug}-{id}",
         defaults: new { controller = "Product", action = "ProdDetail" });
    endpoints.MapControllerRoute(
         name: "chuong-trinh",
         pattern: "chuong-trinh/{slug}",
         defaults: new { controller = "Product", action = "Index" });
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

});
app.Run();