FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80

# ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["Permify.Proto.WebApi.csproj", "./"]
RUN dotnet restore "Permify.Proto.WebApi.csproj"
COPY . .
RUN dotnet publish "Permify.Proto.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Permify.Proto.WebApi.dll"]
