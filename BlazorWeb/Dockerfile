# Sử dụng một hình ảnh base có sẵn .NET SDK để xây dựng ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy file csproj và thực hiện phần restore
COPY BlazorWeb/BlazorWeb.csproj BlazorWeb/
RUN dotnet restore BlazorWeb/BlazorWeb.csproj

# Copy toàn bộ dự án và thực hiện phần build
COPY . .
WORKDIR /src/BlazorWeb
RUN dotnet build -c Release -o /app

# Bước runtime, sử dụng hình ảnh base runtime-only
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "BlazorWeb.dll"]