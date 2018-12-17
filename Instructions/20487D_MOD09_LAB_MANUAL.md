# Module 09: Securing services on-premises and in Microsoft Azure

# Lab: Using ASP.NET Core Identity

### Exercise 1: Add ASP.NET Core Identity middleware

#### Task 1: Add ASP.NET Core Identity NuGet

1.  Open **Command Line** and navigate to **[Repository Root]\Allfiles\Mod09\LabFiles\Lab1\Starter** folder.
2.  Create a new Web API Core project by using the command prompt with the **dotnet** tool.
3.  Install the **Microsoft.EntityFrameworkCore.Sqlite --version 2.1.4** NuGet package by using the command prompt with the **dotnet** tool.

#### Task 2: Create a new DbInitializer with seed for users and groups

1.  Create **DbInitializer** class, with **seed** method which add users to the **DB**.
    ```cs
    private static void Seed(ApplicationDbContext context)
    {
        // Create list with dummy users 
        List<IdentityUser> userList = new List<IdentityUser>
        {
            new IdentityUser("JonDue") { Email = "jond@outlook.com", PasswordHash = Convert.ToBase64String(Encoding.ASCII.GetBytes("password1234"))},
        };
        context.Users.AddRange(userList);
        context.SaveChanges();
    }
    ```
2.  Then add **Initialize** method that call the **Seed** method.
    ```cs
    public static void Initialize(ApplicationDbContext context)
    {
        if (context.Database.EnsureCreated())
        {
            // Code to create initial data
            Seed(context);
        }
    }
    ```
3.  Create **ApplicationDbContext** class, which inherit from **IdentityDbContext** and initialize the **DbInitializer** at the constructor.
    ```cs
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) 
        : base(option)
    {
        DbInitializer.Initialize(this);
    }
    ```

#### Task 3: Register ASP.NET Core Identity in the startup file

1.  In **Startup.cs** file at **ConfigureServices** method configure the authentication service.
    ```cs
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(
            Configuration.GetConnectionString("DefaultConnection")));
    services.AddDefaultIdentity<IdentityUser>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders();

    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    })
    .AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = Configuration["JwtIssuer"],
            ValidAudience = Configuration["JwtIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
            ClockSkew = TimeSpan.Zero // remove delay of token when expire
        };
    });
    ```
2.  In **Configure** method add **app.UseAuthentication();** call.
3.  Then in **appsettings.json** file, add **connection string** and **JWT** configuration.
    ```json
    "ConnectionStrings": {
    "DefaultConnection": "DataSource=app.db"
    },
    "JwtKey": "1234567891011121314151617181920",
    "JwtIssuer": "http://localhost.com",
    "JwtExpireDays": 30,
    ```

### Exercise 2: Add authorization code

#### Task 1: Add user controller

1.  Create new **Models** folder.
2.  In **Model** folder, create **LoginDto** class with **Username** and **Password** property and add the **[Required]** attribute.
3.  In **Model** folder, create **RegisterDto** class with **Username** and **Password** property and add the **[Required]** and **[StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]** to **Password** property.
4.  Then in **Controllers** folder, add **UserController** class.
5.  In **UserController** constructor initialze **UserManager, SignInManager** and **IConfiguration**.
6.  Create **GenerateJwtToken** method which generate token on **signin** and **signup**
    ```cs
    List<Claim> claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id)
    };

    SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes  (_configuration["JwtKey"]));
    SigningCredentials creds = new SigningCredentials(key,  SecurityAlgorithms.HmacSha256);
    DateTime expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration ["JwtExpireDays"]));

    JwtSecurityToken token = new JwtSecurityToken(
        _configuration["JwtIssuer"],
        _configuration["JwtIssuer"],
        claims,
        expires: expires,
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
    ```
7.  Create **Register** action thet receive **RegisterDto** model, return new **Token** if the action secceded.
8.  Create **Login** action thet receive **LoginDto** model, return new **Token** if the **username** and **Password** which provided are correct.

#### Task 2: Add authorization attributes to value controller

1.  In **ValuesController.cs** class, on the the **Get** action add the **[Authorize]** attribute.

### Exercise 3: Run a client application to test the server

#### Task 1: Examine client code to retrieve access token

1.  In **[Repository Root]\Allfiles\Mod09\LabFiles\Lab1\Starter** folder, open **IdentityClient** on **Microsoft Visual Studio Code**
2.  Open **Program.cs** file.
3.  Look at the **Register** and **Login** call using the **HttpClient** class.

#### Task 2: Run client code and inspect user claims in the service

1.  Switch to Command Prompt window.
2.  Using the **dotnet** tool, run the appliction.
3.  Open new command Prompt window and navigate to **[Repository Root]\Allfiles\Mod09\LabFiles\Lab1\Starter\IdentityClient**.
4.  Using the **dotnet** tool, run the appliction.
5.  Verify that **[value1, value2]** return back to your Command Prompt.

©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.