
## Run Project With Mock Data
For using mock data, you should implement `IUltraToneService` with `FakeUltraTone` service. To use the mock data service, you can set the following value in the `appsettings.json` file:

- "UseFakeBackend" : "true"

## Run Project With Real Data
To use the real data, you should set the backend server URL as below:

For dev environment:
- "BackendUrl" : "https://endpoints-dev.ultratone.dev/"

For test production environment:
- "BackendUrl" : "https://endpoints-pt.ultratone.dev/"

## GitLab Registry Configuration

To use SDK nugets from Gitlab Registry first you need generate a Personal Access Token

1. Visit gitlab-registry.maxtld.com
2. Login to your account
3. At top right corner click on the `Profile` and choose `Preferences`
4. On the left menu click on `Access Tokens`
5. In the generate form type a `Token Name`
6. Select a longer expire date (preferred amount 1 year or more)
7. Select `Read Registry` from the list
8. Click on `Create Personal access token`
9. Copy the code and run below command (replace username and token)

```
nuget source Add -Name "gitlab" -Source "https://gitlab-registry.maxtld.com/api/v4/projects/12/packages/nuget/index.json"
```

10. then type below command
```bash
nuget install UltraTone.Backend.ClientSdk.Customer.Mobile -Source "gitlab"

```

## Publish Msix for windows

1. Goto folder src/UltraTone.Desktop
2. Run below command
```
cd ./src/UltraTone.Desktop
dotnet publish -f net7.0-windows10.0.19041.0 -c Release -p:RuntimeIdentifierOverride=win10-x64
```