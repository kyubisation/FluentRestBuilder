param (
    [string]$increment = ""
)

function main {
	switch ($increment) {
		"major" {
			incrementMajorVersion
		}
		"minor" {
			incrementMinorVersion
		}
		"patch" {
			incrementPatchVersion
		}
		default {
			$version = getVersion
			Write-Host Current version is: $version
		}
	}	
}

function incrementMajorVersion {
	$version = getVersion
	$newMajorVersion = $version.Major + 1
	$newVersion = New-Object System.Version($newMajorVersion, 0, 0)
	incrementVersion $version $newVersion
}

function incrementMinorVersion {
	$version = getVersion
	$newMinorVersion = $version.Minor + 1
	$newVersion = New-Object System.Version($version.Major, $newMinorVersion, 0)
	incrementVersion $version $newVersion
}

function incrementPatchVersion {
	$version = getVersion
	$newPatchVersion = $version.Build + 1
	$newVersion = New-Object System.Version($version.Major, $version.Minor, $newPatchVersion)
	incrementVersion $version $newVersion
}

function getVersion {
	$csproj = @(Get-ChildItem src\**\*.csproj)[0]
	$xml = [xml](Get-Content $csproj)
	New-Object System.Version($xml.Project.PropertyGroup.VersionPrefix)
}

function incrementVersion($from, $to) {
	if (confirm $version $newVersion) {
		saveNewVersion $newVersion
		Write-Host Done
	} else {
		Write-Host Aborting increment
	}
}

function confirm($from, $to) {
	$confirmation = Read-Host Incrementing version from $from to $to. Continue? [Y/n]
	$confirmation -eq "y" -or $confirmation -eq ""
}

function saveNewVersion($version) {
	$csprojFiles = Get-ChildItem src\**\*.csproj
	ForEach($csproj in $csprojFiles) {
		$xml = [xml](Get-Content $csproj)
		$xml.Project.PropertyGroup.VersionPrefix = $version.ToString()
		$xml.Save($csproj)
	}
}

main
