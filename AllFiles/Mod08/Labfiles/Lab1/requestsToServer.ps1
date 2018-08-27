

[System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12;
For ($i=0; $i -le 10; $i++) 
{
	Try
	{
		Invoke-WebRequest -Uri http://localhost:5000/api/flights/$i -Method GET
	}
	Catch
	{
		Write-Host "Invoke flights api with id "  $i 
	}
}



