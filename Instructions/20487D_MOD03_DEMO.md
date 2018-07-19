
# Module 3: Creating and consuming ASP.NET Core Web APIs

 Wherever you see a path to file starting at [repository root], replace it with the absolute path to the directory in which the 20487 repository resides. 
 e.g. - you cloned or extracted the 20487 repository to C:\Users\John Doe\Downloads\20487, then the following path: [repository root]\AllFiles\20487D\Mod01 will become C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01

# Lesson 2: Creating an ASP.NET Core Web API

### Demonstration: Creating your first ASP.NET Core Web API

1. Open **Command Line**.
2. Create a new **ASP.NET Core Web API** project, At the **Command Line** paste the following command and press enter:
    ```bash
    dotnet new webapi --name MyFirstEF --output [Repository Root]\Allfiles\Mod03\DemoFiles\FirstWebApi\Starter
    ```  
3. After the project was created, change directory in the **Command Line** by running the following command:
    ```bash
    cd [Repository Root]\Allfiles\Mod03\DemoFiles\FirstWebApi\Starter
    ```
4. Restore all dependencies and tools of a project use the following command in the **Command Line**:
    ```base
    dotnet restore
    ```
5. Open the project in **VSCode** and paste the following command and press enter:
    ```bash
    code .
    ```
6. In **Explorer** blade, under the **STARTER**, expand **Controllers** and double-click on **ValuesController**.
7. Locate the **Get** method and overview the return contant of the method.
8. Switch to **Command Line** and paste the following command in order to run application and then press enter:
    ```bash
    dotnet run
    ```
9. Open a browser and navigate to the following **URL**:
    ```url
    https://localhost:5001/api/values
    ```
10. You should see the following result:
    ```json
    ["value1","value2"]
    ```
11. Switch to **Command Line** and press **Ctrl+C** to stop the process.
12. Close all open windows.


# Lesson 3: Consuming ASP.NET Core Web APIs 

### Demonstration: Consuming Services Using JavaScript
     
#### Demonstration Steps

1. Open **Command Line**.
2. Change directory to the starter project, run the following command in the **Command Line**:
    ```bash
    cd [Repository Root]\Allfiles\Mod03\DemoFiles\JavaScriptClient\Starter
    ```
3. Restore all dependencies and tools of a project use the following command in the **Command Line**:
    ```base
    dotnet restore
    ```
4. Open the project in **VSCode** and paste the following command and press enter:
    ```bash
    code .
    ```
5. Double click on **Startup.cs** and locate **Configure** method.
6. Add the following code in the end of the method, to use static files:
   ```cs
    app.UseStaticFiles();
   ```
   >**Note:** Static files, such as HTML, CSS, images, and JavaScript, are assets an ASP.NET Core app serves directly to clients.
7. Expand **wwwroot** folder and click on **index.html**.
8. Add button element under the **h1** tag, by paste the following code:
   ```html
   <button onclick="getData()">Get All Destination</button>
   ```
9. Open **client.js** file inside the **wwwroot** folder.
10. Add a new function to recieve data from the api, by paste the following code:
    ```js
    function getData(){
     //fetch api default method is GET  
     fetch(uri)
     .then(response => response.json())
     .then(function(data){
         data.forEach(value => {
             document.getElementById('destinations').innerHTML += '<li id="' + value.id + '">'  + value.id + ': ' + value.cityName + ' - ' + value.airport + '</li>';
         });
     })
    }
    ```
11. Switch to **Command Line** and paste the following command in order to run application and then press enter:
    ```bash
    dotnet run
    ```
12. Open a browser and navigate to the following **URL**:
    ```url
    https://localhost:5001/index.html
    ```
13. Click on **Get All Destination** button to view the list of all destinations from the server.
14. Switch to **Command Line** and press **Ctrl+C** to stop the process.
15. Close all open windows.


### Demonstration: Consuming Services Using HttpClient
     
#### Demonstration Steps

1. Open **Command Line**.
2. Change directory to the **Client** starter project, by running the following command in the **Command Line**:
    ```bash
    cd [Repository Root]\Allfiles\Mod03\DemoFiles\HttpClientApplication\Starter\HttpClientApplication.Client
    ```
3. Install the following package **Microsoft AspNet WebApi Client** using the **Command Line**:
    ```base
    dotnet add package Microsoft.AspNet.WebApi.Client --version=5.2.6
    dotnet restore
    ```
4. Run the following command to open the project in **VSCode**:
    ```bash
    code .
    ```
5. Double click on **Program.cs**.
6. Paste the following code in the **Main** method for creating a new **HttpClient** instance:
    ```cs
    using (var client = new HttpClient())
    {

    }
    ```
7. Paste the following code inside the **HttpClient** bracket to create a **GET** request:
    ```cs
    HttpResponseMessage message = await client.GetAsync("http://localhost:5000/api/destinations");
    ```
8. Paste the following code under the **HttpResponseMessage** to read the respone as a string:
    ```cs
    Console.WriteLine("Respone data as string");
    string resultAsString = await message.Content.ReadAsStringAsync();
    Console.WriteLine(resultAsString);
    ```
9. Paste the following code to read the respone and deserialize the **JSON** to a **List\<Destination\>** instance:
    ```cs
    List<Destination> destinationsResult = await message.Content.ReadAsAsync<List<Destination>>();
    Console.WriteLine("\nAll Destination");
    foreach (Destination destination in destinationsResult)
    {
        Console.WriteLine($"{destination.CityName} - {destination.Airport}");
    }

    // ReadKey used that the console will not close when the code end to run.
    Console.ReadKey();
    ```
