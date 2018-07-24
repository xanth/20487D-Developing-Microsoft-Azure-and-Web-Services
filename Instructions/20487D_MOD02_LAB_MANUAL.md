
# Module 2: Querying and Manipulating Data Using Entity Framework

# Lab: Creating a Data Access Layer using Entity Framework 

#### Scenario

In this lab you will work with the Entity Framework Core to create work with SQL Database 

#### Objectives

After completing this lab, you will be able to:

- Create DAL layer.
- Create an entity data model by using Entity Framework Core.
- Connect to SQL Express database by using Entity Framework Core.
- Create a client layer in a Console Application.
- Combine the DAL layer and the client layer in a solution.
- Display the data from the database by using LINQ query

### Exercise 1: Creating a data model

#### Scenario

In this exercise you will create the DAL layer and connect to the database by using Entity Framework Core
to run on the SQL Express database.

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
4. **override** the **OnConfiguring** method to use **SqlExpress**. 

   >**Results** After completing this exercise, you will have finished the DAL layer. 

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
3. Switch back to the **Command Line**.
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

   >**Results** After completing this exercise, you should be able to display all of the data from the database by using LINQ query

©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the  [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are  **not**  included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
