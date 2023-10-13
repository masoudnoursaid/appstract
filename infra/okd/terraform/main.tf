provider "vsphere" {
user                   = var.vsphere_user
  password             = var.vsphere_password
  vsphere_server       = var.vsphere_server
  allow_unverified_ssl = true
}

data "vsphere_datacenter" "dc" {
  name = "NL"
}


data "vsphere_network" "net1" {
  name          = var.network1 
  datacenter_id = data.vsphere_datacenter.dc.id
}

data "vsphere_network" "net2" {
  name          = var.network2 
  datacenter_id = data.vsphere_datacenter.dc.id
}

data "vsphere_virtual_machine" "template" {
  name          = var.template
  datacenter_id = data.vsphere_datacenter.dc.id
}

module "bootstrap_vm"{
 source = "./Modules"
 esxi_name = var.bootstrap_esxi
 datacenter_id = data.vsphere_datacenter.dc.id
 datastore_name = var.bootstrap_datastore
}

module "master1_vm"{
 source = "./Modules"
 esxi_name = var.master_esxis[0]
 datacenter_id = data.vsphere_datacenter.dc.id
 datastore_name = var.master_datastores[0]
}

module "master2_vm"{
 source = "./Modules"
 esxi_name = var.master_esxis[1]
 datacenter_id = data.vsphere_datacenter.dc.id
 datastore_name = var.master_datastores[1]
}

module "master3_vm"{
 source = "./Modules"
 esxi_name = var.master_esxis[2]
 datacenter_id = data.vsphere_datacenter.dc.id
 datastore_name = var.master_datastores[2]
}

module "worker1_vm"{
 source = "./Modules"
 esxi_name = var.worker_esxis[0]
 datacenter_id = data.vsphere_datacenter.dc.id
 datastore_name = var.worker_datastores[0]
}

module "worker2_vm"{
 source = "./Modules"
 esxi_name = var.worker_esxis[1]
 datacenter_id = data.vsphere_datacenter.dc.id
 datastore_name = var.worker_datastores[1]
}

resource "vsphere_virtual_machine" "bootstrap" {
    name = "okd-${var.cluster_name}-bootstrap"
    //resource_pool_id     = "${data.vsphere_resource_pool.pool63.id}"
    resource_pool_id     = module.bootstrap_vm.pool_id
    datastore_id         = module.bootstrap_vm.datastore_id
    num_cpus             = "4"
    num_cores_per_socket = "4"
    memory               = "8192"
    guest_id             = data.vsphere_virtual_machine.template.guest_id

    enable_disk_uuid     = "true"

    wait_for_guest_net_timeout  = "0"
    wait_for_guest_net_routable = "false"

    network_interface {
      network_id = data.vsphere_network.net1.id
      use_static_mac = "true"
      mac_address = var.bootstrap_mac
    }

    disk {
      label            = "disk0"
      size             = "120"
      thin_provisioned = data.vsphere_virtual_machine.template.disks.0.thin_provisioned
    }

    clone {
      template_uuid = data.vsphere_virtual_machine.template.id
    }

    vapp {
      properties = {
        "guestinfo.ignition.config.data"          = "${base64encode(templatefile("append_node.tpl", { ignition_url = var.bootstrap_ignition_url }))}",
        "guestinfo.ignition.config.data.encoding" = "base64"
      }
    }
}

resource "vsphere_virtual_machine" "master1" {
    name  = "okd-${var.cluster_name}-master1"
    resource_pool_id     = module.master1_vm.pool_id
    datastore_id         = module.master1_vm.datastore_id
    num_cpus             = "4"
    num_cores_per_socket = "4"
    memory               = "16384"
    guest_id             = data.vsphere_virtual_machine.template.guest_id

    enable_disk_uuid     = "true"

    wait_for_guest_net_timeout  = "0"
    wait_for_guest_net_routable = "false"

    network_interface {
      network_id = data.vsphere_network.net1.id
      use_static_mac = "true"
      mac_address = var.master_macs[0]
    }

    disk {
      label            = "disk0"
      size             = "120"
      thin_provisioned = data.vsphere_virtual_machine.template.disks.0.thin_provisioned
    }

    clone {
      template_uuid = data.vsphere_virtual_machine.template.id
    }

    vapp {
      properties = {
        "guestinfo.ignition.config.data"          = "${base64encode(templatefile("append_node.tpl", { ignition_url = var.master_ignition_url }))}",
        "guestinfo.ignition.config.data.encoding" = "base64"
      }
    }
}

