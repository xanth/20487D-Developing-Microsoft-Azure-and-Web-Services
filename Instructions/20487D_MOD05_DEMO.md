# Module 5: Hosting Services On-Premises and in Azure

 Wherever you see a path to file starting at [repository root], replace it with the absolute path to the directory in which the 20487 repository resides. 
 e.g. - you cloned or extracted the 20487 repository to C:\Users\John Doe\Downloads\20487, then the following path: [repository root]\AllFiles\20487D\Mod01 will become C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01

# Lesson 1: Hosting Services on-premises

### Demonstration: Hosting Services on-premises by using Windows Services with Kestrel (RunAsService)

#### Demonstration Steps

1. Open **Command Line** as an administrator.
2. Paste the following command and then press **Enter**:
   ```bash
      cd [Repository Root]\Allfiles\Mod05\DemoFiles\Host In Windows Service
   ```
3. Paste the following command in order to publish your **ASP .NET Core** project into a folder and then press **Enter**:
   ```bash
      dotnet publish --configuration Release --output [Repository Root]\Allfiles\Mod05\DemoFiles\Host In Windows Service
   ```
4. Paste the following command in order to create your **Windows Service** and then press **Enter**:
   ```bash
      sc create HotelsService binPath= "[Repository Root]\Allfiles\Mod05\DemoFiles\Host In Windows Service\BlueYonder.Hotels.Service.exe"
   ```
5. Paste the following command in order to start your **Windows Service** and then press **Enter**:
   ```bash
      sc start HotelsService
   ```
6. Verify that the service is working by open a browser and browse to **http://localhost:5000/api/values**.
7. Paste the following command in order to stop your **Windows Service** and then press **Enter**:
   ```bash
      sc stop HotelsService
   ```
8. Close **Command Line**.




### Demonstration: Hosting ASP.NET Core services in IIS

#### Demonstration Steps

1. In the **Start** menu, open **Internet Information Services(IIS) Manager**.
2. Right-click on the **Sites** folder in the left menu and click **Add Website..**.
3. In the **Add Website** dialog box, type **HotelsSite** in the **Site name** box.
4. In the **Content Directory** section, in the **Physical path** box, type **[Repository Root]\Allfiles\Mod05\DemoFiles\Host in IIS**.
5. In the **Binding** section, change the **Port** to be **8080** instead of **80**.
6. Click **OK** to create your new website.
7. Click on **Application Pools** in the left menu and then double-click on your website that you just created.
8. In the **.NET CLR version** combo-box, choose **No Managed Code** and then press **OK**.
9. Close **Internet Information Services(IIS) Manager**.
10. Open **Visual Studio 2017** as an administrator.
11. In **Visual Studio**, on the **File** menu, point to **Open**, and then click **Project/Solution**.
12. In the **Open Project** dialog box, browse to **[Repository Root]\Allfiles\Mod05\DemoFiles\Host in IIS**, click **BlueYonder.Hotels.Service.sln**, and then click **Open**.
13. Right-click on **BlueYonder.Hotels.Service** and then click **Publish**.
14. In the **Publish** blade, choose **IIS, FTP, etc** and then click **Create Profile**.
15. In the **Publish** window, type **localhost** in the **Server** box.
16. Type **HotelsSite** in the **Site name** box and then press **Next**.
17. In the **Settings** pane, choose **Self-Contained** in the **Deployment Mode** combo-box.
18. Choose **win-x64** in the **Target Runtime** combo-bx and then click **Save**.
19. In the **Publish** blade , click **Publish**.
20. Wait until the process will finish.
21. Open the browser and browse to **localhost:8080/api/Values**. Check that you are getting a good response like the following:
	```json
		["value1", "value2"]
	```




# Lesson 2: Hosting Services in Azure Web Apps

### Demonstration: Hosting ASP.NET Core Web APIs in Azure Web Apps

1. Open a browser and browse to Azure portal at **portal.azure.com**.
2. If a page appears, prompting for your email address, type your email address, and then click Continue. Wait for the sign-in page to appear, enter your email address and password, and then click Sign In.

    >**Note:** During the sign-in process, if a page appears prompting you to choose from a list of previously used accounts, select the account that you previously used, and then continue to provide your credentials.

3. If the **Windows Azure Tour** dialog box appears, click close (the **X** button).
4. In the navigation blade, click **App Services**, click **Add**, click **Web App**, and then click **Create**.
5. In the **App name** text box, enter a unique name.
6. Copy the **App name** value to any code editor.
7. Click **App service plan/Location**, and then click **Create new**.
8. From the **Location** drop-down list, select the region that is closest to your location.
9. In the **App Service plan** input, enter **MyAppService**, and then click **OK**.
10. Click **Create**. Wait for the web app to be created. Click the newly created web app.
11. In the newly created web app blade, in the **Deployment** section, click **Deployment Credentials**.
12. In the **FTP/deployment username** type a globally unique name.
13. In the **Password** and **Confirm password** inputs, type a new password.

    >**NOTE:** You will need the cardentials for the next steps. Copy them to any code editor.
14. Click on the **Overview** button in your newly created web app.
15. Open a **Command Line**.
16. Paste the following command in order to create a new **Web App** and then press **Enter**:
   ```bash
      dotnet new webapi --name BlueYonder.Hotels.Service -output "[RepositoryRoot]\Allfiles\Mod05\DemoFiles\Host In Azure"
   ```
17. Open **File Explorer** and browse to **[RepositoryRoot]\Allfiles\Mod05\DemoFiles\Host In Azure**.
18. In the **Properties** folder, create a new folder called **PublishProfiles**.
19. In the **PublishProfiles** create a new file called **Azure** with an extension of **.pubxml**..
20. Open the file with any code editor and paste the following **XML** content in order to define the publish settings:

    ```xml
    <Project>
        <PropertyGroup>
        <PublishProtocol>Kudu</PublishProtocol>
        <PublishSiteName>{Your App name}</PublishSiteName>
        <UserName>{Your FTP/deployment username}</UserName>
        <Password>{Your FTP/deployment password}</Password>
        </PropertyGroup>
    </Project>
    ```
21. Replace the **PublishSiteName**, **UserName** and **Password** values with the values that you have copied earlier.
22. Save the file.
23. In the **Command Line**, paste the following command to point to your newly web app folder:
    ```bash
        cd [RepostiroryRoot]\Allfiles\Mod05\DemoFiles\Host In Azure
    ```
23. Paste the following command in order to host your web app in the **App Service** that you have created in **Azure**:

    ```bash
        dotnet publish /p:PublishProfile=Azure /p:Configuration=Release
    ```
24. Open a browser and browse to **https://{Your App Name}.azurewebsites.net/api/values**.

    >**NOTE:** Replace **{Your App Name}** with your actual app name that you have copied earlier.
25. Check that you are getting a good response like the following:
	```json
		["value1", "value2"]
	```







©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
