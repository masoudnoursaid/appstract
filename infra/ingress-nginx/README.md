## Install NGINX Ingress Controller with Helm
First create the namespace you want to deploy nginx in:
```console
 kubectl create ns nginx-ingress
```
Now you can install NGINX Ingress Controller with helm with the following command:
```console
 helm install nginx-ingress -n nginx-ingress oci://ghcr.io/nginxinc/charts/nginx-ingress --set controller.hostNetwork=true --version 0.18.1
```
## Install NGINX Ingress Controller with Terraform
Go to the terraform directory and run the following commands:
```console
cd terraform
terraform init
terraform plan -out main.tfplan
terraform apply main.tfplan
```