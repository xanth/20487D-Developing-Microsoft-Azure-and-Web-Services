# Module 2: Extending ASP.NET Core HTTP services

 Wherever you see a path to file starting at [repository root], replace it with the absolute path to the directory in which the 20487 repository resides. 
 e.g. - you cloned or extracted the 20487 repository to C:\Users\John Doe\Downloads\20487, then the following path: [repository root]\AllFiles\20487D\Mod01 will become C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01

Before performing the demonstration, you should allow some time for the provisioning of the different Azure resources required for the demonstration. It is recommended to review the demonstrations before the actual class and identify the resources and then prepare them beforehand to save classroom time.

# Lesson 1: The ASP.NET Core request pipeline

### Demonstration: Creating a middleware for custom error handling

1. Open **Command Line**.
2. Change directory to the starter project, run the following command in the **Command Line**:
    ```bash
    cd [Repository Root]\Allfiles\Mod04\DemoFiles\ErrorHandlingMiddleware\Starter
    ```
3. Restore all dependencies and tools of a project use the following command in the **Command Line**:
    ```base
    dotnet restore
    ```
4. Open the project in **VSCode** and paste the following command and press enter:
    ```bash
    code .
    ```
5. Expand **BlueYonder.Flights.DAL** project and then expand **Repository** folder and select **PassengerRepository** file.
6. Locate **GetPassenger** method and add the following code before the **return** to throw **KeyNotFoundException** exception.
    ```cs
     if (passenger == null)
        throw new KeyNotFoundException();
    ```
7. Right click on **BlueYonder.Flights.Service** and select **New Folder** and name it **Middleware**.
8. Right click on **Middleware** folder and select **New File** and name it **ExceptionHandlingMiddleware** then select the new class.
9. Paste the following **using** to the class.
    ```cs
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    ```
10. Paste the following code to add namespace.
    ```cs
    namespace BlueYonder.Flights.Service.Middleware
    {

    }
    ```
11. Paste the following code in namespace brackets to class declaration:
    ```cs
    public class ExceptionHandlingMiddleware
    {

    }
    ```
12. Paste the following code inside the class brackets to add a constructor:
    ```cs
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    ```
13. Paste the following code to add **Invoke** method that **catch** all the exceptions:
    ```cs
    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
             await _next(httpContext);
        }catch(Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    ```
14. Paste the following code to headle the exception and to add **Status Code**:
    ```cs
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        if (exception is KeyNotFoundException) code = HttpStatusCode.NotFound;
        var result = JsonConvert.SerializeObject(new { error = exception.Message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
    ```
15. Paste the following code outside the class brackets but inside namespace brackets to add **extension method** for **IApplicationBuilder**:
    ```cs
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
    ```
16. Inside **BlueYonder.Flights.Service** project, locate and click on **Startup** file:
17. Paste the following **using**:
    ```cs
    using BlueYonder.Flights.Service.Middleware;
    ```
18. Locate **Configure** method and paste the following code to use **Exception Handling Middleware**:
    ```cs
    app.UseExceptionHandlingMiddleware();
    ```
19. Switch to **Command Line**.
20. Run the following command to run the server:
    ```bash
    dotnet run
    ```
21. Open **Microsoft Edge** browser.
22. Open **Develpper Tools** by click on the three dot on the top bar and then select **Develpper Tools** or by pressing **F12**.
23. In the **Develpper Tools** navigate to **Network**.
24. Navigate to the following url:
    ```url
    https://localhost:5001/api/passenger/5
    ```
25. In **Network** tab locate the url and check the result column that you get **404**


# Lesson 2: Customizing controllers and actions

### Demonstration: Creating asynchronous actions

1. Open **Command Line**.
2. Change directory to the starter project, run the following command in the **Command Line**:
    ```bash
    cd [Repository Root]\Allfiles\Mod04\DemoFiles\AsynchronousActions\Starter
    ```
3. Restore all dependencies and tools of a project use the following command in the **Command Line**:
    ```base
    dotnet restore
    ```
4. Open the project in **VSCode** and paste the following command and press enter:
    ```bash
    code .
    ```
5. Expand **BlueYonder.Flights.Service** project, expand **Controllers** folder and click on **PassengerController** file.
6. Paste the following code inside the class to add asynchronous actions get photo as paramter:
    ```cs
    [HttpPut("UpdatePhoto")]
    public async Task<IActionResult> UpdatePhoto(IFormFile file)
    {
        if (file == null || !file.ContentType.Contains("image"))
            return BadRequest();
            
        if (file.Length > 0)
        {
            using (var fileStream = new FileStream(Path.Combine(_environment.WebRootPath, file.FileName), FileMode.CreateNew))
            {
                await file.CopyToAsync(fileStream);
            }
        }
        return Ok();
    }
    ```
    This method get photo as a paramter and save it in **Image** folder inside **wwwroot** folder.
7. Switch to **Command Line**.
8. Run the following command to change directory to **BlueYonder.Flights.Service**:
   ```bash
   cd BlueYonder.Flights.Service
   ```
9. Run the following command to run the service:
   ```cd
   dotnet run
   ```
