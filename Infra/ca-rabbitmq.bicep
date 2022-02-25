param containerAppEnvironmentId string
param containerImage string
param location string
param rabbitUsername string
param rabbitPassword string

var name = 'ca-amped-rabbitmq-westeu-001'

resource containerApp 'Microsoft.Web/containerApps@2021-03-01' = {
  name: name
  kind: 'containerapp'
  location: location
  properties: {
    kubeEnvironmentId: containerAppEnvironmentId
    configuration: {
      secrets: [
        {
          name: 'rabbitmq-default-password'
          value: rabbitPassword
        }
        {
          name: 'rabbitmq-default-user'
          value: rabbitUsername
        }
      ]
      ingress: {
        external: false
        targetPort: 5672
      }
    }
    template: {
      containers: [
        {
          image: containerImage
          name: name
          env: [
            {
              name: 'RABBITMQ_DEFAULT_USER'
              secretRef: 'rabbitmq-default-user'  
            }
            {
              name: 'RABBITMQ_DEFAULT_PASS'
              secretRef: 'rabbitmq-default-password'  
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
