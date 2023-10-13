# Keycloak Terraform

Place your kubeconifg file in current path then :

```sh
terraform init
terraform plan
terraform apply
```

Terraform apply with yes :

```sh
terraform apply -auto-approve
```

Destroy :

```sh
terraform destroy
```

## NOTES

theme binary jar with configmap --from-file

```sh
kubectl create configmap keycloak-keywind-theme --from-file=../themes/keywind/out/keywind.jar --namespace keycloak
```
