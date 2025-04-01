# Usar la imagen base de .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

# Copiar el archivo .csproj y restaurar las dependencias
COPY ./ExcursionistaAPI/ExcursionistaAPI.csproj ./ExcursionistaAPI/
RUN dotnet restore "ExcursionistaAPI/ExcursionistaAPI.csproj"

# Copiar el resto de los archivos del proyecto y construir la aplicación
COPY ./ExcursionistaAPI/. ./ExcursionistaAPI/
WORKDIR /src/ExcursionistaAPI
RUN dotnet build "ExcursionistaAPI.csproj" -c Release -o /app/build

# Publicar la aplicación
RUN dotnet publish "ExcursionistaAPI.csproj" -c Release -o /app/publish

# Usar la imagen base de ASP.NET para correr la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app
COPY --from=build /app/publish .

# Exponer el puerto de la aplicación
EXPOSE 80
ENTRYPOINT ["dotnet", "ExcursionistaAPI.dll"]
