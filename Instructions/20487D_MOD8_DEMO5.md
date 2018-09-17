
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