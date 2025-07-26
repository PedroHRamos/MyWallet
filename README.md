# MyWallet

MyWallet � uma API desenvolvida em .NET 8 para gerenciar a carteira de investimentos de m�ltiplos usu�rios. O objetivo do projeto � fornecer funcionalidades para acompanhamento de rentabilidade, defini��o de carteira meta, compara��o com benchmarks e outras features relacionadas � gest�o de investimentos.

## Funcionalidades
- Cadastro e gerenciamento de usu�rios
- Controle de ativos e movimenta��es
- C�lculo de rentabilidade da carteira
- Defini��o de carteira meta
- Compara��o com benchmarks
- Relat�rios e indicadores de performance

## Como rodar o projeto com Docker

1. Certifique-se de ter o Docker instalado em sua m�quina.
2. No diret�rio do projeto, execute o comando abaixo para construir a imagem:

```sh
docker build -t mywallet-api .
```

3. Ap�s a build, execute o cont�iner:

```sh
docker run -d -p 8080:8080 --name mywallet-api mywallet-api
```

4. A API estar� dispon�vel em `http://localhost:8080`.

> Para ambientes de desenvolvimento, o acesso � via HTTP. Para produ��o, recomenda-se configurar HTTPS e vari�veis de ambiente adequadas.

## Tecnologias utilizadas
- .NET 8
- SQL Server
- Docker
- Kafka (para mensageria)

## Observa��es
- O projeto inclui integra��o com Swagger para documenta��o autom�tica dos endpoints.
- Para persist�ncia, configure a string de conex�o do SQL Server em `appsettings.json`.

---
