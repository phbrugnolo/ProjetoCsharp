# 🎮 Project - Sistema de Torneio Pedra, Papel e Tesoura

Este projeto é uma aplicação desenvolvida em **C# com Entity Framework Core e SQLite**, que simula um sistema de **torneios de Pedra, Papel e Tesoura** entre usuários.

---

## 🚀 Funcionalidades

* 👤 Cadastro de usuários
* 🏆 Criação de torneios
* ⚔️ Sistema de batalhas (Pedra, Papel e Tesoura)
* 📊 Controle de resultados:

  * Vitórias
  * Derrotas
  * Empates
* 💾 Persistência de dados com SQLite
* ✔️ Validação de dados (Email, Telefone, Idade)

---

## 🧠 Lógica do Jogo

O sistema realiza batalhas automáticas contra a máquina:

* Pedra 🪨 vence Tesoura ✂️
* Tesoura ✂️ vence Papel 📄
* Papel 📄 vence Pedra 🪨

O resultado é salvo e atualizado no usuário.

---

## 🗂️ Estrutura do Projeto

```
Project/
│
├── Models/
│   ├── AppDbContext.cs   → Contexto do banco de dados
│   ├── User.cs           → Modelo de usuário
│   ├── Torneio.cs        → Modelo de torneio
│   ├── Battle.cs         → Sistema de batalhas
│   └── ValidaTel.cs      → Validação de telefone
│
├── Migrations/           → Migrações do banco
├── Program.cs            → Arquivo principal
├── app.db                → Banco SQLite
├── appsettings.json      → Configurações
```

---

## 🛠️ Tecnologias Utilizadas

* C#
* .NET
* Entity Framework Core
* SQLite
* Data Annotations (validações)

---

## 🧩 Modelos

### 👤 User

* Nome
* Email
* Telefone (com validação)
* Idade (mínimo 13 anos)
* Estatísticas (Vitória, Derrota, Empate)

---

### 🏆 Torneio

* Nome
* Descrição
* Premiação
* Data de criação

---

### ⚔️ Battle

* Jogada do usuário
* Jogada da máquina (aleatória)
* Resultado da partida
* Associação com usuário e torneio

---

## 📱 Validação de Telefone

Formato aceito:

```
(DDD) 99999-9999
ou
9999999999 / 99999999999
```

---

## 💾 Banco de Dados

O banco é configurado automaticamente com SQLite:

```csharp
optionsBuilder.UseSqlite("Data Source=app.db");
```

---

## ▶️ Como executar

1. Clone o repositório:

```
git clone https://github.com/seu-usuario/seu-repo.git
```

2. Acesse a pasta:

```
cd Project
```

3. Execute as migrações:

```
dotnet ef database update
```

4. Rode o projeto:

```
dotnet run
```

---

## 📌 Observações

* O sistema utiliza GUID como identificador único.
* As batalhas são geradas com aleatoriedade.
* As validações garantem integridade dos dados.

---




