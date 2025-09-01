using Microsoft.EntityFrameworkCore;
using MyTicket.WebApp.Data;
using MyTicket.WebApp.Features.CreateEvent;
using MyTicket.WebApp.Features.ViewCreatedEvents;
using MyTicket.WebApp.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<CreateEventService>();
builder.Services.AddTransient<ViewCreatedEventsService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure DbContext
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();