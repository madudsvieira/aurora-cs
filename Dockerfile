FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source
 
COPY Aurora.sln .
 
COPY src/Api/*.csproj src/Api/
COPY src/Application/*.csproj src/Application/
COPY src/Domain/*.csproj src/Domain/
COPY src/Infrastructure/*.csproj src/Infrastructure/
 
RUN dotnet restore
 
COPY . .
 
RUN dotnet publish "src/Api/Api.csproj" -c Release -o /app/publish
 
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
 
RUN addgroup --system appgroup && adduser --system appuser --ingroup appgroup
 
COPY --from=build /app/publish .
 
RUN chown -R appuser:appgroup /app
USER appuser
 
EXPOSE 8080
 
ENV ASPNETCORE_URLS=http://+:8080
 
ENTRYPOINT ["dotnet", "Api.dll"]