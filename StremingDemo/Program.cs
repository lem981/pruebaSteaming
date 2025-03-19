using StremingDemo.Components;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(7157, listenOptions =>
    {
        listenOptions.UseHttps("C:\\ProgramData\\certify\\assets\\minetflixilegal.duckdns.org\\20250616_976d6984.pfx", "");
    });
});

builder.Services.AddServerSideBlazor();

builder.Services.AddControllers(); // Register controllers
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazorBootstrap();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.MapControllers(); 

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
