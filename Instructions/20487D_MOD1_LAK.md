# Module 1: Overview of Service and Cloud Technologies

# Lab: Exploring the Work Environment

1. Wherever you see a path to file starting at [Repository Root], replace it with the absolute path to the directory in which the 20487 repository resides. 
 e.g. - you cloned or extracted the 20487 repository to C:\Users\John Doe\Downloads\20487, then the following path: [Repository Root]\AllFiles\20487D\Mod01 will become C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01
 2. Wherever you see {YourInitials}, replace it with your actual initials.(for example, the initials for John Do will be jd).

### Exercise 1: Creating an ASP.NET Core project

#### Task 1: Create a new ASP.NET Core project

1. Open **Command Line**.
2. For creating a new **ASP.NET Core Web API** project, At the **Command Line** paste the following command and press enter:
    ```bash
    dotnet new webapi --name BlueYonder.Flights --output [Repository Root]\Allfiles\Mod01\Labfiles\Exercise1
    ```  
3. Now that the project was created, change directory in the **Command Line** by running the following command:
    ```bash
    cd [Repository Root]\Allfiles\Mod01\Labfiles\Exercise1
    ```
4. To open the project in **Visual Studio Code** paste the following command and press enter: 
    ```bash
    code .
    ```

### Exercise 2: Create a simple Entity Framework model

#### Task 1: Create a new POCO entity

1. The **Exercise1** folder opens in **Visual Studio Code (VS Code)**. Select the **Startup.cs** file.
    - Select **Yes** to the Warn message **"Required assets to build and debug are missing from 'Exercise1'. Add them?"**.
    - Select **Restore** to the Info message **"There are unresolved dependencies"**.
2. Add a new folder **Models** by right click on the **Explorer Pane** on the left, and select **New Folder**.
3. Right click on **Models** folder, select **New C# Class**, then type **Flight** in the textbox on the top, and press **Enter**.
4. In the **Flight.cs** Copy the using statement below to the begin of the file:
    ```cs
    using System;
    ```
5. Locate the class **Flight** and paste the following code between the brackets:
    ```cs
    public int Id { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public string FlightNumber { get; set; }
    public DateTime DepartureTime { get; set; }
    ```

#### Task 2: Create a new DbContext class

1. Right click on **Models** folder, select **New C# Class**, then type **FilghtsContext** in the textbox on the top, and press **Enter**.
2. Paste the following using statement in the top of the file **FilghtsContext**:
    ```cs
    using Microsoft.EntityFrameworkCore;
    ```
3. Inherite the class **FlightContext** from **DbContext**,your code should look like:
    ```cs
    public class FlightsContext : DbContext
    ```
4. Inside the class paste the following code: 
    ```cs
    public FlightsContext(DbContextOptions<FlightsContext> options)
        : base(options)
    {
    }

    public DbSet<Flight> Flights { get; set; }
    ```
5. Go to **Startup.cs** and paste the following using statement:
    ```cs
    using BlueYonder.Flights.Models;
    using Microsoft.EntityFrameworkCore;
    ```
6. In the **ConfigureServices** method paste the following code: 
    ```cs
    services.AddDbContext<FlightsContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("FlightsDB")));
    ```    

### Exercise 3: Create a web API class

#### Task 1: Create a new class for the web API

1. Expand **Controllers** folder and rename the **ValuesControllers.cs** to **FlightsControllers.cs**.
2. Open **FlightsControllers.cs** file and rename the class name from **ValuesControllers** to **FlightsControllers**.
3. Add the following using:
    ```cs
    using BlueYonder.Flights.Models;
    ```
4. Add the following field to the class in order to hold the **FlightContext**:
    ```cs
    private readonly FlightsContext _context;
    ```
5. Add the following Constructor to the class for inject the context to the controller:
    ```cs
    public FlightsController(FlightsContext context)
    {
        _context = context;
    }
    ```

#### Task 2: Create an action and use the Entity Framework context

1. For getting the list of all flights replace the first **Get** method with the following code:
    ```cs
    // GET api/flights
    [HttpGet]
    public IEnumerable<Flight> Get()
    {
        return _context.Flights.ToList();
    }
    ```    
2. For adding a new flight to db, replace the **Post** method with the following code:
    ```cs
    // POST api/flights
    [HttpPost]
    public IActionResult Post([FromBody]Flight flight)
    {
        _context.Flights.Add(flight);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), flight.Id);
    }
    ```    
