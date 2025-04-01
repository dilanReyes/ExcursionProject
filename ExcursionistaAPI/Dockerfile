# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Directorio de trabajo en el contenedor
WORKDIR /src

# Copiar el archivo .csproj y restaurar dependencias
COPY ["ExcursionistaAPI/ExcursionistaAPI.csproj", "ExcursionistaAPI/"]
RUN dotnet restore "ExcursionistaAPI/ExcursionistaAPI.csproj"

# Copiar el resto de los archivos de la aplicación
COPY ExcursionistaAPI/. ./ExcursionistaAPI/

# Establecer el directorio de trabajo en ExcursionistaAPI
WORKDIR "/src/ExcursionistaAPI"

# Construir el proyecto
RUN dotnet build "ExcursionistaAPI.csproj" -c Release -o /app/build

# Publicar el proyecto para producción
RUN dotnet publish "ExcursionistaAPI.csproj" -c Release -o /app/publish

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

# Establecer el directorio de trabajo
WORKDIR /app

# Copiar los archivos publicados desde la etapa de construcción
COPY --from=build /app/publish .

# Exponer el puerto 80 para que la aplicación sea accesible
EXPOSE 80

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "ExcursionistaAPI.dll"]
