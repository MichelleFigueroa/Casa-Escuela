#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CasaEscuela.AppWebMVC/CasaEscuela.AppWebMVC.csproj", "CasaEscuela.AppWebMVC/"]
COPY ["CasaEscuela.BL.DTOs/CasaEscuela.BL.DTOs.csproj", "CasaEscuela.BL.DTOs/"]
COPY ["CasaEscuela.EN/CasaEscuela.EN.csproj", "CasaEscuela.EN/"]
COPY ["CasaEscuela.BL.Interfaces/CasaEscuela.BL.Interfaces.csproj", "CasaEscuela.BL.Interfaces/"]
COPY ["CasaEscuela.IoC/CasaEscuela.IoC.csproj", "CasaEscuela.IoC/"]
COPY ["CasaEscuela.DAL/CasaEscuela.DAL.csproj", "CasaEscuela.DAL/"]
COPY ["CasaEscuela.FESV/CasaEscuela.FESV.csproj", "CasaEscuela.FESV/"]
COPY ["CasaEscuela.Mock/CasaEscuela.Mock.csproj", "CasaEscuela.Mock/"]
RUN dotnet restore "./CasaEscuela.AppWebMVC/CasaEscuela.AppWebMVC.csproj"
COPY . .
WORKDIR "/src/CasaEscuela.AppWebMVC"
RUN dotnet build "./CasaEscuela.AppWebMVC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CasaEscuela.AppWebMVC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CasaEscuela.AppWebMVC.dll"]