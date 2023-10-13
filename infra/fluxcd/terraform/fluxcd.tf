terraform {
  required_providers {
    flux = {
      source = "fluxcd/flux"
      version = "1.1.0"
    }
  }
}


provider "flux" {
  kubernetes = {
    config_path = "~/.kube/config"
  }
  git = {
    url = "https://basketasia@dev.azure.com/basketasia/Appstract/_git/appstract"
    branch= "branchname"
    author_email = "EMAIL"
    http = {
      username = "EMAIL"
      password = "PAT"
    }
  }
}

resource "flux_bootstrap_git" "this" {
  path = "infra/fluxcd/clusters"
}
