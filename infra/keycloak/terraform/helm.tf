provider "helm" {
  kubernetes {
    config_path = "./config-appstract"
  }

  # localhost registry with password protection
  #   registry {
  #     url = "oci://localhost:5000"
  #     username = "username"
  #     password = "password"
  #   }

  # private registry
  #   registry {
  #     url = "oci://private.registry"
  #     username = "username"
  #     password = "password"
  #   }
}

resource "helm_release" "keycloak" {
  name             = "keycloak-release"
  repository       = "../"
  chart            = "chart"
  create_namespace = true
  namespace        = "keycloak"
  values = [
    file("../chart/values.yaml")
  ]
  # templatefile("../values.yaml",
  set {
    name  = "global.storageClass"
    value = "local-path"
  }

  # set {
  #   name  = "theme"
  #   # value = filebase64("../themes/keywind/out/keywind.jar")
  #   value = filebase64("../themes/keywind/out/keywind.jar")
  # }

}


# provider "helm" {
#   kubernetes = {
#     config_path = "./config-Shahariyar_Bazaei" # Path to your kubeconfig file
#   }
# }

# resource "helm_release" "keycloak" {
#   name       = "keycloak-release"
#   repository = "https://charts.bitnami.com/bitnami" # URL to your Helm chart repository
#   chart      = "bitnami"                   # Name of the Helm chart
#   version    = "1.0.0"                      # Version of the Helm chart
#   values = [templatefile("../values.yaml",
#     {
#       key   = "key1"
#       value = "value1"
#     },
#   )]
#   # values = [
#   #   # List of values to customize the Helm chart (optional)
#   #   {
#   #     key   = "key1"
#   #     value = "value1"
#   #   },
#   #   {
#   #     key   = "key2"
#   #     value = "value2"
#   #   },
#   # ]
# }
