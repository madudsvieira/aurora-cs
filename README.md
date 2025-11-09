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
- Publicação do artefato (drop) 
- Docker Build  
- Docker Push → ACR  

---

### **2️⃣ CD – Continuous Deployment**

- Release obtém a imagem publicada no **ACR**
- App Service baixa a imagem
- Reinicia o container
- Aplica variáveis de ambiente
- Valida via `/health`

Fluxo totalmente automatizado.

---

## ✅ Integrantes do Grupo

- **Felipe Prometti** — RM555174 — 2TDSPM  
- **Maria Eduarda Pires** — RM558976 — 2TDSPZ  
- **Samuel Damasceno** — RM558876 — 2TDSPM  

---

## ✅ Arquitetura e Estrutura do Código

Projeto baseado em **Clean Architecture + DDD**, separando responsabilidades entre camadas.

