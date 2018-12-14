
Write-Host "Please enter your initials" -NoNewline
Write-Host " - no more than 10 characters: " -ForegroundColor Yellow -NoNewline
$yourName = Read-Host
$yourName = $yourName.ToLower()

$StorageAccountName = "blueyonder" + $yourName

Write-Host "Plase enter storage account key: " -NoNewline
$StorageAccountKey = Read-Host

$ContainerName = "aircraft-images"
$sourceFileRootDirectory = $PSScriptRoot + "\Images"


$ctx = New-AzureStorageContext -StorageAccountName $StorageAccountName -StorageAccountKey $StorageAccountKey
$container = Get-AzureStorageContainer -Name $ContainerName -Context $ctx

$container.CloudBlobContainer.Uri.AbsoluteUri
if ($container) {
    $filesToUpload = Get-ChildItem $sourceFileRootDirectory -Recurse -File

    foreach ($x in $filesToUpload) {
        $targetPath = ($x.fullname.Substring($sourceFileRootDirectory.Length + 1)).Replace("\", "/")

        Write-Verbose "Uploading $("\" + $x.fullname.Substring($sourceFileRootDirectory.Length + 1)) to $($container.CloudBlobContainer.Uri.AbsoluteUri + "/" + $targetPath)"
        Set-AzureStorageBlobContent -File $x.fullname -Container $container.Name -Blob $targetPath -Context $ctx -Force:$Force | Out-Null
    }
}


