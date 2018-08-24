$path = $PSScriptRoot+"\..\Solution\BlueYonder.Flights\BlueYonder.Flights.Service"
Write-Host "Test"



$path2 = (get-item $PSScriptRoot).parent.FullName+"\Solution\BlueYonder.Flights\BlueYonder.Flights.Service"
cd $path2
Write-Host $path2