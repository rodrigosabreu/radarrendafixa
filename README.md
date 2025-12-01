# 💹 Radar Renda Fixa – MVP

O **Radar Renda Fixa** é um MVP criado para demonstrar como um motor de ranking pode identificar as melhores oportunidades de renda fixa com base no prazo, perfil de risco e rentabilidade líquida simulada.

## 🚀 Tecnologias Utilizadas

### Backend (.NET 8)
- Minimal API
- Simulação de rentabilidade
- Tabela regressiva de IR
- CORS
- Servidor estático

### Frontend
- HTML, CSS, JavaScript
- Tema futurista verde

## 📂 Estrutura

RadarRendaFixa/
 ├── Contracts/
 ├── Domain/
 ├── Repositories/
 ├── Services/
 ├── wwwroot/
 │    ├── index.html
 │    └── ajuda.html
 ├── Program.cs
 └── README.md

## 🧪 Executar

dotnet run

Acesse:
http://localhost:50569/

## 📡 API

POST /ranking-renda-fixa

{
  "valor": 10000,
  "prazoEmDias": 720,
  "perfil": "Conservador"
}

## 👨‍💻 Autor

Rodrigo Abreu
