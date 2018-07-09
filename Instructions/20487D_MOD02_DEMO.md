# Module 2: Querying and Manipulating Data Using Entity Framework

 Wherever you see a path to file starting at [repository root], replace it with the absolute path to the directory in which the 20487 repository resides. 
 e.g. - you cloned or extracted the 20487 repository to C:\Users\John Doe\Downloads\20487, then the following path: [repository root]\AllFiles\20487D\Mod01 will become C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01

# Lesson 2: Creating an Entity Data Model

### Demonstration: Creating an entity type, DbContext, and DbInitializer

1. Open **Command Line**.
2. Create a new **ASP.NET Core Web API** project, At the **Command Line** paste the following command and press enter:
    ```bash
    dotnet new console --name MyFirstEF --output [Repository Root]\Allfiles\Mod02\DemoFiles\MyFirstEF\Starter
    ```  
3. After the project was created, change directory in the **Command Line** by running the following command:
    ```bash
    cd [Repository Root]\Allfiles\Mod02\DemoFiles\MyFirstEF\Starter
    ```
4. To use **Entity Framework Core** you need to install the following package using the **Command Line**:
    ```base
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version=2.1.1
    dotnet restore
    ```
5. Open the project in **VSCode** and paste the following command and press enter: 
    ```bash
    code .
    ```
6. Add a new folder **Models** by right click on the **Explorer Pane** on the left, and select **New Folder**.
7. Right click on **Models** folder, select **New File**, then type **Product.cs** in the textbox on the top, and press **Enter**.
8. In the **Product.cs** paste the following code:
    ```cs
    namespace MyFirstEF.Models
    {
        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
    ```
9. Right click on **Models** folder, select **New File**, then type **Store.cs** in the textbox on the top, and press **Enter**.
10. In the **Store.cs** paste the following code:
    ```cs
    using System.Collections.Generic;

    namespace MyFirstEF.Models
    {
        public class Store
        {
             public int Id { get; set; }
             public string Name { get; set; }
             public IEnumerable<Product> Products { get; set; }
        }
    }
    ```
11. Add a new folder **Database** by right click on the **Explorer Pane** on the left, and select **New Folder**.
12. Right click on **Database** folder, select **New File**, then type **MyDbContext.cs** in the textbox on the top, and press **Enter**.
13. In the **MyDbContext.cs** add the following using statements:
    ```cs
    using Microsoft.EntityFrameworkCore;
    using MyFirstEF.Models;
    ```
14. In the **MyDbContext.cs** add the following code:
    ```cs
    namespace MyFirstEF.Database
    {
        public class MyDbContext : DbContext
        {
            public DbSet<Product> Products { get; set; }
            public DbSet<Store> Stores { get; set; }
        
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            { 
                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=MyFirstEF;Trusted_Connection=True;");
            }
        }
    }
    ```
15. Right click on **Database** folder, select **New File**, then type **DbInitializer.cs** in the textbox on the top, and press **Enter**.
16. In the **DbInitializer.cs** add the following code:
    ```cs
    namespace MyFirstEF.Database
    {
        public static class  DbInitializer
        {
            public static void Initialize(MyDbContext context)
            {
                context.Database.EnsureCreated();
                // Code to create initial data
            }
        }
    }
    ```
17. Navigate to **Program.cs** and add the following using statement
    ```cs
    using MyFirstEF.Database;
    ```
18. In the **Program.cs** and replace the **main** methond with the following code:
    ```cs
    static void Main(string[] args)
    {
        using(var context = new MyDbContext())
        {
            DbInitializer.Initialize(context);
        }
        Console.WriteLine("Database created");
    }
    ```
19. To run the application, press **F5**.
20. Open **SQL Operations Studio**.
21. Click on **New Connection**, Connetion window will appear.
22. In **Server** textbox type **.\SQLEXPRESS** and then click on **Connect**.
23. In **Server** blead expand **.\sqlexpress** and then expand **Database**.
24. Make sure you see a database named **MyFirstEF**.
25. In **Server**, expand the **MyFirstEF** node, and then expand the **Tables** node.
26. Notice that both classes defined in the **MyFirstEF** project appear as tables, **dbo.Stores** and **dbo.Products**.
    >**Note:** Database tables are usually named in the plural form, which is why Entity Framework changed the names of the generated tables from Store and Product to Stores and Products. The dbo prefix is the name of the schema in which the tables were created.
27. Expand the **dbo.Products** and **dbo.Stores** tables, and then expand the **Columns** node in each of them to see that both tables have **Id** and **Name** columns, similar to their corresponding class properties.
28. Close **SQL Operations Studio**.
29. Close all open windows.

# Lesson 3: Querying Data

### Demonstration: Using LINQ to Entities

1. Open **Command Line**.
2. Change directory to the starter project, run the following command in the **Command Line**:
    ```bash
    cd [Repository Root]\Allfiles\Mod02\DemoFiles\UsingLINQtoEntities\Starter
    ```
