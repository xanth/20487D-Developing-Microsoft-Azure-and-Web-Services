# Module 9: Securing services on-premises and in Azure

1. Wherever you see a path to file starting at [Repository Root], replace it with the absolute path to the directory in which the 20487 repository resides.
   e.g. - you cloned or extracted the 20487 repository to C:\Users\John Doe\Downloads\20487, then the following path: [Repository Root]\AllFiles\20487D\Mod01 will become C:\Users\John Doe\Downloads\20487\AllFiles\20487D\Mod01
2. Wherever you see **{YourInitials}**, replace it with your actual initials.(for example, the initials for John Do will be jd).
3. Before performing the demonstration, you should allow some time for the provisioning of the different Azure resources required for the demonstration. It is recommended to review the demonstrations before the actual class and identify the resources and then prepare them beforehand to save classroom time.

### Demonstration: Using ASP.NET Identity


### Demonstration: Creating an Azure Active Directory and users

#### Preparation Steps

  You need two available emails, one for the Azure portal and the second for creating a new user.

#### Demonstration Steps

1. Open **Azure Portal**.
2. Click on **Azure Active Directory** on the left menu.
3. In the **Azure Active Directory** blade, click on **Custom domain names**, and then copy the value in the **NAME** field.
4. Go back to the **Azure Active Directory** blade, click on the **Users** in the **Manage** section.
5. Click on **New user** in the top menu.
    - In the **Name** text box, enter your full name.
    - In the **User name** text box, enter your name + **@[Paste Domain name]**. (Domain name which you have copied in point 3)
    - Click **Create**.
6. Click on **New guest user** in the top menu.
7. Enter the email address in the text box, and then click **Invite**.
8. Open the email and accept the invitation.
9. Go back to **Azure Active Directory** in the portal.
10. Click on **Groups** in the **Manage** section.
11. Click on **New group** in the top menu.
    - In the **Group type** list box, select **security**.
    - In the **Group name** text box, enter **Mod9Group**.
    - In the **Membership type** list box, select **Assigned**.
    - Click **Members**, select the guest user, and then click **Select**.
    - Click **Create**.
12. Open **Groups**, see that the **Group** is created.
13. Close all open windows.


### Demonstration: Securing an ASP.NET Core application using OpenID Connect and AAD

#### Preparation Steps

1. Open **Azure Portal**.
2. Click on **Azure Active Directory** on the left menu.
3. In the **Azure Active Directory** blade, click on **App registrations**, and then click **New application registration**
   - In the **Name** text box, enter **Mod10OpenIdConnect**
   - In the **Url** text box, enter **https://localhost:5001/signin-oidc**
   - Click **Create**.
4. Copy the **Application ID** this is the Client ID
5. Close the registerd app window
6. Click **Endpoints**, and then copy one of the links and extract the guid this is the TenentID

#### Demonstration Steps

1. Open **Command Line**.
2. Run the following command to change directory to the **DemoFiles**:
   ```bash
    cd [Repository Root]\Allfiles\Mod09\Demofiles\OpenIDConnectAAD
   ```
3. Run the following command to open the project in **VSCode**:
   ```bash
    code .
   ``` 
4. Open **appsettings.json** file and fill **ClientId** and **Authority** with the data from the preapreation steps
5. Switch to **Command Line**.
6. Run the following command to run the project:
    ```bash
    dotnet run    
    ```
7. Open browser and navigate to the following **URL**:
    ```url
    https://localhost:5001/api/values
    ```
8. You will see your user name as part of the result

### Demonstration: Using AAD B2C with ASP.NET Core

#### Preparation Steps

1. Open **Azure Portal**.
2. Click on **Azure Active Directory** on the left menu.

#### Demonstration Steps

1. Open **Command Line**.
2. Run the following command to change directory to the **DemoFiles**:
   ```bash
    cd [Repository Root]\Allfiles\Mod09\Demofiles\AADB2C
   ```
3. Run the following command to open the project in **VSCode**:
   ```bash
    code .
   ``` 
4. Open **appsettings.json** file and fill **ClientId** and **TenantName** with the data from the preapreation steps
5. Switch to **Command Line**.
6. Run the following command to run the project:
    ```bash
    dotnet run    
    ```
7. Open browser and navigate to the following **URL**:
    ```url
    https://[TenantName].b2clogin.com/[TenantName].onmicrosoft.com/oauth2/v2.0/authorize?p=B2C_1_My&client_id=[ClientId]&nonce=defaultNonce&redirect_uri=https%3A%2F%2Fjwt.ms&scope=openid&response_type=id_token&prompt=login
    ```
8. Copy the token from the text box
9. Switch to **Command Line**.
10. Run the following command to run the project:
    ```bash
    curl https://localhost:44362/api/values -i --header "Authorization: Bearer [Token]"    
    ```
11. You will see your user name as part of the result