using theApp.Components;
using theDatabase;
using theInfrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


// Database Service 
builder.Services.AddScoped<ISqlDatabaseService>(provider =>
{
    // Abruf des WebHostEnvironment aus dem DI-Container
    var environment = provider.GetRequiredService<IWebHostEnvironment>();

    // Auslesen des ContentRootPath
    string rootPath = environment.ContentRootPath;

    // Manuelle Instanziierung und Übergabe des Pfads
    return new SQLiteService(rootPath);
});



// App Service 
builder.Services.AddScoped<IAppService>(provider =>
{
    // Abruf des WebHostEnvironment aus dem DI-Container
    var environment = provider.GetRequiredService<IWebHostEnvironment>();

    // Manuelle Instanziierung und Übergabe 
    return new AppService(environment);
});

// Messaging Bus Service 
builder.Services.AddScoped<IMessagingBusService, MessagingBusService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode()
   .AddAdditionalAssemblies(typeof(theDatabase.Controls.DatabaseSetup).Assembly, 
                                typeof(theControls.Edit.EditBox).Assembly); 

app.Run();
