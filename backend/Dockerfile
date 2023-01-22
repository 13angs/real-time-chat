# initial stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
LABEL Maintainer="13angs" \
    VERSION=v0.0.1
WORKDIR /backend
ENV ASPNETCORE_URLS http://+:5000
EXPOSE 5000

# build stage for dotnetcore
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS dotnet-build
WORKDIR /src
COPY ./backend.csproj ./
RUN dotnet restore "backend.csproj"
COPY ./ ./
RUN dotnet publish "backend.csproj" -c Release -o /backend/publish

# runtime stage
FROM base AS runtime
EXPOSE 5000
ENV TZ=Asia/Bangkok
RUN apk update && \
    apk add --no-cache tzdata
COPY --from=dotnet-build /backend/publish /backend

CMD ["dotnet", "/backend/backend.dll"]
# CMD ["nginx", "-g", "daemon off;", "&"]