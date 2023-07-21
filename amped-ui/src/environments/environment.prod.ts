export const environment = {
  production: true,
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
    bookmarkUrl: 'http://localhost:6063',
    profileUrl: 'http://localhost:6060',
  },
};
