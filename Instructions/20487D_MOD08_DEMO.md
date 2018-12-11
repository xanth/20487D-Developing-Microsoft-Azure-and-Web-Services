# Module 8: Monitoring and Diagnostics

1. Wherever you see a path to file starting at [Repository Root], replace it with the absolute path to the directory in which the 20487 repository resides. 
 e.g. - you cloned or extracted the 20487 repository to C:\Users\John Doe\Downloads\20487, then the following path: [Repository Root]\AllFiles\20487D\Mod01 will become C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01
2. Wherever you see **{YourInitials}**, replace it with your actual initials.(for example, the initials for John Do will be jd).
3. Before performing the demonstration, you should allow some time for the provisioning of the different Azure resources required for the demonstration. It is recommended to review the demonstrations before the actual class and identify the resources and then prepare them beforehand to save classroom time.

# Lesson 1: Logging in ASP.NET Core

### Demonstration: Recording logs to the Console and EventSource providers

#### Demonstration Steps

1. Open **Command Line**.
2. Run the following command to change directory to the **DemoFiles**:
   ```bash
    cd [Repository Root]\Allfiles\Mod08\Demofiles\Mod8Demo1Logger
   ```
3. Run the following command to create a new **WebApi** project:
   ```bash
    dotnet new webapi -n Mod8Demo1LoggerStarter    
   ```
4. Run the following command to change directory to **Mod8Demo1LoggerStarter** project:
   ```bash
    cd Mod8Demo1LoggerStarter
   ```
5. Run the following command to open the project in **VSCode**:
   ```bash
    code .
   ``` 
6. Click on **Program.cs** and paste the following code in **CreateWebHostBuilder** method after **WebHost.CreateDefaultBuilder(args)**:
   ```cs
    .ConfigureLogging((hostingContext, logging) => 
    {
        logging.AddConsole();
        logging.AddEventSourceLogger();
    })
   ```
7. Expand **Controllers** folder and select **ValuesController** file.
8. Add the following **using**:
   ```cs
    using Microsoft.Extensions.Logging;
   ```
9. Add the following **logger** field:
    ```cs
    private readonly ILogger _logger;
    ```
10. Add a new **Constructor** that will inject the **loger**:
    ```cs
    public ValuesController(ILogger<ValuesController> logger)
    {
        _logger = logger;
    }
    ```   
11. Add the following log information to the first **Get** method before the **return**:
    ```cs
    _logger.LogInformation("Getting all values");
    ```
12. Add the following log information to the second **Get** method before the **return**:
    ```cs
    _logger.LogInformation($"Getting value {id}");
    ```
13. Switch to **Command Line**.
14. Run the following command to run the project:
    ```bash
    dotnet run    
    ```
15. Open browser and navigate to the following **URL**:
    ```url
    https://localhost:5001/api/values
    ```
16. In the **Command Line** verify that the log **Getting all the values** is shown.
17. Navigate to the following **URL**:
    ```url
    https://localhost:5001/api/values/1
    ```
18. In the **Command Line** verify that the log **Getting value 1** is shown.
19. Close all windows.

### Demonstration: Using Serilog

#### Demonstration Steps

1. Open **Command Line**.
2. Run the following command to change directory to the **DemoFiles**:
   ```bash
    cd [Repository Root]\Allfiles\Mod08\Demofiles\Mod8Demo2Serilog
   ```
3. Run the following command to create a new **WebApi** project:
   ```bash
    dotnet new webapi -n Mod8Demo2SerilogStarter    
   ```
4. Run the following command to change directory to **Mod8Demo2SerilogStarter** project:
   ```bash
    cd Mod8Demo2SerilogStarter
   ```
5. Run the following cammand to install **Serilog** package:
   ```bash
    dotnet add package Serilog.AspNetCore --version 2.1.1
   ```
6. Run the following cammand to install **Serilog Configuration** package:
   ```bash
    dotnet add package Serilog.Settings.Configuration --version 2.6.1
   ```
7. Run the following cammand to install **Serilog Colored Console** package:
   ```bash
    dotnet add package Serilog.Sinks.ColoredConsole --version 3.0.1
   ```
8. Run the following cammand to restore all packages:
   ```bash
    dotnet restore
   ```
9. Run the following command to open the project in **VSCode**:
   ```bash
    code .
   ``` 
10. Add the following **using** to use **Serilog**:
    ```cs
    using Serilog;
    ```
