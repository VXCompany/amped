param location string = resourceGroup().location

module log 'log.bicep' = {
  name: 'log-analytics-workspace'
  params: {
    location: location
  }
}

module aks 'aks.bicep' = {
  name: 'aks'
  params: {
    location: location
  }
}
