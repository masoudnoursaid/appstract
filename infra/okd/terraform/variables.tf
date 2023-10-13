variable "vsphere_server" {
  type        = string
  description = "This is the vSphere server for the environment."
}


variable "vsphere_user" {
  type        = string
  description = "vSphere server user for the environment."
}

variable "vsphere_password" {
  type        = string
  description = "vSphere server password"
}

variable "network1" {
  type        = string
  description = "Port group to which vms connect"
}

variable "network2" {
  type        = string
  description = "Port group to which vms connect"
}

variable "template" {
  type        = string
  description = "Fedora core os template"
}

variable "bootstrap_esxi" {
  type        = string
  description = "ESXI host on which bootstrap node will create."
}

variable "cluster_name" {
  type        = string
  description = "Name of okd cluster for naming vms."
}

variable "bootstrap_mac" {
  type        = string
  description = "Static Mac address of bootstrap vm"
}

variable "bootstrap_datastore" {
  type        = string
  description = "Datastore on which bootstrap node will create."
}

variable "bootstrap_ignition_url" {
  type        = string
  description = "Url of bootstrap ignition file"
}

variable "master_ignition_url" {
  type        = string
  description = "Url of master ignition file"
}
variable "worker_ignition_url" {
  type        = string
  description = "Url of worker ignition file"
}
variable "master_macs" {
  type        = list
  default = []
  description = "Static Mac addresses of master vm"
}

variable "master_esxis" {
  type        = list
  default = []
  description = "ESXI hosts on which master node will create."
}

variable "master_datastores" {
  type        = list
  default = []
  description = "ESXI hosts on which master node will create."
}
variable "worker_macs1" {
  type        = list
  default = []
  description = "Static Mac addresses of nic 1 of worker vm"
}

variable "worker_macs2" {
  type        = list
  default = []
  description = "Static Mac addresses of nic 2 of worker vm"
}

variable "worker_esxis" {
  type        = list
  default = []
  description = "ESXI hosts on which master node will create."
}

variable "worker_datastores" {
  type        = list
  default = []
  description = "ESXI hosts on which master node will create."
}