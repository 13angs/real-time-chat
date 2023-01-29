# Real-time chat

[![real-time-chat](https://github.com/13angs/real-time-chat/actions/workflows/frontend.yml/badge.svg)](https://github.com/13angs/real-time-chat/actions/workflows/frontend.yml)
[![real-time-chat-api](https://github.com/13angs/real-time-chat/actions/workflows/backend.yml/badge.svg)](https://github.com/13angs/real-time-chat/actions/workflows/backend.yml)

## Run the project

1. build the images

```bash
docker compose build
```

2. run the project using `docker-compose`

```bash
docker compose up

# or

docker-compose up
```

3. navigate to `http://localhost:3000`


## To develop the project

- remove the previous devcontainer (if exist)
- open the folder in container

### Dotnet

- cd into `backend` and build the project

```bash
dotnet build
```

- try running the project

```bash
dotnet run
```

- continue developing

### React

- cd into `frontend`
- install the packages

```bash
npm i
```

- copy and rename `.env.sample` to `.env`
- update the `.env`

```bash
REACT_APP_API_URL=<backend_url> # http://localhost:5245
```

- run the project

```bash
npm start
```