10. Switch to **Command Line** and paste the following command in order to change dirctory to the **Host** folder:
    ```bash
    cd ..\HttpClientApplication.Host
    ```
11. Paste the following command to run the **Host** application:
    ```bash
    dotnet run
    ```
12. Paste the following command in order to change directory to the **Client** folder:
    ```bash
    cd ..\HttpClientApplication.Client
    ```
13. Paste the following command to run the **Client** application:
    ```bash
    dotnet run
    ```
    >**Note** If there is an error in the **Console** after running the application run the following command: **dotnet dev-certs https --trust** , then try again the steps from **step 11**.
    
14. In the **Console**, Check that all the destinations came from the server.
   
15. Close all open windows.

# Lesson 4: Handling HTTP Requests and Responses

### Demonstration: Throwing Exceptions
     
#### Demonstration Steps

1. Open **Command Line**.
2. Run the following command to change directory to the startup project:
    ```bash
    cd [Repository Root]\Allfiles\Mod03\DemoFiles\ThrowHttpResponseException\Starter
    ```
3. Run the following command to open the project in **VSCode**:
    ```bash
    code .
    ```
4. In **Explorer** blade, under the **STARTER**, expand **Controllers** and double-click on **DestinationsController**.
5. Paste the following code to return **destination** by **id**:
    ```cs
     // GET api/destinations/id
    [HttpGet("{id}")]
    public ActionResult<Destination> Get(int id)
    {
        Destination result = _destinations.FirstOrDefault(x => x.Id == id);
        if (result == null)
            return NotFound();
        return result;
    }
    ```
6. Switch to **Command Line**.
7. Paste the following command to run the application:
    ```bash
    dotnet run
    ```
8. Open browser, and navigate to the following **URL**:
    ```url
    https://localhost:5001/destinations/1
    ```
9.  Check that destination: **Seattle** came from the server.
10. Change to the following **URL**: 
    ```url
    https://localhost:5001/destinations/10
    ```
11. Check there is error code **HTTP ERROR 404** (Not Found).
12. Close all open windows.

# Lesson 5: Automatically Generating HTTP Requests and Responses

### Demonstration: Testing HTTP requests with Swagger
     
#### Demonstration Steps

1. Open **Command Line**.
2. Run the following command to change directory to the startup project:
    ```bash
    cd [Repository Root]\Allfiles\Mod03\DemoFiles\TestingHttpWithSwagger\Starter
    ```
3. Install the **Swagger** package, using the **Command Line**:
    ```base
    dotnet add package Swashbuckle.AspNetCore --version=3.0.0
    dotnet restore
    ```
4. Run the following command to open the project in **VSCode**:
    ```bash
    code .
    ```
5. In **Explorer** blade, under the **STARTER**, double-click on **Startup.cs**.
6. Paste the following **using**:
    ```cs
    using Swashbuckle.AspNetCore.Swagger;
    ```
7. Paste the following code in the **ConfigureServices** method to add the **Swagger** generator to the services collection:
    ```cs
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
    });
    ```
8. Paste the following code in the **Configure** method to add **Swagger** middleware:
    ```cs
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
    ```
9. Switch to **Command Line**.
10. Paste the following command to run the application:
    ```bash
    dotnet run
    ```
11. Open browser, and navigate to the following **URL**:
    ```url
    https://localhost:5001
    ```
12. Check in browser that all the **Api** are shown.
13. Click on **GET /api/Destinations**,
    - Click on **Try it out** in the top right corner.
    - Click on **Execute**. 
    - Locate **Response body** and check that all the **Destinations** came from the server.
14. Close all open windows.

### Demonstration: Generating C# HTTP clients using AutoRest
     
#### Demonstration Steps

1. Open **Command Line**.
2. Run the following command to change directory to the **AutoRest.Host** project:
    ```bash
    cd [Repository Root]\Allfiles\Mod03\DemoFiles\AutoRest\Starter\AutoRest.Host
    ```
3. Run the following command to install **AutoRest** package in **npm**:
    ```bash
    npm install -g autorest@2.0.4280
    ```
4. Run the following command to run the application:
    ```bash
    dotnet run
    ```
5. Run the following command to change directory to the **AutoRest.Sdk** project:
    ```bash
    cd ..\AutoRest.Sdk
    ```
6. Run the following command to **Generate** the **swagger** code:
    ```bash
    autorest -Input http://localhost:5000/swagger/v1/swagger.json -CodeGenerator CSharp -Namespace AutoRest.Sdk
    ```
7. Run the following command to change directory to the **AutoRest.Client** project:
    ```bash
    cd ..\AutoRest.Sdk
    ```
8. Run the following command to open the project in **VSCode**:
    ```bash
    code .
    ```
9. In **Explorer** blade, under the **STARTER**, double-click on **Program.cs**.
10. Paste the following **using** statements:
    ```cs
    using AutoRest.Sdk;
    using AutoRest.Sdk.Models;
    ```
11. Paste the following code in the **Main** method to get **destinations** form the server:
    ```cs
    MyAPI client = new MyAPI(new Uri("http://localhost:5000"));
    IList<Destination> destinationList = client.ApiDestinationsGet();
    ```
12. Paste the following code to show all the **destinations** in the **Console**:
    ```cs
    Console.WriteLine("All Destination");
    foreach (Destination destination in destinationList)
    {
        Console.WriteLine($"{destination.CityName} - {destination.Airport}");
    }
    // ReadKey used that the console will not close when the code end to run.
    Console.ReadKey();
    ```
13. Switch to **Command Line**.
14. Run the following command to run the **AutoRest.Client** application:
    ```bash
    dotnet run
    ```
15. In the **Console**, Check that all the destinations came from the server.
16. Close all open windows.

