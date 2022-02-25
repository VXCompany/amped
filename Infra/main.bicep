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

module log 'log.bicep' = {
    name: 'log-analytics-workspace'
    params: {
      location: location
    }
}

module cae 'cae.bicep' = {
  name: 'container-app-environment'
  params: {
    location: location
    logClientId:log.outputs.clientId
    logClientSecret: log.outputs.clientSecret
  }
}

module rabbitmq 'ca-rabbitmq.bicep' = {
  name: 'rabbitmq'
  params: {
    location: location
    containerAppEnvironmentId: cae.outputs.id
    containerImage: 'docker.io/rabbitmq:3-management-alpine'
    rabbitUsername: rabbitUsername
    rabbitPassword: rabbitPassword
  }
}

module api 'ca-api.bicep' = {
  name: 'ampedapi'
  params: {
    location: location
    containerAppEnvironmentId: cae.outputs.id
    containerImage: containerImage
    containerPort: containerPort
    rabbitUsername: rabbitUsername
    rabbitPassword: rabbitPassword
    rabbitHost: rabbitmq.outputs.fqdn
    useExternalIngress: true
    registry: registry
    registryUsername: registryUsername
    registryPassword: registryPassword
  }
}
output fqdn string = api.outputs.fqdn
