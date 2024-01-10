namespace Domain.Entities
{
    public class PedidoStatus
    {
        public PedidoStatus(int pedidoId, Enums.PedidoStatus status)
        {
            this.PedidoId = pedidoId;
            this.Status = status;
            this.Validate();
        }

        public void Validate()
        {
            if (this.PedidoId <=0)
                throw new CustomExceptions.BadRequestException("O Id do pedido é inválido");
        }

        public int PedidoId { get; private set; }

        public Enums.PedidoStatus Status { get; private set; }

        public void AtualizaStatusPedido(Enums.PedidoStatus status)
        {
            this.Status = status;
        }

    }
}
