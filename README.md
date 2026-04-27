# HackerBestStories

API ASP.NET Core para consumir e disponibilizar as melhores histórias do Hacker News.

## 🚀 Início do Desenvolvimento

### Pré-requisitos

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/)
- Git

### Configuração do Ambiente

1. **Clone o repositório**
   ```bash
   git clone <repository-url>
   cd HackerBestStories
   ```

2. **Instale as dependências**
   ```bash
   dotnet restore
   ```

3. **Execute o projeto**
   ```bash
   cd HackerBestStories.API
   dotnet run
   ```

   A API estará disponível em: `https://localhost:5000`

### Acesso à Documentação

A documentação interativa estará disponível em:
- **Scalar**: `https://localhost:5000/scalar/v1`

## 📁 Estrutura do Projeto

```
HackerBestStories/
├── HackerBestStories.API/     # Projeto principal da API
│   ├── Controllers/           # Endpoints da API
│   ├── Services/              # Lógica de negócio
│   ├── Models/                # Modelos de dados
│   └── Program.cs             # Configuração da aplicação
├── appsettings.json           # Configurações padrão
└── README.md                  # Este arquivo
```

## 🛠️ Desenvolvimento

### Adicionar Pacotes NuGet

```bash
dotnet add package <package-name>
```

### Compilar

```bash
dotnet build
```

### Executar Testes

```bash
dotnet test
```

### Publicar

```bash
dotnet publish -c Release
```

## 🐳 Docker

Para construir e executar em Docker:

```bash
docker build -t hackerbest-stories .
docker run -p 8080:8080 hackerbest-stories
```

## 📝 Notas

- O projeto usa .NET 10.0
- Habilitar Nullable Reference Types
- Implicit Using Statements habilitados
- API segura com User Secrets configurado

## 📄 Licença

MIT

## 👨‍💻 Contribuindo

1. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
2. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
3. Push para a branch (`git push origin feature/AmazingFeature`)
4. Abra um Pull Request
