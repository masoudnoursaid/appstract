data "vsphere_resource_pool" "pool" {
    name = "/NL/host/${var.esxi_name}/Resources"
    datacenter_id = var.datacenter_id
}
data "vsphere_datastore" "datastore" {
  name          = var.datastore_name
  datacenter_id = var.datacenter_id
}

output "pool_id" {
  value = data.vsphere_resource_pool.pool.id  
}

output "datastore_id" {
  value = data.vsphere_datastore.datastore.id  
}

