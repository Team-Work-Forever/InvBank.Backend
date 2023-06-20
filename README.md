# Execução do projeto

# De forma a correr o servidor com https, deverá criar um certificado local

```bash
dotnet dev-certs https -ep certs\.aspnet\https\aspnetapp.pfx -p supersecret
dotnet dev-certs https --trust
```

## Execução do Projecto Backend

```bash
docker compose up --build -d
```
