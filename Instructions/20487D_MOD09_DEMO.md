
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
