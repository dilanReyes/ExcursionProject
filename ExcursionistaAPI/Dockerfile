# Usamos la imagen base de .NET SDK para compilar el proyecto
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Establecer el directorio de trabajo
WORKDIR /src

# Copiar el archivo .csproj del proyecto ExcursionistaAPI
COPY ["ExcursionistaAPI.csproj", "./"]

# Restaurar las dependencias del proyecto
RUN dotnet restore "ExcursionistaAPI.csproj"

# Copiar el resto de los archivos del proyecto
COPY ./ExcursionistaAPI/. ./ExcursionistaAPI/

# Establecer el directorio de trabajo en ExcursionistaAPI
WORKDIR "/src/ExcursionistaAPI"

# Compilar el proyecto
RUN dotnet build "ExcursionistaAPI.csproj" -c Release -o /app/build

# Publicar la aplicación
RUN dotnet publish "ExcursionistaAPI.csproj" -c Release -o /app/publish

# Usar la imagen de runtime de .NET para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
COPY --from=build /app/publish .

# Exponer el puerto 80 para la aplicación
EXPOSE 80

# Establecer el comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "ExcursionistaAPI.dll"]
