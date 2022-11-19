$ProjectName = "IdleVillager"
Copy-Item "..\src\bin\Release\netstandard2.0\$ProjectName.dll" -Destination .
Compress-Archive -Path "$ProjectName.dll" -Force -DestinationPath ./$ProjectName.zip