3. To  restore all dependencies and tools of a project use the following command in the **Command Line**:
    ```base
    dotnet restore
    ```
4. Open the project in **VSCode** and paste the following command and press enter:
    ```bash
    code .
    ```
5. In **Explorer** blade, under the **STARTER**, double-click **Program.cs**.
6. Create a new **SchoolContext** object by appending the following code to the **Main** method:
    ```cs
    using (var context = new SchoolContext())
    {
    }
    ```
    You use the **using** statement to control the release of unmanaged resources used by the context, such as a database connection.
7. To create the database, add the following code in the **using** block:
    ```cs
    DbInitializer.Initialize(context);
    ```
8. To select all courses from the database, add the following LINQ to Entities code in the **using** block under the **DBInitializer**:
    ```cs
    var courses = from c in context.Courses 
                  select c;
    ```
9. To print courses and students list to the console window, add the following code in the **using** block after the LINQ to Entities code:
    ```cs
    foreach (var course in courses)
    {
        Console.WriteLine($"Course: {course.Name}");
        foreach (var student in course.Students)
        {
            Console.WriteLine($"\t Student name: {student.Name}");
        }
    }
    Console.ReadLine();
    ```
10. To save the changes, press **Ctrl+S**.
11. To run the application, press **F5**.
12. In the console window, review the course and student lists printed to the console window.  
13. To stop the debugger, press **Shift+F5**.
14. Close all open windows.

### Demonstration 2: Running Stored Procedures with Entity Framework

#### Demonstration Steps

1. Open **Command Line**.
2. Change directory to the starter project, run the following command in the **Command Line**:
    ```bash
    cd [Repository Root]\AllFiles\Mod02\DemoFiles\StoredProcedure
    ```
3. To  restore all dependencies and tools of a project use the following command in the **Command Line**:
    ```base
    dotnet restore
    ```
4. Open the project in **VSCode** and paste the following command and press enter:
    ```bash
    code .
    ```
5. In **Explorer** blade, under the **STARTER**, double-click **Program.cs**.
6. Navigate to the **Main** method, and notice that a **SchoolContext** instance is created to establish a connection to the database.
7. Review the query that is being assigned to the **averageGradeInCourse** variable and notice that the average grade of the **ASP.NET Core** course is calculated, and then printed to the console.
8. The **ExecuteSqlCommand** statement calls the **spUpdateGrades** stored procedure with two parameters, **CourseName** and **GradeChange**.
9. To run the console application, press Ctrl+F5. Notice that the updated average grade is printed to the console before and after the change.
10. To stop the debugger, press **Shift+F5**.
11. Close all open windows.

# Lesson 4: Manipulating Data

### Demonstration: CRUD Operations in Entity Framework

#### Demonstration Steps

1. Open **Command Line**.
2. Change directory to the starter project, run the following command in the **Command Line**:
    ```bash
    cd [Repository Root]\AllFiles\Mod02\DemoFiles\CRUD\Starter
    ```
3. Restore all dependencies and tools of a project use the following command in the **Command Line**:
    ```base
    dotnet restore
    ```
4. Open the project in **VSCode** and paste the following command and press enter:
    ```bash
    code .
    ```
5. In **Explorer** blade, under the **STARTER**, double-click **Program.cs**.
6. Retrieve the **ASP** course from the **Courses** table by appending the following code inside the **using** block under the **DBInitializer**:
    ```cs
    Course ASPCourse = (from course in context.Courses 
    where course.Name == "ASP.NET Core" select course).Single();
    ```
7. Create two new students named **Thomas Anderson** and **Terry Adams**, append the following code to the **using** block.
    ```cs
    Student firstStudent = new Student() { Name = "Thomas Anderson" };
    Student secondStudent = new Student() { Name = "Terry Adams" };
    ```
8. Add the two newly created students to the **ASP** course, append the following code to the **using** block.

    ```cs
    ASPCourse.Students.Add(firstStudent);
    ASPCourse.Students.Add(secondStudent);
    ```
9. Give the teacher of the **ASP** course a $1000 salary raise, append the following code to the **using** block.
    ```cs
    ASPCourse.CourseTeacher.Salary += 1000;
    ```
10. Select a student named **Student_1** from the **ASP** course, append the following code to the **using** block.

    ```cs
    Student studentToRemove = ASPCourse.Students.FirstOrDefault((student) => student.Name == "Student_1");
    ```
11. Remove the student from the **ASP** course, append the following code to the **using** block.
    ```cs
    ASPCourse.Students.Remove(studentToRemove);
    ```
12. Save the changes and print the result, append the following code to the **using** block.
    ```cs
    context.SaveChanges();
    Console.WriteLine(ASPCourse);
    Console.ReadLine();
    ```
