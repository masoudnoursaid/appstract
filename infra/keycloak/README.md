
# Keycloak

## Bitnami Helm Chart

Popular applications, provided by Bitnami, ready to launch on Kubernetes using Kubernetes Helm.

```sh
helm repo add bitnami https://charts.bitnami.com/bitnami
helm repo update
```

## Install via Bitnami helm chart

Use values.yaml to define parameters. Set hostname in values.yaml (i.e. "auth.domain.com" ) then run :

```sh
helm install keycloak-release --namespace keycloak -f values.yaml oci://registry-1.docker.io/bitnamicharts/keycloak --create-namespace
```

## Install via local chart folder

Use values.yaml to define parameters. Set hostname in values.yaml (i.e. "auth.domain.com" ) then run :

```sh
helm install keycloak-release --namespace keycloak -f values.yaml ./chart --create-namespace
```

## Upgrade

```sh
helm upgrade keycloak-release --namespace keycloak -f values.yaml oci://registry-1.docker.io/bitnamicharts/keycloak 
```

## Upgrade via local chart folder

```sh
helm upgrade keycloak-release --namespace keycloak -f values.yaml ./chart

## Admin Console Login

<https://auth.domain.com/>

Get default admin user : 'user' password :

```sh
echo $(kubectl get secret --namespace keycloak keycloak-release -o jsonpath="{.data.admin-password}" | base64 --decode)
```

adminUser: user

adminPassword: ********

## Uninstall relaese

```sh
 helm uninstall keycloak-release -n keycloak
```

* remember to remove PVCs
