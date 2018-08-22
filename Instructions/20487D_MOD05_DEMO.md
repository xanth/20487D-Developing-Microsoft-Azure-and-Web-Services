# Module 5: Hosting Services On-Premises and in Azure

 Wherever you see a path to file starting at [repository root], replace it with the absolute path to the directory in which the 20487 repository resides. 
 e.g. - you cloned or extracted the 20487 repository to [repository root]\Downloads\20487, then the following path: [repository root]\AllFiles\20487D\Mod01 will become [repository root]\Downloads\20487\AllFiles\20487D\Mod01

# Lesson 1: Hosting Services on-premises

### Demonstration: Hosting Services on-premises by using Windows Services with Kestrel (RunAsService)

#### Demonstration Steps

1. Open **Command Line** as an administrator.
2. Paste the following command and then press **Enter**:
   ```bash
      cd [Repository Root]\Allfiles\Mod05\DemoFiles\HostInWindows Service
   ```
3. Paste the following command in order to publish your **ASP .NET Core** project into a folder and then press **Enter**:
   ```bash
      dotnet publish --configuration Release --output [Repository Root]\Allfiles\Mod05\DemoFiles\HostInWindows Service
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

1. Open a browser and navigate to Azure Portal at **portal.azure.com**.
2. If a page appears, asking for your email address, type your email address, and then click Continue. Wait for the sign-in page to appear, enter your email address and password, and then click Sign In.

    >**Note:** During the sign-in process, if a page appears, asking you to choose from a list of previously used accounts, select the account that you previously used, and then continue to provide your credentials.

3. If the **Windows Azure Tour** dialog box appears, click close (the **X** button).
4. In the navigation blade, click **App Services**. 
5. In the top menu, click **Add** in order to create a new **App Service**.
6. Choose **Web App** from the **Web Apps** section inside the **Web** blade, and then click **Create**.
7. In the **App name** text box, enter a unique name.
8. Copy the **App name** value to any code editor.
9. Click **App service plan/Location**, and then click **Create new**.
10. From the **Location** drop-down list, select the region that is closest to your location.
11. In the **App Service plan** input, enter **MyAppService**, and then click **OK**.
12. Click **Create**. Wait for the web app to be created. Click the newly created web app.
13. In the newly created web app blade, in the **Deployment** section, click **Deployment Credentials**.
14. In the **FTP/deployment username** type a globally unique name.
15. In the **Password** and **Confirm password** inputs, type a new password.

    >**NOTE:** You will need the credentials for the next steps. Copy them to any code editor.
    
16. Click on the **Overview** button in your newly created web app.
17. Open a **Command Line**.
18. Paste the following command in order to create a new **Web App** and then press **Enter**:
   ```bash
      dotnet new webapi --name BlueYonder.Hotels.Service -output "[RepositoryRoot]\Allfiles\Mod05\DemoFiles\HostInAzure"
   ```
19. Open **File Explorer** and browse to **[RepositoryRoot]\Allfiles\Mod05\DemoFiles\HostInAzure**.
20. In the **Properties** folder, create a new folder called **PublishProfiles**.
21. In the **PublishProfiles** create a new file called **Azure** with an extension of **.pubxml**..
22. Open the file with any code editor and paste the following **XML** content in order to define the publish settings:

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
23. Replace the **PublishSiteName**, **UserName** and **Password** values with the values that you have copied earlier.
24. Save the file.
25. In the **Command Line**, paste the following command to point to your newly web app folder:
    ```bash
        cd [RepostiroryRoot]\Allfiles\Mod05\DemoFiles\HostInAzure
    ```
26. Paste the following command in order to host your web app in the **App Service** that you have created in **Azure**:

    ```bash
        dotnet publish /p:PublishProfile=Azure /p:Configuration=Release
    ```
27. Open a browser and browse to **https://{Your App Name}.azurewebsites.net/api/values**.

    >**NOTE:** Replace **{Your App Name}** with your actual app name that you have copied earlier.
28. Check that you are getting a good response like the following:
	```json
		["value1", "value2"]
	```







# Lesson 3: Packaging services in containers


### Demonstration: Creating an empty ASP.NET Core Docker container

1. Open a **Coomand Line**.
2. Paste the following command in order to launch a default **ASP.NET Core** container, which listens on a default port:

```bash
    docker run -it --rm -p 8000:80 --name aspnetcore_sample microsoft/dotnet-samples:aspnetapp
