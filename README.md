# Accounting

Sistema para registro de entradas e saídas de valores, com funcionalidade para calcular e retornar saldos diários e o saldo atual.

## Arquitetura da Solução

A aplicação Accounting foi desenvolvida em .NET Core 8, considerando os princípios da arquitetura Clean Code. A arquitetura limpa prioriza a legibilidade, manutenibilidade e escalabilidade do código, resultando em um sistema robusto e fácil de evoluir.

### Componentes Principais

1. **API (Accounting.API):**
   - A API é o ponto central para interações externas, responsável por registrar transações e gerar relatórios de saldos diários e retornar o saldo atual.

2. **Azure Function (Accounting.TransactionProcessor):**
   - A Function Azure desempenha um papel crucial, processando transações assíncronas da API e atualizando o MongoDB com os saldos diários correspondentes.

## Camadas da Arquitetura

### Camada API:
- **Responsabilidades:**
  - Lida com solicitações HTTP e fornece respostas.
  - Mapeia parâmetros da solicitação para objetos de domínio e chama serviços apropriados.
  - Atua como interface de comunicação externa da aplicação.

### Camada Application:
- **Responsabilidades:**
  - Contém lógica de aplicação específica.
  - Orquestra serviços do domínio para realizar operações mais complexas.
  - Mapeia objetos de entrada e saída para objetos de domínio.

### Camada Core (Domínio):
- **Responsabilidades:**
  - Representa o cerne da aplicação, independente de qualquer detalhe de implementação.
  - Define entidades, agregados, valor-objetos e interfaces de repositório.
  - Não possui dependências externas.

### Camada Infrastructure:
- **Responsabilidades:**
  - Implementa detalhes de infraestrutura, como acesso a banco de dados, serviços externos, logging, etc.
  - Contém implementações de repositórios definidos no domínio.
  - Fornece meios de comunicação com o mundo externo.
  - Depende das camadas mais internas, mas não contém lógica de negócios.

![image](https://github.com/rodrigonunesvn/Accounting/assets/51245767/289f3f2b-fd66-4466-816e-f7469667a0f0)

### Componentes de Infraestrutura

- **Banco de Dados SQL Server:**
  - Utilizado para registrar detalhes de todas as transações realizadas na aplicação.

- **Redis (Cache):**
  - O Redis é empregado como um sistema de cache eficiente, acelerando a recuperação do saldo atual.

- **MongoDB:**
  - Escolhido para armazenar os saldos diários, oferecendo flexibilidade e escalabilidade.

- **Azure Service Bus:**
  - A fila do Azure Service Bus atua como canal de comunicação eficiente entre a API e a Function Azure.

### Testes

- A aplicação é submetida a um conjunto de testes unitários utilizando xUnit, garantindo robustez e cobertura completa.

# Executando a Aplicação Localmente

## Requisitos

1. **SDK do .NET Core:**
   - Baixe e instale o SDK do .NET Core em [dotnet.microsoft.com](https://dotnet.microsoft.com/download).

2. **Docker:**
   - Instale o Docker a partir de [docker.com](https://www.docker.com/products/docker-desktop).

3. **Ferramentas Adicionais:**
   - Certifique-se de ter o Git e o cURL instalados.

## Passos

### 1. Clone o Repositório
   ```bash
   git clone https://github.com/rodrigonunesvn/Accounting
   ```
### 2. Acesse o Diretório do Projeto
   ```bash
   cd Accounting
   ```

### 3. Ajuste o arquivo Accounting.API\appseggings.json 
Esse arquivo é um arquivo de configuração que geralmente é utilizado em projetos .NET Core. Ele contém informações sensíveis, como strings de conexão para bancos de dados e serviços. Aqui está um guia passo a passo para configurar este arquivo:

ConnectionStrings:

Preencha as strings de conexão para os diferentes serviços:
SQLServerConnectionString: Forneça a string de conexão para o SQL Server.
ServiceBusConnectionString: Forneça a string de conexão para o Azure Service Bus.
MongoDBConnectionString: Forneça a string de conexão para o MongoDB.
ServiceBusQueueName:

Defina o nome da fila do Azure Service Bus que será usada para comunicação assíncrona.
MongoDBDatabaseName:

Especifique o nome do banco de dados MongoDB que será usado.
Logging:

Configure os níveis de log para diferentes categorias. Neste exemplo, o nível padrão é "Information", e o nível para mensagens relacionadas ao Microsoft.AspNetCore é definido como "Warning".
AllowedHosts:

Especifica quais hosts estão autorizados a acessar a aplicação. O valor "*" permite que qualquer host acesse. Você pode ajustar isso conforme necessário para a segurança da sua aplicação.
Aqui está uma versão preenchida do arquivo de configuração:

### 3. Configuração do Docker   
   Inicialize os serviços do Docker necessários para a aplicação.
   ```bash
   docker-compose up -d
   ```
	
### 4. Build e Execução da Aplicação
No diretório do projeto principal:
   ```bash
   cd src/Accounting.API
   dotnet build
   dotnet run
   ```
	
Acesse a API em http://localhost:5000.

## Utilização da Aplicação

### APIs

![image](https://github.com/rodrigonunesvn/Accounting/assets/51245767/91836022-58eb-4ee0-af86-0b2b978bf4f1)

1. **Registrar Transação de Entrada:**

   curl -X POST \
     http://localhost/api/transactions/entry \
     -H 'Content-Type: application/json' \
     -d '{
       "amount": 100.00
     }'

2. **Registrar Transação de Saída:**

   curl -X POST \
     http://localhost/api/transactions/exit \
     -H 'Content-Type: application/json' \
     -d '{
       "amount": 50.00
     }'

3. **API de saldo diário:**

   curl -X GET \
     'http://localhost/api/reports/DailyBalance?startDate=2023-01-01'&endDate=2023-12-01

4. **API de saldo atual:**

   curl -X GET \
     'http://localhost/api/reports/CurrentBalanca
