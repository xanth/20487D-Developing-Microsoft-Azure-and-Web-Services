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
	ENV COMPlus_EventLogEnabled 1
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