10. Click on **Program.cs** and paste the following code in **CreateWebHostBuilder** method after **UseStartup<Startup>()** to use **Serilog** and write the logs to the console:
    ```cs
    .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
	    .ReadFrom.Configuration(hostingContext.Configuration)
	    .Enrich.FromLogContext()
	    .WriteTo.Console());
    ```
12. switch to **Command Line** and run the following command to run the project:
    ```bash
    dotnet run
    ```
14. Open browser and navigate to the following **URL**:
    ```url
    https://localhost:5001/api/values
    ```
15. Explore the logs in the **Command Line** from the **Serilog**.
16. Close all windows.

# Lesson 2: Diagnostic Tools

### Demonstration: Collecting ASP.NET Core LTTng events on Linux

#### Demonstration Steps

1. Open **Command Line**.
2. Run the following command to change directory to the **DemoFiles**:
   ```bash
    cd [Repository Root]\Allfiles\Mod08\Demofiles
   ```
3. Run the following command to create a new **WebApi** project:
   ```bash
    dotnet new webapi -n MonitorLTTng    
   ```
4. Run the following command to change directory to **MonitorLTTng** project:
   ```bash
    cd MonitorLTTng
   ```
5. Run the following command to open the project in **VSCode**:
   ```bash
    code .
   ```
6. In **VSCode** add new file **Dockerfile** (without file extension) to the root directory.
7. In the **Dockerfile** paste the following commands:
    ```sh
    FROM microsoft/dotnet:sdk AS build-env
	WORKDIR /app

	# Copy csproj and restore as distinct layers
	COPY *.csproj ./
	RUN dotnet restore

	# Copy everything else and build
	COPY . ./
	RUN dotnet publish -c Release -o out

	# Build runtime image
	FROM microsoft/dotnet:aspnetcore-runtime
	WORKDIR /app
	COPY --from=build-env /app/out .
    # Enviroment variable
	ENV COMPlus_EnableEventLog 1
	ENTRYPOINT ["dotnet", "MonitorLTTng.dll"]
    ```
8. Switch to **Command Line**.
9. Run the following command to build the docker image:
    ```bash
    docker build -t monitor .
    ```
10. Run the following command to run new container with **monitor** image:
    ```
    docker run -d -p 8080:80 --name myapp monitor
    ```
11. Run the following command to get a shell in the container:
    ```
    docker exec -it myapp bash
    ```
12. Run the following command to update packages in the container:
    ```bash
    apt update
    ```
13. Run the following command to install software-properties-common:
    ```bash
    apt-get install software-properties-common
    ``` 
14. Run the following command to add **PPA repository**:
    ```bash
    apt-add-repository ppa:lttng/ppa
    ```
15. Run the following command to update the list of packages:
    ```bash
    apt-get update
    ```
16. Run the following command to install the main **LTTng** packages:
    ```bash
    apt-get install lttng-tools lttng-modules-dkms liblttng-ust0
    ```
17. Run the following command to create a new **LTTng** session:
    ```bash
    lttng create exceptions-trace
    ```
18. Run the following command to add context data (process id, thread id, process name) to each event:
    ```bash
    lttng add-context --userspace --type vpid
    lttng add-context --userspace --type vtid
    lttng add-context --userspace --type procname
    ```
19. Run the following command to create an event rule to record all the events starting with **DotNETRuntime**:
    ```bash
    lttng enable-event --userspace --tracepoint DotNETRuntime:*
    ```
20. Run the following command to start recording events:
    ```bash
    lttng start
    ```
21. Open browser and navigate to the following **URL**:
    ```url
    http://localhost:8080/api/values
    ```
22. Refresh the page a few times.
23. Switch to **Command Line**.
24. Run the following command to **stop** the recording:
    ```bash
    lttng stop
    ```
25. Run the following command to **destroy** the session:
    ```bash
    lttng destroy
    ```
26. Paste the following command and press **Tab** then press **Enter** to see all the recorded events:
    ```bash
    babeltrace /root/lttng-traces/exceptions-trace
    ```
27. Explore all the events.
28. Run the following command to exit from **bash**:
    ```bash
    exit
    ```
29. Run the following command to kill the running container:
    ```bash
    docker kill myapp
    ```
30. Run the following command to remove the container:
    ```bash
    docker rm myapp
    ```
31. Close all windows.

# Lesson 3: Application Insights


### Demonstration: Integrating and viewing Application Insights

### Preparation Steps

1. Open **PowerShell** as **Administrator**.
2. In the **User Account Control** modal, click **Yes**.
3. Run the following command: **Install-Module azurerm -AllowClobber -MinimumVersion 5.4.1**.
4. Navigate to **[repository root]\AllFiles\Mod08\DemoFiles\ApplicationInsights\Setup**.
5. Run the following command:
    ```batch
     .\createAzureServices.ps1
    ```

