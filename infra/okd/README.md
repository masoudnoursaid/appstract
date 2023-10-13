# Running OKD cluster on vSphere using Terraform

In this manual required tools and method of running 5 nodes (3 master and 2 worker) okd cluster using Terraform will be reviewed.

## Required server, tools and configuration

Before you enable to run the Terraform script you need to provide different requirements and tools which we review here:   

### vSphere requirements

The requirements in vCenter for okd cluster nodes are:

 - Fedora CoreOS Template: 
Download VMware ova from  [this link](https://fedoraproject.org/coreos/download/?stream=stable#arches) and create a template out of it. You could change the thick disk provisioning to thin using [this method](https://kb.vmware.com/s/article/2014832) (To change vmdk from Thick to Thin provisioning section)
 - Dedicated Port group
Create a port group with a specific vlan id and ip range behind it for using by cluster vms.
 - Specify ESXI hosts and datastore for each cluster node
You need to determine the ESXI host and datastore for each cluster node to be implements on based on your available resources. The minimum required resources for okd cluster nodes are:

```
    Bootstrap       4vCPU   16GB RAM    100Gb HDD (IOPS 300)
    Control plane   4vCPU   16GB RAM    100Gb HDD (IOPS 300)
    Compute         2vCPU    8GB RAM    100Gb HDD (IOPS 300)
```

Bootstrap node is temporary node which could be deleted after successful installation of cluster.
 .

### DHCP, DNS, HAProxy, HTTPD 
You need all these services up and running for the cluster. You could implement them on separate servers or on a single one based on your environment needs. We used an centos vm for running Bind, dhcpd, haproxy and httpd. You could use these packages or any other packages for same services. 
- Bind

You could install bind and use config files in this repo for config the service. YOU NEED TO MODIFY CONFIG FILES TO REFLECT YOUR OWN IP ADDRESSES, DOMAIN AND CLUSTER NODE NAMES:
```
    yum install -y vim bind bind-utils
    cp ./services-config/named.conf /etc/
    cp ./services-config/forward.db /var/named/dynamic/
    cp ./services-config/reverse.db /var/named/dynamic/

    systemctl enable named
    systemctl restart named
    systemctl status named

    firewall-cmd --permanent --add-port=53/udp
    firewall-cmd --reload
```
Change the dns server on the vm:
```
    nmtui

    ifdown ens3;ifup ens3
```
verify dns server works:
```
    nslookup api.os-rg1.maxtld.dev
```
- dhcpd

You could install dhcpd and use config files in this repo for config the service. YOU NEED TO MODIFY CONFIG FILES TO REFLECT YOUR OWN IP AND MAC ADDRESSES, DOMAIN AND CLUSTER NODE NAMES:
```
    yum install dhcp-server -y
    cp ./services-config/dhcpd.conf /etc/dhcp/
    
    systemctl enable dhcpd
    systemctl restart dhcpd
    systemctl status dhcpd

    firewall-cmd --add-service=dhcp --permanent
    firewall-cmd --reload
```
- haproxy

You could install haproxy and use config files in this repo for config the service. YOU NEED TO MODIFY CONFIG FILES TO REFLECT YOUR OWN DOMAIN AND CLUSTER NODE NAMES:
```
    yum install -y haproxy rsyslog
    cp ./services-config/haproxy.cfg /etc/haproxy/

    sudo setsebool -P haproxy_connect_any 1
    sudo systemctl enable haproxy
    sudo systemctl start haproxy
    sudo systemctl status haproxy

    firewall-cmd --permanent --add-port=6443/tcp
    firewall-cmd --permanent --add-port=22623/tcp
    firewall-cmd --permanent --add-service=http
    firewall-cmd --permanent --add-service=https
    firewall-cmd --reload
```
If you want to have multiple haproxy nodes for high avalability reason, you need to install keepalived on each of your haproxy nodes and use the virtaul ip created by keepalived for you services. YOU NEED TO MODIFY CONFIG FILE TO REFLECT YOUR OWN CLUSTER NODES:
```
    yum install -y keepalived
    cp ./services-config/keepalived.conf /etc/keepalived/

    sudo systemctl enable keepalived
    sudo systemctl start keepalived
    sudo systemctl status keepalived
```
- httpd

You could install haproxy and use config it in this way:
```
    yum install httpd
    sed -i 's/Listen 80/Listen 8080/' /etc/httpd/conf/httpd.conf

    setsebool -P httpd_read_user_content 1
    systemctl enable httpd
    systemctl start httpd
    firewall-cmd --permanent --add-port=8080/tcp
    firewall-cmd --reload

    curl localhost:8080
```
### Openshift-installer and oc 
You need a linux machine for installing using openshift installer (openshift-install) and communicate with okd cluster using openshift client (oc), you could use the vm created in previous step for running services or any other linux with network access to okd cluster nodes. 

Almost every 2 weeks a new version of okd comes and you need to check the latest version in [this link](https://github.com/okd-project/okd/releases). 4.13.0-0.okd-2023-07-23-051208 is the latest version at the time of writing this document. In order to download and install okd installer and client you could use these command and replace the latest version:
```
    wget https://github.com/openshift/okd/releases/download/4.13.0-0.okd-2023-07-23-051208/openshift-client-linux-4.13.0-0.okd-2023-07-23-051208.tar.gz
    wget https://github.com/openshift/okd/releases/download/4.13.0-0.okd-2023-07-23-051208/openshift-install-linux-4.13.0-0.okd-2023-07-23-051208.tar.gz

    tar -zxvf openshift-client-linux-4.13.0-0.okd-2023-07-23-051208.tar.gz
    tar -zxvf openshift-install-linux-4.13.0-0.okd-2023-07-23-051208.tar.gz

    sudo mv kubectl oc openshift-install /usr/local/bin/
    oc version
    openshift-install version
```
### Terraform
Also you need Terraform for running the scripts of provisioning okd nodes. Again you could use the vm used for okd services and install Terraform on it:
```
     yum install -y yum-utils
     yum-config-manager --add-repo https://rpm.releases.hashicorp.com/RHEL/hashicorp.repo
     yum -y install terraform 
     terraform -version
```
## Setup openshift-installer
You need a ssh public key, by which you could able to ssh to cluster nodes if needed. You could create it if you already haven't:
```
    ssh-keygen
```
In order to create ignition files which is used in preparing okd nodes. YOU NEED TO MODIFY install-config.yaml FILE TO REFLECT YOUR OWN SSH KEY AND CLUSTER DOMAIN NAME:
```
    cd 
    mkdir install_dir
    cp ./install-config.yaml ./install_dir
    vi ./install_dir/install-config.yaml
```
After modifing the file you could back it up as it will be deleted in next steps:
```
    cp ./install_dir/install-config.yaml ./install_dir/install-config.yaml.bak
```
## Prepare bootstrap, master and worker ignition files
Generate the Kubernetes manifests for the cluster, ignore the warning:
```
    openshift-install create manifests --dir=install_dir/
```
Remove the Kubernetes manifest files that define the control plane machines and compute machine sets. [needs to be reviewed and test]
```
    rm -f openshift/99_openshift-cluster-api_master-machines-*.yaml openshift/99_openshift-cluster-api_worker-machineset-*.yaml
```
Modify the cluster-scheduler-02-config.yaml manifest file to prevent Pods from being scheduled on the control plane machines:
```
    sed -i 's/mastersSchedulable: true/mastersSchedulable: False/' install_dir/manifests/cluster-scheduler-02-config.yml
```
Create ignition-configs:
```
    openshift-install create ignition-configs --dir=install_dir/
```
Note: If you reuse the install_dir, make sure it is empty. Hidden files are created after generating the configs, and they should be removed before you use the same folder on a 2nd attempt.

Finally you need to put ignition files on the webserver:
```
    mkdir /var/www/html/okd4
    cp -R install_dir/* /var/www/html/okd4/
    chown -R apache: /var/www/html/
    chmod -R 755 /var/www/html/

    curl localhost:8080/okd4/metadata.json
```
## Provision OKD nodes by Terraform
Last but not the least you need to provision the okd cluster nodes using terraform script provided in this repo files. Please pay attention that YOU NEED TO CREATE terraform.tfvars AND FILL IT BASED ON YOUR ENVIRONMENT NEEDS AND CONFIGURATION. A sample of this file is available in terraform.tfvars.sample.

- Be aware that ignition url of the nodes based on the httpd server ip would be like these:
```
    bootstrap_ignition_url= "http://httpdServerIPorFQDN:8080/okd4/bootstrap.ign"
    master_ignition_url="http://httpdServerIPorFQDN:8080/okd4/master.ign"
    worker_ignition_url= "http://httpdServerIPorFQDN:8080/okd4/worker.ign"

```
After filling terraform.tfvars carefully you could call terraform to provision you cluster:
```
    #cd to terraform directory
    terraform init
    terraform plan -out main.tfplan
    terraform apply main.tfplan
```
After about half an hour you could check if bootstrap process finished or not using this command:
```
    openshift-install wait-for bootstrap-complete
```
You could also ssh into bootstrap node from the vm that you set pulic ssh key to check the status:
```
    ssh core@BootStrapIporFQDN
    journalctl -b -f -u bootkube.service
```
If the boot process finished succesfully then you should remove bootstrap node from haproxy config file:
```
    sed '/ okd4-bootstrap /s/^/#/' /etc/haproxy/haproxy.cfg
    systemctl reload haproxy
```
You could check the okd cluster nodes status:
```
    oc get nodes
```
And you need to approve certificate requests from worker nodes:
```
    oc get csr -o go-template='{{range .items}}{{if not .status}}{{.metadata.name}}{{"\n"}}{{end}}{{end}}' | xargs --no-run-if-empty oc adm certificate approve
```
After the successful master nodes installation you could remove bootstrap node.

### Install Cilium
The official documentation for installing Cilium is for the IPI method of OKD installation so it doesn't work on the UPI method we have used here. So, we can install Cilium after OKD is installed with Openshift SDN. Note that there is no official way or any guides on the Internet for this so I am using the [Migrating from the OpenShift SDN network plugin](https://docs.openshift.com/container-platform/4.12/networking/ovn_kubernetes_network_provider/migrate-from-openshift-sdn.html) manual to migrate from OpenShift SDN to Cilium.
1. To backup the configuration for the cluster network, enter the following command:
    ```
    oc get Network.config.openshift.io cluster -o yaml > cluster-openshift-sdn.yaml
    ```
2. To prepare all the nodes for the migration, set the migration field on the Cluster Network Operator configuration object by entering the following command:
    ```
    oc patch Network.operator.openshift.io cluster --type='merge' --patch '{ "spec": { "migration": { "networkType": "Cilium" } } }'
    ```
3. As the MCO updates machines in each machine config pool, it reboots each node one by one. You must wait until all the nodes are updated. Check the machine config pool status by entering the following command:
    ```
    oc get mcp
    ```
    A successfully updated node has the following status: `UPDATED=true`, `UPDATING=false`, `DEGRADED=false`.

4. You can use the olm-for-cilium directory in this repository. This is the files obtained from the [Isovalent repository](https://github.com/isovalent/olm-for-cilium) which we are going install `v1.13.0` version of Cilium. Because the latest free OLM image in the quay.io repository is for this version and the latest versions were in the Redhat registry which is a paid service. Please note that the manifest files are different from the original files from the repository because we had to change the image address of cilium-olm and add support for bgp.
    ```
    oc apply -f ./cilium/olm-for-cilium/manifests/cilium.v1.13.0
    ```
5. check the Cilium pods are running:
    ```
    oc get pods -n cilium
    ```
6. Confirm the status of the new machine configuration on the hosts by listing the machine configuration state and the name of the applied machine configuration. To do this enter the following command:
    ```
    oc describe node | egrep "hostname|machineconfig"
    ``` 
    `Example output`
    ```
        kubernetes.io/hostname=master-0
        machineconfiguration.openshift.io/currentConfig: rendered-master-c53e221d9d24e1c8bb6ee89dd3d8ad7b
        machineconfiguration.openshift.io/desiredConfig: rendered-master-c53e221d9d24e1c8bb6ee89dd3d8ad7b
        machineconfiguration.openshift.io/reason:
        machineconfiguration.openshift.io/state: Done
    ```        

     Verify that the following statements are true:

    * The value of `machineconfiguration.openshift.io/state` field is Done.
        
    * The value of the `machineconfiguration.openshift.io/currentConfig` field is equal to the value of the machineconfiguration.openshift.io/desiredConfig field.
7. If a node is stuck in the NotReady state, investigate the machine config daemon pod logs and resolve any errors.

   a. To list the pods, enter the following command:
    ```
    oc get pod -n openshift-machine-config-operator
    ```
    `Example output`
    ```
    NAME                                         READY   STATUS    RESTARTS   AGE
    machine-config-controller-75f756f89d-sjp8b   1/1     Running   0          37m
    machine-config-daemon-5cf4b                  2/2     Running   0          43h
    machine-config-daemon-7wzcd                  2/2     Running   0          43h
    machine-config-daemon-fc946                  2/2     Running   0          43h
    machine-config-daemon-g2v28                  2/2     Running   0          43h
    machine-config-daemon-gcl4f                  2/2     Running   0          43h
    machine-config-daemon-l5tnv                  2/2     Running   0          43h
    machine-config-operator-79d9c55d5-hth92      1/1     Running   0          37m
    machine-config-server-bsc8h                  1/1     Running   0          43h
    machine-config-server-hklrm                  1/1     Running   0          43h
    machine-config-server-k9rtx                  1/1     Running   0          43h
    ```
    The names for the config daemon pods are in the following format: `machine-config-daemon-<seq>`. The <seq> value is a random five character alphanumeric sequence.

    b. Display the pod log for the first machine config daemon pod shown in the previous output by enter the following command:
    ```
    oc logs <pod> -n openshift-machine-config-operator
    ```
    where pod is the name of a machine config daemon pod.

    c. Resolve any errors in the logs shown by the output from the previous command.

8. Verify that the Multus daemon set rollout is complete before continuing with subsequent steps:
    ```
    oc -n openshift-multus rollout status daemonset/multus
    ```
9. To complete the migration, reboot each node in your cluster. For example, you can use a bash script similar to the following example. The script assumes that you can connect to each host by using ssh and that you have configured sudo to not prompt for a password.
    ```
    #!/bin/bash

    for ip in $(oc get nodes  -o jsonpath='{.items[*].status.addresses[?(@.type=="InternalIP")].address}')
    do
    echo "reboot node $ip"
    ssh -o StrictHostKeyChecking=no core@$ip sudo shutdown -r -t 3
    done
    ```
10. Confirm that the migration succeeded:

    a. To confirm that the network plugin is OVN-Kubernetes, enter the following command. The value of status.networkType must be OVNKubernetes.
    ```
    oc get network.config/cluster -o jsonpath='{.status.networkType}{"\n"}'
    ```
    b. To confirm that the cluster nodes are in the Ready state, enter the following command:
    ```
    oc get nodes
    ```
    c. To confirm that your pods are not in an error state, enter the following command:
    ```
    oc get pods --all-namespaces -o wide --sort-by='{.spec.nodeName}'
    ```
      If pods on a node are in an error state, reboot that node.

    d. To confirm that all of the cluster Operators are not in an abnormal state, enter the following command:
    ```
    oc get co
    ```
       The status of every cluster Operator must be the following: `AVAILABLE="True"`, `PROGRESSING="False"`, `DEGRADED="False"`. If a cluster Operator is not available or degraded, check the logs for the cluster Operator for more information.
11. Complete the following steps only if the migration succeeds and your cluster is in a good state:

    a. To remove the migration configuration from the CNO configuration object, enter the following command:
    ```
    oc patch Network.operator.openshift.io cluster --type='merge' \
    --patch '{ "spec": { "migration": null } }'
    ```
    b. To remove custom configuration for the OpenShift SDN network provider, enter the following command:
    ```
    oc patch Network.operator.openshift.io cluster --type='merge' \
     --patch '{ "spec": { "defaultNetwork": { "openshiftSDNConfig": null } } }'
    ```
    c. To remove the OpenShift SDN network provider namespace, enter the following command:
    ```
     oc delete namespace openshift-sdn
    ```
12. Now apply the `IPPOOL` and `BGP` configuration files. please make sure the configuration files are edited according to your enviromnment:

    ```
     oc apply ./cilium/ippool.yaml
     oc apply ./cilium/bgp.yaml
    ```