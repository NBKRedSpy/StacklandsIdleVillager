$ProjectName = "IdleVillager"
Copy-Item "..\src\bin\Release\netstandard2.0\$ProjectName.dll" -Destination .
Copy-Item ..\README.md
Get-ChildItem -Exclude *.zip,*.ps1 | Compress-Archive -Force -DestinationPath ./$ProjectName.zip
