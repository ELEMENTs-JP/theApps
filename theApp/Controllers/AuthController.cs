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
    public async Task<IActionResult> Login([FromForm] string username, [FromForm] string password, [FromForm] bool remember)
    {
        // Password 
        if (string.IsNullOrEmpty(password))
        {
            return Redirect("/login?error=true");
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

        IDTO? user = result.Items.Where(se => se["Mail"].ToSecureString() == username
                                          && se["Password"].ToSecureString() == password).FirstOrDefault();

        // User nicht vorhanden 
        if (user == null)
        {
            return Redirect("/login?error=true");
        }

        if (user != null)
        {
            // Nutzer zuordnen falls nicht zugeordnet 
            await AssignToPrincipal(user);

            // Einloggen 
            await SignIn(username, user.GUID, remember);
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

            // Nutzer zuordnen falls nicht zugeordnet 
            await AssignToPrincipal(newUser);

            // SignIn 
            await SignIn(username, newUser.GUID, false);

            return LocalRedirect("/");
        }

        return Redirect("/login?error=true");
    }

    [HttpPost("setup")]
    public async Task<IActionResult> Setup([FromForm] string principal, [FromForm] string username, [FromForm] string password)
    {
        // Principal 
        if (string.IsNullOrEmpty(principal))
        {
            return Redirect("/setup?error=true");

        }
        // Password 
        if (string.IsNullOrEmpty(password))
        {
            return Redirect("/setup?error=true");
        }

        // Mail Format 
        if (username.IsMailFormat() == false)
        {
            return Redirect("/setup?error=true");
        }

        IQueryParameter query = new QueryParameter();
        query.Matchcode = string.Empty;
        query.MasterGUID = SQLiteService.GeneralMasterGUID;
        query.ItemType = "Principal";
        IQueryResult result = await sqlService.GetItems(query);

  

        return Redirect("/setup?error=true");
    }



    [HttpPost("requestpwd")]
    public async Task<IActionResult> RequestNewPassword([FromForm] string username)
    {
        // Mail Format 
        if (username.IsMailFormat() == false)
        {
            return Redirect("/setup?error=true");
        }

        IQueryParameter query = new QueryParameter();
        query.Matchcode = string.Empty;
        query.MasterGUID = SQLiteService.GeneralMasterGUID;
        query.ItemType = "Principal";
        IQueryResult result = await sqlService.GetItems(query);



        return Redirect("/setup?error=true");
    }


    [HttpPost("setnewpwd")]
    public async Task<IActionResult> SetNewPassword([FromForm] string username)
    {
        // Mail Format 
        if (username.IsMailFormat() == false)
        {
            return Redirect("/setup?error=true");
        }

        IQueryParameter query = new QueryParameter();
        query.Matchcode = string.Empty;
        query.MasterGUID = SQLiteService.GeneralMasterGUID;
        query.ItemType = "Principal";
        IQueryResult result = await sqlService.GetItems(query);



        return Redirect("/setup?error=true");
    }

    [HttpPost("requestaccess")]
    public async Task<IActionResult> RequestAccess([FromForm] string username)
    {
        // Mail Format 
        if (username.IsMailFormat() == false)
        {
            return Redirect("/setup?error=true");
        }

        IQueryParameter query = new QueryParameter();
        query.Matchcode = string.Empty;
        query.MasterGUID = SQLiteService.GeneralMasterGUID;
        query.ItemType = "Principal";
        IQueryResult result = await sqlService.GetItems(query);



        return Redirect("/setup?error=true");
    }
    



    private async Task AssignToPrincipal(IDTO user)
    {
        // Check 
        if (user == null)
            return;

        // Query: Principal 
        IQueryParameter query = new QueryParameter();
        query.Matchcode = string.Empty;
        query.MasterGUID = SQLiteService.GeneralMasterGUID;
        query.ItemType = "Principal";

        // Suche 
        IQueryResult pResult = await sqlService.GetItems(query);

        // Principal filtern 
        IDTO principal = pResult.Items.Where(se => 
                            se.GUID == SQLiteService.GeneralMasterGUID && 
                            se.MasterGUID == SQLiteService.GeneralMasterGUID).FirstOrDefault();
        
        // prüfen ob existiert : falls NEIN 
        if (principal == null)
        {
            // Create : Principal erstellen 
            IQueryParameter c = new QueryParameter();
            c.MasterGUID = SQLiteService.GeneralMasterGUID;
            c.GUID = SQLiteService.GeneralMasterGUID;
            c.ItemType = "Principal";
            c.Title = "Default";
            IQueryResult newResult = await sqlService.Create(c);

            principal = newResult.Items.Where(se =>
                            se.GUID == SQLiteService.GeneralMasterGUID &&
                            se.MasterGUID == SQLiteService.GeneralMasterGUID).FirstOrDefault();
        }

        if (principal == null)
            return;

        // nach zugeordneten User suchen 
        IQueryResult userListResult = await sqlService.GetRelatedItems(principal, "User");
        IDTO findUser = userListResult.Items.Where(se => se.GUID == user.GUID).FirstOrDefault();
        
        // wenn kein Nutzer zugeordnet ist 
        if (findUser == null)
        { 
            // zuordnen 
            await sqlService.Assign(principal, user);
        }
    }
    private async Task SignIn(string username, Guid userGUID, bool remember)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("SecureToken", userGUID.ToSecureString())
            };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties 
        { 
            IsPersistent = remember,

            ExpiresUtc = remember
                ? DateTimeOffset.UtcNow.AddDays(1)
                : DateTimeOffset.UtcNow.AddHours(3)

        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }
}