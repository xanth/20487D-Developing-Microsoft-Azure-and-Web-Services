# Module 6: Deploying and Managing Services

# Lab: Deploying an ASP.NET Core web service on Linux

#### Scenario

In this exercise you will deploy an ASP.NET Core web service on Linux

#### Objectives

After completing this lab, you will be able to:

- C

### Exercise 1: Publish the ASP.NET Core web service for Linux

#### Task 1: Use a Docker container to build a self-contained ASP.NET Core web service

1. Open **Command Line** in **VSCode** the following directory:
   ```bash
   cd [Repository Root]\Allfiles\Mod06\Labfiles\Exercise1\Starter
   ```
2. Add a new **.dockerignore** file to **BlueYonder.Flights.Service** project.
3. Add a new **Dockerfile** file to **BlueYonder.Flights.Service** project.
4.
5. Right click on **BlueYonder.Flights.Service** and select **New File** and name it **Dockerfile**.
6. In the **Dockerfile** file, Get image named **dotnet:2.1-sdk**.
7. Restore and Build the project.
8. Get image named **dotnet:2.1-aspnetcore-runtime** and build it.

#### Task 2: Use a Linux Docker container to host the web service

1. Build the contener and name it **blueYonder**:
   ```bash
   docker build -t blueyonder .
   ```
2. Run the contener on port **1234**.
3. Navigate to the following url in **Microsoft Edge** browser:
   ```url
   http://localhost:1234/api/destinations
   ```
4. The **GET** response should return a json with all the **Destinations**.

### Exercise 2: Configure Nginx as a reverse proxy

#### Task 1: Use Nginx in a container to reverse-proxy the ASP.NET web service

1. Open **BlueYonder.Flights.Service** project in **VSCode**.
2. Add New **nginx** folder .
3. Add new **nginx.conf** file in the **nginx** folder.
4. Paste the following code:

   ```sh
   user  nginx;
   worker_processes  1;

   error_log  /var/log/nginx/error.log warn;
   pid        /var/run/nginx.pid;

   events {
       worker_connections  1024;
   }

   http {
     server {
       listen 80 default_server;

       location /test {
         proxy_pass http://host.docker.internal:1234;
         rewrite ^/test(.*)$ $1 break;
       }
     }
   }
   ```

   check **proxy_pass** this is the path of the revers proxy
   and the **test** is what need to be added for using **proxy_pass**

5. Add a new **dockerfile** file to the **nginx** folder.
6. Copy **nginx** image and change **conf** file, wuth the following code:

   ```sh
   FROM nginx

   COPY ./nginx.conf /etc/nginx/

   CMD ["nginx", "-g", "daemon off;"]
   ```

7. Switch to **Command Line**.
8. Run the following command to change directory to **nginx** folder:
   ```bash
   cd nginx
   ```
9. Open in **Command Line** the **nginx** folder and build the contener and name it **nginxproxy**
   ```bash
   docker build -t nginxproxy .
   ```
10. Run **nginx** on port **1235**.
11. Navigate to the following url in **Microsoft Edge** browser:
    ```url
    http://localhost:1235/test/api/destinations
    ```
12. The **GET** response should return a json with all the **Destinations** just like the in **Exercise 1**.
13. Stop **webapp** process.
14. Stop **ngninx** process.

#### Task 2: Create a Docker Compose file for bringing up both containers

1. Switch to **VSCode**.
2. Add new **docker-compose.yml** file in **nginx** folder..
3. Paste the following code to compose all the services that need to be running:
   ```sh
   version: '3'
   services:
     nginx:
       image: nginxproxy
       ports:
        - "1235:80"
     webapp:
       image: "myapp"
   ```
4. Locate **proxy_pass** in **nginx.conf** and change the **url** to the following url:
   ```url
   http://webapp:80
   ```
5. Build the contener after the change in the **Command Line**.
6. Run the **docker-compose**.
7. Navigate to the following url in **Microsoft Edge** browser:
   ```url
   http://localhost:1235/test/api/destinations
   ```
8. The **GET** response should return a json with all the **Destinations** just like the in **Exercise 1**.
9. Stop all the services in the **docker-compose**.

# Lab: Deploying to Staging and Production

### Exercise 1: Deploy the application to production

#### Task 1: Create a standard Web App

1. Sign in to **https://portal.azure.com** in **Microsoft Edge** browser.
1. Display all the **App Services**.
   - Click on **Add** in the **App Services** blade, letting you select app service template.
   - Click on **Web App** in the **Web** blade, overview of the template will be shown.
   - Click on **Create** button in the **Web App** blade.
