param containerImageApi string
param containerImageRabbit string
param containerPort int
param registry string
param registryUsername string
param rabbitUsername string
param location string = resourceGroup().location

@secure()
param registryPassword string
@secure()
param rabbitPassword string


resource cae 'Microsoft.Web/kubeEnvironments@2021-02-01' existing = {
  name: 'cae-amped-westeu-001'
}

module api 'ca-api.bicep' = {
  name: 'ampedapi'
  params: {
    location: location
    containerAppEnvironmentId: cae.id
    containerImageApi: containerImageApi
    containerImageRabbit: containerImageRabbit
    containerPort: containerPort
    rabbitUsername: rabbitUsername
    rabbitPassword: rabbitPassword
    useExternalIngress: true
    registry: registry
    registryUsername: registryUsername
    registryPassword: registryPassword
  }
}
output fqdn string = api.outputs.fqdn
