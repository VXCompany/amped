param location string
var name = 'log-amped-westeu-001'

resource log 'Microsoft.OperationalInsights/workspaces@2021-06-01' = {
  name: name
  location: location
  properties: any({
    retentionInDays: 30
    features: {
      searchVersion: 1
    }
    sku: {
      name: 'PerGB2018'
    }
  })
}
output clientId string = log.properties.customerId
output clientSecret string = log.listKeys().primarySharedKey
