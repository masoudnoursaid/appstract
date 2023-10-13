
# MinIO

## Bitnami Helm Chart

Popular applications, provided by Bitnami, ready to launch on Kubernetes using Kubernetes Helm.

<https://github.com/bitnami/charts/tree/main/bitnami/minio/#installing-the-chart>

```sh
helm repo add bitnami https://charts.bitnami.com/bitnami
helm repo update
```

## Install via Bitnami helm chart

Use values.yaml to define parameters. Set hostname in values.yaml (i.e. "auth.domain.com" ) then run :

```sh
helm install minio-release --namespace minio -f values.yaml oci://registry-1.docker.io/bitnamicharts/minio --create-namespace
```

## Upgrade

```sh
helm upgrade minio-release --namespace minio -f values.yaml oci://registry-1.docker.io/bitnamicharts/minio 
```

## Admin Console Login

<https://auth.domain.com/>

Get default admin user : 'user' password :

```sh
echo $(kubectl get secret --namespace minio minio-release -o jsonpath="{.data.admin-password}" | base64 --decode)
```

adminUser: user

adminPassword: ********

## Uninstall relaese

```sh
 helm uninstall minio-release -n minio
```

* remember to remove PVCs
