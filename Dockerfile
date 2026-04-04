# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copy project file
COPY ["EFManager.csproj", "./"]

# Restore dependencies
RUN dotnet restore "EFManager.csproj"

# Copy source code
COPY . .

# Build the application
RUN dotnet build "EFManager.csproj" -c Release -o /app/build

# Publish the application
RUN dotnet publish "EFManager.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/runtime:10.0

WORKDIR /app

# Copy published files from build stage
COPY --from=build /app/publish .

# Set the entrypoint
ENTRYPOINT ["dotnet", "EFManager.dll"]
