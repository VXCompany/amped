// params
@allowed([
  'qa'
  'prod'
])
@description('Environment (QA/PROD) for the web app and app service plan.')
param env string

@description('The Runtime stack of current web app.')
param linuxFxVersion string = 'DOTNETCORE|5.0'

// vars
var location = resourceGroup().location
var appServiceName = 'app-amped-api-${env}-westeu-001'
var appServicePlanName = 'plan-amped-${env}-westeu-001'

// resources
resource appPlan 'Microsoft.Web/serverfarms@2021-02-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: 'B1'
  }
  kind: 'linux'
  properties: {
    perSiteScaling: false
    reserved: true
    targetWorkerCount: 0
    targetWorkerSizeId: 0
  }
}

resource site 'Microsoft.Web/sites@2021-02-01' = {
  name: appServiceName
  location: location
  kind: 'app'
  properties: {
    serverFarmId: appPlan.id
    siteConfig: {
      linuxFxVersion: linuxFxVersion
      ftpsState: 'Disabled'
    }
  }
  identity: {
    type: 'SystemAssigned'
  }
}

module sitesettings 'appsettings.bicep' = {
  name: '${site.name}-appsettings'
  params: {
    siteName: site.name
    currentAppSettings: list('${site.id}/config/appsettings', '2021-02-01').properties
    appSettings: {
      'ASPNETCORE_ENVIRONMENT': 'Production'
      'ASPNETCORE_HTTPS_PORT': '443'
      'WEBSITE_TIME_ZONE': 'Europe/Amsterdam'
    }
  }
}
