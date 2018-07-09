
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
