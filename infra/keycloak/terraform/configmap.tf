provider "kubernetes" {
  config_path = "./config-appstract"
}


resource "kubernetes_config_map" "keycloak" {
  metadata {
    name      = "keycloak-keywind-theme"
    namespace = "keycloak"
  }

  # data = {
  #   api_host             = "myhost:443"
  #   db_host              = "dbhost:5432"
  #   "my_config_file.yml" = "${file("${path.module}/my_config_file.yml")}"
  # }

  binary_data = {
    "keywind.jar" = filebase64("../themes/keywind/out/keywind.jar")
  }
}
