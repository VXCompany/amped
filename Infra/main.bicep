param location string = resourceGroup().location

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
