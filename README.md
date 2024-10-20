
# Calculadora CDB

Pequeno projeto feito em Angular 17 e .Net 8 que tem como intuito calcular valores de rendimento do CDB.
## Documentação da API

#### Calcula o rendimento CDB

```http
  POST /Calculator/cdb
```

| Parâmetro   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `value` | `double` | Valor a ser calculado. Deve ser maior que R$ 0,00. **Obrigatório**. |
| `interval` | `int` | Intervalo em meses. Deve ser maior que 1. **Obrigatório**.  |


Exemplo de requisição:
```json
{
  "value": 10,
  "interval": 10
}
```

Exemplo de resposta:
```json
{
  "grossValue": 11.01,
  "netValue": 10.81
}
```
## Preparação do ambiente

- **Webapp**

O primeiro passo a se fazer é acessar a pasta raiz do projeto (padrão *TesteB3.Service/TesteB3.Webapp*) no terminal e executar o comando abaixo:

```bash
  npm i
```

Após a completa instalação das dependências necessárias, basta executar no terminal o comando responsável por iniciar a aplicação:

```bash
  ng serve -o
```

- **API**

No Visual Studio, clique com o botão direito sobre a solução "TesteB3.Service" e depois na opção *Configure Startup Projects*. O projeto *TesteB3.Api* deve ser escolhido como "Single startup project"; após isso altere a opção de inicialização do serviço para *IIS Express* e tecle F5.
## Execução do projeto

Após devidamente configurado o projeto estará pronto para uso, tanto pela aplicação Web quanto pela API, via swagger.
