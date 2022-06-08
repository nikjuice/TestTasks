dotnet build MoveIT.sln

dotnet tool install --global dotnet-ef
dotnet ef database update --project .\MoveIT\MoveITWeb.csproj

start cmd.exe /c dotnet run --project .\MoveITPriceService\MoveITPriceService.csproj
start cmd.exe /c dotnet run --project .\MoveIT\MoveITWeb.csproj --urls http://localhost:5003