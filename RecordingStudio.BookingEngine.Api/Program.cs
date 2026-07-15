using Microsoft.EntityFrameworkCore;
using RecordingStudio.BookingEngine.Api.Components;
using RecordingStudio.BookingEngine.Api.Hubs;
using RecordingStudio.BookingEngine.Core.Interfaces;
using RecordingStudio.BookingEngine.Core.Services;
using RecordingStudio.BookingEngine.Infrastructure.Data;
using RecordingStudio.BookingEngine.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSignalR();

// Blazor (interactive server) UI
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<BookingEngineDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IStudioRepository, StudioRepository>();
builder.Services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
builder.Services.AddScoped<IStudioClosureRepository, StudioClosureRepository>();

// Domain services
builder.Services.AddScoped<BookingValidator>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<StudioService>();
builder.Services.AddScoped<ClosureService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.UseAntiforgery();

app.MapControllers();
app.MapHub<BookingHub>("/hubs/booking");
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
