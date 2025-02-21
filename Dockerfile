FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

RUN apt-get update && apt-get install -y postgresql-client

COPY ./*.sln ./
COPY src/FinancialManagement.Api/FinancialManagement.Api.csproj ./Api/
COPY ["src/FinancialManagement.Api/appsettings*.json", "./Api/"]
COPY src/FinancialManagement.Application/FinancialManagement.Application.csproj ./Application/
COPY src/FinancialManagement.Infrastructure/FinancialManagement.Infrastructure.csproj ./Infrastructure/
COPY src/FinancialManagement.Domain/FinancialManagement.Domain.csproj ./Domain/
COPY src/FinancialManagement.Identity/FinancialManagement.Identity.csproj ./Identity/
COPY db/create_tables_v1.sql /app/db/

COPY . ./
RUN dotnet restore
RUN dotnet publish -c release -o /app/out 

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./
EXPOSE 8080
ENTRYPOINT ["dotnet", "FinancialManagement.Api.dll"]


