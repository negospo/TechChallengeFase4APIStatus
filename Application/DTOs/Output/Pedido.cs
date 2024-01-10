namespace Application.DTOs.Output
{
    public class Pedido
    {
        public int PedidoId { get; set; }

        public Enums.PedidoStatus Status { get; set; }
    }
}