### Exercise 4: Deploy the web application to Azure

#### Task 1: Create an Azure Web App and an SQL database

1. Open **Microsoft Edge**.
2. Navigate to **https://portal.azure.com**.
3. If a page appears prompting for your email address, enter your email address, and then click **Next** and enter your password, and then click **Sign In**.
4. If the **Stay signed in?** dialog appears, click **Yes**.

   >**Note**: During the sign-in process, if a page appears prompting you to choose from a list of previously used accounts, select the account that you previously used, and then continue to provide your credentials.

5. Click **App Services** on the left menu panel.
    - Click on **Add** in the **App Services** blade.
    - Click on **Web App + SQL** in the **Web** blade.
    - Click on **Create** button in the **Web App + SQL** blade.
6. Add the following information to create the **WebApp**:
    - In the **App Name** text box, type the following web app name: **flightsmod1lab**{YourInitials}.
        >**Note:** The **App Name** will be part of the URL.
    - In the **Resource Group**  select **Create new**, and type in the text box below **Mod1Resource**.
    - Click on **SQL Database** and then click on **Create a new database**, then open **SQL Database** blade, add the following information:
        - In the **Name** text box type: **Mod1DB**.
        - Click on **Target server**, then click on **Create a new server**.
        - In the **New server** blade, enter the following information:
            - In the **Server name** text box type: **serverdbmod1**{YourInitials}.
            - In the **Server admin login** text box type: **Admin123**.
            - In the **Password** and **Confirm password** text box type: **Password99**.
            - Click on **Select**.
        - Click on **Pricing tier**, then select **Free** and click on **Apply**.
        - Click on **Select**.
    - Click on **Create**.
7. Click on **SQL Databases** on the left menu panel.
8. Click on **Mod1DB**, then click on **Query editor**.
9. Click on **Login**, then type the following password: **Password99**.
10. To create a new table in our **SQL Database**, inside the **Query 1** tab, paste the following script and the click on **Run**:
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

1. Go back to **Azure Protal**.
2. Click on **App Services** in the left menu.
3. Click on **filghtsmod1lab**{YourInitials} in **App Services** blade.
4. Click on **Deployment credentials** under **DEPLOYMENT** section, and add the following information:
    - In the **FTP/deployment username** type **FTPMod1Lab**{YourInitials}.
    - In the **Password** and **Confirm password** text box type: **Password99**.
    - Click on **Save**.
    >**Note :** The **Deployment credentials** give the options to deploy the app from the **Command Line**.
5. Go back to the project in **Visual Studio Code**.
6. Add a new folder **Properties** by right click on the **Explorer Pane** on the left, and select **New Folder**.
7. Right click on **Properties** folder and select **New Folder** and give the folder name **PublishProfiles**.
8. In the **PublishProfiles** add the file **Azure.pubxml** and double-click on the file.
9. Paste the following code:
    ```xml
    <Project>
        <PropertyGroup>
          <PublishProtocol>Kudu</PublishProtocol>
          <PublishSiteName>flightsmod1lab{YourInitials}</PublishSiteName>
          <UserName>FTPMod1Lab{YourInitials}</UserName>
          <Password>Password99</Password>
        </PropertyGroup>
    </Project>
    ```
    >**Note :** This file have the information to deploy to Azure, with the **Deployment credentials** that we added in point 4.
10. Go back **Command Line**, and paste the following command:
    ```bash
    dotnet publish /p:PublishProfile=Azure /p:Configuration=Release
    ```
    > **Note :** If the there was an error in the publish process, restart the  flightsmod1lab{YourInitials} app services.

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
    ```
    https://flightsmod1lab{YourInitials}.azurewebsites.net/api/flights
    ```
5. Check that you got the following result:
    ```json
    [
        {
            id: 1,
            origin: "Germany",
            destination: "France",
            flightNumber: "GF7625",
            departureTime: "0001-01-01T00:00:00"
        }
    ]
    ```
#### Task 4: View result in the Database

1. Go back to **Azure Protal**.
2. Click on **SQL Databases** on the left menu panel.
3. Click on **Mod1DB**, then click on **Query editor**.
4. Click on **Login**, then type the following password: **Password99**.
5. To get all the flights in the Database, paste the following script inside the **Query 1** tab, then click on **Run**:
    ```sql
    select * from Flight
    ```
6. Check that you got the flight in the **Reults** tab.


©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
