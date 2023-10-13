## Install Cert-manager with Helm

Add the Helm repository:
```console
helm repo add jetstack https://charts.jetstack.io
```
Update your local Helm chart repository cache:
```console
helm repo update
```
To install the cert-manager Helm chart, use the Helm install command as described below.
```console
helm install \
  cert-manager jetstack/cert-manager \
  --namespace cert-manager \
  --create-namespace \
  --version v1.12.4 \
  --set installCRDs=true
  ```

## Issuer Configuration

The first thing you'll need to configure after you've installed cert-manager is an Issuer or a ClusterIssuer. These are resources that represent certificate authorities (CAs) able to sign certificates in response to certificate signing requests.
Now, we are going to create a ClusterIssuer. The ClusterIssuer resource is cluster scoped. This means that when referencing a secret via the secretName field, secrets will be looked for in the Cluster Resource Namespace. 

Create a secret:
```console
apiVersion: v1
kind: Secret
metadata:
  name: ultratone-cf
  namespace: cert-manager
data:
  cloudflare-token: <TOKEN>
type: Opaque
```
Create the clusterissuer:
```console
apiVersion: cert-manager.io/v1
kind: ClusterIssuer
metadata:
  name: appstract-letsencrypt
spec:
  acme:
    email: <EMAIL>
    preferredChain: ''
    privateKeySecretRef:
      name: appstract-letsencrypt
    server: https://acme-v02.api.letsencrypt.org/directory
    solvers:
      - dns01:
          cloudflare:
            apiTokenSecretRef:
              key: <CLOUDFLARE-TOKENKEY>
              name: ultratone-cf
            email: <EMAIL>
        selector:
          dnsZones:
            - <DNS-ZONE>
  ```