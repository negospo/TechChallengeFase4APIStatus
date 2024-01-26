Feature: Domain Pedido Status
  Como um usuário do sistema
  Eu quero gerenciar o status de pedidos
  Para que eu possa manter o controle sobre o estado dos pedidos

  Scenario: Criar um PedidoStatus válido
    Given que eu crio um PedidoStatus com um PedidoId válido e um status
    Then o PedidoStatus é criado com sucesso

  Scenario: Tentar criar um PedidoStatus com PedidoId inválido
    Given que eu crio um PedidoStatus com um PedidoId inválido
    Then uma exceção de 'BadRequestException' com a mensagem 'O Id do pedido é inválido' deve ser lançada
