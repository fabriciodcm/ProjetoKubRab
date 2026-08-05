FROM node:22-alpine AS build

WORKDIR /app

COPY ProjectKubRab.ProductsWebApp/package.json ProjectKubRab.ProductsWebApp/package-lock.json ./
RUN npm ci

COPY ProjectKubRab.ProductsWebApp/ ./
RUN npm run build

FROM nginx:1.29-alpine AS final

COPY --from=build /app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
