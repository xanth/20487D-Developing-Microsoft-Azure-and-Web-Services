
# Module 8: Diagnostics and monitoring

# Lab: Collect and view ETW events 

#### Scenario


#### Objectives


### Exercise 1: Publish the ASP.NET Core web service for Linux
 
#### Task 1: Run the ASP.NET Core application

1. Open **Command Line** and navigate to **[Repository Root]\AllFiles\Mod08\Labfiles\Lab1\Blueyonder.Service** folder.
2. Run the project.

#### Task 2: Record .NET ETW events in PerfView

1. Open **PerfView** and collect all the events

#### Task 3: Run a script to invoke service and meck it to throw exceptions

1. Run the script named **requestsToServer** from the **assets** folder via **Powershell**.
2. Switch to **PerfView** and stop collecting the events.

#### Task 4: View exception details and call stacks in PerfView

1. Click on **Exceptions Stacks** in **Advanced Group**.
2. Locate process **Blueyonder.Service**.
3. Locate the exception and view the call stack 

### Exercise 2: Collect and view LTTng events

#### Task 1: Run the ASP.NET Core application in a Linux container with COMPlus_EventLogEnabled=1

1. Open **Command Line** and navigate to **[Repository Root]\AllFiles\Mod08\Labfiles\Lab1\Blueyonder.Service** folder.
2. Run the project in **docker** on port **8080:80** and named **myapp**.

#### Task 2: Record LTTng events with the lttng CLI tool

1. Enter to **myapp** container the shell.
2. Update packages in the container.
3. Install **software-properties-common** package.
4. Add **PPA repository**.
5. Install **LTTng** package.
6. Create a session in **LTTng**.
7. Run a command to add context data (process id, thread id, process name) to each event.
8. Run a command to create an event rule to record all the events starting with **DotNETRuntime**.
9. Start recording events.
10. Open browser and navigate to the service few times.

#### Task 3: View LTTng exception and GC events with the babeltrace CLI tool

1. Stop the recording the events and destroy the session.
2. Run a command **babeltrace** to view all the recorded event.

#### Task 4: Open the recording file on Windows with PerfView

1. Run the following command to archive the trace folder:
   ```bash
   zip -r /root/lttng-traces/sample-trace sample.trace.zip
   ```
2. Copy the archive to the local file system.
3. Remove the container named  **myapp**.
4. Open the zip file in PerfView and look at the events.

# Lab: Monitoring Azure Web Apps with Application Insights

#### Scenario

#### Objectives


### Exercise 1: Add the Application Insights SDK

#### Task 1: Add the Application Insights SDK to the web service project

1. Open **Azure Portal**.
2. Setup the **Application Insights** for the new create app service named **blueyondermod08lab2**{YourInitials}.
 - In the setup config the **Runtime/Framework** to **ASP.NET Core**.
3. Copy **Instrumentation Key** from the new **Application Insights**.
4. Open **Command Line** and navigate to **[Repository Root]\Allfiles\Mod08\Labfiles\Lab2\Starter** folder.
5. Install **Microsoft.ApplicationInsights.AspNetCore** package via **Command Line**.
6. Add the following key to **appsettings.json**:
    ```json
    "ApplicationInsights": {
        "InstrumentationKey": "{InstrumentationKey}"
    }
    ```
7. Replace the **InstrumentationKey** key with value copied in point 3.
8. Add the **ApplicationInsights** to the host builder in the **Program** class.

#### Task 2: Publish the service to Azure Web Apps

1. Publish the service via **Command Line**.

### Exercise 2: Load test the web service

#### Task 1: Create a new performance test in the Azure Portal

1. Switch to **Azure Portal**.
2. Naviage to **Performance test** and configute with the following steps:
    - Create new **Organization** and named it **blueyondervsts**{YourInitials}.
    - In the new resources **blueyondervsts** naviage to **VSTS**..
    - Create new project in the **VSTS** and named it **Blueyonder**.

#### Task 2: Run the performance test for a few minutes with multiple simulated users

1. Switch to **Azure Portal**.
2. Create new **Performance test** with the following conifgration:
    - In **TEST TYPE** select **Manual Test**.
    - In **URL** type **http://blueyondermod08lab2{YourInitials}.azurewebsites.net/api/destinations**.
    - In **Name** type **DestinationsTest**.
    - In **USER LOAD** type **20**.
    - In **DURATION (MINUTES)** type **5**.

### Exercise 3: Analyze the performance results

#### Task 1: View the overall website performance and request metrics

1. Switch to **VSTS**.
2. View the Summary of the test named **DestinationsTest**.

#### Task 2: Examine specific requests and view their timelines and dependencies

1. Scroll down and locate **OPERATION NAME**.
2. Click on **GET destinations/Get**.
3. View on the right side info about the request.

#### Task 3: Drill down into the Application Insights Profiler results for slow requests

1. Click on **Samples** on the right bottom corner.
2. View all the request and click the request with high **duration** time.
3. View all the details about the request.

©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.