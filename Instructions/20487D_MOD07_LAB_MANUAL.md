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
2. Create  **Storage account** with the name **blueyonder{YourInitials}**.
3. Create new **Blob** container with **Name** of **aircraft-images** and **Public access level** of **Container (anonymous read access for containers and blobs)**.

#### Task 2: Upload a file to the Azure Blob container from the Azure CLI

1. Use the **Azure CLI** in the **Command Line** to upload **france** image to the **Blob** container from the following path:
    ```url
    [Repository Root]\AllFiles\Mod07\Labfiles\Lab1\Assets\france.jpg
    ```

#### Task 3: Run a script to upload multiple aircraft images

1. Open **PowerShell** and run the following **uploadPhotos** script in **Assets** folder.
2. To run the script is needed **key** from the **Storage account**.
    >**Note:** The script is to upload images folder to the **Blob** container.

#### Task 4: Modify the flight reservations service to return aircraft images from the container

1. Open the **Starter** project.
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

1. Create a new **Container** and name it **manifests** with **Private** accessibility.

#### Task 2: Generate a passenger manifest and store it in the container

1. Switch to **Starter** project.
2. Locate **FinalizeFlight** method inside the **FlightsController**.
3. Refactor  the method to upload **manifests.txt** file to **manifests** container by using **GeneratedManifests** method this method create new manifests file.
   >**Note:** use **WindowsAzure.Storage** package.

#### Task 3: Generate a shared access signature to the passenger manifest and return it from the service

1. Create **SAS** token.
2. Add **GetPassengerManifest** action that return file url with **SAS** token.
3. Switch to **Command Line** and run the service.
4. Open **Microsoft Edge** browser and navigate to the following url:
   ```url
   https://localhost:5001/api/flights/finalizeflight
   ```
5. Verify **manifests.txt** file was upload to the container.
6. Navigate to the following url:
   ```url
   https://localhost:5001/api/flights/passengermanifest
   ```
7. Copy the **URL** from the response and open a new tab in the browser and paste the copied **URL**.
8. Verify that the image is shown.
9.  Wait one minute and then refresh the page, to access the storage account with expired **SAS** token.
10. Verify that the response is **Error**.

# Lab: Querying Graph Data with CosmosDB

### Exercise 1: Create the CosmosDB graph database

#### Task 1: Create a new CosmosDB graph database in the Azure Portal

1. Open **Azrue Portal**.
2. Create **Azure Cosmos DB** with the following information:
   - In **ID** type **blueyonder-destinations**{YourInitials}.
   - In **API** select **Gremlin (graph)**.
3. Click on **blueyonder-destinations**{YourInitials} Type **Azure Cosmos DB account**.
4. Add Graph with the following information:
   - In **Database id** check **Create new** and type **blueyonder**.
   - In **Graph Id** type **traveler**.

#### Task 2: Run a script to import itinerary data (flights and destinations) to the database

1. Upload data to the graph by uploading **GraphData.json** file from the **Assets** folder.
2. Explor all **vertex** and **edges** in the **traveler** graph collection.

#### Task 3: Explore the generated database

1. View all the attractions **Paris**.
2. Write a **Gremlin Query** to view the **Moscow** vertex data.
3. Write a **Gremlin Query** to get all the flight from **Moscow**.

### Exercise 2: Query the CosmosDB database

#### Task 1: Add a query to return related attractions from the itinerary service

1. Open the **Starter** project.
2. Install the package **Gremlin.Net**.
3. Update the **HostName** and **Authkey** in **appsettings.json** file.
4. Open **DestinationController** file.
5. Refactor **GetAttractions** method retrun all the attraction a giving **destination** paramter and filter by **distanceKm** paramter.

#### Task 2: Add a query to return possible interesting stop-overs to a destination