2. Fill-in the following fields, to create the **Web App**:
   - In the **App Name** text box, type the following web app name: **blueyonder-flights**-{YourInitials}
   - In the **Resource Group** select **Create new**, and type in the text box below **Mod6Resource**.
   - **Create new** **App Service plan/Location**, then open **New App Service Plan** blade, fill-in the following information:
     - In the **App Service plan** text box type: **Mod6Lab2ServicePlan**.
     - Click on **Pricing tier**.
       - Select **Production** tab.
       - In **Recommended pricing tiers** select **S1**.
       - Click on **Apply**.
     - Click on **OK**.
   - Click on **Create** and wait that **App Services** is created.

#### Task 2: Configure environment variables in the production slot

1. Display all the **App Services**.
2. Click on **blueyonder-flights**-{YourInitials} app service.
3. Click on **Application settings** on the left blade menu under **SETTINGS** section.
   - Locate **Application settings** and click on **Add new setting** add the following information:
     - In **Enter a name** type **BLUEYONDER_TENANT**.
     - In **Enter a value** type **Production**.
   - Click on **Save** on top of the blade.

#### Task 3: Deploy an ASP.NET Core application to the production slot

1. Click on **Deployment credentials** under **DEPLOYMENT** section, fill-in the following information:
   - In the **FTP/deployment username** type **FTPMod6Lab2**{YourInitials}.
   - In the **Password** and **Confirm password** text box type: **Password99**.
2. Open the **Starter** project in **VSCode**.
3. Click on **TenantMiddleware** file in the **Middleware** folder.
4. Locate the **Invoke** method and paste the following code to add **X-Tenant-ID** header:
   ```cs
   string tenant = _configuration["BLUEYONDER_TENANT"] ?? "Localhost";
   httpContext.Response.Headers.Add("X-Tenant-ID", tenant);
   ```
5. Add new **PublishProfiles** folder, under **Properties** folder.
6. In the **PublishProfiles** add the file **Azure.pubxml** and add the following code:
   ```xml
   <Project>
       <PropertyGroup>
         <PublishProtocol>Kudu</PublishProtocol>
         <PublishSiteName>blueyonder-flights-{YourInitials}</PublishSiteName>
         <UserName>FTPMod6Lab2{YourInitials}</UserName>
         <Password>Password99</Password>
       </PropertyGroup>
   </Project>
   ```
   > **Note :** This file have the information to deploy to Azure, with the **Deployment credentials** that we added in point 1.
8.  Switch to **Command Line**, and paste the following command:
    ```bash
    dotnet publish /p:PublishProfile=Azure /p:Configuration=Release
    ```
    > **Note :** If the there was an error in the publish process, restart the blueyonder-flights-{YourInitials} app services.

#### Task 4: Run the application and verify its output

2. Open **Develpper Tools** in **Microsoft Edge** browser.
3. In the **Develpper Tools** navigate to **Network**.
4. Navigate to the following url:
   ```url
   https://blueyonder-flights-{YourInitials}.azurewebsites.net/api/destinations
   ```
5. In **Network** tab locate the url and check the following info:
   - Locate the **X-Tenant-ID** in **Response Headers** section and verified that the value is **Production**.

### Exercise 2: Create a staging slot

#### Task 1: Create a staging deployment slot

2. Display all the **App Services** in your **Azure Portal**.
3. Click on **blueyonder-flights**-{YourInitials} app service.
4. Click on **Deployment slots** on the left blade menu under **DEPLOYMENT** section.
   - Click on **Add Slot**:
     - In **Name** type **Staging**
     - In **Configuration Source** select **blueyonder-flights**-{YourInitials}.
     - Click on **OK**.

#### Task 2: Configure environment variables in the staging slot

1. In **Deployment slots** blade click on **blueyonder-flights**-{YourInitials}-**staging**.
2. Click on **Application settings** under **SETTINGS** section.
3. Change the value to **Staging** in the **BLUEYONDER_TENANT** in **Application settings** and click **Save**.

#### Task 3: Deploy a newer version to the staging slot

1. Switch to **VSCode**.
2. Double click on **DestinationsController** under **Controllers** folder.
3. Add more **destinations**
   ```cs
   _destinations.Add(new Destination { Id = 6, CityName = "Milan", Airport = "Malpensa" });
   _destinations.Add(new Destination { Id = 7, CityName = "Rome", Airport = "Leonardo da Vinci-Fiumicino" });
   ```
