# initial stage
FROM node:16-alpine3.12 AS build
WORKDIR /app
COPY ./package*.json ./
RUN npm install
COPY ./ ./
RUN npm run build

# runtime stage
FROM nginx:1.21.6-alpine AS runtime
EXPOSE 80
ENV TZ=Asia/Bangkok
RUN apk update && \
    apk add --no-cache tzdata
    
# delete all files inside /usr/share/nginx/html/ and in /etc/nginx/conf.d/
RUN rm -rf /usr/share/nginx/html/* && \
    rm -rf /etc/nginx/conf.d/*
COPY ./nginx/nginx.conf /etc/nginx/conf.d
COPY --from=build /app/build /usr/share/nginx/html

CMD ["nginx", "-g", "daemon off;"]