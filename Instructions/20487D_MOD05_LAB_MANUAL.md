# Module 1: Hosting services

# Lab: Host an ASP.NET Core service in a Windows Service

#### Scenario

In this lab you will learn how to create a new **ASP .NET Core** project and use **Kestrel - Run As Service** to
host this project in a new Windows Service.
In this lab you will explore several frameworks and platforms,
such as Entity Framework Core, ASP.NET Core Web API,
and Azure, that are used for creating distributed applications.

#### Objectives

After completing this lab, you will be able to:

- Create an ASP.NET Core Web API service.
- Register a new Windows Service.
- Host a web application in a Windows Service.

### Exercise 1: Creating an ASP.NET Core project

#### Scenario

In this exercise, you will create an Web API via ASP.NET Core by using command line.

#### Task 1: Create a new ASP.NET Core project

1.  Create a new Web API Core project by using **Command Line** with **dotnet** tool.
    > **Results** : After completing this exercise, you should have a basic Web API ASP.NET Core project.

#### Task 2: Install the Microsoft.AspNetCore.Hosting.WindowsServices NuGet package

1.  Install the **Microsoft.AspNetCore.Hosting.WindowsServices** NuGet package by using **Command Line** with **dotnet** tool.
    > **Results** : After completing this exercise, you should have Microsoft.AspNetCore.Hosting.WindowsServices NuGet package
    > on your ASP .NET Core project that you have created in task 1 which allows you to run the ASP.NET Core project as a Windows Service.

#### Task 3: Modify the Main method to use Kestrel RunAsService hosting

1. Use **VSCode** to add **using Microsoft.AspNetCore.Hosting.WindowsServices;** to the **program.cs** class which allows you to use the Microsoft.AspNetCore.Hosting.WindowsServices NuGet package that you have installed in task 2.
2. Change the **Main** method implementation using **Kestrel’s RunAsService hosting** to host your **ASP .NET Core** project in a Windows Service:

```cs
    CreateWebHostBuilder(args).Build().RunAsService();
```

### Exercise 2: Register the Windows Service

#### Scenario

In this exercise, you will create a new Windows Service, start and stop it using the **Command Line** with **sc** tool.

#### Task 1: Register the Windows Service

1. Publish your **ASP .NET Core** project that you have created earlier, into a folder that will allow you create a new Windows Service.<br/>
   You will do it using a command line with **dotnet** tool.
2. Create a new Windows Service using the **sc** command tool, that will take the **.exe** file from the publish in the last point and will host your application in a new windows Service.<br/>
   To do it, you will need to run the following command in the **Command Line**:<br/>
   ```bash
       sc create {The Service Name} binPath= "{Your publish folder path.exe}"
   ```

#### Task 2: Start the Windows Service and test it

1. Use **sc Command Line** to start your newly Windows Service.
2. Navigate to your api using port **5000** , and make sure you are getting a good response as expected.
3. Use **sc Command Line** to stop the service.
4. Navigate to the same address to see that you are not getting a good response.
   > **Results** : After completing this exercise, you will have a new **ASP .NET Core** api project which is hosted by a \*\*Windows Service.

### Exercise 1: Create a Web App in the Azure portal

#### Task 1: Run a setup script to upload a database to Azure

1. Run the following command: **Install-Module azurerm -AllowClobber -MinimumVersion 5.4.1** in **PowerShell** as **Administrator**.
2. Run the following command:
   ```bash
     .\createAzureSQL.ps1
   ```
   and follow the steps to sign in your **Azure Subscription**.

#### Task 2: Create a free website

1. Add a new **App Service** in **http://portal.azure.com** with unique **App name**:
   ```
       blueyonder-flights-{Your Initials}
   ```
2. In the **App Service plan**, enter new **MyFlightAppService** and in the **Pricing tier**, choose **F1**.

#### Task 3: Configure an environment variable and the database connection string

1. Add a new setting with key named **BLUEYONDER_TENANT** and **Testing** as a value to the newly created web app **blueyonder-flights-{Your Initials}**.
2. Add a new setting with key named **dbConnectionString** and the following value:
   ```
   Server=tcp:blueyonder05-{Your Initials}.database.windows.net,1433;Initial Catalog=BlueYonder.Flights.Lab05;Persist Security Info=False;User ID=BlueYonderAdmin;Password=Pa$$w0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
   ```

#### Task 4: Configure IIS logs

