### Login to the account
Login-AzureRmAccount

Write-Host "Please enter your subscription id: " -NoNewline
$SubscriptionId = Read-Host

Select-AzureRmSubscription -SubscriptionId $SubscriptionId

### get the user name for resources naming in the script
Write-Host "Please enter your name" -NoNewline
Write-Host " - no more than 10 characters: " -ForegroundColor Yellow -NoNewline
$yourName = Read-Host

$yourName = $yourName.ToLower()
$websiteName = "blueyonderMod10Demo1" + $yourName
$resourcesGroupName = "Mod10Demo1-RG"

Function GetLocation {
$Title = "Please choose customer location"
$Info = "Select location"
$options = [System.Management.Automation.Host.ChoiceDescription[]] @("&WestEurope", "&WestUS", "&EastUS", "&SoutheastAsia")
[int]$defaultchoice = 0
$opt =  $host.UI.PromptForChoice($Title , $Info , $Options,$defaultchoice)

switch($opt)
    {
        0 {$location = "WestEurope"}
        1 {$location = "WestUS"}
        2 {$location = "EastUS"}
        3 {$location = "SoutheastAsia"}
    }

$location
} #prompts to choose a location for resources

$location = GetLocation
### Create Resource group
$RG = New-AzureRmResourceGroup -Name "$resourcesGroupName" -Location $location 

### Deploy the webapp
New-AzureRmResourceGroupDeployment -ResourceGroupName $RG.ResourceGroupName -TemplateFile $PSScriptRoot\template.json -TemplateParameterFile $PSScriptRoot\parameters.json -webappname $websiteName -hostingPlanName "plan$websiteName" -location $location -serverFarmResourceGroup $RG.ResourceGroupName -subscriptionId $SubscriptionId

### Deploy the code to the webapp
$path = (get-item $PSScriptRoot).parent.FullName+"\Code\BlueYonder.Hotels.Service\BlueYonder.Hotels.Service"
Write-Host $path
cd $path
$profile = Get-AzureRmWebAppPublishingProfile -ResourceGroupName $RG.ResourceGroupName -Name $websiteName -Format WebDeploy -OutputFile "$path\Properties\PublishProfiles\Azureprofile.xml"

$azureProfilePath = "$path\Properties\PublishProfiles\Azureprofile.xml"
$publishProfilePath = "$path\Properties\PublishProfiles\Azure.pubxml"

$azureProfileXml = [xml](Get-Content $azureProfilePath)
$publishProfileXml = [xml](Get-Content $publishProfilePath)

$publishProfileXml.Project.PropertyGroup.PublishSiteName = $websiteName
$MSDeployPublishProfile = $azureProfileXml.publishData.publishProfile | where {$_.publishMethod -eq 'MSDeploy'}
$publishProfileXml.Project.PropertyGroup.UserName = $MSDeployPublishProfile.userName
$publishProfileXml.Project.PropertyGroup.Password = $MSDeployPublishProfile.userPWD

$publishProfileXml.Save($publishProfilePath)

&dotnet publish /p:PublishProfile=Azure /p:Configuration=Release

### Get the webapp site url
$webapp = Get-AzureRmWebApp -ResourceGroupName $RG.ResourceGroupName -Name $websiteName 
$urlname = $webapp.DefaultHostName
$siteurl = "https://$urlname"
Write-Host "The WebApp URL is: $siteurl"

cd ..