# MyWallet

.NET 8 API for managing investment portfolios for multiple users, with users registration, assets control, profitability calculation, target portfolio, and reports.

## How to run locally

- Clone the project
- Enter the `MyWallet/MyWalletWebAPI` directory
- Open the terminal in the current directory

- **To run with Docker Compose:**
Make sure Docker is installed and run:
    - `docker compose up -d --build`
    - Access `http://localhost:8080/swagger/index.html`
- **To debug with SQL Server in a container:**
    - `docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Your_password123" -p 1433:1433 --name sql_server_container -d mcr.microsoft.com/mssql/server:latest`
    - Then, run the project in Visual Studio using the `http` option.


## Technologies

- .NET 8
- SQL Server
- Docker


## Notes

- Automatic documentation via Swagger.
- Configure the SQL Server connection string in `appsettings.json`.

