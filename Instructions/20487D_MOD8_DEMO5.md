
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