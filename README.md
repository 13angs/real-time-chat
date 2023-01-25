# Real-time chat

## Run the project

1. run the project using `docker-compose`

```bash
docker compose up

# or

docker-compose up
```

2. navigate to `http://localhost:3000/chat`

3. open the same in another tab

4. try typing on one of theme

5. see if the text exist on both tab or not

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