1. Add a unique name to **FTP/deployment username** under **Deployment Credentials**, and **Password**.
2. Inside **MONITORING** menu:

   - Turn-On **Application Logging (Filesystem)** option.
   - Choose **Verbose** as the log **Level**.
   - Turn-On **Failed request tracing**.
   - Choose **File System** with **Retention Period (Days)** of **21** days.

### Exercise 2: Deploy an ASP.NET Core Web API to the Web App

#### Task 1: Deploy an ASP.NET Core project to the Web App

1. Open **File Explorer** and browse to **[RepositoryRoot]\Allfiles\Mod05\LabFiles\Exercise2\HostInAzure\BlueYonder.Flights.Service\Properties\PublishProfiles** folder.
2. In the **Azure.pubxml** file:
   - Replace the **PublishSiteName**.
   - Replace the **UserName** and **Password** values.
3. Publish the **BlueYonder.Flights.Service**, in order to host your web app in the newly created **App Service**.

#### Task 2: Test and verify the Web App uses the database and environment variable

1. Make a **HTTP Request** to your **Web App** via **PowerShell**.
   ```ps
    Invoke-WebRequest https://{Your App Name}.azurewebsites.net/api/flights
   ```
2. Check that you are getting the **"X-Tenant-ID"** header with value of **Testing**.
3. Check that you are getting a good **JSON** response of flights.

#### Task 3: Use FTP Deployment Server to view the Web App and its log files

1. Make a **bad** HTTP request to your **Web App** via **PowerShell**.
2. Open a browser and navigate to your **FTP hostname** address, and **Sign in**.
3. Locate **W3SVC#########** folder under **LogFiles\\** Folder.
4. Review the newer **xml** file:
   - **Request Summary** section
   - **Errors & Warnings** section
5. Login to your **Azure Account** via **PowerShell**.
6. Start streaming logs into the **PowerShell**.
7. Run a command in order to see a log in the **PowerShell** instance.
8. Run a command in order to see another log in the other **PowerShell** instance.
9. You can see the status code in the end of the log.

# Lab: Host an ASP.NET Core service in Azure Container Instances

### Exercise 1: Publish the service to a Docker container

#### Task 1: Create a Dockerfile for the service

1. Open **File Explorer** and navigate to provided **Hotels Service** folder in **[Repository Root]\AllFiles\Mod05\Labfiles\Exercise3\Starter\BlueYonder.Hotels.Service**.
2. Add a new file called **DockerFile** (without extension) in **BlueYonder.Hotels.Service** folder, make sure you download a base docker image for **ASP.NET CORE** and define docker settings for your **BlueYonder.Hotels.Service** project.
3. Build your project using the **DockerFile** that you have created earlier via **Command Line**.
4. Run the docker container which listening on a default port.
5. Open a browser and navigate to **localhost:8080/api/hotels**, and Check that you are getting a good response in **JSON** fromat.

#### Task 2: Publish the application into a local container using Visual Studio

1. Open **[Repository Root]\AllFiles\Mod05\Labfiles\Exercise3\Starter\BlueYonder.Hotels.Service\BlueYonder.Hotels.Service.sln** in **Visual Studio 2017**.
2. Add **Docker Support** and choose **Linux** on the **Docker Support Options** window.
3. **Build Solution**, and **Start Debugging (F5)**.
4. Check that you are getting a good response in **JSON** format.

#### Task 3: Push the container to a public container registry

1. Run **Docker Client** on your machine using your **Docker Hub** credentials.
2. Login to **Docker Hub** using a **Command Line**.
3. Create a tag name for your docker image in **Docker Hub**.
4. push your container image into **Docker Hub**
5. Open a browser and navigate to **https://hub.docker.com**, and **Sign In** with your credentials.
6. On the **Repositories** page, verify that you are seeing the last push as **{Your Docker Hub UserName}/hotels_service**.

### Exercise 2: Host the service in Azure Container Instances

#### Task 1: Create a Resource Group for Azure Container Instances

1. Login to your **Azure** account via **Microsoft Azure Command Prompt (Azure CLI)**.
2. Write the code that you have got from the command line in **https://microsoft.com/devicelogin** url.
3. Create a new **Resource Group** called **blueyounder-hotels**.
   

#### Task 2: Create an Azure Container Instance from the container image

1. Create a new **container** instance using the previously-created **Docker container** for the hotels service in **Command Line**.
2. Run **az container list** in order get details of your container instance.
3. Copy **"ip"** property inside the **"ipAddress"** object.
4. Add **/api/hotels** and the **IP address** in your browser's address bar.
5. Check that you are getting a good response in **JSON** format.
   
