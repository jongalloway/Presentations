param($installPath, $toolsPath, $package, $project)

$fieldTemplatesPath = $project.FullName + "\DynamicData\FieldTemplates"
$entityTemplatesPath = $project.FullName + "\DynamicData\EntityTemplates"

function Get-ProjectItem($project, $path) {
    $item = $project.ProjectItems
    $path.Split('\') | %{ 
        $item = $item.Item($_)
        if($item.ProjectItems) {
            $item = $item.ProjectItems
        }
    }
    $item
}

# Delete the .designer.cs files that were copied to the websites
if($project.Type -eq "Web Site"){
    Get-ChildItem $fieldTemplatesPath -exclude *.ascx.cs,*.ascx | %{ (Get-ProjectItem $project "DynamicData\FieldTemplates\$($_.Name)").Delete() }
    Get-ChildItem $entityTemplatesPath -exclude *.ascx.cs,*.ascx | %{ (Get-ProjectItem $project "DynamicData\EntityTemplates\$($_.Name)").Delete() }
    Get-ChildItem $fieldTemplatesPath -Filter *.ascx | %{         
        (($_ | Get-Content) -replace "CodeBehind", "CodeFile") | Set-Content $_.FullName
    }

    Get-ChildItem $entityTemplatesPath -Filter *.ascx | %{ 
        (($_ | Get-Content) -replace "CodeBehind", "CodeFile") | Set-Content $_.FullName
    }
}