using Microsoft.EntityFrameworkCore;
using SiteRelogios.Controllers;
using SiteRelogios.Data;
using SiteRelogios.Repositories;
using SiteRelogios.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=relogios.db"));

builder.Services.AddScoped<IRelogioRepositorio, RelogioRepositorio>();
builder.Services.AddScoped<RelogioServico>();

var app = builder.Build();

using (var escopo = app.Services.CreateScope())
{
    var contexto = escopo.ServiceProvider.GetRequiredService<AppDbContext>();
    contexto.Database.Migrate();
}

app.MapearRelogioEndpoints();

app.Run();
