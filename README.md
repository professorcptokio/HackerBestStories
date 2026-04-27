# HackerBestStories

API ASP.NET Core para consumir e disponibilizar as melhores histórias do Hacker News.

## Pré-requisitos

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)

## Execução

### Local
```bash
dotnet restore
cd HackerBestStories.API
dotnet run
```
API disponível em: `https://localhost:5000`  
Documentação: `https://localhost:5000/scalar/v1`

### Docker
```bash
docker build -t hackerbest-stories .
docker run -p 8080:8080 hackerbest-stories
```

### Compilar
```bash
dotnet build
```

### Testes
```bash
dotnet test
```

### Publicar
```bash
dotnet publish -c Release
```