#### Demonstration Steps

1. Open **Azure Portal**.
2. Click on **All resources** then click on **blueyondermod08demo5**{YourInitials}.
3. Click on **Application Insights** in the **SETTINGS** section on the left menu.
4. Click on **Setup Application Insights** then add the following information:
    - Select **Create new resource**.
    - In **Runtime/Framework** select **ASP.NET Core**.
    - Click on **OK**.
    - In **Apply monitoring settings** popup click on **Continue**.
5. Click on **View more in Application Insights** in the **Application Insights** blade.
6. Copy **Instrumentation Key** in the top bar.
7. Open **Command Line**.
8. Run the following command to change directory to **Starter** folder:
    ```bash
    cd [Repository Root]\Allfiles\Mod08\DemoFiles\Lab2\ApplicationInsights\Starter
    ```
9. Run the following command to install **ApplicationInsights** package:
    ```base
    dotnet add package Microsoft.ApplicationInsights.AspNetCore --version=2.4.1
    ```
10. Run the following command to open the project in **VSCode**: 
    ```bash
    code .
    ```
11. Click on **appsettings.json** and add the following code:
    ```json
    "ApplicationInsights": {
        "InstrumentationKey": "{InstrumentationKey}"
    }
    ```
12. Replace the **InstrumentationKey** key with value copied in point 6.
13. Locate **CreateWebHostBuilder** lamda in **Progam** class and replace it with the following code:
    ```cs
    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseApplicationInsights();
    ```
14. Switch to **Command Line**.
15. Paste the following command to publish the service:
    ```bash
    dotnet publish /p:PublishProfile=Azure /p:Configuration=Release
    ```
16. Switch to **Azure Portal**.
17. Click on **Live Metrics Stream** under **INVESTIGATE** to monitor the service.
18. Open new browser tab and navigate to the following url number of times:
    ```url
    https://blueyondermod08demo5{YourInitials}.azurewebsites.net/api/values
    ```
19. Switch to **Azure Portal** to diagnose the service.
20. Close **Live Metrics Stream** blade.
21. Click on **Performance** under **INVESTIGATE**.
22. Click on **Load more** under **Select operation** to view all the request to service.
23. Click on **GET Values/Get** then click on **Samples** to view all request to action get in values controller.
24. Select one of the sample and view all the detail of the request. 

### Demonstration: Viewing application dependencies and request timelines

#### Demonstration Steps

1. Open **Command Line** as an administrator.
2. Paste the following command in the **Command Line** in order to create a new **ASP.NET Core Web API** project, and then press **Enter**:
    ```bash
    dotnet new webapi --name BlueYonder.Flights.Service --output [Repository Root]\Allfiles\Mod08\DemoFiles\Mod8Demo6\Starter\Code\BlueYonder.Flights.Service 
    ```  
3. After the project was created, change directory in the **Command Line** by running the following command and then press **Enter**:
    ```bash
    cd [Repository Root]\Allfiles\Mod08\DemoFiles\Mod8Demo6\Starter\Code\BlueYonder.Flights.Service
    ```    
4. Add the following package:
   ```bash
    dotnet add package Microsoft.ApplicationInsights.AspNetCore --version 2.4.1
   ```
5.  Run the following command to restore all dependencies and tools of a project:
    ```base
    dotnet restore
    ```
5. Paste the following command in order to open the project in **VSCode** and then press **Enter**: 
    ```bash
    code .
    ```
6. Close **Command Line**.
7. Expand the **Controllers** folder and double-click on **ValuesController.cs** file.
8. Paste the following code to add **using** to the **class**:
    ```cs
    using System.Net.Http;
    ```
9. Inside **ValuesController.cs** class brackets add the following method:
    ```cs
    // GET api/values
    [HttpGet]
    public async Task<string> SendRequest()
    {
        var client = new HttpClient();
        await client.GetAsync("http://httpbin.org/get");
        return "Success";
    }
    ```
10. In **Explorer** blade, under the **BlueYonder.Flights.Service** pand, double-click on **Program.cs** class.
11. Locate the **CreateWebHostBuilder** method, and replace it with the following code:
    ```cs
    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
       WebHost.CreateDefaultBuilder(args)
              .UseApplicationInsights()
              .UseStartup<Startup>();
    ```
