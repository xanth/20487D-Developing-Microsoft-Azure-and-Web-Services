# Module 7: Implementing data storage in Azure

#### Scenario

In this exercise you will be able to store files in Azure Storage, Querying Graph Data with CosmosDB, Caching out-of-process with Azure Redis cache.

#### Objectives

After completing this lab, you will be able to:

- Store publicly accessible files in Azure Blobs
- Generate and store private files in Azure Blobs
- Create the CosmosDB graph database
- Query the CosmosDB database
- Create the Azure Redis Cache service
- Access the cache service from code

# Lab: Storing Files in Azure Storage

### Exercise 1: Store publicly accessible files in Azure Blobs

#### Task 1: Store publicly accessible files in Azure Blobs

1. Open **Azure Portal**.
2. Add a **Storage account** with **Account kind** of **StorageV2**, and **Replication** of **Locally-redundant storage (LRS)**.
3. Create new container with **Name** of **aircraft-images** and **Public access level** of **Container (anonymous read access for containers and blobs)**.

#### Task 2: Upload a file to the Azure Blob container from the Azure CLI

1. Login with your credentials to **Azure Portal** via **Command Line**.
2. Get the **connection string** of **Azure Blob container** and copy it.
3. Set the **connection string** as environment variable.
4. Upload image from **Assets** to container.

#### Task 3: Run a script to upload multiple aircraft images

1. Get the storage account keys list via the **Command Line**.
2. Copy the **key** from **value property** in the first object in the array.
3. Open **Powershell** and change directory to **Assets** folder.
4. Run the following command to run **uploadPhotos** script to upload images folder to the **Storage account** and fill the following details:
   ```bash
   .\uploadPhotos.ps1
   ```
   Follow the instructions and in **Plase enter storage account key** paste the value from **step 2**.

#### Task 4: Modify the flight reservations service to return aircraft images from the container

1. Open the **Starter** project in **VSCode**.
2. In **AircraftController** Locate **\_baseUrl** field and replace **YourInitials** with your actual initials.

#### Task 5: Test the service from a browser

1. Run the service via the **Command Line**
2. Open **Microsoft Edge** browser and navigate to the following url:
   ```url
   https://localhost:5001/api/aircraft/image/france
   ```
3. Copy the **URL** from the response.
4. Open a new tab in the browser and paste the copied **URL**.
5. Verify that the image is shown.
6. Stop the service.

### Exercise 2: Generate and store private files in Azure Blobs

#### Task 1: Create an Azure Blob container

1. Switch to **Azure Portal** and click on **blueyonder{YourInitials}** stroage account
2. In **Blobs** click on **Container**, to Create new container with the following information:
   - In the **Name** textbox type **manifests**.
   - In the **Public access level** combobox select **Private (no anonymous access)**.

#### Task 2: Generate a passenger manifest and store it in the container

1. Switch to **VSCode**.
2. Add **connection string** to the **appsettings.json** file, in **BlueYonder.Flights.Service**.
3. Add the following private fields inside the **FlightsController**:
   - CloudBlobContainer
   - CloudStorageAccount
   - string \_manifests
4. Add **IConfiguration** injection as a parameter to the constructor.
5. Upload a file to the container in the **FinalizeFlight** method.

#### Task 3: Generate a shared access signature to the passenger manifest and return it from the service

1. Create **SAS** token.
2. Add **GetPassengerManifest** action that return file url with **SAS** token.
3. Switch to **Command Line** and run the service.
4. Open **Microsoft Edge** browser and navigate to the following url:
   ```url
   https://localhost:5001/api/flights/finalizeflight
   ```
5. Switch to **Azure Portal** and navigate to **manifests** container in the **blueyonder{YourInitials}** blade.
6. Refresh the page and verify **manifests.txt** file was uploaded to the container.
7. Navigate to the following url:
   ```url
   https://localhost:5001/api/flights/passengermanifest
   ```
8. Copy the **URL** from the response and open a new tab in the browser and paste the copied **URL**.
9. Verify that the image is shown.
10. Wait one minute and then refresh the page, to access the storage account with expired **SAS** token.
11. Verify that the response is **Error**.

# Lab: Querying Graph Data with CosmosDB

### Exercise 1: Create the CosmosDB graph database

