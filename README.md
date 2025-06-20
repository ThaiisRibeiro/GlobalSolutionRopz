# 🌡️ Ropz - Rede de Observação e Previsão de Zonas Quentes

## 📖 Sobre o Projeto

Com o avanço das mudanças climáticas, eventos de calor extremo têm se tornado cada vez mais frequentes, prolongados e intensos, trazendo sérias consequências à saúde pública. Grupos vulneráveis como crianças, idosos e pessoas com doenças crônicas são particularmente afetados, estando mais propensos a condições graves como desidratação, insolação e agravamento de doenças respiratórias.

Apesar da gravidade do problema, a maioria da população ainda não recebe alertas preventivos eficazes, nem tem acesso a informações claras sobre como agir diante de altas temperaturas. Essa lacuna de comunicação e prevenção aumenta o risco de impactos severos, tanto para indivíduos quanto para os sistemas de saúde.

## 🧠 Solução: Ropz – Sistema Inteligente de Alerta Climático

O Ropz é uma solução inteligente desenvolvida com foco em prevenir riscos à saúde causados por temperaturas elevadas. Trata-se de uma API RESTful construída com C#, ASP.NET Core e ML.NET, capaz de prever alertas de risco climático com base em dados meteorológicos históricos e em tempo real.

A aplicação utiliza algoritmos de Machine Learning supervisionado para treinar um modelo preditivo que considera variáveis como:

- 🌡️ Temperatura (em Celsius)
- 🗺️ Estado
- 📅 Mês
- 🕒 Dia da semana
- 📆 Ano

Com isso, o sistema consegue identificar padrões críticos de calor e emitir alertas personalizados, baseando-se na localização do usuário, prevenindo assim riscos à saúde por meio de ações informadas e proativas.

## 📐 Escopo

### ✅ Funcionalidades Principais

✅ Treinamento do modelo com dados climáticos históricos.  
✅ Predição de alerta com base em dados meteorológicos.  
✅ API RESTful desenvolvida em ASP.NET Core.  
✅ Uso de ML.NET para Machine Learning supervisionado.  
✅ Testes automatizados com xUnit.  
✅ Funcionalidades completas de CRUD para as seguintes classes: Usuário, Alerta e Mensagem.  
✅ Integração com RabbitMQ.


## 🧠 Integração com ML.NET
A API utiliza o ML.NET para treinar um modelo de classificação binária, que prevê se determinada condição climática gerará um alerta.

## 🔍 Como Funciona

Treinamento com CSV estruturado.  
Algoritmo: FastTree (classificação binária).  

### Variáveis de entrada:
- Temperatura (em Celsius)  
- Estado (por extenso)  
- Mês  
- Dia da semana  
- Ano  

### ✨ Resultado:
- Previsão de alerta (`true` ou `false`)  
- Probabilidade da previsão  

## 🔁 Endpoints da API

### 🔧 Treinamento do Modelo
```
GET /api/temperatura/treinar
```
Treina o modelo a partir de um arquivo CSV salvo em `ML/DadosTreinamento.csv`.

### 📊 Predição de Alerta
```
POST /api/temperatura/verificar
```
**Exemplo de Requisição:**
```json
{
  "temperatura": 32.5,
  "estado": "São Paulo",
  "mes": "Janeiro",
  "diaDaSemana": "Segunda",
  "ano": 2024
}
```
**Exemplo de Resposta:**
```json
{
  "alerta": true,
  "probabilidade": 0.87
}
```

## 🧪 Testes Automatizados com xUnit

O projeto conta com testes automatizados utilizando o framework xUnit.

### Tipos de Testes
✅ Testes de Unidade: validação de predições, comportamento de entrada, resposta do modelo.  
✅ Testes de Integração: chamadas reais aos endpoints da API simulando requisições POST e GET.  
✅ Testes de Sistema: fluxo completo de treinamento e predição.  

