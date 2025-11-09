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
- Re
