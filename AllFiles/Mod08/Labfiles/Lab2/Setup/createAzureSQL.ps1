<#
.SYNOPSIS
    Deploys a template to Azure
 
.DESCRIPTION
    Deploys an Azure Resource Manager template
 
.PARAMETER subscriptionId
    The subscription id where the template will be deployed.
#>
 
param(
[Parameter(Mandatory=$True)]
[string]
$subscriptionId
)
 
Function RegisterRP {
    Param(
        [string]$ResourceProviderNamespace
    )
 
    Write-Host "Registering resource provider '$ResourceProviderNamespace'";
    Register-AzureRmResourceProvider -ProviderNamespace $ResourceProviderNamespace;
}
 
$ErrorActionPreference = "Stop"
$resourceGroupName = "BlueYonder.Lab.08"
$today = Get-Date -format yyyy-MM-dd;
 
# sign in
Write-Host "Logging in...";
Login-AzureRmAccount;
 
# select subscription
Write-Host "Selecting subscription '$subscriptionId'";
Select-AzureRmSubscription -SubscriptionID $subscriptionId;
 
# Register RPs
$resourceProviders = @("microsoft.sql");
if($resourceProviders.length) {
    Write-Host "Registering resource providers"
    foreach($resourceProvider in $resourceProviders) {
        RegisterRP($resourceProvider);
    }
}
 
#Create or check for existing resource group
$resourceGroup = Get-AzureRmResourceGroup -Name $resourceGroupName -ErrorAction SilentlyContinue
if(!$resourceGroup)
{  
     Write-Host "Resource group '$resourceGroupName' does not exist.";
    $resourceGroupLocation = "eastus"
    Write-Host "Creating resource group '$resourceGroupName' in location '$resourceGroupLocation'";
    New-AzureRmResourceGroup -Name $resourceGroupName -Location $resourceGroupLocation
}
else{
    Write-Host "Using existing resource group '$resourceGroupName'";
    $resourceGroupLocation = $resourceGroup[0].Location;
}
 
# Get user's initials
Write-Host "Please enter your name's initials: (e.g. - John Doe = jd)";
$initials = Read-Host "Initials";
$serverName = "blueyonder08-$initials";
$databaseName = "BlueYonder.Flights.Lab08";
$password = 'Pa$$w0rd';
 
 
 
# Start the deployment
# Resource creation
Write-Host "Starting deployment of Azure SQL...";
New-AzureRmResourceGroupDeployment -ResourceGroupName $resourceGroupName -TemplateFile "./azureSql.json" -serverName $serverName -databaseName $databaseName;
 
# post-creation
$dbConnectionString = "Server=tcp:$serverName.database.windows.net,1433;Initial Catalog=$databaseName;Persist Security Info=False;User ID=BlueYonderAdmin;Password=$password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=180;"
 
$ipAddress= Invoke-RestMethod http://ipinfo.io/json | Select -exp ip
 
 # Create a server firewall rule that allows access from the specified IP range
$serverfirewallrule = New-AzureRmSqlServerFirewallRule -ResourceGroupName $resourceGroupName `
    -ServerName $serverName `
    -FirewallRuleName "AllowedIPs" -StartIpAddress $ipAddress -EndIpAddress $ipAddress
 
Write-Host $dbConnectionString
 
Write-Host "Database has been created successfully"