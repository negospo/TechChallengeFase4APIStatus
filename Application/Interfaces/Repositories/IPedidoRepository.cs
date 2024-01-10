namespace Application.Interfaces.Repositories
{
    public interface IPedidoRepository
    {
        public IEnumerable<Application.DTOs.Output.Pedido> List(List<int> pedidoIds);
        public Application.DTOs.Output.Pedido Get(int pedidoId);
        public bool Save(Domain.Entities.PedidoStatus pedido);
        public bool Update(Domain.Entities.PedidoStatus pedido);
    }
}
