# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiar el archivo .csproj y restaurar las dependencias
COPY ./ExcursionistaAPI/ExcursionistaAPI.csproj ./ExcursionistaAPI/
RUN dotnet restore "ExcursionistaAPI/ExcursionistaAPI.csproj"

# Copiar el resto de los archivos del proyecto y construir la aplicación
COPY . .
WORKDIR /src/ExcursionistaAPI
RUN dotnet publish -c Release -o /app/publish

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

# Copiar los archivos publicados desde la etapa de compilación
COPY --from=build /app/publish .

# Exponer el puerto en el que correrá la aplicación
EXPOSE 5000
EXPOSE 5001

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "ExcursionistaAPI.dll"]
