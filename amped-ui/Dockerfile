FROM node:16.20.1-alpine AS build

WORKDIR /usr/src/app
COPY . /usr/src/app
RUN npm ci
RUN npm run build-prod

FROM nginx:1.21.3-alpine
COPY --from=build /usr/src/app/dist/amped-ui /usr/share/nginx/html
COPY --from=build /usr/src/app/nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
