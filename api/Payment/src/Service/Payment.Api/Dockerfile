FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Service/Payment.Api/Payment.Api.csproj", "src/Service/Payment.Api/"]
COPY ["src/Service/Web.Common/Web.Common.csproj", "src/Service/Web.Common/"]
COPY ["src/Service/Infrastructure/Infrastructure.csproj", "src/Service/Infrastructure/"]
COPY ["src/Service/Application/Application.csproj", "src/Service/Application/"]
COPY ["src/Service/Domain/Domain.csproj", "src/Service/Domain/"]
COPY ["src/Service/ErrorHandling/ErrorHandling.csproj", "src/Service/ErrorHandling/"]
RUN dotnet restore "src/Service/Payment.Api/Payment.Api.csproj"
COPY . .
WORKDIR "/src/src/Service/Payment.Api"
RUN dotnet build "Payment.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Payment.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY *.pfx .
ENTRYPOINT ["dotnet", "Payment.Api.dll"]
