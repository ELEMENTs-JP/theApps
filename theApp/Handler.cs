using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using NLog;
using NLog.Web;
using theApp.Components;
using theDatabase;
using theInfrastructure;

namespace theApp
{
    public static class Handler
    {
        public static void Init(string[] args)
        {
            // nLog 
            var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("Anwendung wird gestartet");
            logger.Error("Testweiser Fehler produziert");

            try
            {
                // Builder 
                var builder = WebApplication.CreateBuilder(args);

                // Preparation 
                var filesPath = Path.Combine(builder.Environment.ContentRootPath, "FILES");
                if (!Directory.Exists(filesPath))
                {
                    Directory.CreateDirectory(filesPath);
                }

                // NLog als Logging-Provider hinzufügen
                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

                // Add services to the container.
                builder.Services.AddRazorComponents()
                    .AddInteractiveServerComponents();



                builder.Services.AddServerSideBlazor()
                    .AddHubOptions(options =>
                    {
                        // für Copy Paste Dateiupload 
                        options.MaximumReceiveMessageSize = 30 * 1024 * 1024; // 10 MB Limit
                    })
                    .AddCircuitOptions(options =>
                    {
                            // Detail Informatiopnen bei rekursiven Fehlern 
                        if (builder.Environment.IsDevelopment()) //Only add details when debugging.
                        {
                            options.DetailedErrors = true;
                        }
                    });

                // Controllers für Security API 
                builder.Services.AddControllers();


                // Messaging Bus Service 
                builder.Services.AddScoped<IMessagingBusService, MessagingBusService>();

                // Wichtig: MUSS Singleton sein, damit alle User dieselbe Instanz teilen
                builder.Services.AddSingleton<INotificationService, NotificationService>();

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

                // Language Service 
                builder.Services.AddScoped<ILocalizationService>(provider =>
                {
                    // Abruf des WebHostEnvironment aus dem DI-Container
                    var environment = provider.GetRequiredService<IWebHostEnvironment>();
                    string rootPath = environment.ContentRootPath;

                    // Manuelle Instanziierung und Übergabe 
                    return new LocalizationService(environment, new SQLiteService(rootPath));
                });

                // Security Service 
                builder.Services.AddScoped<ISecurityService>(provider =>
                {
                    // Abruf des WebHostEnvironment aus dem DI-Container
                    var environment = provider.GetRequiredService<IWebHostEnvironment>();
                    string rootPath = environment.ContentRootPath;

                    // Manuelle Instanziierung und Übergabe 
                    return new SecurityService(environment, new SQLiteService(rootPath));
                });

                // App Service 
                builder.Services.AddScoped<IAppService>(provider =>
                {
                    // Abruf des WebHostEnvironment aus dem DI-Container
                    var environment = provider.GetRequiredService<IWebHostEnvironment>();
                    string rootPath = environment.ContentRootPath;

                    // Manuelle Instanziierung und Übergabe 
                    return new AppService(environment, new SQLiteService(rootPath));
                });

                // Search Service 
                builder.Services.AddScoped<ISearchService>(provider =>
                {
                    // Abruf des WebHostEnvironment aus dem DI-Container
                    var environment = provider.GetRequiredService<IWebHostEnvironment>();
                    string rootPath = environment.ContentRootPath;

                    // Manuelle Instanziierung und Übergabe 
                    return new SearchService(environment, new SQLiteService(rootPath));
                });

                // A valid antiforgery token was not provided with the request. Add an antiforgery token, or disable antiforgery validation for this endpoint.

                // --- 1. Cookie-Authentifizierung konfigurieren
                builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.Cookie.Name = "SecureAppAuthCookie";
                        options.LoginPath = "/Login"; // Wohin bei @attribute [Authorize]

                        // DYNAMISCHE ANPASSUNG:
                        // Im Development-Mode erlauben wir Cookies über HTTP.
                        // In Produktion erzwingen wir Secure (HTTPS).
                        options.Cookie.SecurePolicy = builder.Environment.IsDevelopment()
                            ? CookieSecurePolicy.SameAsRequest
                            : CookieSecurePolicy.Always;

                        options.Cookie.HttpOnly = true;
                        options.ExpireTimeSpan = TimeSpan.FromHours(12);
                        options.SlidingExpiration = true; // Erneuert das Cookie bei Aktivität
                    });

                builder.Services.AddHttpContextAccessor(); // Wichtig für den Zugriff auf den User
                                                           // --- 1. Cookie-Authentifizierung konfigurieren

                builder.Services.AddCascadingAuthenticationState();





                // App 
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

              

    
                // Ergänzung für Audio-Formate (falls spezifische Extensions genutzt werden)
                var contentTypeProvider = new FileExtensionContentTypeProvider();
                contentTypeProvider.Mappings[".m4a"] = "audio/mp4";
                contentTypeProvider.Mappings[".mp3"] = "audio/mpeg";
                contentTypeProvider.Mappings[".wav"] = "audio/wav";
                contentTypeProvider.Mappings[".ogg"] = "audio/ogg";
                contentTypeProvider.Mappings[".opus"] = "audio/opus";
                contentTypeProvider.Mappings[".aac"] = "audio/aac";
                contentTypeProvider.Mappings[".flac"] = "audio/flac";

                // Wird für Audio Player benötigt !!! 
                app.UseStaticFiles(new StaticFileOptions
                {
                    // FILES Verzeichnis muss in Visual Studio existieren 
                    // Wird für Audio Player benötigt !!! 
                    FileProvider = new PhysicalFileProvider(filesPath),
                    RequestPath = "/MEDIA",
                    ContentTypeProvider = contentTypeProvider,
                    ServeUnknownFileTypes = false
                });

                app.UseStaticFiles(); // notwendig für CSS und JS Dateideployment in den Razor Class Libraries 
                app.MapStaticAssets(); // Optimiert alle statischen Dateien, die beim Build existierten

                app.UseRouting();

                app.UseAuthentication();
                app.UseAuthorization();

                app.UseAntiforgery();

                app.MapRazorComponents<App>()
                   .AddInteractiveServerRenderMode()
                   .AddAdditionalAssemblies(typeof(theDatabase.Controls.List).Assembly,
                                                typeof(theComponents.Pages.Item_Page).Assembly,
                                                typeof(theControls.Edit.EditBox).Assembly);

          

                // 2. Controller-Routen nach app.Build() mappen:
                app.MapControllers();

                // Videostreaming 
                app.MapGet("/stream/{**path}", (string path, IWebHostEnvironment env) =>
                {
                    var filePath = Path.Combine(env.ContentRootPath, "FILES", path);

                    if (!System.IO.File.Exists(filePath))
                    {
                        return Results.NotFound();
                    }

                    return Results.File(
                        filePath,
                        "video/mp4",
                        enableRangeProcessing: true);
                });

                app.Run();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Anwendung aufgrund einer Exception gestoppt");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }

        }
    }
}
