# Module 7: Implementing data storage in Azure

1. Wherever you see a path to file starting at [Repository Root], replace it with the absolute path to the directory in which the 20487 repository resides. 
 e.g. - you cloned or extracted the 20487 repository to C:\Users\John Doe\Downloads\20487, then the following path: [Repository Root]\AllFiles\20487D\Mod01 will become C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01
2. Wherever you see **{YourInitials}**, replace it with your actual initials.(for example, the initials for John Do will be jd).
3. Before performing the demonstration, you should allow some time for the provisioning of the different Azure resources required for the demonstration. It is recommended to review the demonstrations before the actual class and identify the resources and then prepare them beforehand to save classroom time.

# Lesson 2: Accessing Data in Azure Storage

### Demonstration: Accessing blob storage from an ASP.NET Core application

#### Demonstration Steps

1. Open **Azure Portal**.
2. Click **Storage account** on the left menu panel, to display all the **Storage account**.
3. Click on **Add** in the **Storage account** blade and add the following information:
    - In the **Name** textbox type: **blueyonder{YourInitials}**.
    - In the **Account kind** combobox select **StorageV2 (general purpose v2)**.
    - In the **Replication** combobox select **Locally-redundant storage (LRS)**.
    - In the **Resource group** section check **Create new** and in the textbox type **Mod7Demo1**.
    - Click on **Create**.
4. Click on **blueyonder{YourInitials}** stroage account.
5. Click on **Blobs** in **BLOB SERVICE** section on the left menu.
6. Click on **Container** on the top bar to create new container. then add the following information:
    - In the **Name** textbox type **vouchers**.
    - In the **Public access level** combobox select **Private (no anonymous access)**.
    - Click on **OK**.
7. Click on **Access keys** in **SETTINGS** section on the left menu.
8. Copy the **Connection string** value.
9. Open **Command Line**.
10. Run the following command to change directory to the **BlueYonder.Hotels.Service** project:
    ```bash
    cd [Repository Root]\Allfiles\Mod07\Demofiles\Mod7Demo1Blob
    ```
11. Run the following command to open the project in **VSCode**:
    ```bash
    code .
    ```
12. Click on **appsettings** file and replace the **connection string**.
13. Expand **Controllers** and click on **ReservationController** and explore the following method:
    - Explore the constructor that connected to **blob container**.
    - Explore the **CreateVoucher** method that create and upload **voucher** file to the storage.
    - Explore  the **GetVoucher** method that get the file by id from the storage.
    >**Note:** Connection to **Storage** via **WindowsAzure.Storage** nuget.
14. Switch to **Command Line** and run the following command to run the application:
    ```bash
    dotnet run
    ```
15. Open **Powershell**.
16. Run the following command to invoke **CreateVoucher** action:
    ```bash
    [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12;
    Invoke-WebRequest -Uri https://localhost:5001/api/reservation/createvoucher -ContentType "application/json" -Method POST -Body "'{Your Name}'"
    ```
17. Copy the **Guid** key form the **Content**.
18. Switch **Azure Portal** locate **vouchers** container and verify that new file was added.
19. Switch **Powershell** and run the following command to invoke **GetVoucher** action to receive the new file from the blob continar:
    ```bash
    $request = Invoke-WebRequest -Uri https://localhost:5001/api/reservation/Voucher/{Guid voucher}  -Method Get
    $request.Content
    ```
20. View **voucher** file contnet from the blob continar. 


# Lesson 3: Working with Structured Data in Azure

### Demonstration: Uploading an SQL database to Azure and accessing it locally

#### Demonstration Steps

1. Open **Microsoft Edge** browser.
2. Navigate to **https://portal.azure.com** and login with your credentials.
3. Click **Storage accounts** on the left menu panel.
4. Click on **Add** on the top bar.
5. In **Create storage account** view fill in the following details:
   - In **Name** textbox type **mod7demo2**{YourInitials}.
   - In **Resource group** select **Create new** and type **Mod7demo2ResourceGroup**.
   - Click on **Create**.
