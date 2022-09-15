#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LibraryManagement/LibraryManagement.csproj", "LibraryManagement/"]
COPY ["DataLibrary/DataLibrary.csproj", "DataLibrary/"]
COPY ["LibraryServices/LibraryServices.csproj", "LibraryServices/"]
COPY ["LibraryManagementWeb.Tests/LibraryManagementWeb.Tests.csproj", "LibraryManagementWeb.Tests/"]
RUN dotnet restore "LibraryManagement/LibraryManagement.csproj"
COPY . .
WORKDIR "/src/LibraryManagement"
RUN dotnet build "LibraryManagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LibraryManagement.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibraryManagement.dll"]