#### Task 1: Create a new CosmosDB graph database in the Azure Portal

1. Open **Microsoft Edge** browser and navigate to **https://portal.azure.com**.
2. Create **Azure Cosmos DB** with the following information:
   - In **ID** type **blueyonder-destinations**{YourInitials}.
   - In **API** select **Gremlin (graph)**.
3. Click on **blueyonder-destinations**{YourInitials} Type **Azure Cosmos DB account**.
4. Add Graph with the following information:
   - In **Database id** check **Create new** and type **blueyonder**.
   - In **Graph Id** type **traveler**.

#### Task 2: Run a script to import itinerary data (flights and destinations) to the database

1. Upload all the data with **JSON** file.
   - Select **GraphData.json** file in the path **[Repository Root]\Allfiles\Mod07\LabFiles\Lab2\Assets** and click on **Upload**.
2. Click on **Data Explorer** and expand **blueyonder**.
3. Click on **Graph** under **traveler**.
4. In textbox type **g.V()** and click on **Execute Gremlin Query** to execute the gremalin query to return all the **vertex**.

#### Task 3: Explore the generated database

1. Click on **Paris** vertex to see all the edges that connected from **Paris**.
2. Double click on the small vertex under **Paris** to see all the attractions in **Paris**.
3. Click on **JSON** tab to view the result in **JSON** format.
4. In textbox type **g.V('Moscow')** then click on **Execute Gremlin Query**.
5. View the **Moscow** vertex data.
6. In textbox type **g.V('Moscow').outE('flight')** then click on **Execute Gremlin Query**.
7. View all the flight from **Moscow**.

### Exercise 2: Query the CosmosDB database

#### Task 1: Add a query to return related attractions from the itinerary service

1. Click on **Keys** under **SETTINGS** section.
2. Copy the **PRIMARY KEY** for the next step.
3. Open **Command Line** and change directory to the **starter** project.
4. Install the package **Gremlin.Net**.
5. Open the project in **VSCode**.
6. Click on **appsettings.json** under **BlueYonder.Itineraries.Service** project.
7. Replace **{YourInitials}** in **HostName**
8. Paste the **PRIMARY KEY** from the previous step, in **Authkey**
9. Add **GremlinServer** field to **DestinationController**.
10. Add a **Constructor**:
    ```cs
    public DestinationController(IConfiguration configuration)
    {
        string authKey =  configuration["Authkey"];
        string hostname = configuration["HostName"];
        string database = "blueyonder";
        string collection = "traveler";
        int port = 443;
        _gremlinServer = new GremlinServer(hostname, port, enableSsl: true,
                                             username: "/dbs/" + database + "/colls/" + collection,
                                             password: authKey);
    }
    ```
11. Query all the attractions in the destination flitered by distance, in **GetAttractions** method:
    ```cs
    string gremlinQuary = $"g.V('{destination}').inE('located-in').has('distance', lt({distanceKm})).outV()";
    ```
12. Execute the query with:
    ```cs
    using (var client = new GremlinClient(_gremlinServer, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
    {
        var result = await client.SubmitAsync<dynamic>(gremlinQuary);
        return JsonConvert.SerializeObject(result); ;
    }
    ```

#### Task 2: Add a query to return possible interesting stop-overs to a destination

1. Query all the flight's from **source** to **destination** filtered by **maxDurationHours** in the **GetStopOvers** method:
   ```cs
   string gremlinQuary = $"g.V('{source}').repeat(outE().inV().simplePath()).until(hasId('{destination}')).path().by('id').by('duration')";
   ```
2. Execute the query witch get the flights that are less duration then maxDurationHours.

#### Task 3: Test the new service operations from a browser

1. Run the service via **Command Line**.
2. Open **Microsoft Edge** browser.
3. Navigate to the following url and verify that the **JSON** respone with attractions are displayid without **Eiffel Tower**:
   ```url
   https://localhost:5001/api/destination/attractions/Paris/4
   ```
4.
5. Navigate to the following url and verify that **JSON** respone with attractions are displayid with **Eiffel Tower**:
   ```url
   https://localhost:5001/api/destination/attractions/Paris/6
   ```
6. Navigate to the following url and verify that **JSON** respone is only flight that are from Paris:
   ```url
   https://localhost:44355/api/Destination/StopOvers/Moscow/New York/14
   ```
