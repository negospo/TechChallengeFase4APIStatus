using Application.Interfaces.Repositories;
using Dapper;

namespace Infrastructure.Persistence.Repositories
{
    public class Pedido : Application.Interfaces.Repositories.IPedidoRepository
    {
        IEnumerable<Application.DTOs.Output.Pedido> IPedidoRepository.List(List<int> pedidoIds)
        {
            string query = "select pedido_id,pedido_status_id as status from pedido where pedido_id = any(@pedidoIds)";
            var payments = Database.Connection().Query<Application.DTOs.Output.Pedido>(query, new { pedidoIds = pedidoIds });
            return payments;
        }

        Application.DTOs.Output.Pedido IPedidoRepository.Get(int pedidoId)
        {
            string query = "select pedido_id,pedido_status_id as status from pedido where pedido_id = @pedido_id";
            var item = Database.Connection().QueryFirstOrDefault<Application.DTOs.Output.Pedido>(query, new { pedido_id = pedidoId });
            return item;
        }

        bool IPedidoRepository.Save(Domain.Entities.PedidoStatus pedido)
        {
            string queryExists = "select true from pedido where pedido_id = @pedidoId";
            var exists = Database.Connection().QueryFirstOrDefault<bool>(queryExists, new { pedidoId = pedido.PedidoId });
            if (exists)
                throw new Domain.CustomExceptions.ConflictException("Pedido já existe");

            string queryPaymentInsert = "insert into pedido (pedido_id,pedido_status_id,created_at) values (@pedido_id,@pedido_status_id,now())";
            Database.Connection().Execute(queryPaymentInsert, new
            {
                pedido_id = pedido.PedidoId,
                pedido_status_id = pedido.Status
            });
            return true;
        }

        bool IPedidoRepository.Update(Domain.Entities.PedidoStatus pedido)
        {
            string queryExists = "select true from pedido where pedido_id = @pedidoId";
            var exists = Database.Connection().QueryFirstOrDefault<bool>(queryExists, new { pedidoId = pedido.PedidoId });
            if (!exists)
                throw new Domain.CustomExceptions.BadRequestException("Pedido não encontrado");

            string query = "update pedido set pedido_status_id = @pedido_status_id where pedido_id = @pedidoId";
            int affected = Database.Connection().Execute(query, new
            {
                pedidoId = pedido.PedidoId,
                pedido_status_id = pedido.Status
            });

            return affected > 0;
        }
    }
}
