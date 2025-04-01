# Usar la imagen base de .NET para ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Usar la imagen SDK de .NET para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiar el archivo .csproj
COPY ["ExcursionistaAPI/ExcursionistaAPI.csproj", "ExcursionistaAPI/"]  # Copiar el archivo .csproj

# Restaurar las dependencias
RUN dotnet restore "ExcursionistaAPI/ExcursionistaAPI.csproj"

# Copiar todo el contenido desde la carpeta ExcursionistaAPI al contenedor
COPY ExcursionistaAPI/. ./ExcursionistaAPI/  # Copiar todo desde ExcursionistaAPI

# Establecer el directorio de trabajo en ExcursionistaAPI
WORKDIR "/src/ExcursionistaAPI"

# Construir el proyecto
RUN dotnet build "ExcursionistaAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExcursionistaAPI.csproj" -c Release -o /app/publish  # Publicar la aplicación

FROM base AS final
WORKDIR /app

# Copiar los archivos publicados al contenedor final
COPY --from=publish /app/publish .

# Definir el comando de inicio
ENTRYPOINT ["dotnet", "ExcursionistaAPI.dll"]
