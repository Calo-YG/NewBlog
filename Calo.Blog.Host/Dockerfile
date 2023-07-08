#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Calo.Blog.Host/Calo.Blog.Host.csproj", "Calo.Blog.Host/"]
COPY ["Calo.Blog.Application/Calo.Blog.Application.csproj", "Calo.Blog.Application/"]
COPY ["Calo.Blog.Core/Calo.Blog.Domain.csproj", "Calo.Blog.Core/"]
COPY ["Calo.Blog.Common/Calo.Blog.Common.csproj", "Calo.Blog.Common/"]
COPY ["Y.Module/Y.Module.csproj", "Y.Module/"]
COPY ["Y.SqlsugarRepository/Y.SqlsugarRepository.csproj", "Y.SqlsugarRepository/"]
COPY ["Calo.Blog.EntityCore/Calo.Blog.EntityCore.csproj", "Calo.Blog.EntityCore/"]
RUN dotnet restore "Calo.Blog.Host/Calo.Blog.Host.csproj"
COPY . .
WORKDIR "/src/Calo.Blog.Host"
RUN dotnet build "Calo.Blog.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Calo.Blog.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Calo.Blog.Host.dll"]