resource "vsphere_virtual_machine" "master2" {
    name  = "okd-${var.cluster_name}-master2"
    resource_pool_id     = module.master2_vm.pool_id
    datastore_id         = module.master2_vm.datastore_id
    num_cpus             = "4"
    num_cores_per_socket = "4"
    memory               = "16384"
    guest_id             = data.vsphere_virtual_machine.template.guest_id

    enable_disk_uuid     = "true"

    wait_for_guest_net_timeout  = "0"
    wait_for_guest_net_routable = "false"

    network_interface {
      network_id = data.vsphere_network.net1.id
      use_static_mac = "true"
      mac_address = var.master_macs[1]
    }

    disk {
      label            = "disk0"
      size             = "120"
      thin_provisioned = data.vsphere_virtual_machine.template.disks.0.thin_provisioned
    }

    clone {
      template_uuid = data.vsphere_virtual_machine.template.id
    }

    vapp {
      properties = {
        "guestinfo.ignition.config.data"          = "${base64encode(templatefile("append_node.tpl", { ignition_url = var.master_ignition_url }))}",
        "guestinfo.ignition.config.data.encoding" = "base64"
      }
    }
}

resource "vsphere_virtual_machine" "master3" {
    name  = "okd-${var.cluster_name}-master3"
    resource_pool_id     = module.master3_vm.pool_id
    datastore_id         = module.master3_vm.datastore_id
    num_cpus             = "4"
    num_cores_per_socket = "4"
    memory               = "16384"
    guest_id             = data.vsphere_virtual_machine.template.guest_id
    enable_disk_uuid     = "true"

    wait_for_guest_net_timeout  = "0"
    wait_for_guest_net_routable = "false"

    network_interface {
      network_id = data.vsphere_network.net1.id
      use_static_mac = "true"
      mac_address = var.master_macs[2]
    }

    disk {
      label            = "disk0"
      size             = "120"
      thin_provisioned = data.vsphere_virtual_machine.template.disks.0.thin_provisioned
    }

    clone {
      template_uuid = data.vsphere_virtual_machine.template.id
    }

    vapp {
      properties = {
        "guestinfo.ignition.config.data"          = "${base64encode(templatefile("append_node.tpl", { ignition_url = var.master_ignition_url }))}",
        "guestinfo.ignition.config.data.encoding" = "base64"
      }
    }
}

resource "vsphere_virtual_machine" "worker1" {
    name  = "okd-${var.cluster_name}-worker1"
    resource_pool_id     = module.worker1_vm.pool_id
    datastore_id         = module.worker1_vm.datastore_id
    num_cpus             = "4"
    num_cores_per_socket = "4"
    memory               = "16384"
    guest_id             = data.vsphere_virtual_machine.template.guest_id
    enable_disk_uuid     = "true"

    wait_for_guest_net_timeout  = "0"
    wait_for_guest_net_routable = "false"

    network_interface {
      network_id = data.vsphere_network.net1.id
      use_static_mac = "true"
      mac_address = var.worker_macs1[0]
    }

    network_interface {
      network_id = data.vsphere_network.net2.id
      use_static_mac = "true"
      mac_address = var.worker_macs2[0]
    }

    disk {
      label            = "disk0"
      size             = "120"
      thin_provisioned = data.vsphere_virtual_machine.template.disks.0.thin_provisioned
    }

    clone {
      template_uuid = data.vsphere_virtual_machine.template.id
    }

    vapp {
      properties = {
        "guestinfo.ignition.config.data"          = "${base64encode(templatefile("append_node.tpl", { ignition_url = var.worker_ignition_url }))}",
        "guestinfo.ignition.config.data.encoding" = "base64"
      }
    }
}

resource "vsphere_virtual_machine" "worker2" {
    name  = "okd-${var.cluster_name}-worker2"
    resource_pool_id     = module.worker2_vm.pool_id
    datastore_id         = module.worker2_vm.datastore_id
    num_cpus             = "4"
    num_cores_per_socket = "4"
    memory               = "16384"
    guest_id             = data.vsphere_virtual_machine.template.guest_id
    enable_disk_uuid     = "true"

    wait_for_guest_net_timeout  = "0"
    wait_for_guest_net_routable = "false"

    network_interface {
      network_id = data.vsphere_network.net1.id
      use_static_mac = "true"
      mac_address = var.worker_macs1[1]
    }

    network_interface {
      network_id = data.vsphere_network.net2.id
      use_static_mac = "true"
      mac_address = var.worker_macs2[1]
    }

    disk {
      label            = "disk0"
      size             = "120"
      thin_provisioned = data.vsphere_virtual_machine.template.disks.0.thin_provisioned
    }

    clone {
      template_uuid = data.vsphere_virtual_machine.template.id
    }

    vapp {
      properties = {
        "guestinfo.ignition.config.data"          = "${base64encode(templatefile("append_node.tpl", { ignition_url = var.worker_ignition_url }))}",
        "guestinfo.ignition.config.data.encoding" = "base64"
      }
    }
} 