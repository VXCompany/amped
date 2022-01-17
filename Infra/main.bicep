// params
@allowed([
  'qa'
  'prod'
])
@description('Environment (QA/PROD) for the web app and app service plan.')
param env string

// vars

// modules
module webapp './webapp.bicep' = {
  name: 'webapp'
  params: {
    env: env
  }
}
