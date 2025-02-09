FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ["Customer_Backend/Customer_Backend.csproj", "Customer_Backend/"]
COPY ["CustomerBackend.Application/CustomerBackend.Application.csproj", "CustomerBackend.Application/"]
COPY ["CustomerBackend.Domain/CustomerBackend.Domain.csproj", "CustomerBackend.Domain/"]
COPY ["CustomerBackend.Infrastructure/CustomerBackend.Infrastructure.csproj", "CustomerBackend.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "./Customer_Backend/Customer_Backend.csproj"

# Copy the entire solution
COPY . .

# Build the project
WORKDIR "/src/Customer_Backend"
RUN dotnet build "Customer_Backend.csproj" -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "Customer_Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 9000
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customer_Backend.dll"]