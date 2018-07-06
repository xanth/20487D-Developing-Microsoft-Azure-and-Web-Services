# Module 1: Hosting services

# Lab: Host an ASP.NET Core service in a Windows Service

#### Scenario

In this lab you will learn how to create a new **ASP .NET Core** project and use **Kestrel - Run As Service** to 
host this project in a new Windows Service.

In this lab you will explore several frameworks and platforms, 
such as Entity Framework Core, ASP.NET Core Web API, 
and Azure, that are used for creating distributed applications.

#### Objectives

After completing this lab, you will be able to:

- Create an ASP.NET Core Web API service.
- Register a new Windows Service.
- Host a web application in a Windows Service.

### Exercise 1: Creating an ASP.NET Core project

#### Scenario

In this exercise, you will create an Web API via ASP.NET Core by using command line.

#### Task 1: Create a new ASP.NET Core project

1. Create a new Web API Core project by using command line with **dotnet** tool.

   >**Results** : After completing this exercise, you should have a basic Web API ASP.NET Core project.
   
#### Task 2: Install the Microsoft.AspNetCore.Hosting.WindowsServices NuGet package

1. Install the Microsoft.AspNetCore.Hosting.WindowsServices NuGet package by using command line with **dotnet** tool.

   >**Results** : After completing this exercise, you should have Microsoft.AspNetCore.Hosting.WindowsServices NuGet package 
   on your ASP .NET Core project that you have created in task 1 which allows you to run the ASP.NET Core project as a Windows Service.

#### Task 3: Modify the Main method to use Kestrel RunAsService hosting

1. Use **VSCode** to add **using Microsoft.AspNetCore.Hosting.WindowsServices;** to the **program.cs** class which allows you to use 
the Microsoft.AspNetCore.Hosting.WindowsServices NuGet package that you have installed in task 2.
2. Change the **Main** method implementation to use **Kestrel’s RunAsService hosting** to host your **ASP .NET Core** project in a Windows Service:
  ```cs
      CreateWebHostBuilder(args).Build().RunAsService();
  ```

### Exercise 2: Register the Windows Service

#### Scenario

In this exercise, you will create a new Windows Service, start and stop it using the **Command Line** with **sc** tool.

#### Task 1: Register the Windows Service

1. Publish your **ASP .NET Core** project that you have created earlier, into a folder that will allow you create a new Windows Service.<br/>
   You will do it using a command line with **dotnet** tool.
2. Create a new Windows Service using the **sc** command tool, that will take the **.exe** file from the publish in the last point and will host your application in a new windows Service.<br/>
   To do it, you will need to run the following command in the command line:<br/>
   ```bash
       sc create {The Service Name} binPath= "{Your publish folder path.exe}"
   ```       

#### Task 2: Start the Windows Service and test it

1. Use **sc Command Line** to start your newly Windows Service.
2. Navigate to your api using port **5000** to see that you are getting a good response as expected.
4. Use **sc Command Line** to stop the service.
5. Navigate to the same address to see that you are not getting a good response.

   >**Results** : After completing this exercise, you will have a new **ASP .NET Core** api project which is hosted by a **Windows Service.









