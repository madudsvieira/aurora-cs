# 🚀 AuroraTrace – DevOps (Resumo da API .NET)

API desenvolvida em **.NET 9** para o gerenciamento de motos e pátios do projeto AuroraTrace.  
Esta solução utiliza **Docker**, **Azure DevOps CI/CD**, **Azure App Service** e **CosmosDB (API MongoDB)**.

---

## ✅ Arquitetura (Visão Rápida)

- **.NET 9 + Docker**
- **Azure DevOps – CI/CD**
- **Azure Container Registry (ACR)**
- **Azure App Service (Linux)**
- **CosmosDB – API MongoDB**
- **JWT (Firebase + Local)**
- **Swagger**
- **Health Check `/health`**

---

## ✅ Pipeline no Azure DevOps

### **1️⃣ CI – Continuous Integration**

O pipeline executa automaticamente:

- Restore  
- Build  
- Test  
- Docker Build  
- Docker Push → ACR  
- Publicação do artefato (drop) da sprint ✅  

---

### **2️⃣ CD – Continuous Deployment**

- A Release obtém a imagem no **ACR**
- O App Service baixa a nova versão da imagem
- Reinicia o container
- Aplica variáveis de ambiente
- Valida o endpoint `/health`

Processo totalmente automatizado após cada push.

---

## ✅ Conexão com o Banco de Dados (CosmosDB – Mongo API)

A API se conecta ao banco através das **Connection Strings do Azure App Service**:

```
Azure Portal  
→ App Service  
→ Configuration  
→ Connection Strings
```

A variável utilizada é:

```
MongoDB
```

O App Service injeta essa connection string dentro do container, e a aplicação lê via:

```csharp
builder.Configuration.GetConnectionString("MongoDB");
```

---

## ✅ Integrantes do Grupo

- **Felipe Prometti** — RM555174 — 2TDSPM  
- **Maria Eduarda Pires** — RM558976 — 2TDSPZ  
- **Samuel Damasceno** — RM558876 — 2TDSPM  

---

## ✅ Arquitetura do Código

```plaintext
📦 src
 ┣ 📂 Api             → Controllers, JWT, Swagger e Health
 ┣ 📂 Application     → DTOs e Services
 ┣ 📂 Domain          → Entidades, Enums e Value Objects
 ┗ 📂 Infrastructure  → MongoDB, Repositórios e Contexto
📦 AuroraTrace.Tests  → Testes automatizados
```

---

## ✅ Endpoints Principais

| Endpoint   | Descrição                         |
|------------|-----------------------------------|
| `/swagger` | Interface de documentação e testes |
| `/health`  | Verificação de saúde da API        |

---

## ✅ Como Rodar Localmente

### 1. Subir MongoDB com Docker

```bash
docker run -d -p 27017:27017 --name aurora-mongo mongo
```

### 2. Executar a API

```bash
dotnet run --project src/Api
```

### 3. Configuração local (`appsettings.json`)

```json
"ConnectionStrings": {
  "MongoDB": "mongodb://localhost:27017"
}
```

---

**AuroraTrace – Challenge FIAP 2025 | Sprint 4**
