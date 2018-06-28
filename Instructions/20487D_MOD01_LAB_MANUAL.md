# Module 1: Overview of Service and Cloud Technologies

# Lab: Exploring the Work Environment

#### Scenario

In this lab you will explore several frameworks and platforms, such as Entity Framework Core, ASP.NET Core Web API, and Azure, that are used for creating distributed applications.

#### Objectives

After completing this lab, you will be able to:

- Create an entity data model by using Entity Framework Core.
- Create an ASP.NET Core Web API service.
- Create an Azure SQL database.
- Deploy a web application to an Azure website.

### Exercise 1: Creating an ASP.NET Core project

#### Scenario

In this exercise, you will create an Web API via ASP.NET Core by using command line.

#### Task 1: Create a new ASP.NET Core project

1. Create a new Web API Core project by using command line with **dotnet** tool.

   >**Results** : After completing this exercise, you should have a basic Web API ASP.NET Core project.

### Exercise 2: Create a simple Entity Framework model

#### Scenario

In this exercise, you will create data model classes to represent flights, implement a **DbContext** -derived class, and create a new repository class for the **Flight** entity.

The main tasks for this exercise are as follows:

1. Prepare the data model classes for Entity Framework Core

1. Add the newly created entity to the context

#### Task 1: Create a new ASP.NET Core project

1. Open the project in **VSCode**.
2. Install the **Microsoft.EntityFrameworkCore.SqlServer** with **dotnet** tool.
3. Create a new class **Flight**, and add the following properties:
    ```cs
    public int Id { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public string FlightNumber { get; set; }
    public DateTime DepartureTime { get; set; }
    ```

#### Task 2: Create a new DbContext class

1. Create a new class **FlightsContext**, this class will inherite from **DbContext**.
2. Add a **Constructor** to the class, with a parameter type of **DbContextOptions<FlightsContext>**.
3. Call the base constructor and pass the option parameters.
4. Add a new **DbSet\<T\>** property for the **Flight** entity. Name the property Flights.
5. Locate **ConfigureServices** in **Startup.cs**, and register **FlightsContext** as service and configure it to use SQL server with **defaultConnection**.
    >**Note:** In exercise 4 we will create **Web App** and **SQL Database** in azure, it will generate a connection string. For access the database we need to use **defaultConnection** key.

    >**Results** : After completing this exercise, you should have created Entity Framework wrappers for the **BlueYonder** database

### Exercise 3: Create a Web API Class

#### Scenario

Implement the flight service by using ASP.NET Core Web API. Start by creating a new ASP.NET Web API controller, and implement CRUD functionality using the POST, GET HTTP methods.

#### Task 1: Create a new class for the web API

1. Replace the **ValuesController.cs** file name to **FlightsController.cs**.
2. Inside the class **FlightsController.cs** replace the class name to **FlightsController**.
3. Add a new field **FlightContext**.
4. Add a **Constructor** to the class, with parameter type of **FlightContext** and the parameter name **_context**.
5. Inside the **Constructor**, assign the param to the field.

#### Task 2: Create an action and use the Entity Framework context

1. Locate the **Get** method without params, and modify the method with the following steps:
  - Replace the method signature from **string** to **Flight**.
  - Inside the method replace the **return** value to return all the flights from the **Database**.
2. Locate the **Post** method, and modify with the following steps:
  - Replace the **string** type param with **Flight** type.
  - Replace the method signature from **void** to **IActionResault**.
  - Add the **Flight** param to the context and save the changes.
  - return **CreatedAtAction** with the **flight** id.
  >**Note:** **CreatedAtResult** returns 201 status code along with a URI to the created resource

  >**Results** : After completing this exercise, you should have created a    web app that exposes the Web API.

### Exercise 4: Deploy the web application to Azure

#### Scenario

In this exercise, you will create an Azure web app and SQL Database to host the ASP.NET Core Web API application.

The main tasks for this exercise are as follows:

1. Create a New Azure Web App and SQL Database

2. Deploy the web application to the Azure web app

1. Test the Web API using PowerShell

4. View results in the Database

#### Task 1: Create an Azure Web App and an SQL database

1. Open Azure portal at **https://portal.azure.com**.
2. On the left side navigation pane, click **App Services**, then click **Add**, select **Web App + SQL**.
3. Name the web app **flightsmod1lab**{YourInitials}. (Replace {YourInitials} with your initials).
4. Create new **Resource Group**.
5. Configure **SQL Database** and **Server**.
6. Click **Create** and wait until the new web app is ready.
7. Navigate to the database that was created (**SQL Databases**), then run the following script to create a new table in our database in the **Query editor**:
    ```sql
    CREATE TABLE [dbo].[Flights](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Origin] [varchar](50) NOT NULL,
    [Destination] [varchar](50) NOT NULL,
    [FlightNumber] [varchar](50) NOT NULL,
    [DepartureTime] [date] NOT NULL,

    PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    ))
    ```

#### Task 2: Deploy the web application to the Azure Web App

1. Navigate to the **App Services** and select our **Web App**.
2. In **Deployment Credentials** add fill-in the form and save it.
3. Switch to **VSCode**, create new folder named **Properties**, and inside this folder create another folder named **PublishProfiles**.
4. Inside **PublishProfiles** folder create a new file named **Azure.pubxml** with the following code:
    ```xml
    <Project>
      <PropertyGroup>
        <PublishProtocol>Kudu</PublishProtocol>
        <PublishSiteName>[App Service Url]</PublishSiteName>
        <UserName>[FTP/Deployment UserName]</UserName>
        <Password>[Password]</Password>
      </PropertyGroup>
    </Project>
    ```
  >**Note:** Replace the the information inside the brakets with the information you fill-in **Deployment Credentials** on Azure Portal.
5. Switch to **Command Line**, and paste the following command:
    ```bash
    dotnet publish /p:PublishProfile=Azure /p:Configuration=Release
    ```
  >**Note:** If the there was an error in the publish process, restart the      flightsmod1lab{YourInitials} app services. 
 
 #### Task 3: Test the web API

1. Open **PowerShell**.
2. To Add a flight to the Database paste the following command:
    ```bash
    $postParams = "{'origin': 'Germany',
        'destination': 'France',
        'flightNumber': 'GF7625',
        'departureTime': '0001-01-01T00:00:00'}"
    Invoke-WebRequest -Uri https://flightsmod1lab{YourInitials}.azurewebsites.net/api/flights -ContentType "application/json" -Method POST -Body $postParams
    ```
3. Open **Microsoft Edge**.
4. Browse to the following url:
    
    https://flightsmod1lab{YourInitials}.azurewebsites.net/api/flights
    
5. Check that you got the expected result.

#### Task 4: View result in the Database

1. Switch to **Azure Protal**.
2. Navigate to the your database, and check in **Query editor** with the following script to see that the flight was added to the database:
    ```sql
    select * from flights
    ```

    >**Results** : After completing this exercise, you should have ensured that all your products are hosted on the Microsoft Azure cloud by using SQL Databases and Azure Web Apps.


©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the  [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are  **not**  included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
