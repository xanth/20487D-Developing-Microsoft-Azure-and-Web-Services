
# Module 2: Querying and Manipulating Data Using Entity Framework

# Lab: Creating a Data Access Layer using Entity Framework 

#### Scenario

In this lab you will use the Entity Framework Core to connect to an SQL Database. 

#### Objectives

After completing this lab, you will be able to:

- Create DAL layer.
- Create an entity data model by using Entity Framework Core.

### Exercise 1: Creating a data model

#### Scenario

In this exercise you will create the data access layer and connect to the database by using Entity Framework Core
to perform CRUD operations on the SQL Express database.

#### Task 1: Create a class library for the data model

1. Open **Command Line**.
2. Create a new **ASP.NET Core Class Library** and name it **DAL** in the following path [Repository Root]\Allfiles\Mod02\LabFiles\Lab1\Starter\DAL
3. Install the  **Entity Framework Core** package via the **Command Line**.
4. Open the project with **VSCode**.
5. Add a new folder to the project and name it **Models**.

#### Task 2: Create data model entities using code-first approach

1. Create a new class and name it **User** and add the following properties:
    ```cs
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email {get; set; }
    ```
2. Create a new class and name it **Room** and add the following properties:
    ```cs
    public int RoomId { get; set; }
    public int Number { get; set; }
    public decimal Price { get; set; }
    public bool Available { get; set; }
    public IEnumerable<Booking> Bookings { get; set; } = new List<Booking>();
    ```
3. Create a new class and name it **Hotel** and add the following properties:
    ```cs 
    public int HotelId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public ICollection<Room> Rooms { get; set; }
    ```
4. Create a new class and name it **Hotel** and add the following properties:
    ```cs
    public int BookingId { get; set; }
    public Room Room { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public int Guests { get; set; }
    public decimal TotalFee { get; set; }
    public bool Paid { get; set; }
    public User User { get; set; }
    ```

#### Task 3: Create a DbContext

1. Create a new folder and name it **Database**.
2. Create a new class and name it **MyDbContext** that inherits from **DbContext**.
3. Add **DbSet** property to every one of the models that you made in the previous task.
4. Peste the following code to add two constructies:
    ```cs
    private void InitialDBContext()
    {
        DbInitializer.Initialize(this);
    }
    // Default Constructor
    public MyDbContext()
    {
        InitialDBContext();
    }
    // Constructor with options
    public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
    {
        InitialDBContext();
    }
    ```
5. **override** the **OnConfiguring** method to use **SqlExpress**. 

   >**Results** Your application now has a functioning data access layer. 

### Exercise 2: Query your database

#### Scenario

In this exercise you will use the DAL class libary to create a new Console Application
to display all the data from the database.


#### Task 1: Create a database initializer with dummy data

1. Create new class and name it **DbInitializer** inside the **Database** folder.
2. Add the following code to create the **Seed** method:
    ```cs
    private static void Seed(MyDbContext context)
    {
        // Create list with dummy users 
        List<User> userList = new List<User>
        {
            new User(){ Name = "Jon Due", Email = "jond@outlook.com"},
            new User(){ Name = "Jon Due2", Email = "jond2@outlook.com"},
            new User(){ Name = "Jon Due3", Email = "jond3@outlook.com"}
        }   
        // Create list with dummy bookings 
        List<Booking> bookingList = new List<Booking>
        {
            new Booking()
            { 
                DateCreated = DateTime.Now, 
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(2),
                Guests = 2, 
                Paid = true, 
                User = userList[0]
            },
            new Booking()
            { 
                DateCreated = DateTime.Now.AddDays(3),
                CheckIn = DateTime.Now.AddDays,
                CheckOut = DateTime.Now.AddDays(8), 
                Guests = 3, Paid = true, 
                User = userList[1]
            },
            new Booking()  
            { 
                DateCreated = DateTime.Now.AddDays(-10), 
                CheckIn = DateTime.Now.AddDays( CheckOut = DateTime.Now.AddDays(11),
                Guests = 1, 
                Paid = false, 
                User = userList[2]
            }
        };
        // Create list with dummy rooms
        List<Room> roomList = new List<Room>
        {
            new Room(){ Number = 10, Price = 300},
            new Room(){ Number = 20, Price = 200},
            new Room(){ Number = 30, Price = 100}
        }   
        roomList[0].Bookings.Add(bookingList[0]);
        roomList[1].Bookings.Add(bookingList[1]);
        roomList[1].Bookings.Add(bookingList[2])

        Hotel hotel = new Hotel()
        {
            Name = "Azure Hotel",
            Address = "Cloud",
            Rooms = roomList
        };
        // Insert the dummy data to the database
        context.Users.AddRange(userList);
        context.Bookings.AddRange(bookingList);
        context.Rooms.AddRange(roomList);
        context.Hotels.Add(hotel)   
        context.SaveChanges()   
    }
    ```
3. Add a new **static** method and name it **Initialize** that gets **MyDbContext** as a parameter that does the following logic:
    - make sure that the database was created. 
    - if the database was created for the frist time, use the **Seed** method.

#### Task 2: Write a LINQ query to query the data

