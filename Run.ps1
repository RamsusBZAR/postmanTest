﻿dotnet dev-certs https --clean
dotnet dev-certs https --export-path $env:USERPROFILE\.aspnet\https\aspnetapp.pfx --password localhost --trust
dotnet dev-certs https --trust

$project = "Server"

$password = [guid]::NewGuid().ToString()

Write-Host "Starting SQL Server"
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
$database = "SE_training"
$connectionString = "Server=localhost;Database=$database;User Id=sa;Password=$password;Trusted_Connection=False;Encrypt=False"

Write-Host "Configuring Connection String"
dotnet user-secrets init --project $project
dotnet user-secrets set "ConnectionStrings:SE_training" "$connectionString" --project $project


Write-Host "Starting App"
dotnet run --project $project