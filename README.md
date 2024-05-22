# Projeto: Microondas Digital

Este projeto simula o funcionamento de um microondas digital, permitindo controlar e monitorar o processo de aquecimento de alimentos via API. A aplicação foi desenvolvida em ASP.NET WebAPI com .NET Framework 4.8.1. O projeto inclui uma interface de cliente desenvolvida em React com Vite.

## Funcionalidades

### API Endpoints

- **GET /microwave/get**: Retorna o estado atual do microondas.
- **POST /microwave/start**: Inicia ou continua o aquecimento com parâmetros configuráveis no corpo.
- **POST /microwave/stop**: Pausa ou cancela o aquecimento.
- **POST /microwave/start-proc/{id}**: Inicia um procedimento de aquecimento predefinido.
- **GET /heatingProcedure/get**: Retorna todos os procedimentos de aquecimento cadastrados.
- **GET /heatingProcedure/get/{id}**: Retorna um procedimento de aquecimento específico.

## Tecnologias Utilizadas

- **ASP.NET WebAPI**
- **.NET Framework 4.8.1**
- **Hangfire** para execução de tarefas assíncronas.
- **Owin** para self-hosting.
- **Ninject** para injeção de dependências.
- **FluentValidation** para validação dos corpos de requisição.
- **SQL Server** para persistência de dados.

## Estrutura da Aplicação

### Camadas da API

1. **Negócio**

   - Modelos de entidades
   - Implementações de repositórios e serviços

2. **Infraestrutura**

   - Migrations
   - Contexto de dados
   - Contratos de repositórios e serviços

3. **Rede**
   - Controllers
   - InputModels/ViewModels

### Cliente

- Aplicação cliente desenvolvida em **React** utilizando **Vite** e estilizado com **tailwindcss**.

## Como Iniciar a Aplicação

### Servidor ASP.NET

1. **Configurar Connection String**

   - Edite o arquivo `Web.Config` para configurar a connection string apropriada para o seu ambiente SQL Server.

2. **Executar a Aplicação**
   - Abra a solução no Visual Studio.
   - Compile e execute o projeto.

### Cliente React

1. **Instalar Dependências**

   - No diretório do projeto React, execute:
     ```bash
     npm install
     ```

2. **Executar a Aplicação**
   - Ainda no diretório do projeto React, execute:
     ```bash
     npm run dev
     ```

## Pontos Observados Durante a Implementação

1. **WebSocket com SignalR**

   - Tentou-se implementar WebSocket com SignalR para evitar long polling, mas devido à falta de tempo e familiaridade, não foi possível concluir.

2. **Regras de Negócio**

   - Algumas regras de negócio foram implementadas com dúvidas sobre sua espeficiação, como o acréscimo de tempo acima de 2 minutos e a formatação em mm:ss apenas para valores menores que 100. Essas dúvidas foram marcadas com comentários no código.

3. **TODO**
   - A injeção de dependência do repositório de procedimento de aquecimento no serviço de microondas estava impedindo o funcionamento correto do Hangfire. Foi usada injeção no controller como workaround temporário.
   - Foram utilizadas Exception base para controle de fluxo, ao invés de exceptions customizadas e filtros para padronização de resposta de tratamento.
   - Foi feito uso de um objeto estático do microondas para simular um singleton, mas será necessário que cada usuário interaja com uma instância separada de microondas em futuras etapas do projeto.
   - Reestruturar projeto do cliente de forma mais componentizada.
