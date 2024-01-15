using Application.DTOs.Output;
using Application.Enums;
using Application.Interfaces.Repositories;

namespace Application.Implementations
{
    public class PedidoUseCase : Interfaces.UseCases.IPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoUseCase(IPedidoRepository pedidoRepository)
        {
            this._pedidoRepository = pedidoRepository;
        }

        public DTOs.Output.Pedido Get(int pedidoId)
        {
            var result = this._pedidoRepository.Get(pedidoId);
            if (result == null)
                throw new CustomExceptions.NotFoundException("Pedido não encontrado");

            return result;
        }

        public IEnumerable<DTOs.Output.Pedido> List(List<int> pedidoIds)
        {
            var result = this._pedidoRepository.List(pedidoIds);
            return result;
        }

        public IEnumerable<Pedido> ListByStatus(PedidoStatus status)
        {
            var result = this._pedidoRepository.ListByStatus(status);
            return result;
        }

        public bool Save(DTOs.Imput.Pedido pedido)
        {
            var entity = new Domain.Entities.PedidoStatus(
                 pedido.PedidoId.Value,
                 (Domain.Enums.PedidoStatus)pedido.Status.Value);

            return _pedidoRepository.Save(entity);
        }

        public bool Update(int pedidoId, Domain.Enums.PedidoStatus status)
        {
            var entity = new Domain.Entities.PedidoStatus(pedidoId, status);
            return _pedidoRepository.Update(entity);
        }
    }
}
