# Module 10: Scaling Services

# Lab: Load Balancing Azure Web Apps

### Exercise 1: Prepare the application for load-balancing

#### Task 1: Add code to print out the server name

1. Create **WebApp** in **http://portal.azure.com**.
2. Open **[Repository Root]\AllFiles\Mod10\Labfiles\Lab1\Starter** project in **VSCode**
3. Add **Middleware** folder, and inside it, new file **ExceptionHandlingMiddleware.cs**.
4. Add **RequestDelegate** instance to **ExceptionHandlingMiddleware.cs**, and init it is the **constructor**.
5. Add **Invoke** method that add **X-BlueYonder-Server** header with the **MachineName** as value.
6. Add **extension method** for **IApplicationBuilder**.
7. Use **Exception Handling Middleware** in the **Configure** method, inside **Startup** class.

#### Task 2: Deploy the application to an Azure Web App

1. Publish the **blueyonder{YourInitials}** service
2. Open **Azure Portal** and click the **blueyonder{YourInitials}** service.
3. Click on **Scale up (App Service plan)** in **Settings** section
4. Under **Dev/Test** tab, Change the **Recommended pricing tiers** to **B1**.

#### Task 3: Configure the Web App for multiple instances

1. Click on **Scale out (App Service plan)** in **Settings** section.
2. Under **Configure** tab, increase the **Override condition** scale to **2**.

### Exercise 2: Test the load balancing with instance affinity

#### Task 1: Browse the website multiple times

1. Open **Microsoft Edge** browser and navigate to:
   ```url
   https://blueyonder{YourInitials}.azurewebsites.net/api/destinations
   ```
2. Vertify that the respone have list of destinations in **JSON**.

#### Task 2: Verify you reached the same server each time

1. Open **Develpper Tools** and navigate to **Network** in the **Microsoft Edge** browser.
2. Navigate to the following url:
   ```url
   https://blueyonder{YourInitials}.azurewebsites.net/api/destinations.
   ```
3. View the respone with all the destinations as the task above.
4. In **Network** tab locate the url and check the following info:
   - Locate the **X-BlueYonder-Server** in **Response Headers** section and verified that the **Status** is **200**.
     > **Note:** You should always get the same **machine name** value in **X-BlueYonder-Server** header.

### Exercise 3: Test the load balancing without affinity

#### Task 1: Update the application to not use affinity

1. Click **blueyonder{YourInitials}** web app, in the **Azure Portal**.
2. Click on **Application settings** inside **Settings** section, click **General settings**.
3. Switch the **ARR Affinity** to **Off**, in the **General settings**.

#### Task 2: Retest and verify you reached more than one instance

1. Refresh the page couple of times, in **Microsoft Edge** browser.
2. View the respone with all the destinations as the previous exercise.
3. View the **X-BlueYonder-Server** in the **Network** tab.
   > **Note:** Now there is 2 server instances, in **X-BlueYonder-Server**
   > can get 2 defferant value of **machine name**.

# Lab 2: Load Balancing with Azure Traffic Manager

### Exercise 1: Deploy an Azure Web App to multiple regions

#### Task 1: Deploy an Azure Web App to the West Europe region

1. Open **PowerShell** as **Administrator**.
2. Run the following command from the **[repository root]\AllFiles\Mod10\Labfiles\Lab2\Starter\Setup** directory:
   ```batch
    .\createAzureServicesWestEurope.ps1
   ```
3. Sign in with your **Subscription ID** and follow the on-screen instructions.
p
#### Task 2: Deploy an Azure Web App to the West US region

1.  1. Open **PowerShell** as **Administrator**.
2.  Run the following command from the **[repository root]\AllFiles\Mod10\Labfiles\Lab2\Starter\Setup** directory:
    ```batch
     .\createAzureServicesWestUS.ps1
    ```
3.  Sign in with your **Subscription ID** and follow the on-screen instructions.

### Exercise 2: Create an Azure Traffic Manager profile

#### Task 1: Create an Azure Traffic Manager profile in the Azure Portal

1. Create **Traffic Manager profile** with **Routing method** of **Priority** routing method.

#### Task 2: Configure the profile to point to the two Azure Web Apps

1.  Create primary end point, in **Traffic Manager profile**:
    - In **Type**, choose **Azure endpoint**.
    - In **Target resource type**, choose **App Service**.
    - In **Priority**, type **1**.
2. Create secondary end point, in **Traffic Managerprofile**:
    - In **Type**, choose **Azure endpoint**.
    - In **Target resource type**, choose **App Service**.
    - In **Priority**, type **2**.

#### Task 3: Test the Azure Traffic Manager DNS resolver by seeing which instance of the app was reached

1. Navigate to:
    ```url
    http://blueyondermod10lab2TM.trafficmanager.net/api/destinations
    ```
    > Note: All requests are routed to the primary endpoint that is set to Priority 1.
2. Run the following command to find our trafficmanager:
    ```bash
    nslookup blueyondermod10lab2TM.trafficmanager.net
    ```
3. You should see the primary endpoint, **blueyondermod10lab2t1** service.
4. Change the **Status** to **Disabled**, of the **mySecondaryEndpoint**, in the **Settings** section of the **Azure Traffic Manager**.
5. Navigate to:
    ```url
    http://blueyondermod10lab2TM.trafficmanager.net/api/destinations
    ```
    > Note: All requests are routed to the secondary endpoint that is set to Priority 2.
6. Run the following command to find our trafficmanager:
    ```bash
    nslookup blueyondermod10lab2TM.trafficmanager.net
    ```
7. You should see the secondary endpoint, **blueyondermod10lab2t2** service.