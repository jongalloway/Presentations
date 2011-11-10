[T4Scaffolding.Scaffolder(Description = "Enter a description of Monkey here")][CmdletBinding()]
param(        
    [string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

$outputPath = "ExampleOutput"  # The filename extension will be added based on the template's <#@ Output Extension="..." #> directive
$namespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value

Add-ProjectItemViaTemplate $outputPath -Template MonkeyTemplate `
	-Model @{ Namespace = $namespace; ExampleValue = "Hello, everyone!!!!" } `
	-SuccessMessage "Added Monkey output at {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force