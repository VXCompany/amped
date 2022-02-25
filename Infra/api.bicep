param containerImage string
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

resource rabbitmq 'Microsoft.Web/containerApps@2021-03-01' existing = {
  name: 'ca-amped-rabbitmq-westeu-001'
}

module api 'ca-api.bicep' = {
  name: 'ampedapi'
  params: {
    location: location
    containerAppEnvironmentId: cae.id
    containerImage: containerImage
    containerPort: containerPort
    rabbitUsername: rabbitUsername
    rabbitPassword: rabbitPassword
    rabbitHost: rabbitmq.properties.configuration.ingress.fqdn
    useExternalIngress: true
    registry: registry
    registryUsername: registryUsername
    registryPassword: registryPassword
  }
}
output fqdn string = api.outputs.fqdn
