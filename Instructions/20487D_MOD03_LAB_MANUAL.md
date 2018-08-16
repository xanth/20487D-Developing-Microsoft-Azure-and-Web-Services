
# Module 3: Creating and consuming ASP.NET Core Web APIs 

# Lab: Creating an ASP.NET Core Web API 

#### Scenario

In this exercise you will create and consume ASP.NET Core Web APIs 

#### Objectives

After completing this lab, you will be able to:

- Create a Web API controller to expose API's.
- Invoke the API via browser.
- Use httpClient, Create ConsoleApplication and connect to the server by httpClient


### Exercise 1: Create a controller class
 
#### Task 1: Add a controller class

1. Open **Command Line** and navigate to **Repository Root]\Allfiles\Mod03\LabFiles\Lab1\Starter**.
2. Open the project with **VSCode**.
3. Add a new controller - **HotelBookingController** in **BlueYonder.Hotels.Service** project.
4. In **HotelBookingController**, Add **HotelBookingRepository** **field** and initialize it using the constructor.

#### Task 2: Add action methods to the controller for GET, POST, and PUT

1. Add new **GET** action with **id** param and return **Booking** entity.
2. Add new **POST** action with **Booking** param and return **Booking** entity.
3. Add new **PUT** action with  **id** and **Booking** param and return **Booking** entity.

### Exercise 2: Use the API from a browser

#### Task 1: Use a browser to access the GET action

1. Run the **BlueYonder.Hotels.Service** service.
2. Open browser, and and navigate to the **GET** action with id 1.
   
   >**Result:** You should see the **Booking** reservation with **Booking Id: 1**.

### Exercise 3: Create a client

#### Task 1: Create a console project and add reference to System.Net.Http

1. Create new **Console Application** called **BlueYonder.Hotels.Client**.
2. Add **BlueYonder.Hotels.Client** project to solution.
3. Install the following package **Microsoft AspNet WebApi Client** to the **BlueYonder.Hotels.Client** project.

#### Task 2: Use HttpClient to call the GET and PUT actions of the controller

1. Open **BlueYonder.Hotels.Client** with **VSCode**.
2. In **BlueYonder.Hotels.Client.csproj**, add a reference DAL class library.
3. In **Program.cs** change the **Main** method return type declaration to **async Task**.
4. In the **Main** method, create a new **HttpClient** instance.
5. Inside the **HttpClient** bracket, create a **HotelBooking** **GET** request, and pass it **id=1**.
6. Under the **HttpResponseMessage**, To read the respone content as a string, use **ReadAsStringAsync**, and print the result to the console.
7. Read the response content as a **Booking** entity.
8. Modify the entity from the server, **booking.Paid** to **false**.
9. Create a **PUT** request, and pass it **id=1**, and a **Booking** entity.
10. Read the **PUT** respone content as a string, and print the result to the console.
11. Run **BlueYonder.Hotels.Service** project.
12. Run **BlueYonder.Hotels.Client** project.
13. Check in the **Command Line** that we see the **Bookings** from the **GET** and the **PUT** requests. 

# Lab: Self-Hosting an ASP.NET Core Web API  

#### Scenario

In this exercise you will Self-Hosting an ASP.NET Core Web API with HttpSys and Kestrel.

#### Objectives

After completing this lab, you will be able to:

- Use HttpSys and Kestrel to host Web API project.
- Use command line arguments to invoke hosting enviroment.

### Exercise 1: Use HttpSys

#### Task 1: Add a new HttpSys option to the list of commands

1. Open **Command Line** and navigate to **Repository Root]\Allfiles\Mod03\LabFiles\Lab2\Starter**.
2. Open the project with **VSCode**.
3. Locate **Program.cs** In **BlueYonder.Hotels.Service** folder and paste the following code inside the **Main** method to get arguments from the **Command Line** :
    ```cs
     IConfigurationRoot config = new ConfigurationBuilder()
                                        .AddCommandLine(args)
                                        .Build();

    string userServicePreference = config["mode"];
    ```
4. Create a **WebHostBuilder**, and check if argument has **HttpSys** mode.
5. Add a default mode using **IIS express**
6. Run the **Builder** with:
   ```cs
    builder.Build().Run();
   ```

#### Task 2: Run the project using HttpSys and retest with a browser

1. Open **BlueYonder.Hotels.Service** in **Command Line**.
2. Run the following command to run the project with **HttpSys** mode:
   ```bash
    dotnet run --mode=HttpSys
   ```
3. In the browser, navigate to **Hotel api** on port **5000**, you should see **[Hotel1,Hotel2]**.

### Exercise 2: Use Kestrel

#### Task 1: Add a new Kestrel option to the list of commands

1. Switch to **VSCode**.
2. In **BlueYonder.Hotels.Service**, add new **Kestrel mode** hosting, inside **Program.cs** between the **if** and **else** code.

#### Task 2: Run the project using Kestrel and retest with the .NET console client

1. Open **Repository Root]\Allfiles\Mod03\LabFiles\Lab2\Starter\BlueYonder.Hotels** in **Command Line**.
2. Run the project with **Kestrel** mode.
3.  Open **Repository Root]\Allfiles\Mod03\LabFiles\Lab2\Starter\BlueYonder.Client** in **Command Line**, and run the project.
4. In the **Command Line** check that the **POST** and **PUT** request succeeded.
