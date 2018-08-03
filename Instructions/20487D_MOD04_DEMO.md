# Module 2: Extending ASP.NET Core HTTP services

 Wherever you see a path to file starting at [repository root], replace it with the absolute path to the directory in which the 20487 repository resides. 
 e.g. - you cloned or extracted the 20487 repository to C:\Users\John Doe\Downloads\20487, then the following path: [repository root]\AllFiles\20487D\Mod01 will become C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01

# Lesson 2: Creating an Entity Data Model

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
19. Open **Microsoft Edge** browser.
20. Open **Develpper Tools** by click on the three dot on the top bar and then select **Develpper Tools** or by pressing **F12**.
21. In the **Develpper Tools** navigate to **Network**.
22. Navigate to the following url:
    ```url
    https://localhost:5001/api/passenger/5
    ```
23. In **Network** tab locate the url and check the result column that you get **404**