### Exemplo - Classe `AlertaTest`
```csharp
[Fact]
public void TipoMensagemVazio_DeveLancarExcecao()
{
    var alerta = new Alerta();
    var ex = Assert.Throws<ArgumentException>(() => alerta.tipo_mensagem = 0);
    Assert.Equal("Tipo de mensagem é obrigatório.", ex.Message);
}
```

### Exemplo - Classe `UsuarioTest`
```csharp
[Fact]
public void EmailSemArroba_DeveLancarExcecao()
{
    var usuario = new Usuario();
    var ex = Assert.Throws<ArgumentException>(() => usuario.email = "testeemail.com");
    Assert.Equal("E-mail inválido. Deve conter '@'.", ex.Message);
}
```

### Exemplo - Teste de Predição
```csharp
[Fact]
public void DeveRetornarAlerta_QuandoTemperaturaAlta()
{
    var builder = new TemperaturaModelBuilder();
    builder.TreinarModelo();

    var entrada = new TemperaturaModelBuilder.TemperaturaData
    {
        temperatura = 38.5f,
        estado = "Rio de Janeiro",
        mes = "Janeiro",
        diaDaSemana = "Segunda",
        ano = 2024
    };

    var resultado = builder.Prever(entrada);
    Assert.True(resultado.Probability > 0.5);
}
```


### Exemplo de Teste com xUnit
```csharp
[Fact]
public void DeveRetornarAlerta_QuandoTemperaturaAlta()
{
    var builder = new TemperaturaModelBuilder();
    builder.TreinarModelo();

    var entrada = new TemperaturaModelBuilder.TemperaturaData
    {
        temperatura = 38.5f,
        estado = "Rio de Janeiro",
        mes = "Janeiro",
        diaDaSemana = "Segunda",
        ano = 2024
    };

    var resultado = builder.Prever(entrada);

    Assert.True(resultado.Probability > 0.5);
}
```
## ✅ RabbitMQ

O projeto contém integração com o **RabbitMQ**, permitindo a troca de mensagens de forma eficiente e escalável.  

### ✔️ Demonstração de funcionamento

Abaixo seguem os prints que comprovam o correto funcionamento da integração com o RabbitMQ:  

![RabbitMQ funcionando](https://i.imgur.com/y76KN13.png)

![RabbitMQ configuração](https://i.imgur.com/qfsq1iE.png)  

![RabbitMQ conectado](https://i.imgur.com/9QLKjAN.png)  


## 🧱 Boas Práticas Aplicadas

### 🧼 Clean Code
- Métodos pequenos e reutilizáveis  
- Separação clara de responsabilidades  
- Nomenclaturas semânticas  

### 🧱 Princípios SOLID
| Princípio | Aplicação |
|----------|------------|
| SRP - Responsabilidade Única | Cada classe realiza uma função específica |
| OCP - Aberto/Fechado | Fácil de estender sem modificar |
| ISP - Segregação de Interfaces | Controladores e DTOs são específicos para seu propósito |
| DIP - Inversão de Dependência | Abstrações são utilizadas sempre que possível |

## 🚀 Como Rodar a API

Clone o repositório:
```bash
git clone https://github.com/ThaiisRibeiro/GlobalSolutionRopz.git
cd GlobalSolutionRopz
```

Restaure os pacotes:
```bash
dotnet restore
```

Execute a aplicação:
```bash
dotnet run
```
## 🛠️ Configurações Importantes

### 🔐 Configurar Chave da API OpenWeather
No arquivo `WeatherService.cs`, altere a seguinte linha para incluir sua chave da API:

```csharp
private readonly string _apiKey = "SUA_CHAVE_OPENWEATHER";
```

Você pode obter sua chave gratuita em: https://openweathermap.org/api

### 🗃️ Configurar Login e Senha do Banco de Dados


⚠️ Verifique se o arquivo `DadosTreinamento.csv` está em `ML/DadosTreinamento.csv`.




