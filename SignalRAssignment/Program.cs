using Microsoft.EntityFrameworkCore;
using SignalRAssignment.DataAccess;
using SignalRAssignment.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
var config = builder.Configuration;
var connectStr = config.GetConnectionString("SignalRConnectStr");
builder.Services.AddDbContext<SignalRDbContext>(option => option.UseSqlServer(connectStr));
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(60);//You can set Time   
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<SignalRDbContext>();
    context.Database.EnsureCreated();
    //DbInitializer.Initialize(context);
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();
app.MapHub<SignalRServer>("/signalRServer");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
