# 🦷 GlobalSolutionRopz OdontoPrev Sprint 4 - API com C#, ML.NET e xUnit

## 📖 Sobre o Projeto

Este projeto tem como objetivo desenvolver uma solução inteligente para a **OdontoPrev**, utilizando uma API RESTful em C#. A aplicação gerencia operações de Pacientes, Dentistas, Clínicas, Agendamentos, Tabela de Preços, Contas a Receber/Pagar, além de detectar possíveis fraudes com o auxílio de **Machine Learning (ML.NET)**.
## 📐 Escopo

### Funcionalidades Principais:

- ✅ Gerenciamento de **Pacientes**, **Dentistas**, **Clínicas**, **Agendamentos**, **Tabela de Preços**, **Contas a Receber/Pagar**.
- ✅ Cadastro, leitura, atualização e exclusão (CRUD).
- ✅ Detecção automática de **fraudes** com ML.NET.
- ✅ Testes unitários, de integração e de sistema com **xUnit**.
- ✅ Documentação completa com **Swagger**.

## 🧠 Integração com ML.NET 

A API utiliza **ML.NET** para treinar um modelo de detecção de sinistros com base em padrões de agendamentos:

### 🔍 Como Funciona:
- Treinamento com CSV usando `FastTree` binário.
- Predição com base em:
  - Quantidade de agendamentos do paciente.
  - Quantidade de agendamentos do dentista.

## ✅ Testes Automatizados com xUnit

Foram desenvolvidos testes de:
- **Unidade:** para validar comportamentos isolados dos serviços e repositórios.
- **Integração:** validando conexão com o banco Oracle e endpoints REST.
- **Sistema:** simulação de fluxo completo (ex: agendamento com possível fraude).

> Todos os testes foram escritos com `xUnit` 

## 🧼 Aplicação de Clean Code e Princípios SOLID

### 🧹 Clean Code

- **Nomes claros e objetivos.**
- **Métodos pequenos e coesos.**
- **Reutilização de lógica.**
- **Separação de responsabilidades.**

### 🧱 Princípios SOLID

| Princípio | Aplicação |
|----------|-----------|
| **SRP** - Single Responsibility | Cada classe faz apenas uma coisa. Ex: `RepositoryPaciente` trata só de pacientes. |
| **OCP** - Open/Closed | Classes podem ser estendidas sem modificação. Ex: serviços validam novas regras via extensões. |
| **LSP** - Liskov Substitution | Interfaces e heranças respeitam substituição. |
| **ISP** - Interface Segregation | Interfaces específicas:`IPacienteRepository` etc. |
| **DIP** - Dependency Inversion | Controllers e serviços dependem de abstrações, usando injeção de dependência. |

## 🏗️ Arquitetura da API

Utilizamos arquitetura baseada em **Microservices**:

- 🔹 **Escalável**: cada funcionalidade pode ser isolada.
- 🔹 **Flexível**: serviços independentes entre si.
- 🔹 **Facilita deploys e testes.**
- 🔹 **Alta disponibilidade.**

## 📌 Endpoints CRUD

A API realiza operações CRUD com banco Oracle para os seguintes recursos:

- `GET /api/pacientes`
- `POST /api/pacientes`
- `PUT /api/pacientes/{id}`
- `DELETE /api/pacientes/{id}`

(Endpoints similares para dentistas, clínicas, agendamentos, fraudes, contas e preços)

## 🧪 Exemplo de Integração ML.NET (Treinamento e Predição)

```csharp
[HttpGet("treinar")]
public IActionResult TreinarModelo() { ... }

[HttpPost("verificar")]
public IActionResult VerificarSinistro([FromBody] VerificacaoSinistroDTO entrada) { ... }
```

## 🏭 Padrão de Criação: Simple Factory

```csharp
public class UsuarioFactory
{
    public static Usuario CriarUsuario(string nome, string email)
    {
        return new Usuario { Nome = nome, Email = email };
    }
}
```

## 🚀 Como Rodar a API

1. Clone o repositório:
```bash
git clone https://github.com/ThaiisRibeiro/GlobalSolutionRopz.git
cd GlobalSolutionRopz
```

2. Configure o banco Oracle no `Program.cs` e `DbContext.cs`.

3. Restaure as dependências:
```bash
dotnet restore
```

4. Execute o projeto:
```bash
dotnet run
```

5. Acesse o Swagger em `https://localhost:{porta}/swagger`

## 👥 Integrantes do Grupo

- **Thaís Ribeiro Asfur** (RM553870) 🎯  
- **Lucas Minozzo Bronzeri** (RM553745)  
- **Diego Costa Silva** (RM552648)
