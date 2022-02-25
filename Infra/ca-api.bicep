param containerAppEnvironmentId string
param containerImage string
param useExternalIngress bool = false
param containerPort int
param registry string
param registryUsername string
param location string
param rabbitUsername string
param rabbitHost string

@secure()
param registryPassword string

@secure()
param rabbitPassword string

var name = 'ca-amped-api-westeu-001'

resource containerApp 'Microsoft.Web/containerApps@2021-03-01' = {
  name: name
  kind: 'containerapp'
  location: location
  properties: {
    kubeEnvironmentId: containerAppEnvironmentId
    configuration: {
      secrets: [
        {
          name: 'container-registry-password'
          value: registryPassword
        }
        {
          name: 'rabbitmq-password'
          value: rabbitPassword
        }
        {
          name: 'rabbitmq-user'
          value: rabbitUsername
        }
      ]      
      registries: [
        {
          server: registry
          username: registryUsername
          passwordSecretRef: 'container-registry-password'
        }
      ]
      activeRevisionsMode: 'single'
      ingress: {
        external: useExternalIngress
        targetPort: containerPort
      }
    }
    template: {
      containers: [
        {
            image: containerImage
            name: name
            env: [
              {
                name: 'ASPNETCORE_ENVIRONMENT'
                value: 'Development'
              }
              {
                name: 'ASPNETCORE_URLS'
                value: 'http://+:80'  
              }
              {
                name: 'RABBITMQ_HOST'
                value: rabbitHost
              }
              {
                name: 'RABBITMQ_PORT'
                value: '5672'  
              }        
              {
                name: 'RABBITMQ_USER'
                secretRef: 'rabbitmq-password'
              }
              {
                name: 'RABBITMQ_PASSWORD'
                secretRef: 'rabbitmq-user'  
              }
          ]
          resources: {
            cpu: 1
            memory: '2Gi'
          }
        }
      ]
      scale: {
        minReplicas: 0
        maxReplicas: 1
      }
    }
  }
}

output fqdn string = containerApp.properties.configuration.ingress.fqdn
