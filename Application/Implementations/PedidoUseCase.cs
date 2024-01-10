using Application.Interfaces.Repositories;
using Application.Interfaces.UseCases;

namespace Application.Implementations
{
    public class PedidoUseCase : Interfaces.UseCases.IPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoUseCase(IPedidoRepository pedidoRepository)
        {
            this._pedidoRepository = pedidoRepository;
        }

        DTOs.Output.Pedido IPedidoUseCase.Get(int pedidoId)
        {
            var result = this._pedidoRepository.Get(pedidoId);
            if (result == null)
                throw new CustomExceptions.NotFoundException("Pedido não encontrado");

            return result;
        }

        IEnumerable<DTOs.Output.Pedido> IPedidoUseCase.List(List<int> pedidoIds)
        {
            var result = this._pedidoRepository.List(pedidoIds);
            return result;
        }

        bool IPedidoUseCase.Save(DTOs.Imput.Pedido pedido)
        {
            var entity = new Domain.Entities.PedidoStatus(
                 pedido.PedidoId.Value,
                 (Domain.Enums.PedidoStatus)pedido.Status.Value);

            return _pedidoRepository.Save(entity);
        }

        bool IPedidoUseCase.Update(int pedidoId, Domain.Enums.PedidoStatus status)
        {
            var entity = new Domain.Entities.PedidoStatus(pedidoId, status);
            return _pedidoRepository.Update(entity);
        }
    }
}