7. Navigate to the following url and verify that **JSON** respone is only flight that are from Paris and Rome:
   ```url
   https://localhost:44355/api/Destination/StopOvers/Moscow/New York/20
   ```

# Lab: Caching out-of-process with Azure Redis cache

### Preparation Steps

1. Open **PowerShell** as **Administrator** and run the following command: **Install-Module azurerm -AllowClobber -MinimumVersion 5.4.1**.
2. Navigate to **[repository root]\Mod07\Labfiles\Lab3\Setup**.
3. Run the following command:
   ```batch
    .\createAzureServices.ps1
   ```
4. Enter your details, and then sign in and follow the on-screen instructions. Wait for the deployment to complete successfully.
5. Write down the name of the Azure App Service that is created.

### Exercise 1: Create the Azure Redis Cache service

#### Task 1: Create the Azure Redis Cache service

2. Navigate to **https://portal.azure.com** in **Microsoft Edge** browser.
3. Sign In and click **+ Create a resource**.
4. Create **Redis Cache** with the following information:
   - In **DNS name** type **blueyonder-cache**{YourInitials}
   - In **Resource Group** select **Create new** and type **Mod07Lab3**.

#### Task 2: Locate the service key

1. Display all the **Resources**.
2. Click on **blueyonder-cache**{YourInitials}.
3. Click on **Access keys** in the **SETTINGS** section.
4. Copy the **Primary connection string (StackExchange.Redis)** to the next exercise.

### Exercise 2: Access the cache service from code

#### Task 1: Install the StackExchange.Redis NuGet package

1. Change directory to the **BlueYonder.Flights.Service** service in the **Command Line**:
   ```bash
   cd [Repository Root]\Allfiles\Mod07\LabFiles\Lab3\Starter\BlueYonder.Flights\BlueYonder.Flights.Service
   ```
2. Install the following package **StackExchange.Redis**:
   ```bash
    dotnet add package StackExchange.Redis --version=1.2.6
   ```

#### Task 2: Configure the cache credentials

1. Open the project in **VSCode**.
2. Expand **BlueYonder.Flights.Service** folder then click on **appsettings.json** file.
3. Paste the following code to add redis connection string:
   ```json
   "RedisConnectionString": "[RedisConnectionString]"
   ```
   replace the value with connection string from the previous exercise

#### Task 3: Add code to use the cache service

1. Click on **Startup** file.
2. Locate the **ConfigureServices** method and paste the following code:
   ```cs
   services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(Configuration["RedisConnectionString"]));
   ```
   **IConnectionMultiplexer** is injected with a connection to the Redis in Azure
3. Add **IDatabase** field in **FlightsController** controller:
4. Inject **IConnectionMultiplexer** in the constructor.
5. Add key to check in the **redis** cache in the **Get** method with **source** and **destination** paramters
   ```cs
   var key = source + destination + date.Date.ToShortDateString();
   ```
6. Check if the key is exist in the **redis** cache.
7. Add the following code if the key dont exist:
   ```cs
   if (!cacheResult.HasValue)
   {
       var result = _flightsRepository.GetFlightByDate(sourcedestination, date);
       if (result == null)
           return NotFound();
       _redisDB.StringSet(key,JsonConvert.SerializeObject(result));
       return Ok(result);
   }
   ```
   if the key dont exist in the **redis** cache then get the data from the database and add new key and data to **redis** cache
8. Paste the following code if the key is exist
   ```cs
   Request.HttpContext.Response.Headers.Add("X-Cache","true");
   return Ok(cacheResult.ToString());
   ```
   Then add **header** that the data is cached and return the data

#### Task 4: Configure the web application to disable instance affinity

1. Switch to **Azure Portal**.
2. Click on **blueyondermod07lab03**{YourInitials} in **App Services**
3. Click on **Scale up (App Service plan)** in the **SETTING** section:
4. Select **B1** box in the **Dev/Test** tab.
5. Click on **Scale out (App Service plan)** in the **SETTING** section.
6. Change the **Instance count** to **2** in the **Configure** tab.
7. Click on **Application settings** in the **SETTING** section.
8. Turn the **ARR affinity** to **Off**.
