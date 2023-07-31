export const environment = {
  production: false,
  auth0: {
    domain: 'vxcompany-kennisdag.eu.auth0.com',
    clientId: 'bqdP0OizKP5LFH7lRuZfqokdbf54Y4sO',
    authorizationParams: {
      audience: 'https://totallyamped.tech',
      redirect_uri: 'http://localhost:4040/callback',
      scope: 'openid profile email offline_access write:bookmark read:profile write:profile'
    },
    useRefreshTokens: true,
  },
  api: {
    bookmarkUrl: 'http://localhost:6060',
    profileUrl: 'http://localhost:6060',
    functionUrl: 'https://fa-amped-prod-westeu-001.azurewebsites.net',
    functionKey: 'tjgEV6Gp6GKWiIPf43ZDXuLNyYBOP7pZIoAZNVk0jt4vAzFuWY2OZw=='
  },
};
