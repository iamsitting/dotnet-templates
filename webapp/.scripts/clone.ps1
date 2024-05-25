# Define the old and new names
$oldName = "TemplateProject"
$newName = "NewCloneProject"

# Define the source and target directories
$sourceDir = ".\"
$targetDir = "..\" + $newName

# Define if target directory already exists, if so, delete it
if(Test-Path -Path $targetDir) {
    Write-Host "$targetDir already exists, deleting its contents..."
    Remove-Item -Path $targetDir -Recurse -Force
}

# Clone the local repository
git clone $sourceDir $targetDir
Write-Host "Cloned successfully"

# Remove the .git directory
Remove-Item -Path "$targetDir\.git" -Recurse -Force
Write-Host "Removed .git/"

# Get all the files in the target directory
$files = Get-ChildItem -Path $targetDir -Recurse

# Rename files and replace text within files
foreach ($file in $files) {
    if($file.PSIsContainer) {
        continue
    }
    Write-Host "Touching $file"
    # Replace text within the file
    (Get-Content $file.PSPath) |
    Foreach-Object { $_ -replace $oldName, $newName } |
    Set-Content $file.PSPath

    # Rename the file if it contains the old name
    if ($file.Name -like "*$oldName*") {
        Rename-Item -Path $file.PSPath -NewName $file.Name.Replace($oldName, $newName)
    }
}

# Rename directories
$dirs = Get-ChildItem -Path $targetDir -Directory -Recurse | Sort-Object -Property FullName -Descending
foreach ($dir in $dirs) {
    if ($dir.Name -like "*$oldName*") {
        Rename-Item -Path $dir.PSPath -NewName $dir.Name.Replace($oldName, $newName)
    }
}