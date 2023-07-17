#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["POC_Auth_Consumer/POC_Auth_Consumer.csproj", "POC_Auth_Consumer/"]
COPY ["POC_Shared/POC_Shared.csproj", "POC_Shared/"]
RUN dotnet restore "POC_Auth_Consumer/POC_Auth_Consumer.csproj"
COPY . .
WORKDIR "/src/POC_Auth_Consumer"
RUN dotnet build "POC_Auth_Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "POC_Auth_Consumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "POC_Auth_Consumer.dll"]