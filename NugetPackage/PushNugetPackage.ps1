$ErrorActionPreference = 'Stop';
$ScriptLocation = (Split-Path -Path $MyInvocation.MyCommand.Path -Parent);


function Assert-LastExternalCallWasOK([Parameter(Mandatory=$true)]$Message)
{
    if ($LASTEXITCODE -ne $nil -And $LASTEXITCODE -ne 0)
    {
        throw "$Message Error code: $LASTEXITCODE";
    }
}

function Get-PathToNugetPackage($OutputPath)
{
    $Packages = @(Get-ChildItem -Path $OutputPath -Filter "*.nupkg")
    if ($Packages.Count -gt 1) { throw "Found more than one package" }
    if ($Packages.Count -lt 1) { throw "Found no packages" }
    
    $Package = $Packages[0]        
    return $Package.FullName
}

$PathToNugetPackage = Get-PathToNugetPackage -OutputPath "$ScriptLocation\..\Output"

& ..\tools\Nuget\Nuget.exe push $PathToNugetPackage
Assert-LastExternalCallWasOK -Message "Execution of nuget.exe  failed. Check log for more details";         


Write-Host  "All Good"
