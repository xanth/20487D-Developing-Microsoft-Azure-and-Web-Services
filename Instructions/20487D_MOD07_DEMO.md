# Module 7: Implementing data storage in Azure

1. Wherever you see a path to file starting at [Repository Root], replace it with the absolute path to the directory in which the 20487 repository resides. 
 e.g. - you cloned or extracted the 20487 repository to C:\Users\John Doe\Downloads\20487, then the following path: [Repository Root]\AllFiles\20487D\Mod01 will become C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01
2. Wherever you see **{YourInitials}**, replace it with your actual initials.(for example, the initials for John Do will be jd).
3. Before performing the demonstration, you should allow some time for the provisioning of the different Azure resources required for the demonstration. It is recommended to review the demonstrations before the actual class and identify the resources and then prepare them beforehand to save classroom time.


# Lesson 3: Working with Structured Data in Azure

### Demonstration: Uploading an SQL database to Azure and accessing it locally

#### Demonstration Steps

1. Open **Microsoft Edge** browser.
2. Navigate to **https://portal.azure.com** and login with your credentials.
3. Click **Storage accounts** on the left menu panel.
4. Click on **Add** on the top panel.
5. In **Create storage account** view fill in the following details:
   - In **Name** textbox type **mod7demo2**{YourInitials}.
   - In **Resource group** select **Create new** and type **Mod7demo2ResourceGroup**.
   - Click on **Create**.
6. Wait to the **Storage Account** to create and then click on **Refresh** button on the top panel in **Storage accounts** view.
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
11. In **mod7demo2sqlserver**{YourInitials} view click on  **Import database** on the top panel.
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

# Lesson 3: Working with Structured Data in Azure

### Demonstration: Using CosmosDB with the MongoDB API

#### Demonstration Steps

1. Open **Microsoft Edge** browser.
2. Navigate to **https://portal.azure.com** and login with your credentials.
3. Click **Azure Cosmos DB** on the left menu panel.
4. Click **Add** on the top panel.
5. In **Azure Cosmos DB** view fill the following details:
   - In **ID** textbox type **mod7demo3**{YourInitials}.
   - In **API** select **MongoDB**.
   - In **Resource group** select **Create new** and type **Mod7Demo3ResourceGroup**
   - click on **Create**.
6. Wait to the database to be created.
7. Click **Azure Cosmos DB** on the left menu panel, then click on **mod7demo3**{YourInitials}.
8. Click on **Data Explorer** on the left blade menu in **mod7demo3**{YourInitials} view.
9. Click on **New Collection** on the top panel and fill the following details:
   - In **Database id** select **Create new** and type **mydb**.
   - In **Collection id** type **customers**.
   - Click on **Add unique key**, then type **customerId**.
   - Click on **Create**.
10. Click on **New Collection** on the top panel and fill the following details:
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

# Lesson 3: Working with Structured Data in Azure

### Demonstration: Using CosmosDB with a graph database API

#### Demonstration Steps

1. Open **Microsoft Edge** browser.
2. Navigate to **https://portal.azure.com** and login with your credentials.
3. Click **Azure Cosmos DB** on the left menu panel.
4. Click **Add** on the top panel.
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