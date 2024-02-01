# Sử dụng một hình ảnh base có sẵn .NET SDK để xây dựng ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy file csproj và thực hiện phần restore
COPY BlazorApp/BlazorApp.csproj BlazorApp/
RUN dotnet restore BlazorApp/BlazorApp.csproj

# Copy toàn bộ dự án và thực hiện phần build
COPY . .
WORKDIR /src/BlazorApp
RUN dotnet build -c Release -o /app

# Bước runtime, sử dụng cùng một hình ảnh base có sẵn .NET SDK để có phiên bản runtime tương ứng
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "BlazorApp.dll"]