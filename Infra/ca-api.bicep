param containerAppEnvironmentId string
param containerImageApi string
param containerImageRabbit string
param useExternalIngress bool = false
param containerPort int
param registry string
param registryUsername string
param location string
param rabbitUsername string

@secure()
param registryPassword string

@secure()
param rabbitPassword string

var nameApi = 'ca-amped-api-westeu-001'
var nameRabbit = 'ca-amped-rabbit-westeu-001'

resource containerApp 'Microsoft.Web/containerApps@2021-03-01' = {
  name: nameApi
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
            image: containerImageApi
            name: nameApi
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
                value: 'localhost'
              }
              {
                name: 'RABBITMQ_PORT'
                value: '5672'  
              }        
              {
                name: 'RABBITMQ_USER'
                secretRef: 'rabbitmq-user'
              }
              {
                name: 'RABBITMQ_PASSWORD'
                secretRef: 'rabbitmq-password'  
              }
          ]
          resources: {
            cpu: 1
            memory: '2Gi'
          }
        } 
        {
          image: containerImageRabbit
          name: nameRabbit
          env: [
            {
              name: 'RABBITMQ_DEFAULT_USER'
              secretRef: 'rabbitmq-user'  
            }
            {
              name: 'RABBITMQ_DEFAULT_PASS'
              secretRef: 'rabbitmq-password'  
            }
          ]
          resources: {
            cpu: 1
            memory: '2Gi'
          }
        }
      ]
      scale: {
        minReplicas: 1
        maxReplicas: 1
      }
    }
  }
}

output fqdn string = containerApp.properties.configuration.ingress.fqdn
