# Setup

## Integration tests

### User-secrets

User-secrets are stored via the `dotnet user-secrets` command and here as an example.  
_Set up user-secrets management:_
```ps
dotnet user-secrets init
```
_Define sa password:_
```ps
$sa_password = "P455w0rd!"
```
_Add secrets to the store:_
```ps
dotnet user-secrets set "ConnectionStrings:WeatherApi" "Server=localhost; Database=WeatherAPI_Tests; User Id=sa; Password=$sa_password; TrustServerCertificate=True"
```