4. Add new **Staging.pubxml** file in **PublishProfiles** folder, under **Properties** folder.
5. Paste the following code:
   ```xml
   <Project>
       <PropertyGroup>
         <PublishProtocol>Kudu</PublishProtocol>
         <PublishSiteName>blueyonder-flights-{YourInitials}-staging</PublishSiteName>
         <UserName>FTPMod6Lab2{YourInitials}</UserName>
         <Password>Password99</Password>
       </PropertyGroup>
   </Project>
   ```
7. Publish in the staging slot in the **Command Line**.
   ```bash
   dotnet publish /p:PublishProfile=Staging /p:Configuration=Release
   ```

#### Task 4: Run the application from staging and verify its output

2. Open **Develpper Tools** in **Microsoft Edge** browser.
3. In the **Develpper Tools** navigate to **Network**.
4. Navigate to the following url:
   ```url
   https://blueyonder-flights-{YourInitials}-staging.azurewebsites.net/api/destinations
   ```
5. View the respone with all the destinations include **Milan** and **Rome**.
6. In **Network** tab locate the url and check the following info:
   - Locate the **X-Tenant-ID** in **Response Headers** section and verified that the value is **Staging**.


### Exercise 3: Swap the environments

#### Task 1: Perform a swap of the staging and production environments

1. Switch to **Azure Portal**.
2. Display all the **App Services**.
3. Click on **blueyonder-flights**-{YourInitials} app service.
4. In **Overview** blade click on **Swap** on the top bar.
5. In **Swap** blade added the following steps:
    - In **Swap type** select **Swap**.
    - In **Source** select **production**.
    - In **Destination** select **Staging**.
    - Click **OK**. 

#### Task 2: Run the production application and verify its output

1. Switch to **Microsoft Edge** browser with the production url (Exercise 1, Task 4), and refresh the page (prass **F5**).
2. View the respone with all the destinations include **Milan** and **Rome**.
3.  In **Network** tab locate the url and check the following info:
    - Locate the **X-Tenant-ID** in **Response Headers** section and verified that the value is **Staging**.

#### Task 3: Undo the swap

1. Switch to **Azure Portal**.
2. Display all the **App Services**.
3. Click on **blueyonder-flights**-{YourInitials} app service.
4. In **Overview** blade click on **Swap** on the top bar.
5. In **Swap** blade added the following steps:
    - In **Swap type** select **Swap**.
    - In **Source** select **Staging**.
    - In **Destination** select **production**.
    - Click **OK**. 
6. Switch to **Microsoft Edge** browser with the production url (Exercise 3, Task 2), and refresh the page (prass **F5**).
8. View the respone with all the destinations without **Milan** and **Rome**.
9.  In **Network** tab locate the url and check the following info:
    - Locate the **X-Tenant-ID** in **Response Headers** section and verified that the value is **Production**.
   
#### Task 4: Configure the application settings as sticky to the slot

1. Switch to **Azure Portal**.
2. Display all the **App Services**.
3. Click on **blueyonder-flights**-{YourInitials} app service.
4. Click on **Application settings** on the left blade menu under **SETTINGS** section.
5. Locate **BLUEYONDER_TENANT** in **Application settings** and check the **SLOT SETTING**.
6. Click on **Save** on top of the blade.

#### Task 5: Redo the swap and re-test the output of the production slot

1. Switch to **Azure Portal**.
2. Display all the **App Services**.
3. Click on **blueyonder-flights**-{YourInitials} app service.
4. In **Overview** blade click on **Swap** on the top bar.
5. In **Swap** blade added the following steps:
    - In **Swap type** select **Swap**.
    - In **Source** select **production**.
    - In **Destination** select **Staging**.
    - Click **OK**. 
6. Switch to **Microsoft Edge** browser with the production url (Exercise 3, Task 2).
7. Refresh the page (prass **F5**).
8. View the respone with all the destinations with **Milan** and **Rome**.
9.  In **Network** tab locate the url and check the following info:
    - Locate the **X-Tenant-ID** in **Response Headers** section and verified that the value is **Production**.


# Lab: Publishing a Web API with Azure API Management

### Preparation Steps

1. Run: **Install-Module azurerm -AllowClobber -MinimumVersion 5.4.1** in **PowerShell** as **Administrator**.
2. Navigate to **[repository root]\Mod06\Labfiles\Exercise3\Setup** and run:
    ```batch
     .\createAzureServices.ps1
    ```