```
1. Open a browser and nevigate to **localhost:8000**.
2. Verify that you are getting the default **ASP.NET Core** starter page.



### Demonstration: Publishing into a container


1. Open a **Command Line**.
2. Paste the following command in order to create a new **Web App** and then press **Enter**:
   ```bash
      dotnet new webapi --name BlueYonder.Hotels.Service --output [RepositoryRoot]\Allfiles\Mod05\DemoFiles\Host_In_Docker
   ```
3. Open **File Explorer** and browse to **[RepositoryRoot]\Allfiles\Mod05\DemoFiles\Host_In_Docker**.
4. Add a new file called **DockerFile** (without extension) in **Host_In_Docker** folder.
5. Open the newly **DockerFile** with any code editor and paste the following docker commands in order to download a base docker image for **ASP.NET CORE** and define docker settings for your **BlueYonder.Hotels.Service** project. Then - save the file:
    ```
        FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
        WORKDIR /app

        EXPOSE 55419
        EXPOSE 44398

        FROM microsoft/dotnet:2.1-sdk AS build
        WORKDIR /src
        COPY Host_In_Docker/BlueYonder.Hotels.Service.csproj Host_In_Docker/
        RUN dotnet restore Host_In_Docker/BlueYonder.Hotels.Service.csproj
        COPY . .
        WORKDIR /src/Host_In_Docker
        RUN dotnet build BlueYonder.Hotels.Service.csproj -c Release -o /app

        FROM build AS publish
        RUN dotnet publish BlueYonder.Hotels.Service.csproj -c Release -o /app

        FROM base AS final
        WORKDIR /app
        COPY --from=publish /app .
        ENTRYPOINT ["dotnet", "BlueYonder.Hotels.Service.dll"]
    ```
6. Paste the following command in the **Command Line** to locate your parent project folder and then press **Enter**:
    ```bash
        cd [RepositoryRoot]\Allfiles\Mod05\DemoFiles\
    ```
7. Paste the following command to build your project using the **DockerFile** that you have created earlier and then press **Enter**:
    ```bash
        docker build -t hotels_service -f Host_In_Docker\DockerFile .
    ```
8. Paste the following command in order to run the docker container which listening on a default port and then press **Enter**:
    ```bash
        docker run --rm -it -p 8080:80 hotels_service
    ```
9. Open a browser and nevigate to **localhost:8080/api/values**.
10. Check that you are getting a good response like the following:
	```json
		["value1", "value2"]
	```
11. Run **Docker Client** on your machine and login using your **Docker Hub** credentials.
12. Paste the following command in order to login to **Docker Hub** using a command line:
    ```bash
        docker login
    ```
    >**NOTE :** Enter your **Docker Hub** user name and password and then press **Enter**.

13. Paste the following into a command line in order to create a tag name for your docker image in **Docker Hub** and then press **Enter**:
     >**NOTE**: Replace the **{Your Docker Hub Username}** variable with your actual **Docker Hub** user name.
    ```bash
        docker tag hotels_service {Your Docker Hub Username}/hotels_service
    ```
14. Paste the following command in order to push your container image into **Docker Hub** and then press **Enter**:

    >**NOTE**: Replace the **{Your Docker Hub Username}** variable with your actual **Docker Hub** user name.
    ```bash
        docker push {Your Docker Hub Username}/hotels_service
    ```
15. Open a browser and nevigate to **https://hub.docker.com**. Then **Sign In** with your credentials.
16. On the **Repositories** page, verify that you are seeing the last push as **{Your Docker Hub UserName}/hotels_service**.
17. Paste the following command in order to pull your docker image from **Docker Hub** and then press **Enter**:

    >**NOTE**: Replace the **{Your Docker Hub Username}** variable with your actual **Docker Hub** user name.
    ```bash
        docker pull {Your Docker Hub Username}/hotels_service
    ```
18. Paste the following command in order to see all your local and remote images which exists on your machine and then press **Enter**:
    ```bash
        docker images
    ```
19. Verify that you are seeing both locally and remotely **hotels_service** images.
20. Paste the following command in order to run the remotely docker image on your machine and then press **Enter**:
    ```bash
        docker run -p 4000:80 {Your Docker Hub Username}/hotels_service        
    ```
21. Open a browser and nevigate to **localhost:4000/api/values**.
22. Check that you are getting a good response like the following:
	```json
		["value1", "value2"]
	```    


# Lesson 4: Implementing serverless services

### Demonstration: HTTP-triggered Azure Function

1. Open a browser and navigate to Azure Portal at **portal.azure.com**.
2. If a page appears, asking for your email address, type your email address, and then click Continue. Wait for the sign-in page to appear, enter your email address and password, and then click Sign In.

    >**Note:** During the sign-in process, if a page appears, asking you to choose from a list of previously used accounts, select the account that you previously used, and then continue to provide your credentials.

3. If the **Windows Azure Tour** dialog box appears, click close (the **X** button).
4. In the navigation blade, click **Create a resource**. 
5. In the **New** window, select **Compute** and then choose **Function App**.
6. In the **Function App - Create** blade, enter a globally unique name in the **App name** box.
7. From the **Location** drop-down list, select the region that is closest to your location.
8. On the **Application Insights** buttons, choose **Off**.
9. Click **Create**. Wait for the function app to be created. Click the newly created **Function App**.
10. In the newly created **Function App** blade, click on the **+** sign near the **Functions** section.
11. In the new function blade, verify that **Webhook + API** is selected for the scenario and **CSharp** is selected for the language.
12. Click **Create this function** button.
13. In your new function blade, click **</> Get function URL** at the top right, select **default (Function key)**, and then click **Copy**.
14. Paste the function URL into your browser's address bar and add the following query string value to the end of this URL and press the Enter to execute the request:
    ```cs
        &name=<yourname>
    ```
    >**NOTE:** Replace **<yourname>** variable with your actual name.
 15. Check that you are getting a good response like the following:
	 ```cs
        "Hello {Your Name}"
     ```

     >**NOTE:** This is the response from **Microsoft Edge**. Other browsers may include displayed XML.
16. To see the trace output from the previous execution, return to your function in the portal and click the arrow at the bottom of the screen to expand the **Logs**.



### Demonstration: Developing, testing, and publishing an Azure Function from CLI

1. Open a browser and navigate to Azure Portal at **portal.azure.com**.
2. If a page appears, asking for your email address, type your email address, and then click Continue. Wait for the sign-in page to appear, enter your email address and password, and then click Sign In.

    >**Note:** During the sign-in process, if a page appears, asking you to choose from a list of previously used accounts, select the account that you previously used, and then continue to provide your credentials.

3. If the **Windows Azure Tour** dialog box appears, click close (the **X** button).
4. In the navigation blade, click **Create a resource**. 
5. In the **New** window, select **Compute** and then choose **Function App**.
6. In the **Function App - Create** blade, enter a globally unique name in the **App name** box.
    >**NOTE:** Save the **App name** in any code editor. You will need it when publishing a new **Azure Function** to **Azure**.
7. From the **Location** drop-down list, select the region that is closest to your location.
8. On the **Application Insights** buttons, choose **Off**.
9. Click **Create**. Wait for the function app to be created.
10. Open a **Command Line**.
11. Change directory in the command line by pasting the following command and then press **Enter**:
    ```bash
        cd [RepositoryRoot]\AllFiles\Mod05\DemoFiles\Azure Functions
    ```
12. Paste the following command in order to create a **local Functions Project**:
    ```bash
        func init MyAzureFunctionProj -n --worker-runtime dotnet
    ```
13. Change directory to the newly created function project folder by pasting the following command and then press **Enter**:
    ```bash
        cd MyAzureFunctionProj
    ```
14. Paste the following command in order to create a new **Azure Function** for your newly **Function** project and then press **Enter**:
    ```bash
        func new --language C# --template "HttpTrigger" --name MyAzureFunc
    ```
15. Paste the following command in order to test the newly **Azure Function** locally and then press **Enter**:
    ```bash
        func host start -- build
    ```
16. Open a browser and nevigate to **http://localhost:7071/api/MyAzureFunc**.
17. Add the following query string value to the end of this URL and press the Enter to execute the request:
    ```cs
        ?name=<yourname>
    ```
    >**NOTE:** Replace **<yourname>** variable with your actual name.
18. Check that you are getting a good response like the following:
	 ```cs
        "Hello {Your Name}"
     ```

     >**NOTE:** This is the respnse from **Microsoft Edge**. Other browsers may include displayed XML.       
19. Paste the following command in order to login to **Azure** with your credentials before publishing the newly **Azure Function** to **Azure** and then press **Enter**:
    ```bash
        func azure login
    ```
    >**NOTE:** You will get the following message: "To sign in, use a web browser to open the page https://microsoft.com/devicelogin and enter the code **{some code}** to authenticate​". Follow the instructions to login with your user name and password.
20. Paste the following command in order to publish your new **Azure Function** into **Azure** and then press **Enter**:
    ```bash
        func azure functionapp publish {Your App name}
    ```            
    >**NOTE:** Replace **{Your App name}** with your actual app name in azure that you wrote in point number 6.
21. Open a browser and nevigate to **https://{Your App name}.azurewebsites.net/api/MyAzureFunc?name={Your Name}**.
    >**NOTE:** Replace **{Your App name}** with your actual app name and replace **{Your Name}** variable with your actual name.
22. Check that you are getting a good response like the following:
	 ```cs
        "Hello {Your Name}"
     ```

     >**NOTE:** This is the response from **Microsoft Edge**. Other browsers may include displayed XML.  


     
©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