6. Wait to the **Storage Account** to create and then click on **Refresh** button on the top bar in **Storage accounts** view.
7. Click on **mod7demo2**{YourInitials} storage account.
8. Click on **Blobs** on the left blade menu under **BLOB SERVICE** section.
   - Click on **+ Container** and type **bacpaccontainer**.
   - Click **OK**
   - Click on **bacpaccontainer**.
   - Click on **Upload**.
   - Click on the folder button and navigate to [Repository Root]\Allfiles\Mod07\Demofiles\Mod7Demo2bacpac then select **Mod7Demo2DB.bacpac** file.
   - Click on **Upload**.
9. Click **Create a resource** on the left menu panel.
    - Type **SQL server** in the **search** textbox.
    - Select **SQL server (logical server)**.
    - Click on **Create**.
    - In **Server name** type **mod7demo2sqlserver**{YourInitials}.
    - In **Server admin login** type **MyAdmin**.
    - In **Password** and **Confirm Password** type **Passowrd123!**.
    - In **Resource group** select **Use existing** and select **Mod7demo2ResourceGroup** resource group.
    - Click **Create**.
10. Click on **All resources**, then click on **mod7demo2sqlserver**{YourInitials}.
11. In **mod7demo2sqlserver**{YourInitials} view click on  **Import database** on the top bar.
12. In **Import database** view fill the following details:
    - Click on **Storage**,
    - Click on **mod7demo2**{YourInitials}.
        - In **Containers** view click on **bacpaccontainer**.
        - Click on **Mod7Demo2DB.bapac** file.
        - Click on **Select**.
    - In **Password** textbox type **Password123!**
    - Click on **Create**.
13. Wait for the db to be created.
14. click **SQL databases** on the left menu panel.
15. Click on **Mod7Demo2DB**.
16. Cick on **Set server firewall**.
17. Click on **Add client IP**, then click on **Save**.
18. Open **SQL Operations Studio**, and fill in the following details:
    - In **Connection type** select **Microsoft SQL Server**.
    - In **Server** textbox type **mod7demo2sqlserver{YourInitials}.database.windows.net**.
    - In **User name** type **MyAdmin**.
    - In **Password** type **Password123!**
    - Click on **Connect**.
19. Now you can query the database locally.

### Demonstration: Using CosmosDB with the MongoDB API

#### Demonstration Steps

1. Open **Microsoft Edge** browser.
2. Navigate to **https://portal.azure.com** and login with your credentials.
3. Click **Azure Cosmos DB** on the left menu panel.
4. Click **Add** on the top bar.
5. In **Azure Cosmos DB** view fill the following details:
   - In **ID** textbox type **mod7demo3**{YourInitials}.
   - In **API** select **MongoDB**.
   - In **Resource group** select **Create new** and type **Mod7Demo3ResourceGroup**
   - click on **Create**.
6. Wait to the database to be created.
7. Click **Azure Cosmos DB** on the left menu panel, then click on **mod7demo3**{YourInitials}.
8. Click on **Data Explorer** on the left blade menu in **mod7demo3**{YourInitials} view.
9. Click on **New Collection** on the top bar and fill the following details:
   - In **Database id** select **Create new** and type **mydb**.
   - In **Collection id** type **customers**.
   - Click on **Add unique key**, then type **customerId**.
   - Click on **Create**.
10. Click on **New Collection** on the top bar and fill the following details:
   - In **Database id** select **Use existing** then select **mydb**.
   - In **Collection id** type **orders**.
   - Click on **Create**.
11. Right click on **customers** collection, then select **New Shell**.
12. Copy all the content from **CustomersCollectionData.json** file in [Repository Root]\Allfiles\Mod07\DemoFiles\Mod7Demo3Assets.
13. In **shell 1** console paste the content from **CustomersCollectionData.json** and Press **Enter**, to add **customers** data.
12. Copy all the content from **OrdersCollectionData.json** file in [Repository Root]\Allfiles\Mod07\DemoFiles\Mod7Demo3Assets.
13. In **shell 1** console paste the content from **OrdersCollectionData.json** and Press **Enter**, to add **orders** data.
14. Right click on **orders** collection and select **New Query**
15. Paste the following query and click on **Execute Query** to get all the orders:
    ```json
    {}
    ```
