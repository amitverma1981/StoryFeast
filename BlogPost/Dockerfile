#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BulkyBook/BulkyBook.csproj", "BulkyBook/"]
COPY ["BulkyBook.Utility/BulkyBook.Utility.csproj", "BulkyBook.Utility/"]
COPY ["BulkyBook.DataAccess/BulkyBook.DataAccess.csproj", "BulkyBook.DataAccess/"]
COPY ["BulkyBook.Models/BulkyBook.Models.csproj", "BulkyBook.Models/"]
RUN dotnet restore "BulkyBook/BulkyBook.csproj"
COPY . .
WORKDIR "/src/BulkyBook"
RUN dotnet build "BulkyBook.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BulkyBook.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BulkyBook.dll"]