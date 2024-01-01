# Accounting

**Descrição do Projeto:**
Sistema para registro de entradas e saídas de valores, com funcionalidade para calcular saldos diários.

## Arquitetura da Solução

A aplicação Accounting foi cuidadosamente desenvolvida em .NET Core 8, considerando os princípios da arquitetura Clean Code. A arquitetura limpa prioriza a legibilidade, manutenibilidade e escalabilidade do código, resultando em um sistema robusto e fácil de evoluir.

### Componentes Principais

1. **API (Accounting.API):**
   - A API é o ponto central para interações externas, responsável por registrar transações e gerar relatórios de saldos diários.
   - Estruturada nas camadas Application, Core e Infrastructure, a API segue as melhores práticas de desenvolvimento.

2. **Azure Function (Accounting.TransactionProcessor):**
   - A Function Azure desempenha um papel crucial, processando transações assíncronas da API e atualizando o MongoDB com os saldos diários correspondentes.

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

- A aplicação é submetida a um rigoroso conjunto de testes unitários utilizando xUnit, garantindo robustez e cobertura completa.

A arquitetura da aplicação Accounting reflete nosso compromisso com a excelência técnica e a entrega de uma solução eficaz, escalável e de alta qualidade.

## Execução da Aplicação

### Ambiente Local

1. **Pré-requisitos:**
   - [.NET Core SDK](https://dotnet.microsoft.com/download)
   - [Docker](https://www.docker.com/get-started)

2. **Configuração:**
   ```bash
   # Clone o repositório
   git clone https://github.com/seu-usuario/accounting.git
   cd accounting
