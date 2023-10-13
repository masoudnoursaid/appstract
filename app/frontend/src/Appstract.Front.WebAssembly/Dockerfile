FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

ARG GITLAB_NUGET_USERNAME
ARG GITLAB_NUGET_PASSWORD

ENV GITLAB_NUGET_USERNAME=$GITLAB_NUGET_USERNAME
ENV GITLAB_NUGET_PASSWORD=$GITLAB_NUGET_PASSWORD

COPY ["nuget.config", "."]
COPY ["src/UltraTone.Account.Client/UltraTone.Account.Client.csproj", "src/UltraTone.Account.Client/"]
COPY ["src/UltraTone.SharedUI/UltraTone.SharedUI.csproj", "src/UltraTone.SharedUI/"]
COPY ["src/UltraTone.Account.Infrastructure/UltraTone.Account.Infrastructure.csproj", "src/UltraTone.Account.Infrastructure/"]
COPY ["src/UltraTone.Account.Application/UltraTone.Account.Application.csproj", "src/UltraTone.Account.Application/"]
COPY ["src/UltraTone.Account.Domain/UltraTone.Account.Domain.csproj", "src/UltraTone.Account.Domain/"]
RUN dotnet restore "src/UltraTone.Account.Client/UltraTone.Account.Client.csproj"
COPY . .

WORKDIR "/src/src/UltraTone.Account.Client"
RUN dotnet build "UltraTone.Account.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UltraTone.Account.Client.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
EXPOSE 443
