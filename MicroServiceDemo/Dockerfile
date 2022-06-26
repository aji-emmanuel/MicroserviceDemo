#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["MicroServiceDemo/MicroServiceDemo.csproj", "MicroServiceDemo/"]
RUN dotnet restore "MicroServiceDemo/MicroServiceDemo.csproj"
COPY . .
WORKDIR "/src/MicroServiceDemo"
RUN dotnet build "MicroServiceDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroServiceDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroServiceDemo.dll"]