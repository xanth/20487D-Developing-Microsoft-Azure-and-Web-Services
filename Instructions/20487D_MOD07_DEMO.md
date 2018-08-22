# Module 7: Implementing data storage in Azure

 Wherever you see a path to file starting at [repository root], replace it with the absolute path to the directory in which the 20487 repository resides. 
 e.g. - you cloned or extracted the 20487 repository to C:\Users\John Doe\Downloads\20487, then the following path: [repository root]\AllFiles\20487D\Mod01 will become C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01

Before performing the demonstration, you should allow some time for the provisioning of the different Azure resources required for the demonstration. It is recommended to review the demonstrations before the actual class and identify the resources and then prepare them beforehand to save classroom time.

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