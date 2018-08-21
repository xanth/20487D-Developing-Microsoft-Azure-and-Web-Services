
# Module 4: Extending ASP.NET Core HTTP services

# Lab: Customizing the ASP.NET Core Pipeline

#### Scenario

In this lab you will Customize the ASP.NET Core Pipeline.

#### Objectives

After completing this lab, you will be able to:

- Add inversion of control by using Dependency Injection to the project.
- Create cache mechanism and action filters.
- Add middleware to inform the client via header response.
  

### Exercise 1: Use Dependency Injection to get a Repository object

#### Task 1: Create interface for the repository 

1.  Open **Command Line** and navigate to **Repository Root]\Allfiles\Mod04\LabFiles\Lab1\Starter**.
2.  Open the project with **VSCode**.
3.  Add new interface **IHotelBookingRepository.cs** in **BlueYonder.Hotels.DAL**, under **Repository** folder.
4.  Add the following methods signatures:
    ```cs
    IEnumerable<Room> GetAvaliabileByDate(DateTime date);
    IEnumerable<Reservation> GetAllReservation();
    Task DeleteReservation(int reservationId);
    ```

#### Task 2: Implement the interface on the repository

1. Use **IHotelBookingRepository** interface in **HotelBookingRepository**, In the **BlueYonder.Hotels.DAL** project.

#### Task 3: Register the repository object in the ASP.NET Core Dependency Injection mechanism

1. Locate **Startup.cs** in **BlueYonderHotels.Service** project.
2. Register **HotelBookingRepository** in **ConfigureServices** method.

#### Task 4: Change the controller’s constructor to request an injected repository

1. Navigate to **HotelBookingController** and replace **HotelBookingRepository** field to **IHotelBookingRepository**, and initialize it using the constructor.

### Exercise 2: Create a cache filter

#### Task 1: Create an action filter for cache headers

1. Create **Attirbutes** folder in **BlueYonderHotels.Service** project.
2. Create new **CacheAttribute** class, and make sure it derived from **ActionFilterAttribute**.
3. Add the following fields:
      ```cs
    private string _headerMessage { get; set; }
    private TimeSpan _durationTime;
    private const int _defulatDuration = 60;
    private Dictionary<string,(DateTime, IActionResult)> _cache = new Dictionary<string, (DateTime,IActionResult)>();
    ```
4. Add constructor with **int** optionl parameter, which init **_durationTime** feild.
5. Add constructor with **string** parameter, which init **_headerMessage** feild. 
6. Add new **CacheValid** method to check the cache validation, with **FilterContext**  param and return **bool** result.
7. override **OnActionExecuting** method:
   ```cs
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (CacheValid(context))
        {
            context.Result = _cache[context.HttpContext.Request.Path].Item2;
            return;
        }
        base.OnActionExecuting(context);
    }
   ```
8.  override **OnResultExecuted** method:
    ```cs
    public override void OnResultExecuted(ResultExecutedContext context)
    {
        if(!CacheValid(context))
             _cache[context.HttpContext.Request.Path] = (DateTime.Now,context.Result);
        base.OnResultExecuted(context);
    }
    ```

#### Task 2: Add the cache filter to several actions

1. Add **Cache("X-No-Cache")** attribute above **GetAvailability** method, in **HotelBookingController**.
   
#### Task 3: Test cacheable and non-cacheable actions from a browser

1. Run **BlueYonderHotels.Service** service.
2. Make sure you get the properly results by Invoke-WebRequest in **PowerShell**, to the following requests: 
    - Get the reservations.
    - Display the http result.
    - Display the http **Contnet**.
    - Get **available rooms** currnet date.
    - Remove a reservation.

3. Redo **Get available rooms currnet date** step and see that the room list didnt change, wait one more mintue and run the step again and see that under room is now appern in the list.

### Exercise 3: Create a debugging middleware

#### Task 1: Create a middleware class to calculate execution time

1. Create new **Middleware** folder, under **BlueYonderHotels.Service** folder.
2. Create new static **ExecutionTimeMiddleware** class.
3. Add new static **AddResponeHeaders** method, with **HttpContext** and **Func<Task>** params and return **Task** entity.

#### Task 2: Write server and debug information to response headers
 
1. Inside the method **AddResponeHeaders**:
    - Add **Server Name** header.
    - Add **OS Version** header.
    - Add **Request Execution Time** header.
2. Add **Request Execution Time** header under the **X-OS-Version**:
    ```cs
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();

    context.Response.OnStarting(state => 
    {
        var httpContext = (HttpContext)state;
        stopwatch.Stop();
        httpContext.Response.Headers.Add("X-Request-Execution-Time", stopwatch.ElapsedMilliseconds.ToString());
        return Task.CompletedTask;
    }, context);
    ```
3. Continue to the next middleware with:
   ```cs
    await next();
   ```
   
#### Task 3: Create the IApplicationBuilder helper class

1. Create static extension method to **IApplicationBuilder**, with **IApplicationBuilder** param and return **IApplicationBuilder** entity. 

#### Task 4: Register the middleware to the ASP.NET Core pipeline

1. Locate the **Configure** method in **Startup.cs** under **BlueYonderHotels.Service**.
2. Use the **UseExecutionTimeMiddleware** above the **app.UseHttpsRedirection**.
   
#### Task 5: Test the new middleware from a browser

1. Navigate to **Get HotelBooking Reservation** on **Microsoft Edge** browser.
2. Open **Develpper Tools**, navigate to **Network** and select **Reservation**.
3. Expand the **Respone Header**.
4. Verified that you got **x-os-vesrion** and **x-server-name** and **x-request-execution-time** from the server.