using Pomelo.EntityFrameworkCore.MySql;// se añade este using **
using Microsoft.EntityFrameworkCore; // se añade este using **
using HospitalRiwi.Data;

var builder = WebApplication.CreateBuilder(args);

//Obtener la cadena de conexión **
var connectionString = builder.Configuration.GetConnectionString("Default");
//Registra AppDbContext como un servicio y lo configura para usar MySQL.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));



// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