12. In **Explorer** blade, under the **BlueYonder.Flights.Service** pand, double-click on **Properties** folder.
13. Double-click on **Properties** folder, and add new folder **PublishProfiles**.
14. Add new file **Azure.pubxml** and past the following code:
    ```cs
    <Project>
        <PropertyGroup>
          <PublishProtocol>Kudu</PublishProtocol>
          <PublishSiteName>[App Service Url]</PublishSiteName>
          <UserName>[FTP/Deployment UserName]</UserName>
          <Password>[Password]</Password>
        </PropertyGroup>
    </Project>
    ``` 
15. Add new file **Azureprofile.xml** and past the following code:
    ```cs
    <?xml version="1.0" encoding="utf-8"?>
    <publishData>
      <publishProfile profileName="" publishMethod="MSDeploy" publishUrl="" msdeploySite="" userName="" userPWD="" destinationAppUrl=""     SQLServerDBConnectionString="" mySQLDBConnectionString="" hostingProviderForumLink="" controlPanelLink="http://windows.azure.com"   webSystem="WebSites">
        <databases />
      </publishProfile>
      <publishProfile profileName="" publishMethod="FTP" publishUrl="" ftpPassiveMode="True" userName="" userPWD="" destinationAppUrl=""    SQLServerDBConnectionString="" mySQLDBConnectionString="" hostingProviderForumLink="" controlPanelLink="http://windows.azure.com"  webSystem="WebSites">
        <databases />
      </publishProfile>
    </publishData>
    ``` 
16. On the Start menu, search for and right-click **PowerShell**, and then **click Run as Administrator**.
17. In the **User Account Control** modal, click Yes.
18. Run the following command: **Install-Module azurerm -AllowClobber -MinimumVersion 5.4.1**
19. Change directory in the **PowerShell** by running the following command and then press **Enter**:
    ```bash
      cd [Repository Root]\AllFiles\Mod08\DemoFiles\Mod8Demo6\Setup
    ```
20. Run the following command:
    ```bash
      .\createAzureServices.ps1
    ```        
21. You will be asked to supply a **Subscription ID**, which you can get by performing the following steps:
    1. Open a browser and navigate to **http://portal.azure.com**. If a page appears, asking for your email address, type your email address, and then click Continue. Wait for the sign-in page to appear, enter your email address and password, and then click Sign In.
    2. In the search text box on the top bar, type **Cost** and then in results click **Cost Management + Billing(Preview)**. The **Cost Management + Billing** window should open.
    3. Under **BILLING ACCOUNT**, click **Subscriptions**.
    4. Under **My subscriptions**, you should have at least one subscription. Click on the subscription that you want to use.
    5. Copy the value from **Subscription ID**, and then paste it at the **PowerShell** prompt. 
    6. In the **Sign in** window that appears, enter your details, and then sign in.
    7. In the **Administrator: Windows PowerShell** window, enter your initials when prompted.    

22. Open **Microsoft Edge** browser.
23. Navigate to **https://portal.azure.com** and login with your credentials.
24. Click **App Services** on the left menu panel.
25. Click on **blueyonderMod8Demo6{yourinitials}**.
26. Click on **Application Insights** under **Setting** on the left menu panel.
27. Click on **Setup Application Insights**
    - Select **Create new resource**.
    - Choose **blueyonderMod8Demo6{yourinitials}**
28. Choose **ASP.NET Core** in the **Runtime/Framework** under **Instrument your application**.
29. Click **OK** and then **Continue**.
30. Wait until all changes saved.
31. Click on **View more in Application Insights**.
32. Copy the **Instrumentation Key** on the right panel.
33. Switch to **VSCode**.
34. Locate the **appsettings.json** in **BlueYonder.Flights.Service** and add the following code:
    ```cs 
    "ApplicationInsights": {
                    "InstrumentationKey": "{Your Instrumentation Key}"
                    }
    ```
35. In the **{Your Instrumentation Key}**, paste the Instrumentation Key you copied in previos step (32).
36. Switch to **Command Line**
    ```bash
    dotnet publish /p:PublishProfile=Azure /p:Configuration=Release
    ```
37. Open **Microsoft Edge** browser.
38. Navigate to the following url:
    ```url
    https://localhost:5001/api/Values/SendRequest
    ```
39. Refresh the page (press F5) couple of times.
40. Switch to **Azure Portal**.
41. Click on **blueyonderMod8Demo6{YourInitial}** App Service.
42. Click on **Performance test** under **Development Tools**.
43. Click on **Dependencies** and then click on **Load more**, and locate the **Values\GET** request.
44. Make sure the result is from the following url **http://httpbin.org/get**.

