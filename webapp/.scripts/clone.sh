#!/bin/bash

# Define the old and new names
oldName="TemplateProject"
newName="NewCloneProject"

# Define the source and target directories
sourceDir="./"
targetDir="../$newName"

# Check if target directory already exists, if so, delete it
if [ -d "$targetDir" ]; then
    echo "$targetDir already exists, deleting its contents..."
    rm -rf "$targetDir"
fi

# Clone the local repository
git clone "$sourceDir" "$targetDir"
echo "Cloned successfully"

# Remove the .git directory
rm -rf "$targetDir/.git"
echo "Removed .git/"

# Get all the files in the target directory and rename files and replace text within files
find "$targetDir" -type f | while read file; do
    echo "Touching $file"
    # Replace text within the file
    sed -i "s/$oldName/$newName/g" "$file"

    # Rename the file if it contains the old name
    if [[ $file == *"$oldName"* ]]; then
        mv "$file" "${file//$oldName/$newName}"
    fi
done

# Rename directories
find "$targetDir" -type d | sort -r | while read dir; do
    if [[ $dir == *"$oldName"* ]]; then
        mv "$dir" "${dir//$oldName/$newName}"
    fi
done
