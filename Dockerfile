FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Y.Blog.Host/Y.Blog.Host.csproj", "Y.Blog.Host/"]
COPY ["Calo.Blog.Application/Calo.Blog.Application.csproj", "Calo.Blog.Application/"]
COPY ["Calo.Blog.Core/Calo.Blog.Domain.csproj", "Calo.Blog.Core/"]
COPY ["Calo.Blog.Common/Calo.Blog.Common.csproj", "Calo.Blog.Common/"]
COPY ["Y.Module/Y.Module.csproj", "Y.Module/"]
COPY ["Y.SqlsugarRepository/Y.SqlsugarRepository.csproj", "Y.SqlsugarRepository/"]
COPY ["Calo.Blog.EntityCore/Calo.Blog.EntityCore.csproj", "Calo.Blog.EntityCore/"]
COPY ["FreeInterface/FreeInterface.csproj", "FreeInterface/"]
RUN dotnet restore "Y.Blog.Host/Y.Blog.Host.csproj"
COPY . .
WORKDIR "/src/Y.Blog.Host"
RUN dotnet build "Y.Blog.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Y.Blog.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Y.Blog.Host.dll"]