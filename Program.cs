using ALEXforums.Data;
using ALEXforums.Middleware;
using ALEXforums.Services.Hash;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddSingleton<IHashService, SHA256HashService>();

// Add Data Context
builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("PlanetDb"),
        new MySqlServerVersion(new Version(8, 0, 23)),
        serverOptions =>
            serverOptions
                .MigrationsHistoryTable(
                    tableName: HistoryRepository.DefaultTableName,
                    schema: "ALEXforums")
                .SchemaBehavior(
                    MySqlSchemaBehavior.Translate,
                    (schema, table) => $"{schema}_{table}")
));


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



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

app.UseSession();

app.UseSessionAuth();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Main}/{id?}");

app.Run();
