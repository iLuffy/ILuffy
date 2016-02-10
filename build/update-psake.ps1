function Expand-ZipFile($file, $destination)
{
	$shell = new-object -com shell.application
	$zip = $shell.NameSpace($file)
	foreach($item in $zip.items())
	{
		$shell.Namespace($destination).copyhere($item)
	}
}

$workingDirectory = split-path -path $MyInvocation.MyCommand.Path -Parent
$psakeZipFile = $workingDirectory + '\psake.zip'

write-host "clean the paske.zip and paske-master folder"
Remove-Item -Path $psakeZipFile -Force
Remove-Item -Path "$workingDirectory\\psake-master" -Force -Recurse

write-host "download the psake.zip"
..\tools\curl -L https://github.com/psake/psake/archive/master.zip -o "$psakeZipFile"

Expand-ZipFile -File $psakeZipFile  -Destination $workingDirectory