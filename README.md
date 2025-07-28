# MyWallet

API em .NET 8 para gerenciamento de carteiras de investimentos de múltiplos usuários, com funcionalidades como cadastro, controle de ativos, cálculo de rentabilidade, carteira meta e relatórios.

## Como rodar localmente

- **Com Docker Compose:**  
   Certifique-se de ter Docker instalado e execute:  
   - `docker compose up -d --build`
   

- **Debug local com SQL Server Docker:**  
    Execute o contêiner do SQL Server:  
    - `docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Your_password123" -p 1433:1433 --name sql_server_container -d mcr.microsoft.com/mssql/server:latest`

Depois, execute o projeto no Visual Studio usando a opção `http`.

## Tecnologias

- .NET 8  
- SQL Server  
- Docker  
- Kafka (No Futuro)  

## Observações

- Documentação automática via Swagger.  
- Configure a string de conexão do SQL Server em `appsettings.json`.


