$ErrorActionPreference = 'Stop';
$ScriptLocation = (Split-Path -Path $MyInvocation.MyCommand.Path -Parent);

function Call-MsBuild([Parameter(Mandatory=$true)]$Target, $Parameters, [Parameter(Mandatory=$true)]$BuildFile)
{
 #    $test = Start-Process "powershell.exe" -Verb Runas -ArgumentList '-command "Get-Process"'

    #$property = Get-ItemProperty -Path hklm:\SOFTWARE\Microsoft\MSBuild\ToolsVersions\4.0 -Name 'MSBuildToolsPath';
    #$msBUildPath = $property.MSBuildToolsPath + "msbuild.exe" ;
    $msBUildPath = "C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe";

    & $msBUildPath /t:$Target /p:$Parameters $BuildFile;
    Assert-LastExternalCallWasOK -Message "Execution of [$msBUildPath /t:$Target /p:$Parameters $BuildFile] failed. Check log for more details";
 
    Write-Host "[$msBUildPath /t:$Target /p:$Parameters $BuildFile] has been executed successfully."
}

function Assert-LastExternalCallWasOK([Parameter(Mandatory=$true)]$Message)
{
    if ($LASTEXITCODE -ne $nil -And $LASTEXITCODE -ne 0)
    {
        throw "$Message Error code: $LASTEXITCODE";
    }
}

function Update-ProjectOutputPath($PathToProject, $OutputPath)
{   
    Add-Type -AssemblyName System.Xml.Linq
    $projectFileContent =  [System.Xml.Linq.XDocument]::Load($PathToProject); 
    $projectFileContent.Descendants([System.Xml.Linq.XName]::Get("OutputPath", "http://schemas.microsoft.com/developer/msbuild/2003")) | ForEach-Object { $_.SetValue($OutputPath)}
    $projectFileContent.Save($PathToProject)
    
    Write-Host "Updated output path of $PathToProject"
}

function Prepare-OutputFolder
{
    $OutputPath = $ScriptLocation + "\..\Output"

    if (Test-Path $OutputPath)
    {
        Remove-Item $OutputPath -Recurse -Force   
    }
    New-Item "$OutputPath\tools" -type directory | Out-Null
        
    return Resolve-Path $OutputPath
}

function Build-NugetPackage($Folder, $Version)
{
    & ..\tools\Nuget\Nuget.exe pack ..\NugetPackage\CodeMetricsReportProcessor.nuspec /BasePath $Folder /OutputDirectory $Folder /Version $Version
    Assert-LastExternalCallWasOK -Message "Execution of nuget.exe  failed. Check log for more details";         
}

function Get-Version($PathToFile)
{
    return [System.Diagnostics.FileVersionInfo]::GetVersionInfo($PathToFile).ProductVersion
}

$PathToProject = Resolve-Path ($ScriptLocation + "\..\CodeMetricsReportProcessor\CodeMetricsReportProcessor.csproj")
$OutputPath = Prepare-OutputFolder

Update-ProjectOutputPath -PathToProject $PathToProject -OutputPath "$OutputPath\tools"
Call-MsBuild -Target "Build" -Parameters "Configuration=Release" -BuildFile $PathToProject
$Version = Get-Version -PathToFile "$OutputPath\tools\cmrp.exe"
Build-NugetPackage -Folder $OutputPath -Version $Version

Write-Host  "All Good"
