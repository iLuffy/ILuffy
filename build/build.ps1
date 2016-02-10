## VS 2015 msbuild http://stackoverflow.com/questions/32007871/how-to-upgrade-msbuild-to-c-sharp-6
## define conditional compilation symbols http://stackoverflow.com/questions/479979/msbuild-defining-conditional-compilation-symbols

properties {
  $build_dir = resolve-path .
  $base_dir = split-path $build_dir
  $sln_file="$base_dir\Halo\Halo.sln"
  $build_artifacts_dir = "$base_dir\buildArtifacts"
  $35output_dir = "$base_dir\buildArtifacts\net35\"
  $40output_dir = "$base_dir\buildArtifacts\net40\"
  $40clientoutput_dir = "$base_dir\buildArtifacts\net40client\"
  $45output_dir = "$base_dir\buildArtifacts\net45\"
  $nuget_package_dir = "$base_dir\nugetPackage\"
  $nuget_spec = "$build_dir\halo.nuspec"
  $nuget_target_spec = "$nuget_package_dir\halo.nuspec"
  $nuget_version = "1.0.0"
  $nuget_tool = "$base_dir\tools\nuget.exe"
  $VS2015MSBuild = "C:\Program Files (x86)\MSBuild\14.0\Bin\MsBuild.exe"
  $binaryList = @("Halo.dll","Halo.dll.config","HaloI18N.dll","HaloPrinter.dll","HaloWPFLib.dll")
}

Function Clean-Directory([string]$folder) {
	if ( Test-Path $folder)
    {
        Remove-Item -Force -Recurse $folder -ErrorAction SilentlyContinue
    }
    mkdir $folder | out-null
}

task default -depends Release

task Clean -depends CleanMSBuild { 
    Clean-Directory $build_artifacts_dir
	Clean-Directory $nuget_package_dir
}

task Init -depends Clean {
    

}

task CleanMSBuild {

    exec { msbuild "$sln_file" /t:clean /p:Configuration=Release /v:quiet }
}

task Compile35 -depends CleanMSBuild {

    exec { msbuild "$sln_file" /t:clean /p:Configuration=Release /v:quiet }

    & "$VS2015MSBuild" `
        "$sln_file" `
        /p:OutDir="$35output_dir" `
        /t:Build `
		/v:quiet `
        "/p:Configuration=Release;TargetFrameworkVersion=v3.5" /m
}

task Compile40 -depends CleanMSBuild {
    
    exec { msbuild "$sln_file" /t:clean /p:Configuration=Release /v:quiet }

    & "$VS2015MSBuild" `
        "$sln_file" `
        /p:OutDir="$40output_dir" `
        /t:Build `
		/v:quiet `
        "/p:Configuration=Release;TargetFrameworkVersion=v4.0" /m
}

task Compile40Client {
    
    exec { msbuild "$sln_file" /t:clean /p:Configuration=Release /v:quiet }

    & "$VS2015MSBuild" `
        "$sln_file" `
        /p:OutDir="$40clientoutput_dir" `
        /t:Build `
		/v:quiet `
        "/p:Configuration=Release;TargetFrameworkVersion=v4.0;TargetFrameworkProfile=Client" /m

}

task Compile45 {

    exec { msbuild "$sln_file" /t:clean /p:Configuration=Release /v:quiet }

    & "$VS2015MSBuild" `
        "$sln_file" `
        /p:OutDir="$45output_dir" `
        /t:Build `
		/v:quiet `
        "/p:Configuration=Release;TargetFrameworkVersion=v4.5;DefineConstants=NetFramework45" /m

}

task Compile -depends Init, Compile35, Compile40, Compile45 {

}

task Test -depends Compile {

}

task Merge {
}

task NugetMakePackage -depends Release {
	Copy-Item $nuget_spec $nuget_package_dir
	Clean-Directory $nuget_package_dir\lib
	Clean-Directory $nuget_package_dir\lib\net35
	Clean-Directory $nuget_package_dir\lib\net40
	Clean-Directory $nuget_package_dir\lib\net45
	$binaryList | % { Copy-Item $35output_dir\$_ $nuget_package_dir\lib\net35 }
	$binaryList | % { Copy-Item $40output_dir\$_ $nuget_package_dir\lib\net40 }
	$binaryList | % { Copy-Item $45output_dir\$_ $nuget_package_dir\lib\net45 }
	
	cd $nuget_package_dir
	
	& $nuget_tool pack $nuget_target_spec -Verbosity quiet -Version $nuget_version
	
	cd $build_dir
}

task NugetPushPackage {
	& $nuget_tool push $nuget_package_dir\halo.$nuget_version.nupkg
}

task Release -depends Test {
    
}


task ? -Description "Helper to display task info" {
	Write-Documentation
}