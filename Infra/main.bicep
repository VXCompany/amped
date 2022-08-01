param location string = resourceGroup().location

module aks 'aks.bicep' = {
  name: 'aks'
  params: {
    location: location
  }
}
