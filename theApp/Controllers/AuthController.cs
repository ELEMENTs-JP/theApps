using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using theDatabase;
using theInfrastructure;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ISqlDatabaseService sqlService;

    public AuthController(ISqlDatabaseService sql)
    {
        sqlService = sql;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] string username, [FromForm] string password)
    {
        // Password 
        if (string.IsNullOrEmpty(password))
        {
            return Redirect("/register?error=true");
        }

        // Mail Format 
        if (username.IsMailFormat() == false)
        {
            return Redirect("/login?error=true");
        }

        IQueryParameter query = new QueryParameter();
        query.Matchcode = string.Empty;
        query.MasterGUID = SQLiteService.GeneralMasterGUID;
        query.ItemType = "User";
        IQueryResult result = await sqlService.GetItems(query);

        IDTO user = result.Items.Where(se => se["Mail"].ToSecureString() == username
                                          && se["Password"].ToSecureString() == password).FirstOrDefault();

        if (user != null)
        {
            await SignIn(username);
            return LocalRedirect("/");
        }

        return Redirect("/login?error=true");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return LocalRedirect("/login");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] string username, [FromForm] string password)
    {
        // Password 
        if (string.IsNullOrEmpty(password))
        {
            return Redirect("/register?error=true");
        }

        // Mail Format 
        if (username.IsMailFormat() == false)
        {
            return Redirect("/register?error=true");
        }

        IQueryParameter query = new QueryParameter();
        query.MasterGUID = SQLiteService.GeneralMasterGUID;
        query.ItemType = "User";
        query.Matchcode = string.Empty;
        IQueryResult result = await sqlService.GetItems(query);

        IDTO user = result.Items.Where(se => se["Mail"].ToSecureString() == username).FirstOrDefault();
        if (user != null)
        {
            // Nutzer existiert bereits 
            return Redirect("/login?error=true");
        }

        // Create 
        query = new QueryParameter();
        query.MasterGUID = SQLiteService.GeneralMasterGUID;
        query.ItemType = "User";
        query.Title = username;
        result = await sqlService.Create(query);

        // Get User 
        result = await sqlService.GetItem(query);

        IDTO newUser = result.Items.FirstOrDefault();
        if (newUser != null)
        {
            // Values 
            newUser["Mail"] = username;
            newUser["Password"] = password;

            // Update 
            await sqlService.Update(newUser);

            // SignIn 
            await SignIn(username);
            return LocalRedirect("/");
        }

        return Redirect("/login?error=true");
    }


    private async Task SignIn(string username)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("SecureToken", "STARK-VERSCHLUESSELTES-TOKEN-123")
            };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties { IsPersistent = true };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }
}