4. You will be asked to supply a **Subscription ID**, which you can get by **http://portal.azure.com** under **My subscriptions**.  
5. Enter your details, and then sign in to your subscription.
6. Follow the on-screen instructions. Wait for the deployment to complete successfully.

### Exercise 1: Create an Azure API Management instance

#### Task 1: Create an Azure API Management instance in the Azure Portal

1. Navigate to **https://portal.azure.com** in **Microsoft Edge** browser.
2. Sign In your **Azure** subscription.
3. Click on **Create a resource**.
4. Create **API management**.
5. In the **API Management service** page, enter:
   - Name: **blueyonder-api**{YourInitials}
   - Resorce group:
        - Select **Use existing**.
        - Combobox select **Mod6Lab3-RG**.
   - Organization name: Your organization
6. Go to **All resources** and then click on the new **API Management** that was created.

#### Task 2: Create an Azure API product and API manually

1. Click on **Add** new **Products** under **API MANAGEMENT**, on the top menu bar then add:
    - In **Display name** type **Blueyonder**.
    - In **Description** type **Blueyounder API**.
    - Mark **Requires subscription**.
2. Enter the following details in **Blank API**, under **APIs** under **API MANAGEMENT** section;
    - In **Display name** type **BlueYonder**.
    - In **Name** type **blueyonder**.
    - In **Web service URL** type **https://blueyonder{YourInitials}.azurewebsites.net/api**.
    - In **Api URL suffix** type **api**.
    - In **Products** select **blueyonder**.
3. Click on **+ Add operation**, and enter the following details to create **GET** destinations operation:
    - In **Display name** type **Destinations**.
    - In **Name** type **destinations**.
    - In **URL** select **GET** and type **/destinations**.
4. Click on **+ Add operation**, then enter the following details to create **GET** destinations by id operation:
    - In **Display name** type **Destinations By ID**.
    - In **Name** type **destinations-by-id**.
    - In **URL** select **GET** and type **/destinations/{id}**.
    - Select **Template** tab, then enter the following details:
        - In **Name** type **id**.

### Exercise 2: Test and manage the API

#### Task 1: Test the API from the Azure API Management Portal

1. Click on **Destinations** in **BlueYonder** API.
2. Click on **Test** tab in **Destinations** operation blade.
3. Click on **Send** to test the **GET Destinations** API.
4. Verified that the response is 200 with destinations list.
5. Click on **Destinations By ID** in **BlueYonder** API.
6. Click on **Test** tab in **Destinations By ID** operation blade.
7. Type **1** under **VALUE** in the **id** parameter in the **Template parameters** section.  
8. Click on **Send** to test the **GET Destinations by ID** API.
9. Verified that the response is 200 with **Seattle** destination.

#### Task 2: Configure a caching policy for flight availability and weather information APIs

1. Click on **Destinations** in **BlueYonder** API.
2. Click on **Design** tab.
3. In the **Inbound processing** window, click the pencil.
4. Click on **Caching**, then enter the following details:
    - Under **Cache respones** select **On**.
    - In **Duration** type **60**.
    - Click on **Save**
5. Click on **Test** tab.
6. Click on **Send** then click on **Trace** tab.
7. Verified that **Backend** section exist that resquest was handle.
8. Click on **Send** then click on **Trace** tab.
9. Verified that **Backend** section not exist that resquest was cache.

#### Task 3: Configure a request rate limit (throttling) for the flight availability API

1. Click on **Destinations By ID** in **BlueYonder** API, and then on **Design** tab.
2. In the **Inbound processing** window, click the triangle and select **Code editor**.
3. Position the cursor inside the **\<inbound\>** element.
4. In the right window, under **Access restriction policies**, click **+ Limit call rate per key**.
5. Modify your **rate-limit-by-key** code (in the **\<inbound\>** element) to the following code:
    ```xml
     <rate-limit-by-key calls="2" renewal-period="60" counter-key="@(context.Subscription.Id)" />
    ```

#### Task 4: Test the rate limit

1. Click on **Destinations By ID** in **BlueYonder** API.
2. Click on **Test** tab.
3. Type **1** under **VALUE** in the **id** parameter in the **Template parameters** section.  
4. Press **Send** two times in a row.
5. After sending the request 2 times, you get **429 Too many requests** response.
6. Wait 60 seconds and press **Send** again. This time you should get a **200 OK** response.
