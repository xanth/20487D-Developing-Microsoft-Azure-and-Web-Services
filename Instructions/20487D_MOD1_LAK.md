# Module 1: Overview of Service and Cloud Technologies

# Lab: Exploring the Work Environment

Wherever you see a path to file starting at [Repository Root], replace it with the absolute path to the directory in which the 20487 repository resides. 
 e.g. - you cloned or extracted the 20487 repository to C:\Users\John Doe\Downloads\20487, then the following path: [Repository Root]\AllFiles\20487D\Mod01 will become C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01

### Exercise 1: Creating an ASP.NET Core project

#### Task 1: Create a new ASP.NET Core project

1. Open **Command Line**.
2. For creating a new **ASP.NET Core Web API** project, At the **Command Line** paste the following command and press enter:
    ```bash
        dotnet new webapi -name BlueYonder.Flights -output [Repository Root]\20487D-Developing-Microsoft-Azure-and-Web-Services\Allfiles\Mod01\Labfiles\Exercise1
    ```  
3. Now that the project was created, change directory in the **Command Line** by running the following command:
    ```bash
        cd [Repository Root]\20487D-Developing-Microsoft-Azure-and-Web-Services\Allfiles\Mod01\Labfiles\Exercise1
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
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string FlightNumber { get; set; }
        public DateTime DepartureTime { get; set; }
        public TimeSpan Duration { get; set; }
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

        public DbSet<FlightsContext> Flights { get; set; }
    ```
5. Go to **Startup.cs** and paste the following using statement:
    ```cs
        using Exercise1.Models;
        using Microsoft.EntityFrameworkCore;
    ```
6. In the **ConfigureServices** method paste the following code: 
    ```cs
        services.AddDbContext<FlightsContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("FlightsDB")));
    ```    
















©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