10. Open new **Command Line**.
11. Change directory to the **BlueYonder.Flights.Client** project, run the following command in the **Command Line**:
    ```bash
    cd [Repository Root]\Allfiles\Mod04\DemoFiles\AsynchronousActions\Starter\BlueYonder.Flights.Client
    ```
12. Run the following command to run the client application:
   ```cd
   dotnet run
   ```
13. In the console verified the status code is **OK**.
14. Check the following path that you have image inside:
    ```bash
    [Repository Root]\Allfiles\Mod04\DemoFiles\AsynchronousActions\Starter\BlueYonder.Flights.Server\wwwroot\Image
    ```

### Creating custom filters and formatters

1. Open **Command Line**.
2. Change directory to the starter project, run the following command in the **Command Line**:
    ```bash
    cd [Repository Root]\Allfiles\Mod04\DemoFiles\CustomFiltersAndFormatters\Starter\CustomFiltersAndFormatters
    ```
3. Right click on  **CustomFiltersAndFormatters** project, select **New Folder** and name it **Formatter**.
4. Right click on **Formatter** folder, select **New File** and name it **ImageFormatter.cs**.
5. Paste the following code:
    ```cs
    using CustomFiltersAndFormatters.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using System.Threading.Tasks;

    namespace CustomFiltersAndFormatters.Formatter
    {
        public class ImageFormatter : OutputFormatter 
        {
            public ImageFormatter()
            {
                SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("image/png"));
            }

            public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
            {
                HttpResponse response = context.HttpContext.Response;
                Value value = context.Object as Value;
                if(value != null)
                    await response.SendFileAsync((value).Thumbnail);
            }
        }
    }
    ```
    In **WriteResponseBodyAsync** method get **Value** class and take path of the photo and send the photo it salf to the client.
6. Click on **Startup** class and locate the **ConfigureServices** and replace it with the following code:
    ```cs
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc(options =>
        {
            options.OutputFormatters.Insert(0, new ImageFormatter());
        }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    }
    ```
    This code add the new **ImageFormatter** to the **OutputFormatters**.
7. Expand the **Controllers**, select **PassengerController** and veiw the class
    >**Note:** see **GetPhoto** method return **Value** class but the client will get the photo via the **ImageFormatter**.
8. Switch to **Command Line**.
9. Run the following command to run the server:
    ```cd
    dotnet run
    ```
10. Open to **Microsoft Edge** browser.
11. Navigate to the following url:
    ```url
    https://localhost:5001/api/passenger/photo/2
    ```
12. In the browser you should see an image.

#Lesson 3: Injecting Dependencies into Controllers

### Demonstration: Using dependency injection with controllers

1. Open **Command Line**.
2. Change directory to the starter project, run the following command in the **Command Line**:
    ```bash
    cd [Repository Root]\Allfiles\Mod04\DemoFiles\DependencyInjection\Starter
    ```
3. Restore all dependencies and tools of a project use the following command in the **Command Line**:
    ```base
    dotnet restore
    ```
4. Open the project in **VSCode** and paste the following command and press enter:
    ```bash
    code .
    ```
5. Expand **BlueYonder.Flights.DAL** project and then right click on **Repository** folder and select **New File** and name it **IPassengerRepository**.
6. Paste the following code to implement **IPassengerRepository**:
    ```cs
    using BlueYonder.Flights.DAL.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    namespace BlueYonder.Flights.DAL.Repository
    {
        public interface IPassengerRepository
        {
            Task<IEnumerable<Passenger>> GetAllPassengers();
            Task<Passenger> GetPassenger(int passengerId);
            Task<Passenger> Add(Passenger newPassenger);
            Task<Passenger> Update(Passenger passengerToUpdate);
            void Delete(int passengerId);
        }
    }
    ```
7. In the **Repository** folder click on **PassengerRepository** file.
8. Locate class declaration and replace it with the following code to use **IPassengerRepository** interface.
    ```cs
    public class PassengerRepository : IPassengerRepository
    ```
9. Expand the **BlueYonder.Flights.Service** folder and double-click on **Startup.cs** file.
10. Locate **ConfigureServices** method and add the following code to register the repository.
    ```cs
    services.AddTransient<IPassengerRepository, PassengerRepository>();
    ```
11. In the **BlueYonderHotels.Service** project and expand **Controllers** folder and double-click on **PassengerController** file.
12. Locate **_passengerRepository** field and replce it with the following code to use **IPassengerRepository**:
    ```cs
    private readonly IPassengerRepository _passengerRepository;
    ```
13. Locate class constructor and replace it with the following code to get **IHotelBookingRepository** interface as paramter.
    ```cs
    public PassengerController(IPassengerRepository passengerRepository)
    {
        _passengerRepository = passengerRepository;
    }
    ```
14. Switch to **Command Line**.
15. Run the following command to change directory to **BlueYonder.Flights.Service**:
   ```bash
   cd BlueYonder.Flights.Service
   ```
16. Run the following command to run the service:
   ```cd
   dotnet run
   ```
17. Open **Microsoft Edge** browser.
18. Navigate to the following url:
    ```url
    https://localhost:5001/api/passenger
    ```
19. Check that you get all the data from the server.
