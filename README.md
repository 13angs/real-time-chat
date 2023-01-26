# Real-time chat

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