16. Paste the following query and click on **Execute Query** to get all the orders **price** greater than 20:
    ```json
    { price: {$gt: 20} }
    ```
17. Paste the following query and click on **Execute Query** to get all the orders of customer 1:
    ```json
    { customerId: "1" }
    ```
18. close all windows.

### Demonstration: Using CosmosDB with a graph database API

#### Demonstration Steps

1. Open **Microsoft Edge** browser.
2. Navigate to **https://portal.azure.com** and login with your credentials.
3. Click **Azure Cosmos DB** on the left menu panel.
4. Click **Add** on the top bar.
5. In **Azure Cosmos DB** view fill the following details:
   - In **ID** textbox type **mod7demo4**{YourInitials}.
   - In **API** select **Gremlin (graph)**.
   - In **Resource group** select **Create new** and type **Mod7Demo4ResourceGroup**
   - click on **Create**.
6. Wait to the database to be created.
7. Click **Azure Cosmos DB** on the left menu panel, then click on **mod7demo4**{YourInitials}.
8. Click on **+ Add Graph** in the top menu bar then add the following details:
    - In **Database id** check **Create new** and type **mygraphgdb**.
    - In **Graph Id** type **taskManager**.
    - Click on **OK**.
9. Expand **mygraphdb** and click on **taskManager**.
10. Click on **Upload** in the top menu bar to upload all the data with **JSON** file.
    - Click on the **folder** icon and select **GraphData.json** file in the path **[Repository Root]\Allfiles\Mod07\DemoFiles\Mod7Demo4Assets**.
    - Click on **Upload**.
11. Expand **taskManager** and click on **Graph**.
12. Click on **Execute Gremlin Query** to get all the vertex in the graph.
13. Explore all the graph connections.
14. Click on **JSON** tab to view the **gremlin query** result in **JSON** format.
15. Type **g.V('Lab 1').outE('assigned-to')** in the textbox to get all the edges from **Lab 1** vertex and click on **Execute Gremlin Query**.
16. View the **JSON** result that show all the users that assigned to **Lab 1** task. 
17. Type **g.V('Sean Stewart').inE('managed-by')** in the textbox to get all the edges from **Sean Stewart** vertex and click on **Execute Gremlin Query**.
18. View the **JSON** result that show all the users that managed by **Sean Stewart**.
19. Close all windows. 

# Lesson 4: Geographically Distributing Data with Azure CDN

### Demonstration: Configuring a CDN endpoint for a static website

#### Demonstration Steps

1. Open **Microsoft Edge** browser.
2. Navigate to **https://portal.azure.com** and login with your credentials.
3. Click **Storage accounts** on the left menu panel.
4. Click **Add** on the top bar.
5. In **Create storage account** view fill the following details:
   - In **Name** textbox type **mod7demo5**{YourInitials}.
   - In **Account kind** select **StorageV2(general purpose v2)**.
   - In **Resource group** select **Create new** and type **Mod7Demo5ResourceGroup**
   - click on **Create**.
6. Wait for to the storage account to be created.
7. Click **Storage accounts** on the left menu panel then click on **mod7demo5**{YourInitials}.
8. In **mod7demo5**{YourInitials} view click on **Static website** under **SETTINGS** section on the left navigation bar.
9. Click on **Enabled**.
10. Click on **Save** on the top bar.
11. Click on **$web** container that was created as a part of **static website** enablement.
12. Click on **Upload** on the top bar.
    - Click on **folder** icon and navigate to **[Repository Root]\Allfiles\Mod07\DemoFiles\Mod7Demo5Assets**.
    - Upload all files in the repository.
    - Click on **Upload**.
13. Return to **mod7demo5{YourInitials} - Static website (preview)**.
14. In **Index document name** type **index.html**.
15. Click on **Save** on the top bar.
16. Copy **Primary endpoint** value for later use.
    >**Note:** You can browse now to the **Primary endopint** to **index.hml** or **airplane1.jpg**.
