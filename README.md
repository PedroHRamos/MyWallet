# MyWallet

MyWallet é uma API desenvolvida em .NET 8 para gerenciar a carteira de investimentos de múltiplos usuários. O objetivo do projeto é fornecer funcionalidades para acompanhamento de rentabilidade, definição de carteira meta, comparação com benchmarks e outras features relacionadas à gestão de investimentos.

## Funcionalidades
- Cadastro e gerenciamento de usuários
- Controle de ativos e movimentações
- Cálculo de rentabilidade da carteira
- Definição de carteira meta
- Comparação com benchmarks
- Relatórios e indicadores de performance

## Como rodar o projeto com Docker

1. Certifique-se de ter o Docker instalado em sua máquina.
2. No diretório do projeto, execute o comando abaixo para construir a imagem:

```sh
docker build -t mywallet-api .
```

3. Após a build, execute o contêiner:

```sh
docker run -d -p 8080:8080 --name mywallet-api mywallet-api
```

4. A API estará disponível em `http://localhost:8080`.

> Para ambientes de desenvolvimento, o acesso é via HTTP. Para produção, recomenda-se configurar HTTPS e variáveis de ambiente adequadas.

## Tecnologias utilizadas
- .NET 8
- SQL Server
- Docker
- Kafka (para mensageria)

## Observações
- O projeto inclui integração com Swagger para documentação automática dos endpoints.
- Para persistência, configure a string de conexão do SQL Server em `appsettings.json`.

---
