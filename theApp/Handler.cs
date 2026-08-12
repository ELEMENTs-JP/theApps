using Microsoft.AspNetCore.Authentication.Cookies;
using theApp.Components;
using theDatabase;
using theInfrastructure;

namespace theApp
{
    public static class Handler
    {
        public static void Init(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // Detail Informatiopnen bei rekursiven Fehlern 
            builder.Services.AddServerSideBlazor()
                .AddCircuitOptions(options =>
                {
                    if (builder.Environment.IsDevelopment()) //Only add details when debugging.
                    {
                        options.DetailedErrors = true;
                    }
                });

            // Controllers für Security API 
            builder.Services.AddControllers();

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
                });

            builder.Services.AddHttpContextAccessor(); // Wichtig für den Zugriff auf den User
                                                       // --- 1. Cookie-Authentifizierung konfigurieren

            builder.Services.AddCascadingAuthenticationState();




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
                                            typeof(theComponents.Pages.Item_Page).Assembly,
                                            typeof(theControls.Edit.EditBox).Assembly);

            app.UseAuthentication();
            app.UseAuthorization();

            // 2. Controller-Routen nach app.Build() mappen:
            app.MapControllers();

            app.Run();

        }
    }
}
