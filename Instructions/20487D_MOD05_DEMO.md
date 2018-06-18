# Module 5: Hosting Services On-Premises and in Azure

 Wherever you see a path to file starting at [repository root], replace it with the absolute path to the directory in which the 20487 repository resides. 
 e.g. - you cloned or extracted the 20487 repository to C:\Users\John Doe\Downloads\20487, then the following path: [repository root]\AllFiles\20487D\Mod01 will become C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01

# Lesson 1: Hosting Services on-premises

### Demonstration: Hosting Services on-premises by using Windows Services with Kestrel (RunAsService)

#### Demonstration Steps

1. Open **Command Line** as an administrator.
2. Paste the following command and then press **Enter**:
   ```bash
      cd [Repository Root]\Allfiles\Mod05\DemoFiles\BlueYonder.Hotels.Service
   ```
3. Paste the following command in order to publish your **ASP .NET Core** project into a folder and then press **Enter**:
   ```bash
      dotnet publish --configuration Release --output [Repository Root]\Allfiles\Mod05\DemoFiles\BlueYonder.Hotels.Service
   ```
4. Paste the following command in order to create your **Windows Service** and then press **Enter**:
   ```bash
      sc create HotelsService binPath= "[Repository Root]\Allfiles\Mod05\DemoFiles\BlueYonder.Hotels.Service\BlueYonder.Hotels.Service.exe"
   ```
5. Paste the following command in order to start your **Windows Service** and then press **Enter**:
   ```bash
      sc start HotelsService
   ```
6. Verify that the service is working by open a browser and browse to **http://localhost:5000/api/values**.
7. Paste the following command in order to stop your **Windows Service** and then press **Enter**:
   ```bash
      sc stop HotelsService
   ```
8. Close **Command Line**.




©2018 Microsoft Corporation. All rights reserved.

The text in this document is available under the [Creative Commons Attribution 3.0 License](https://creativecommons.org/licenses/by/3.0/legalcode), additional terms may apply. All other content contained in this document (including, without limitation, trademarks, logos, images, etc.) are **not** included within the Creative Commons license grant. This document does not provide you with any legal rights to any intellectual property in any Microsoft product. You may copy and use this document for your internal, reference purposes.

This document is provided &quot;as-is.&quot; Information and views expressed in this document, including URL and other Internet Web site references, may change without notice. You bear the risk of using it. Some examples are for illustration only and are fictitious. No real association is intended or inferred. Microsoft makes no warranties, express or implied, with respect to the information provided here.
