namespace Application.Interfaces.UseCases
{
    public interface IPedidoUseCase
    {
        public IEnumerable<Application.DTOs.Output.Pedido> List(List<int> pedidoIds);
        public Application.DTOs.Output.Pedido Get(int pedidoId);
        public bool Save(DTOs.Imput.Pedido pedido);
        public bool Update(int pedidoId, Domain.Enums.PedidoStatus status);
    }
}