17. Click on **+ Create a resource** on the left menu panel.
18. In the search box type **CDN** and select **CDN**, then click **Create**.
19. In **CDN profile** view fill in the following details:
    - In **Name** type **MyCDNProfile**{YourInitials}.
    - In **Resource group** select **Use existing**, then select **Mod7Demo5ResourceGroup**.
    - In **Pricing tier** select **Premium Verizon**.
    - Check in **Create a new CDN endpoint now**.
    - In **CDN endpoint name** type **mod7demo5endpoint**{YourInitials}.
    - In **Origin type** select **Custom origin**.
    - In **Origin hostname** type paste **Primary endpoint** value from point 16 with the following format **<account-name>.<zone-name>.web.core.windows.net**.
    - Click on **Create**.
20. wait for the **CDN profile** and **Endpoint** to be created.
    > **Note:** It can take up to one hour the sync between the **storage static website** and the **endpoint**.
21. Click on **All resources**, then click on the endpoint **mod7demo5endpoint**{YourInitials}.
22. Click on **Endpoint hostname** link.
    >**Note:** You should see **My Static Website**, if not wait a litle more.
23. In the browser press on **F12** and click on **Network** tab.
24. In the **URL** add **/airplane1.jpg** and press **Enter**.
25. In the **Network** tab click on **airplane1.jpg** and check that in the **Response Headers** there is not **x-cache: HIT**.
26. Refresh 5 time the page in a row.
27. Check again in the **Response Headers** and locate **x-cache: HIT**.
28. Close all windows.

# Lesson 5: Scaling with Out-of-Process Cache

### Demonstration: Using Azure Redis cache for caching data

#### Demonstration Steps

1. Open **Azure Portal**.
2. Click **+ Create a resource** on the left menu panel.
3. Type in the search box **Redis Cache**, click on **Create** and add the following information:
    - In **DNS name** type **mod7demo6redis**{YourInitials}
    - In **Resource Group** select **Create new** and type **Mod07Demo6ResourceGroup**.
    - Click on **Create**.
4. Wait that the **Service** will create.
5. Click on **All resource** on the left menu panel, to display all the **Resources**.
6. Click on **mod7demo6redis**{YourInitials}.
7. Click on **Access keys** in the **SETTINGS** section.
8. Copy the **Primary connection string (StackExchange.Redis)** for later use.
9.  Open **Command Line**.
10. Run the following command to change directory to the **BlueYonder.Hotels.Service** service:
    ```bash
    cd [Repository Root]\Allfiles\Mod07\DemoFiles\Mod7Demo6Redis\BlueYonder.Hotels.Service
    ```  
11.  Run the following command to open the project in **VSCode**:
    ```bash
    code .
    ```
12. Click on **appsettings.json** file and paste the **connection string** from point 8.
13. Click on **HotelsController** file in the **Controllers** folder and explore the following code: 
    - **Constructor injection** of **Redis** connection.
    - **Get** method, which get data from cache as long as it`s available otherwise getting the data from the repository and cached it for 1 minute.
    - **Post** method, adding new hotel to the repository.  
14. Switch to **Command Line** and run the following command to run the service:
    ```bash
    dotnet run
    ```
15. Open **Microsoft Edge** browser and navigate the following **URL**:
    ```url
    https://localhost:5001/api/hotels
    ```
16. Verify that there is a response with array **Hotels**.
17. Press on **F12** and select **Network** tab.
18. Refresh the page and check that the response header has **X-cache: true**.
19. Open **PowerShell**.
20. Paste the following command to add a new hotel to the repository:
    ```bash
    [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
    $hotelName = Read-Host -Prompt 'Enter hotel name' 
    Invoke-WebRequest -Uri https://localhost:5001/api/hotels -ContentType "application/json" -Method POST -Body "'$hotelName'"
    ```
21. Refresh the browser page again and check if the new hotel was added to the list.
    > **Note:** The cache is expires after 1 minute.

 