13. Press **Ctrl+S** to save the changes. 
14. Press **F5** in order to run the application.
15. After a few seconds the list of students appears in the console window. Notice that there are two new students at the bottom of the list, and student 1 is missing from the list. Also notice that the salary of the teacher is now 101000.
16. Notice the SQL update, delete, and insert statements that correspond to the salary update, student deletion, and the addition of the two new students.
17. Press **Shift+F5** to stop the debugger. 
18. Close all open windows.

### Demonstration: Using Entity Framework with in-memory database

#### Demonstration Steps

1. Open **Command Line**.
2. Change directory to the starter project, run the following command in the **Command Line**:
    ```bash
    cd [Repository Root]\AllFiles\Mod02\DemoFiles\InMemory\Starter\InMemory.Dal.Test
    ```
3. To use **Entity Framework Core InMemory** you need to install the following package using the **Command Line**:
    ```base
    dotnet add package Microsoft.EntityFrameworkCore.InMemory --version=2.1.1
    ```
4. Restore all dependencies and tools of a project use the following command in the **Command Line**:
    ```base
    dotnet restore
    ```
5. Move to the solution folder, paste the following command and press enter:
   ```bash
   cd ..
   ```
6. Open the solution in **VSCode** and paste the following command and press enter:
    ```bash
    code .
    ```
7. Expand **InMemory.Dal**, expand **Database**  and click on **SchoolContext**.
8. Add an empty constructor to the class, paste the following code:
    ```cs
    public SchoolContext()
    {

    }
    ```
9. Add a constructor with paramter of **DbContextOptions\<SchoolContext\>** to the class, paste the following code:
   ```cs
   public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options)
   }
   }
   ```
10. Located the method **OnConfiguring** add the following code:
    ```cs
    if(!optionsBuilder.IsConfigured)
    {
        optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Server=.\SQLEXPRESS;Database=SchoolDB;Trusted_Connection=True;");
    }
    ```
    >**Note:** In your tests you are going to externally configure the context to use the **InMemory** provider. If you are configuring a database provider by overriding **OnConfiguring** in your context, then you need to add some conditional code to ensure that you only configure the database provider if one has not already been configured.
11. Expand **InMemory.Dal.Test** and click on **DBInMemoryTest**.
12. Add the following property to the class:
    ```cs
    private DbContextOptions<SchoolContext> _options =
                new DbContextOptionsBuilder<SchoolContext>()
                    .UseInMemoryDatabase(databaseName: "TestDatabase")
                    .Options;
    ```
13. Add the following **Test Method** to the class:
    ```cs
    [TestMethod]
    public void UpdateTeacherSalaryTest()
    {
        Teacher teacher = new Teacher { Name = "Terry Adams", Salary = 10000 };
        teacher = _teacherRepository.Add(teacher);
        teacher.Salary += 10000;
        teacher = _teacherRepository.Update(teacher);

        using (var context = new SchoolContext(_options))
        {
            var result = context.Teachers.FirstOrDefault((s) => s.PersonId == teacher.PersonId);
            Assert.AreEqual(result.Salary, 20000);
        }
    }
    ```
14. Switch to **Command Line** and paste the following command in order to run the all the test methods and then press enter:
    ```bash
    dotnet test
    ```
15. Close all open windows.

### Demonstration: Using Entity Framework with SQLite

#### Demonstration Steps

1. Open **Command Line**.
2. Change directory to the starter project, run the following command in the **Command Line**:
    ```bash
    cd [Repository Root]\AllFiles\Mod02\DemoFiles\Sqlite\Starter\Sqlite.Dal.Test
    ```
3. To use **Entity Framework Core Sqlite** you need to install the following package using the **Command Line**:
    ```base
    dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version=2.1.1
    ```
4. Restore all dependencies and tools of a project use the following command in the **Command Line**:
    ```base
    dotnet restore
    ```
5. Move to the solution folder, paste the following command and press enter:
    ```bash
    cd ..
    ```
6. Open the solution in **VSCode** and paste the following command and press enter:
    ```bash
    code .
    ```
7. Expand **Sqlite.Dal.Test** and click on **SqliteTest**.
8. Add the following property to the class:
    ```cs
     private DbContextOptions<SchoolContext> _options =
                new DbContextOptionsBuilder<SchoolContext>()
                    .UseSqlite(@"Data Source = [Repository Root]\AllFiles\Mod02\DemoFiles\SQLite\Database\SqliteSchool.db")
                    .Options;
    ```
9. Switch to **Command Line** and paste the following command in order to run the all the test methods and then press enter:
    ```bash
    dotnet test
    ```
10. Open **DB Browser for SQLite**.
11. Top menu click on **File** and select **Open Database**.
12. In **Choose a database file** window browse to **[Repository Root]\AllFiles\Mod02\DemoFiles\SQLite\Database** and double-click on **SqliteSchool.db**.
13. In **Database Structure** tab, expand **Tables**.
14. Right click on **Persons** table and select **Browse Table**.
15. In **Browse Data** tab, verified  that **Kari Hensien** is in the Database.
16. Close all open windows.
