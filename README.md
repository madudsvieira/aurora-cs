# AuroraTrace - Challenge FIAP 2025

---

## Visão Geral da Solução

Nossa solução é uma aplicação completa para gerenciar as motos dentro dos pátios da Mottu, controlando em que setor elas estão, seus status (Disponível, Manutenção, Ocupada) e seus eventos de entrada/saída.

A solução é composta por três repositórios principais:

| Repositório | Tecnologia | URL                                                                                   |
| :--- | :--- |:--------------------------------------------------------------------------------------|
| **Mobile App** | React Native/Expo | [Mobile](https://github.com/Challenge2025-Aurora/aurora-mobile) |
| **API Java** | Spring Boot | [Java](https://github.com/Challenge2025-Aurora/challenge2025-java) |
| **API C#** | .NET 9 / MongoDB | [C#](https://github.com/Challenge2025-Aurora/aurora-cs) |

---

## Integrantes do Grupo

- **Felipe Prometti** - RM555174 - 2TDSPM  
- **Maria Eduarda Pires** - RM558976 - 2TDSPZ  
- **Samuel Damasceno** - RM558876 - 2TDSPM  

---

## Arquitetura e Estrutura do Código

O projeto segue os princípios de **Clean Architecture** e **Domain Driven Design (DDD)**, garantindo alta manutenibilidade e clareza na separação de responsabilidades entre as camadas.

```plaintext
📦 src
 ┣ 📂 Api             -> Controllers e rotas, incluindo JWT e /health
 ┣ 📂 Application     -> DTOs, serviços e lógica de aplicação
 ┣ 📂 Domain          -> Entidades, enums e value objects
 ┗ 📂 Infrastructure  -> Repositórios e conexão com o banco MongoDB
 📦 AuroraTrace.Tests
```

---

## Detalhes da Arquitetura

- **Entidades Ricas (no Domain)**: Entidades como `Moto` e `Pátio` encapsulam lógica de negócio (ex: `AtualizarStatus`), com `private setters` para preservar a integridade dos dados.
- **Enums e Value Objects**: Mostram conceitos imutáveis e padronizados (`StatusMoto` e `Placa`)
- **MongoDB**: A aplicação agora usa **MongoDB** como banco de dados principal, substituindo o Oracle. Foram adicionados **dados iniciais automáticos (seed)** que populam o banco ao iniciar a API para testes locais
- **Health Endpoint**: Implementado o endpoint `/api/health` para verificar a saúde da aplicação e a conexão com o banco.
- **Autenticação JWT**: Todas as rotas protegidas exigem um token JWT válido, gerado e validado pela própria API.
- **Swagger**: O Swagger UI permite autenticação JWT diretamente, facilitando o teste de endpoints protegidos.

---

## Tecnologias Usadas (.NET)

| Categoria | Tecnologia |
| :--- |:----------------------------|
| **Linguagem** | C# / .NET 9 |
| **Banco de Dados** | MongoDB |
| **Mapeamento / ORM** | MongoDB.Driver |
| **Arquitetura** | Clean Architecture + DDD |
| **Autenticação** | JWT |
| **Testes** | xUnit |
| **Documentação** | Swagger |
| **Monitoramento** | Health |

---

## Como Rodar o Projeto

### Pré-requisitos

- .NET SDK 9.0 instalado.  
- Docker instalado (para executar o MongoDB localmente).

---

### 1. Clonar o projeto

```bash
git clone https://github.com/Challenge2025-Aurora/aurora-cs.git
```

---

### 2. Rodar o MongoDB localmente

Com Docker instalado, execute:

```bash
docker run -d -p 27017:27017 --name aurora-mongo mongo:latest
```

Isso iniciará um container com o MongoDB disponível em `mongodb://localhost:27017`.

---

### 3. Configurar o `appsettings.json`

```json
"ConnectionStrings": {
  "MongoDB": "mongodb://localhost:27017"
},
"JwtSettings": {
  "Key": "chave_local_super_secreta_para_testes_1234567890123456",
  "Issuer": "AuroraTraceAPI",
  "Audience": "AuroraTraceClients",
  "ExpirationMinutes": 60
}
```

---

### 4. Executar a API

```bash
cd src/Api
dotnet run
```

A API iniciará e criará automaticamente os dados iniciais no banco MongoDB.

---

### 5. Acessar o Swagger

```bash
http://localhost:5002/swagger
```

Dentro do Swagger, clique em Authorize e insira o token JWT no formato:

`Bearer {seu_token}`

Você pode gerar esse token dentro do próprio swagger ou acessando `POST /api/auth/login
` e enviando um corpo JSON assim:

```bash
{
  "userId": "user-teste"
}
```

O valor de userId pode ser qualquer string — ele serve apenas como identificador simbólico para gerar o token.

Copie o token e, no Swagger, clique em Authorize (ícone de cadeado no topo).
Cole no formato:

```bash
Bearer [token]
```

---

### 6. Verificar o Health Check

```bash
http://localhost:5002/api/health
```

Se tudo estiver configurado corretamente, o endpoint retornará o status de funcionamento da API e da conexão com o banco.

---

## Testes Automatizados

A área de testes usa:

- xUnit para testes unitários e de integração
- Mongo2Go para um banco MongoDB temporário de teste
- WebApplicationFactory para levantar a API em ambiente isolado

### Executar todos os testes

```bash
dotnet test
```

Os testes verificam:

- Geração e validação de tokens JWT
- Endpoints principais (/api/moto, /api/patio)
- Seed e conexão do MongoDB
- Comportamento esperado da API em cenários reais

---

**AuroraTrace - Challenge FIAP 2025 | Sprint 4**