1. Query all the flight's from **source** to **destination** filtered by **maxDurationHours** in the **GetStopOvers** method:
   ```cs
   string gremlinQuary = $"g.V('{source}').repeat(outE().inV().simplePath()).until(hasId('{destination}')).path().by('id').by('duration')";
   ```
2. Execute the query witch get the flights that are less duration then maxDurationHours and modify the data to sum each path duration.

#### Task 3: Test the new service operations from a browser

1. Run the service via **Command Line**.
2. Open **Microsoft Edge** browser.
3. Navigate to the following url and verify that the **JSON** respone with attractions are displayid without **Eiffel Tower**:
   ```url
   https://localhost:5001/api/destination/attractions/Paris/4
   ```
4. Navigate to the following url and verify that **JSON** respone with attractions are displayid with **Eiffel Tower**:
   ```url
   https://localhost:5001/api/destination/attractions/Paris/6
   ```
5. Navigate to the following url and verify that **JSON** respone is only flight that are from Paris:
   ```url
   https://localhost:44355/api/Destination/StopOvers/Moscow/New York/14
   ```
6. Navigate to the following url and verify that **JSON** respone is only flight that are from Paris and Rome:
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

1. Open **Azure Portal**.
2. Create **Redis Cache** with the following information:
   - In **DNS name** type **blueyonder-cache**{YourInitials}.

#### Task 2: Locate the service key

1. Copy **Primary connection string** from the new instace **Redis Cache**

### Exercise 2: Access the cache service from code

#### Task 1: Install the StackExchange.Redis NuGet package

1. Add **StackExchange.Redis** package to the **BlueYonder.Flights.Service** project in **Starter** folder

#### Task 2: Configure the cache credentials

1. Open the project in **VSCode**.
2. Add the **Redis Connection String** to the **appsettings.json** file.

#### Task 3: Add code to use the cache service

1. Registered **IConnectionMultiplexer** interface as a **Singleton** in the **Startup** file.
2. Inject **IConnectionMultiplexer** in the **FlightsController** constructor class.
3. Refactor the **Get** action to chack if the data is cached or not
    - if the data is cached return the chach object and add a header that the data is cached.
    - else use the **FlightsRepository** to get the data and then add the new data to the redis.
    - The key to the redis cache is build by **source** and **destination** and the current date. 


#### Task 4: Configure the web application to disable instance affinity

1. Switch to **Azure Portal**.
2. Click on **blueyondermod07lab03**{YourInitials} in **App Services**
3. Click on **Scale up (App Service plan)** in the **SETTING** section:
4. Select **B1** box in the **Dev/Test** tab.
5. Click on **Scale out (App Service plan)** in the **SETTING** section.
6. Change the **Instance count** to **2** in the **Configure** tab.
7. Click on **Application settings** in the **SETTING** section.
8. Turn the **ARR affinity** to **Off**.

#### Task 5: Deploy the application to a scaled Azure Web App

1. Publish the service.

### Exercise 3: Test the application

#### Task 1: Run the application multiple times

1. Open **Microsoft Edge**  browser and navigate to the following url:
    ```url
    https://blueyondermod07lab3{YourInitials}.azurewebsites.net/api/flights
    ``` 
2. Check the **X-BlueYonder-Server** header and view the server name.

#### Task 2: Verify you are accessing multiple instances

1. Refresh the page couple of times and check that **X-BlueYonder-Server** header is change 

#### Task 3: Verify you are getting the cached data

1. Navigate to the following url:
    ```url
    https://blueyondermod07lab3{YourInitials}.azurewebsites.net/api/flights/New York/Paris/MM-DD-YYYY
    ```
    replace the end of the url with the current date
2. In **Network** tab locate the url and check the following info:
    - Try to locate the **X-Cache** header in **Response Headers** section.
    - If you dont see the header the mean that the result is not cached
3. Refresh the page and get the **X-Cache** header.
4. Refresh the page couple of times and check that **X-BlueYonder-Server** is change and that the result are **cached**.