# Lab: Implement an Azure Function 

### Exercise 1: Develop the service locally

#### Task 1: Create a new Function App project in Visual Studio

1. Open **Command Prompt** directory in the command line by pasting the following command and then press **Enter**:
    ```bash
    cd [RepositoryRoot]\AllFiles\Mod05\Labfiles\Exercise4
    ```
2.  Create a **local Functions Project** named **BlueYonder.Flights.GroupProxy**.
3.  Open the project in **VSCode** and add new **Traveler.cs** file with the following code:  
    ```bash
    using System;

    namespace BlueYonder.Flights.GroupProxy
    {
       public class Traveler
       {
    	  public int TravelerId { get; set; }
          public string FirstName { get; set; }
          public string LastName { get; set; }
          public string MobilePhone { get; set; }
          public string Passport { get; set; }
          public string Email { get; set; }
       }
    }
    ```
4.  Create a new Azure Function for your newly Function project with **template** of **Http Triger**.

#### Task 2: Implement an HTTP trigger that invokes the flights booking Azure Web App

1. In your newly **BookFlightFunc** class, locate the **usings** assemblies, and then press **Enter** after the last **using** which is **Newtonsoft.Json**.
2. Add the following **usings** in the new line after **using Newtonsoft.Json**:
    ```
    using System.Net.Http;
    using System.Text;
    using System.Collections.Generic;
    ```
3. Replace all the **Run** method implementation with the following code that makes a request to your **Flights booking Azure Web App** that you have created in **Lab 2**:
    ```cs
    log.Info("C# HTTP trigger function processed a request to the flights booking service.");

    var flightId = req.Query["flightId"];

    var flightServiceUrl = $"http://blueyonder-flights-{Your Initials}.azurewebsites.net/api/flights/bookFlight?flightId={flightId}";

    log.Info($"Flights service url:{flightServiceUrl}");

    var travelers = new List<Traveler>
     {
        new Traveler { Email = "204837Dazure@gmail.com" , FirstName = "Jonathan", LastName = "James", MobilePhone = "+61 0658748", Passport = "204837DCBA" },
        new Traveler { Email = "204837Dfunction@gmail.com", FirstName = "James", LastName = "Barkal", MobilePhone = "+61 0658355", Passport = "204837DCBABC" }
     };

    var travelersAsJson = JsonConvert.SerializeObject(travelers);

    using (var client = new HttpClient())
    {
        client.PostAsync(flightServiceUrl,
                         new StringContent(travelersAsJson,
                                           Encoding.UTF8,
                                           "application/json")).Wait();
    }

    return (ActionResult)new OkObjectResult($"Request to book flight was sent successfully");
    ```
    >**NOTE:** Replace **{Your Initials}** in the **flightServiceUrl** with your actual initials.

#### Task 3: Test the Function App locally in a browser

1.  Test the newly **Azure Function** locally.
2. Open a browser and navigate to **http://localhost:7071/api/BookFlightFunc?flightId=1**.
3. Check that you are getting a good response.
4. Check that the travelers were actually booked to flight number 1 by opening a browser and navigate to the following address:<br/>

   **http://blueyonder-flights-{Your Initials}.azurewebsites.net/api/flights**

    >**NOTE:** Replace **{Your Initials}** with your actual initials.

5. Check that you are getting a good response in **JSON** format.

### Exercise 2: Deploy the service to Azure Functions

#### Task 1: Deploy the service to Azure Functions from Visual Studio

1. Create **Function App** in Azure Portal at **portal.azure.com**.
2. Test the newly **Azure Function** locally, { func host start --build } in **Comand Prompt**.
3.  Login to **Azure** with your credentials.
4.  Publish your new **Azure Function** into **Azure** and then press **Enter**:
    ```bash
        func azure functionapp publish {Your App name}
    ```            
    >**NOTE:** Replace **{Your App name}** with your actual app name in azure that you wrote in previos point.
    
#### Task 2: Test the Function App on Azure in a browser 

1. Open a browser and  to **https://bookflightfunctionapp-{Your Initials}.azurewebsites.net/api/navigateBookFlightFunc?flightId=1**.
    >**NOTE:** Replace **{Your Initials}** with your actual initials.
2. Check that you are getting a good response.
3. Check that the travelers were actually booked to flight number 1 by opening a browser and navigate to the following address:<br/>

   **http://blueyonder-flights-{Your Initials}.azurewebsites.net/api/flights**

    >**NOTE:** Replace **{Your Initials}** with your actual initials.

4. Check that you are getting a good response.