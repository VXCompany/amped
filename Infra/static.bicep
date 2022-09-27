resource staticwebapp 'Microsoft.Web/staticSites@2022-03-01' = {
  name: 'stapp-ampedfrontend-prod-westeu-001'
  location: resourceGroup().location
  tags: {     
    tagName1: 'tagValue1'     
    tagName2: 'tagValue2'   
  }
  properties: {
    
  }
  sku:{
    name: 'Standard'
    tier: 'Standard'
  }
}
