# Usar la imagen base de .NET para ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Usar la imagen SDK de .NET para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ExcursionistaAPI/ExcursionistaAPI.csproj", "ExcursionistaAPI/"]  # Copiar el archivo .csproj
RUN dotnet restore "ExcursionistaAPI/ExcursionistaAPI.csproj"  # Restaurar dependencias
COPY . .  # Copiar el resto de los archivos
WORKDIR "/src/ExcursionistaAPI"
RUN dotnet build "ExcursionistaAPI.csproj" -c Release -o /app/build  # Construir el proyecto

FROM build AS publish
RUN dotnet publish "ExcursionistaAPI.csproj" -c Release -o /app/publish  # Publicar la aplicación

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .  # Copiar los archivos publicados
ENTRYPOINT ["dotnet", "ExcursionistaAPI.dll"]  # Iniciar la aplicación
