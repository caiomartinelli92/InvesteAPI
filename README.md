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

#### investeapi/investimentosresgatados
Retorna os 100 investimentos resgatados mais recentes, independente do tipo.

#### investeapi/investimento/id
Retorna o investimento específico em detalhes.


#### investeapi/regras
Retorna as regras de investimentos.


### Métodos POST
Todos os invesmentos tem rendimento de 15% ao ano.

#### investeapi/criar/rendafixa
Cadastra um novo investimento de renda fixa a partir dos seguintes dados:
* Tipo de investimento;
* Investimento Inicial;
* Aporte mensal;
* Data Inicial;
* Tempo de vigência(em meses).
Ao cadastrar com sucesso, retorna os dados do investimento e o valor estimado de resgate ao final da vigência.

#### investeapi/criar/rendavariavel
Cadastra um novo investimento de renda variável a partir dos seguintes dados:
* Tipo de investimento;
* Investimento Inicial ("F" para renda fixa e "V" para renda variável);
* Aporte mensal;
* Data Inicial;
* Tempo de vigência(em meses),
* Data Final;
* Resgatado.
Ao cadastrar com sucesso, retorna os dados do investimento, o valor estimado de resgate ao final da vigência e data final estimada.


### Métodos PUT
#### investeapi/editar
Edita os dados de um investimento já feito. A data inicial é o único dado que não pode ser alterado. Ao alterar o investimento, a data final, vigência e valor a ser resgatado são recalculados.

#### investeapi/resgatar
Realiza o resgate do investimento e o remove da lista de investimentos. Caso o investimento tenha sido feito a menos de um mês da data atual, não poderá ser resgatado.
