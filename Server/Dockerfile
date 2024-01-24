# Sử dụng hình ảnh .NET SDK để xây dựng ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy file csproj và thực hiện phần restore
COPY Server/Server.csproj Server/
RUN dotnet restore Server/Server.csproj

# Copy toàn bộ dự án và thực hiện phần build
COPY . .
WORKDIR /src/Server
RUN dotnet build -c Release -o /app

# Bước runtime, sử dụng hình ảnh base runtime-only
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Server.dll"]