1. Close **VSCode** window.
2. Create a new **ASP.NET Core Console Application** project via the **Command Line**.
3. Switch to **Command Line**.
4. Create a new **ASP.NET Core Console Application** and name it **DatabaseTester** in the following path [Repository Root]\Allfiles\Mod02\LabFiles\Lab1\Starter\DatabaseTester
5. Create a new **Solution** and name it **Mod2Lab1**.
6. Add **DAL** and **DatabaseTester** projects to **Mod2Lab1** solution.
7. Open **VSCode** in the folder.
8. In **DatabaseTester.csproj** add a reference to the **DAL** project.
9. Navigate to **Program.cs** in the **DatabaseTester** folder and locate the **main** method.
10. Create a new **MyDbContext** instance.
11. Use **Initialize** method in **DbInitializer** class with the new **MyDbContext** instance.
12. Display to the screen the following info:
    - The **Hotel** name.
    - All the **Rooms** number and price.
    - All **Users** name and email.
13. Run the **DatabaseTester** application via **Command Line**

   >**Results** Your application can now display all of the data from the database by using LINQ queries.

# Lab: Manipulating Data

#### Scenario

In this lab you will create a repository with CRUD methods and inject two kinds of database configurations in order to work with SQL Express and SQLite.

#### Objectives

After completing this lab, you will be able to:

- Create a HotelBooking repository and populate it with CRUD methods
- Test the queries with SQL Express and SQLite databases

### Exercise 1: Create repository methods

#### Scenario

In this exercise you will create the HotelBookingRepository.

#### Task 1: Create a method to add entities

1. Open **Command Line**.
2. Run the following command to change directory to the startup project:
    ```bash
    cd [Repository Root]\Allfiles\Mod02\LabFiles\Lab2\Starter
    ```
3. Open the folder in **VSCode**.
4. In **VSCode** expand **DAL** folder then expand **Repository** folder and double-click on **HotelBookingRepository.cs**.
5. Add a **DbContextOptions** **field** and name it **_options**.
6. Add a **constructor** with **DbContextOptions** as an optional parameter to initialize **_options** field.
7. Create and implement a new method and name it **Add** that get those parameters:
   ```cs
   int travelerId, int roomId, DateTime checkIn, int guest = 1
   ```
#### Task 2: Create a method to update entities

1. Add new method and name it **Update** that get class **Booking** as a parameter and update the database.

#### Task 3: Create a method to delete entities

1. Add new method and name it **Delete** that get int **BookingId** as a parameter and delete the database.

    >**Results** You finish creating a repository with Add, Update, Delete methods.

#### Exercise 2: Test the model using SQL Server and SQLite

#### Scenario

In this exercise you will inject SQL Lite DB to the repository,
and you will create testing and run then on SQL Lite and SQL Server. 

#### Task 1: Create test code with transactions

1. Open **Command Line**.
2. Run the following command to change directory to **Starter** folder:
   ```bash
    cd [Repository Root]\Allfiles\Mod02\LabFiles\Lab1\Starter
   ```
3. Create new **Unit Test Project** and name it **DAL.Test**.
4. Add the new testing project to the **solution**.
5. Open **VSCode** via **Command Line**.
6. Add reference to **DAL.Test** from **DAL** project.
7. Locate **UnitTest1.cs** file, and rename to **BookingRepositoryTests**.
8. Change **public void TestMethod1()** to **public async void AddTwoBookingsTest()**.
9. Paste the following code in **AddTwoBookingsTest** method, to test the **Transaction Scope** with two bookings:
    ```cs
    Booking fristBooking;
    Booking secondBooking
    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
    {
        HotelBookingRepository repository = new HotelBookingRepository();
        fristBooking = await repository.Add(1, 1, DateTime.Now.AddDays(6), 4);
        secondBooking = await repository.Add(1, 2, DateTime.Now.AddDays(8), 3)
        scope.Complete();
    }
    
    using (MyDbContext context = new MyDbContext())
    {
        int bookingsCounter = context.Bookings.Where(booking => booking.BookingId == fristBooking.BookingId ||
                                                                booking.BookingId == secondBooking.BookingId).ToList().Count
        Assert.AreEqual(2, bookingsCounter);
    }
    ```

#### Task 2: Test code against a local SQL Server database

1. Switch to **Command Line**.
2. Change directory to **DAL.Test** folder. 
3. Run the following command to test **AddTwoBookingsTest** method:
   ```bash
   dotnet test
   ```
4. Open **SQL Operations Studio**:
    - Check that **database** name **Mod2Lab2DB** was created.
    - And that the two **booking** saved in the database.

#### Task 3: Replace SQL Server provider with SQLite

1. Navigate to **DAL** folder with **Command Line**.
2. Install **Entity Framework Core Sqlite** package version 2.1.1.
3. Open the solution in **VSCode**.
4. Expand **DAL.Test** folder, then double-click on **BookingRepositoryTests.cs**.
5. Paste the following **field**:
    ```cs
    private DbContextOptions<MyDbContext> _options =
               new DbContextOptionsBuilder<MyDbContext>()
                   .UseSqlite(@"Data Source = [Repository Root]\Allfiles\Mod02\LabFiles\Lab2\Database\SqliteHotel.db")
                   .Options;
    ```
6. Add testing method and name it **AddTwoBookingsSQLiteTest** that test insertion of two book to **SQL Lite**.

#### Task 4: Test code against SQLite

1. In the **Command Line** run the testing projcet.
2. Check the **SqliteHotel.db** file was created in the folder **[Repository Root]\Allfiles\Mod02\LabFiles\Lab2\Database**.
3. Open **DB Browser for SQLite**. 
4. Verify that the two bookings exist is in the Database. 

    >**Results** You now just finish testing your database with SQL Lite and SQL Server.

©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the  [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are  **not**  included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
