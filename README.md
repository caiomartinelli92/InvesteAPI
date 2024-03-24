# InvesteAPI
O objetivo desta API é simular como um sistema simples de investimentos funciona. A ideia é aplicar os conhecimentos do curso de ASP.Net do prof. Macoratti.
Para esta API as tecnologias usadas são o ASP.Net, Entity Framework, linguagem C# e banco de dados MySQL.

## Endpoints

### Métodos GET

#### investeapi/todosinvesimentos
Retorna os 100 investimentos mais recentes realizados.

#### investeapi/rendafixa
Retorna os 100 investimentos de renda fixa mais recentes realizados.

#### investeapi/rendavariavel
Retorna os 100 investimentos de renda variável mais recentes realizados.

#### investeapi/invesimento/id
Retorna o investimento específico em detalhes.

#### investeapi/taxaderentabilidade
Retorna a taxa de rentabilidade dos investimentos.

#### investeapi/regras
Retorna as regras de investimentos


### Métodos POST
Todos os invesmentos tem rendimento de 15% ao ano.

#### investeapi/criar/rendafixa
Cadastra um novo investimento de renda fixa a partir dos seguintes dados:
* Investimento Inicial;
* Aporte mensal;
* Data Inicial;
* Tempo de vigência.
Ao cadastrar com sucesso, retorna os dados do investimento e o valor estimado de resgate ao final da vigência.

#### investeapi/criar/rendavariavel
Cadastra um novo investimento de renda variável a partir dos seguintes dados:
Investimento Inicial;
Aporte mensal;
Data Inicial;
Ao cadastrar com sucesso, retorna os dados do investimento e o valor estimado de resgate ao final da vigência.


### Métodos PUT
#### investeapi/editar
Edita os dados de um investimento já feito. A data inicial é o único dado que não pode ser alterado.


### Métodos DELETE
#### investeapi/resgatar
Realiza o resgate do investimento e o remove da lista de investimentos. Caso o investimento tenha sido feito a menos de um mês da data atual, não poderá ser resgatado.
