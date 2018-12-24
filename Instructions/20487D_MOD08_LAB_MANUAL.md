
# Module 8: Diagnostics and Monitoring

# Lab: Collect and view ETW events 

#### Scenario

In this lab, you will use Event Tracing for Windows (ETW) on Windows and LTTng on Linux to monitor exception and garbage collection (GC) events in a Microsoft ASP.NET Core application.

#### Objectives

After you complete this lab, you will be able to:
-	Collect and analyze ETW events with PerfView for an ASP.NET Core application on Windows.
-	Collect and analyze LTTng events for an ASP.NET Core application on a Linux Docker container.

### Exercise 1: Publish the ASP.NET Core web service for Linux
 
#### Task 1: Run the ASP.NET Core application

1. Open the command prompt, and then navigate to **[Repository Root]\AllFiles\Mod08\Labfiles\Lab1\Blueyonder.Service** folder.
2. Run the project.

#### Task 2: Record .NET ETW events in PerfView

   Open **PerfView**, and then collect all the events

#### Task 3: Run a script to invoke the service and meck it to throw exceptions

1. In PowerShell, from the **assets** folder, run the **requestsToServer** script.
2. Switch to **PerfView** and stop collecting the events.

#### Task 4: View exception details and call stacks in PerfView

1. In **Advanced Group**, click **Exceptions Stacks**.
2. Locate the **Blueyonder.Service** process.
3. Locate the exception and view the call stack.

### Exercise 2: Collect and view LTTng events

#### Task 1: Run the ASP.NET Core application in a Linux container with COMPlus_EventLogEnabled=1

1. Open the command prompt, and then navigate to the **[Repository Root]\AllFiles\Mod08\Labfiles\Lab1\Blueyonder.Service** folder.
2. Run the project in docker on port **8080:80** and name **myapp**.

#### Task 2: Record LTTng events with the Lttng CLI tool

1. Enter to **myapp** container the shell.
2. Update the packages in the container.
3. Install the **software-properties-common** package.
4. Add **PPA repository**.
5. Install the **LTTng** package.
6. Create a session in **LTTng**.
7. Run a command to add context data (process id, thread id, process name) to each event.
8. Run a command to create an event rule to record all the events starting with **DotNETRuntime**.
9. Start recording events.
10. Open the browser and navigate to the service a few times.

#### Task 3: View LTTng exception and GC events with the babeltrace CLI tool

1. Stop recording the events, and then destroy the session.
2. To view all the recorded events, run the **babeltrace** command.

#### Task 4: Open the recording file on Windows with PerfView

1. To archive the **trace** folder, run the following command:
   ```bash
   zip -r /root/lttng-traces/sample-trace sample.trace.zip
   ```
2. Copy the archive to the local file system.
3. Remove the **myapp** container.
4. Open the zip file in PerfView and look at the events.

# Lab: Monitor Azure Web Apps with Microsoft Azure Application Insights

#### Scenario

In this lab, you will use Application Insights to monitor and diagnose a web service running in Azure Web Apps.

#### Objectives

After you complete this lab, you will be able to:
- Add Application Insights to your application.
- Load test your service by using Azure.
- Analyze performance by using Application Insights.

### Exercise 1: Add the Application Insights SDK

#### Task 1: Add the Application Insights SDK to the web service project

1. Open **Azure Portal**.
2. Setup **Application Insights** for the new **blueyondermod08lab2***{YourInitials}* app service.
 - In the setup config the **Runtime/Framework** to **ASP.NET Core**.
3. From the new **Application Insights**, copy **Instrumentation Key**.
4. Open the command prompt, and then navigate to the **[Repository Root]\Allfiles\Mod08\Labfiles\Lab2\Starter** folder.
5. At the command prompt, install the **Microsoft.ApplicationInsights.AspNetCore** package.
6. To **appsettings.json**, add the following key:
    ```json
    "ApplicationInsights": {
        "InstrumentationKey": "{InstrumentationKey}"
    }
    ```
7. Replace the **InstrumentationKey** key with the value copied in point 3.
8. In the **Program** class, add **ApplicationInsights** to the host builder.

#### Task 2: Publish the service to Azure Web Apps

   At the command prompt, publish the service.

### Exercise 2: Load test the web service

#### Task 1: Create a new performance test in the Azure Portal

1. Switch to **Azure Portal**.
2. Navigate to **Performance test** and configure with the following steps:
    - Create a new **Organization** and name it **blueyondervsts***{YourInitials}*.
    - In the new **blueyondervsts** resource, navigate to **VSTS**.
    - In **VSTS**, create a new project and name it **Blueyonder**.

#### Task 2: Run the performance test for a few minutes with multiple simulated users

1. Switch to **Azure Portal**.
2. Create a new **Performance test** with the following conifgration:
    - In **TEST TYPE**, select **Manual Test**.
    - In **URL**, type **http://blueyondermod08lab2***{YourInitials}***.azurewebsites.net/api/destinations**.
    - In **Name**, type **DestinationsTest**.
    - In **USER LOAD**, type **20**.
    - In **DURATION (MINUTES)**, type **5**.

### Exercise 3: Analyze the performance results

#### Task 1: View the overall website performance and request metrics

1. Switch to **VSTS**.
2. View the summary of **DestinationsTest**.

#### Task 2: Examine specific requests and view their timelines and dependencies

1. Scroll down and locate **OPERATION NAME**.
2. Click **GET destinations/Get**.
3. On the right-hand side, view info about the request.

#### Task 3: Drill down into the Application Insights Profiler results for slow requests

1. In the right-hand bottom corner, click **Samples**.
2. View all the requests and click the request with the highest duration.
3. View all the details about the request.

©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
