# CloudTasker ☁️✅

**CloudTasker** is a serverless task manager built with **Azure Functions** and **Cosmos DB**.  
It allows you to create, update, delete, and list tasks in a fully cloud-native environment.

---

## ✨ Features

- 📌 Create, read, update, and delete tasks
- 🗂️ Persistent storage with Azure Cosmos DB
- ⚡ Serverless execution with Azure Functions
- 🔑 Config-driven setup via `appsettings.json`
- 🌍 Ready for deployment to Azure Functions

> 🧪 Still in development – new features and refinements are being added iteratively.

---

## 🏗️ Project Structure

The backend follows a lightweight **clean separation** of responsibilities:

- `Contracts` → DTOs and mapping configuration (e.g., `TaskDtos`, `Mapping`)  
- `Data` → Repository interfaces and implementations (`ITaskRepository`, `CosmosTaskRepository`, `InMemoryTaskRepository`)  
- `Functions` → Azure Functions entry points (`TaskFunctions`)  
- `Infra` → Infrastructure helpers (e.g., `HttpJson`)  
- `Models` → Core business entities (`TaskItem`)  
- `Services` → Reserved for additional business logic/services  

---

## 🛠 Tech Stack

- **Serverless**: Azure Functions (.NET Isolated)
- **Database**: Azure Cosmos DB (SQL API)
- **Language**: C# 12 / .NET 8

---

## 📚 API Documentation

- 🌐 **Live Docs (Postman):** [CloudTasker API Docs](https://www.postman.com/santidev21/cloudtasker-api-docs/collection/y2tugv2/cloudtasker-api)  
- 📦 **Collection JSON (local):** [`docs/CloudTasker-API.postman_collection.json`](./backend/CloudTasker.Functions/CloudTasker.Api/Docs/CloudTasker-API.postman_collection.json)

---

## 🚀 Getting Started

### 🧩 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Azure Functions Core Tools](https://learn.microsoft.com/azure/azure-functions/functions-run-local)
- An [Azure Cosmos DB account](https://learn.microsoft.com/azure/cosmos-db/)

### ⚙️ Local Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/santidev21/cloudtasker.git
   cd cloudtasker
```
