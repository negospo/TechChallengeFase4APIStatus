Feature: UseCase Pedido Status

Scenario: Listar pedidos por IDs
  Given Eu tenho os seguintes IDs de pedido: 1,2,3
  When Eu solicito a lista desses pedidos
  Then Os pedidos correspondentes aos IDs são retornados

Scenario: Listar pedidos com um determinado status
  Given O status do pedido é 'Recebido'
  When Eu solicito a lista de pedidos com esse status
  Then Todos os pedidos com status 'Recebido' são retornados

Scenario: Obter detalhes de um pedido específico
  Given O ID do pedido é 1
  When Eu solicito os detalhes desse pedido
  Then Os detalhes do pedido com ID 1 são retornados

  Scenario: Obter detalhes de um pedido inválido
    Given O ID do pedido é 5
    When Eu solicito os detalhes desse pedido
    Then Uma exceção de 'NotFoundException' deve ser lançada

Scenario: Salvar um pedido
  Given Eu tenho um novo pedido com ID 5 e status 'EmPreparacao'
  When Eu tento salvar esse pedido
  Then O pedido é salvo com sucesso

Scenario: Atualizar o status de um pedido existente
  Given O pedido com ID 3 precisa ter seu status atualizado para 'Pronto'
  When Eu atualizo o status desse pedido
  Then O status do pedido é atualizado com sucesso
