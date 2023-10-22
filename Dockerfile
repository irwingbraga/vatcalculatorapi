FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/VatCalculator.Api/VatCalculator.Api.csproj", "VatCalculator.Api/"]
COPY ["src/VatCalculator.Application/VatCalculator.Application.csproj", "VatCalculator.Application/"]
COPY ["src/VatCalculator.Contracts/VatCalculator.Contracts.csproj", "VatCalculator.Contracts/"]
RUN dotnet restore "VatCalculator.Api/VatCalculator.Api.csproj"
COPY ./src/ .
WORKDIR "VatCalculator.Api"
RUN dotnet build "VatCalculator.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VatCalculator.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VatCalculator.Api.dll"]
