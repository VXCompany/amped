// params specific
param currentAppSettings object
param appSettings object
param siteName string

// resources
resource siteconfig 'Microsoft.Web/sites/config@2021-02-01' = {
  name: '${siteName}/appsettings'
  properties: union(appSettings, currentAppSettings)
  // Union combines settings + deduplicates where the last parameter values win (i.e. we want to keep any updated settings)
}
