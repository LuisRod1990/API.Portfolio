# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar la solución
COPY *.sln ./

# Copiar los proyectos (ajusta si tu carpeta es distinta)
COPY Api_Portfolio/*.csproj Api_Portfolio/

# Restaurar dependencias
RUN dotnet restore

# Copiar todo el código
COPY . .

# Compilar y publicar el proyecto principal
WORKDIR /src/Api_Portfolio
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Configurar ASP.NET Core para Cloud Run
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Api_Portfolio.dll"]
