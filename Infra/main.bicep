param rabbitUsername string
param location string = resourceGroup().location

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
