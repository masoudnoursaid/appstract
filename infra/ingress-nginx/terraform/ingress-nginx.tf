terraform {
  required_providers {
    helm = {
      source = "hashicorp/helm"
      version = "2.11.0"
    }
  }
}

provider "helm" {
  kubernetes {
    config_path = "~/.kube/config"
  }
}
resource "helm_release" "nginx-ingress" {
  name       = "nginx-ingress"
  namespace  = "nginx-ingress"
  repository = "https://charts.bitnami.com/bitnami"
  chart      = "nginx-ingress-controller"
  version = "0.18.1"

  set {
    name  = "controller.hostNetwork"
    value = "true"
  }
}