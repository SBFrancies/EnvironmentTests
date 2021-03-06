param location string = resourceGroup().location
param registryName string
param appServicePlanName string
param appServicePlanSku string = 'F1'
param appServiceName string
param sqlServerName string
param linuxFxVersion string = 'DOTNETCORE|6.0' 
param sqlDatabaseName string
@secure()
param sqlServerPassword string

resource containerRegistry 'Microsoft.ContainerRegistry/registries@2022-02-01-preview' = {
  name: registryName
  location: location
  sku: {
    name: 'Standard'
  }
}

resource appServicePlan 'Microsoft.Web/serverfarms@2020-06-01' = {
  name: appServicePlanName
  location: location
  properties: {
    reserved: true
  }
  sku: {
    name: appServicePlanSku
  }
  kind: 'linux'
}

resource appService 'Microsoft.Web/sites@2020-06-01' = {
  name: appServiceName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: linuxFxVersion
    }
    httpsOnly:true
  }
}

resource sqlServer 'Microsoft.Sql/servers@2021-11-01-preview' = {
  name: sqlServerName
  location: location
  properties:{
    administratorLogin: 'sqlAdmin'
    administratorLoginPassword: sqlServerPassword
  }
}

resource sqlDatabase 'Microsoft.Sql/servers/databases@2021-11-01-preview' = {
  parent:sqlServer
  name: sqlDatabaseName
  location: location
  sku: {
    name: 'Standard'
    tier: 'Standard'
  }
}
