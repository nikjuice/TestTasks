dotnet clean GoGreen.sln
dotnet build --force --no-incremental GoGreen.sln 
dotnet test .\GoGreenServiceTests\GoGreenServiceTests.csproj

start cmd.exe /c dotnet run --project .\GoGreenService\GoGreenService.csproj
start cmd.exe /c dotnet run --project .\GoGreenClient\GoGreenClient.csproj
start http://localhost:58868