# Use the official Microsoft .NET SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Assuming the Docker build context is the parent directory of UserService
# Adjust paths based on the Docker build context being set to the parent directory
COPY ./SharedLibrary/*.csproj ./SharedLibrary/
COPY ./DTOLibrary/*.csproj ./DTOLibrary/
COPY ./ServiceLibrary/*.csproj ./ServiceLibrary/
COPY ./UserService/*.csproj ./UserService/

# Restore dependencies
WORKDIR /app/UserService
RUN dotnet restore

# Copy the project files
WORKDIR /app
COPY ./SharedLibrary ./SharedLibrary/
COPY ./DTOLibrary ./DTOLibrary/
COPY ./ServiceLibrary ./ServiceLibrary/
COPY ./UserService ./UserService/

# Publish the application
WORKDIR /app/UserService
RUN dotnet publish -c Release -o out

COPY ./UserService/aspnetapp.pfx /https/aspnetapp.pfx

ENV ASPNETCORE_Kestrel__Certificates__Default__Password="3RjjjjjG3"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path="/https/aspnetapp.pfx"

# Generate the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/UserService/out .
ENTRYPOINT ["dotnet", "UserService.dll"]


ENV ASPNETCORE_URLS=http://+:80;https://+:443
EXPOSE 80
EXPOSE 443
