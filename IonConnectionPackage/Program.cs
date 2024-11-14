using InforIonClientLibrary;
using IonConnectionPackage.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure Infor ION services
builder.Services.AddHttpClient();
builder.Services.AddSingleton(sp =>
{
    var httpClient = sp.GetRequiredService<HttpClient>();
    var configPath = "ionapi/IonConnectionPackage.ionapi"; // Path to the configuration file
    return ConnectionOptions.FromIonApiFile(configPath);
});
builder.Services.AddSingleton<ITokenService, TokenService>();

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
