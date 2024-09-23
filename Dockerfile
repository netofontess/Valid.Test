# Etapa 1: Construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar somente arquivos relevantes para o build, ignorando os diretórios de teste através do .dockerignore
COPY . .

# Restaurar dependências (somente os projetos não excluídos pelo .dockerignore serão incluídos)
WORKDIR /app/Valid.Test.Api
RUN dotnet restore

# Publicar o projeto API, ignorando os projetos de teste
RUN dotnet publish ./Valid.Test.Api.csproj -c Release -o /out --no-restore

# Etapa 2: Configurar o ambiente de produção
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar os arquivos publicados da etapa de build
COPY --from=build /out .

# Expor a porta 80 para acesso HTTP
EXPOSE 80

# Definir o comando para rodar o aplicativo
ENTRYPOINT ["dotnet", "Valid.Test.Api.dll"]
