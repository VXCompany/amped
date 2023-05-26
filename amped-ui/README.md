Installeer dependencies:

```bash
npm install
```

Maak de environment file (.env in de root van het Angular project) met de juiste waarden:

```bash
API_BOOKMARK_URL=http://localhost:6060
API_PROFILE_URL=http://localhost:6060
AUTH0_DOMAIN=<tenant>.eu.auth0.com
AUTH0_CLIENT_ID=<client id>
AUTH0_CALLBACK_URL=http://localhost:4040/callback
AUTH0_AUDIENCE=<audience>
```

Draai de facade API

```bash
npm run api
```

Start de applicatie 

```bash
npm start
```

Bezoek [`http://localhost:4040/`](http://localhost:4040/).

