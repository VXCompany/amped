param location string
var name = 'aks-amped-westeu-001'

param nodeCount int = 1
param vmSize string = 'standard_d2s_v3'

resource aks 'Microsoft.ContainerService/managedClusters@2022-05-02-preview' = {
  name: name
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    dnsPrefix: name
    enableRBAC: true
    agentPoolProfiles: [
      {
        name: 'ampedap1'
        count: nodeCount
        vmSize: vmSize
        mode: 'System'
      }
    ]
  }
}


