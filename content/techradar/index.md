# Tech Radar

On this page we will list concepts, technniques, languages, frameworks and tools used within Amped. As Amped evolves, so does this list. This means that not every item on the list is necessarily still part of the (main) codebase. 

## Svelte

We needed a simple way to quickly create an interactive UI. Angular, React and Vue were discussed but we decided to go for a newer "kid on the block". 

Things we liked about Svelte:

- Less code needed compared to Angular or React;
- Easy to setup, easy to get started;
- Centered around components and we liked that a lot!

Things that were a bit rough with Svelte:

- We were not sure if we needed SvelteKit so we started with plain Svelte. Maybe we will need to move to SvelteKit later on.

Status:

We will see :)

## Mass Transit

Writing distributed applications can be hard sometimes and with Amped we will try to solve things in a number of ways. In our Bookmark API we opted for CQRS as a design pattern and picked Mass Transit to do most of our heavy lifting.

Things we liked about Mass Transit:

- Very easy to setup;
- Good docs! Check them out here: https://masstransit-project.com/getting-started/ ;
- Support for all the things we know (RabbitMQ, Azure Service Bus, Amazon SQL, etc.);

Things we didn't like:

- gRPC as a transport was still a bit in the early stage.

Status:

We will revisit and see if we can swap out RabbitMQ for gRPC.

## Azure CosmosDB

Our first prototype relied on SQLite, but we recently switched to CosmosDB for our Bookmarks API and we are not going back. 

Status: Document based persistence fits our design better.

## Azure Functions

Azure Functions are used to update the materialized views.

Status: they work as expected. 

## Azure Static Web App

This runs our frontend (Svelte) app. 

Status: best static option on Azure ever (compared to Web Apps, Storage, CDN, etc.).

## Azure Container Apps

Status: TBD

## RabbitMQ

This message broker is our transport for Mass Transit in our Bookmarks API. 

Status: it works flawlessly, but we might swap it out for gRPC (less dependencies).

## ASP.NET Minimal API

Our Profile API is based on ASP.NET Minimal API.

Status: still a lot of work to do here and to early to call.

## Sonar Cloud

Status: TBD

## Continuous Testing

Status: TBD

## Bicep

We just took our first steps with Bicep for the deployment of the infrastructure. We like it, but we need to do some real work with it first.

Status: TBD

## ZAP

Status: TBD
