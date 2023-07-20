const { writeFile } = require('fs');
const { promisify } = require('util');
const dotenv = require('dotenv');
const { argv } = require('yargs');

dotenv.config();

const environment = argv.environment;
const isProduction = environment === 'production';

if (!process.env['API_BOOKMARK_URL'] || !process.env['API_PROFILE_URL'] ||
    !process.env['AUTH0_DOMAIN'] || !process.env['AUTH0_CLIENT_ID'] ||
    !process.env['AUTH0_AUDIENCE'] || !process.env['AUTH0_CALLBACK_URL']) 
    {
      console.error('Not all the required environment variables were provided!');
      process.exit(-1);
}


const writeFilePromisified = promisify(writeFile);

const targetPath = isProduction
   ? `./src/environments/environment.prod.ts`
   : `./src/environments/environment.ts`;

const envConfigFile = `export const environment = {
  production: ${isProduction},
  auth0: {
    domain: '${process.env['AUTH0_DOMAIN']}',
    clientId: '${process.env['AUTH0_CLIENT_ID']}',
    authorizationParams: {
      audience: '${process.env['AUTH0_AUDIENCE']}',
      redirect_uri: '${process.env['AUTH0_CALLBACK_URL']}',
      scope: 'openid profile email offline_access write:bookmark read:profile write:profile'
    },
    useRefreshTokens: true,
  },
  api: {
    bookmarkUrl: '${process.env['API_BOOKMARK_URL']}',
    profileUrl: '${process.env['API_PROFILE_URL']}',
  },
};
`;

(async () => {
  try {
    await writeFilePromisified(targetPath, envConfigFile);
  } catch (err) {
    console.error(err);
    throw err;
  }
})();
