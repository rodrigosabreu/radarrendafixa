# 💹 Radar Renda Fixa – MVP  
[![.NET](https://img.shields.io/badge/.NET_8-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)]()  
[![Build Status](https://img.shields.io/badge/Build-Passing-brightgreen?style=for-the-badge)]()  
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg?style=for-the-badge)]()  
[![Status](https://img.shields.io/badge/Status-MVP-blue?style=for-the-badge)]()  

---

## 📘 Sobre o Projeto

O **Radar Renda Fixa** é um MVP que escaneia ofertas de renda fixa e gera um **ranking inteligente** baseado em:

- Perfil do investidor  
- Risco do título  
- Rentabilidade líquida simulada  
- Prazo solicitado  
- Liquidez diária  

Ele foi desenvolvido como um desafio pessoal para criar um produto funcional com backend + frontend em **menos de um final de semana**.

---

## 🚀 Tecnologias Utilizadas

### 🖥 Backend – .NET 8
- Minimal API  
- Injeção de dependência  
- Regras de simulação financeira  
- Tabela regressiva de IR  
- Motor de score  
- CORS habilitado  
- Servidor de arquivos estáticos  

### 🌐 Frontend
- HTML + CSS + JavaScript  
- Tema futurista verde  
- Tabela dinâmica  
- Página de ajuda detalhada  

---

## 📂 Estrutura do Projeto

```
RadarRendaFixa/
 ├── Contracts/        # DTOs de requisição e resposta
 ├── Domain/           # Entidades e enums
 ├── Repositories/     # Repositório in-memory
 ├── Services/         # Regras de negócio e simulação
 ├── wwwroot/          # Frontend estático
 │    ├── index.html
 │    └── ajuda.html
 ├── Program.cs        # Endpoints e configuração da API
 └── README.md
```

---

## ▶ Como Rodar

### 1. Clonar o repositório:
```bash
git clone https://github.com/seu-usuario/seu-repo.git
cd RadarRendaFixa
```

### 2. Rodar o backend:
```bash
dotnet run
```

### 3. Acessar o frontend:
```
http://localhost:50569/
```

---

## 📡 Endpoints Disponíveis

### ✔ Healthcheck
```
GET /health
```
Retorno:
```json
{ "status": "ok" }
```

### ✔ Ranking
```
POST /ranking-renda-fixa
```

Body de exemplo:
```json
{
  "valor": 10000,
  "prazoEmDias": 720,
  "perfil": "Conservador"
}
```

---

## 🧠 Regras de Negócio

### 🔹 SimuladorRendaFixaService
- Cálculo de juros compostos  
- IR regressivo  
- Valor líquido  
- Rentabilidade anualizada  

### 🔹 RankingService
- Filtragem por perfil  
- Cálculo de risco (Baixo / Médio / Alto)  
- Score final ponderado por:  
  - retorno  
  - risco  
  - prazo solicitado  
  - liquidez diária  

### 🔹 TabelaIrService
| Prazo (dias) | IR |
|--------------|------|
| até 180 | 22,5% |
| 181–360 | 20% |
| 361–720 | 17,5% |
| > 720 | 15% |

---

## 🖥 Frontend

### `index.html`
- Formulário  
- Chamada da API via Fetch  
- Tabela futurista com ranking  

### `ajuda.html`
- Guia completo da interface  
- Explicação dos campos  
- Glossário  

---

## 🛠 Melhorias Planejadas (Roadmap)

- Integração com dados reais (Tesouro Direto, corretoras)
- Favoritos e histórico
- Dashboard avançado
- Deploy em AWS
- Métricas adicionais (volatilidade, drawdown, rating)
- Página pública de comparador

---

## 🤝 Contribuições

Pull requests, issues e sugestões são bem-vindos.  
Esse projeto é um MVP e tem **muito espaço para evolução**.

---

## 📄 Licença

Este projeto está sob a licença **MIT**.

---

## 👨‍💻 Autor

**Rodrigo Abreu**  
Tech Lead | Engenharia de Software | Investimentos  
LinkedIn: https://www.linkedin.com/in/seu-perfil  
GitHub: https://github.com/seu-usuario
