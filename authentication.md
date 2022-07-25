# Authentication


## TLDR

We authenticate with Github (OAuth, Auth Code flow). Got to [https://github.com/settings/developers](https://github.com/settings/developers) and create your own client_id + secret. We aren't sharing ours. __Make sure you don't commit your client_id+secret!!!__.

## What do we need it for?

We want people to be able to use Amped to manage their own content lists. Whether or not a bookmark has been read depends on context: Who has read the content?

## Preliminary application architecture

Our application will be a composition of several APIs and web components. These components are stitched together into a single, functional webapp. The mobile app will use all sorts of APIs too.

You are the target audience of Amped. And you are a github user. So we need people to log in using github. Github supports the OAuth Auth Code flow (without PKCE).

As a result, our architecture looks as follows:

```
+-----------------------------------------------------+
| +-------------------------------------------------+ |
| |                     Shell                       | |
| |                                                 | |
| | +-------------+ +-------------+ +-------------+ | |
| | |   Bookmark  | |    Rating   | |   Profile   | | |
| | |  Component  | |  Component  | |  Component  | | |
| | +-------------+ +-------------+ +-------------+ | |
| |        |               |               |        | |
| +------- | ------------- | ------------- | -------+ |
|          v               v               v          |
|   +-------------+ +-------------+ +-------------+   |
|   |   Bookmark  | |    Rating   | |   Profile   |   |
|   |     BFF     | |     BFF     | |     BFF     |   |
|   | (Implement  | | (Implement  | | (Implement  |   |
|   | AuthN here) | | AuthN here) | | AuthN here) |   |
|   +-------------+ +-------------+ +-------------+   |
|          |               |               |          |
|          v               v               v          |
|   +-------------+ +-------------+ +-------------+   |
|   |   Bookmark  | |    Rating   | |   Profile   |   |
|   |    API(s)   | |    API(s)   | |    API(s)   |   |
|   | (Implement  | | (Implement  | | (Implement  |   |
|   | AuthZ here) | | AuthZ here) | | AuthZ here) |   |
|   +-------------+ +-------------+ +-------------+   |
|      |      ^ |     |       ^ |      |      ^ |     |   
|      v      | |     v       | |      v      | |     |
|   +----+    | |   +----+    | |   +----+    | |     |
|   |data|    | |   |data|    | |   |data|    | |     |
|   +----+    | |   +----+    | |   +----+    | |     |
|             | v             | v             | v     |
|   +---------------------------------------------+   |
|   |            Messaging service (RabbitMQ)     |   |
|   +---------------------------------------------+   |
+-----------------------------------------------------+

```

Note: This picture is a BHAG. It doesn't exist yet. Currently (25-7-2022), we only have the bookmark front-end + api.

## Ok. Nice.. But I am a contributor and I want to create a new service. How do I set up authentication for dev purposes?

Wat to contribute? Awesome! Please do. There's just one thing we can't do for you. We are not going to give our client_id/secret. You need to create your own credentials. 

Here's a step by step description how to do so:
* Go to [https://github.com/settings/developers](https://github.com/settings/developers)
* Click "new OAuth App" in the upper right corner
* Fill out the form:
    * Application name: Amped (dev)
    * Homepage: https://localhost:5001
    * Description: Lorum ipsum dolar sit amet!
    * Authorization callback url: https://localhost:5001/redirect
    * Do not enable the device flow (yet).
* Click [Register application]
* Github will redirect you to a new page. You'll see you own the application and there are 0 users yet. It also shows the client id which looks somewhat like this: `Client ID 47f5d03f9b5fc8545114`.
* Below the Client ID you'll see a section `Client secrets` with 0 secrets. Click the button `Generate a new client secret`.
* Github will ask you to authenticate again. Do that.
* You'll be redirected to the application overview page and below the client ID you'll see a sercet has appeared. It looks somewhat like this: `4e3f385d428ef651d0c213d71cec26be6c522a3b`.
* Use these details to configure authentication in your API or in the existing APIs.
    * Find details about Githubs OAuth implementation here: [https://docs.github.com/en/developers/apps/building-oauth-apps/authorizing-oauth-apps](https://docs.github.com/en/developers/apps/building-oauth-apps/authorizing-oauth-apps)
    * This is the authorize endpoint: https://github.com/login/oauth/authorize?client_id=...&client_secret=....&code=...&redirect_uri=https%3A%2F%2Flocalhost%3A5001%2Fredirect
    * This is the token endpoint: https://github.com/login/oauth/access_token

## Git
We don't want to know your client_id or secret. Don't commit it.