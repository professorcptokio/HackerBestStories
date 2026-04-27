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

## Benchmark

### Executar Benchmark

O projeto inclui benchmarks de performance usando **BenchmarkDotNet**. Para executar:

```bash
cd HackerBestStories.Benchmark
dotnet run -c Release
```

### Configuração do Benchmark

Os resultados são exportados automaticamente em:
- JSON: `BenchmarkDotNet.Artifacts/`
- Markdown: `BenchmarkDotNet.Artifacts/`
- HTML: `BenchmarkDotNet.Artifacts/`

### Resultado - Sem Cache

![Benchmark Results](withoutcache.png)
