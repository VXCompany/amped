param logClientId string
param logClientSecret string
param location string
var name = 'cae-amped-westeu-001'

resource cae 'Microsoft.Web/kubeEnvironments@2021-02-01' = {
  name: name
  location: location
  properties: {
    type: 'managed'
    internalLoadBalancerEnabled: false
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logClientId
        sharedKey: logClientSecret
      }
    }
  }
}
output id string = cae.id
