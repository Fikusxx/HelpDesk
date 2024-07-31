FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore --configfile /App/nuget.config
# Remove nuget.config from container
RUN rm /App/nuget.config
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .

EXPOSE 80
#If you’re using the Linux Container
# HEALTHCHECK CMD curl --fail http://localhost || exit 1

ENTRYPOINT ["dotnet", "Api.dll"]