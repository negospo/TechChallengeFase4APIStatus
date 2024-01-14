using Application.DTOs.Output;
using Application.Enums;
using Application.Interfaces.Repositories;
using FluentAssertions;
using Moq;

namespace Tests
{
    public class PedidoRepositoryTest
    {
        private readonly Mock<IPedidoRepository> _pedidoRepoMock = new Mock<IPedidoRepository>();
        [Fact]
        public void ShouldListPedido()
        {
            //Arrange
            var pedido1 = new Pedido()
            {
                PedidoId = 1,
                Status = PedidoStatus.Finalizado
            };

            var pedido2 = new Pedido()
            {
                PedidoId = 2,
                Status = PedidoStatus.Finalizado
            };

            var pedidos = new List<Pedido>()
            {
                pedido1,
                pedido2,
            };

            var ids = new List<int> { 1, 2 };

            _pedidoRepoMock.Setup(s => s.List(ids)).Returns(pedidos);

            //Act
            var pagamento = _pedidoRepoMock.Object.List(ids);

            //Assert
            _pedidoRepoMock.Verify(repo => repo.List(ids), Times.Once());
            pagamento.Should().NotBeEmpty()
                .And.HaveCount(2)
                .And.BeEquivalentTo(new[] { pedido1, pedido2 });
        }

        [Fact]
        public void ShouldGetPedido()
        {
            //Arrange
            var pedido = new Pedido()
            {
                PedidoId = 1,
                Status = PedidoStatus.Finalizado
            };

            _pedidoRepoMock.Setup(s => s.Get(1)).Returns(pedido);

            //Act
            var pagamento = _pedidoRepoMock.Object.Get(1);

            //Assert
            _pedidoRepoMock.Verify(repo => repo.Get(1), Times.Once());
            pagamento.Should().NotBeNull();
            pagamento.Should().BeEquivalentTo(pedido);
        }

        [Fact]
        public void ShouldSavePedido()
        {
            //Arrange
            var pedidoId = 1;
            var pedido = new Domain.Entities.PedidoStatus(
                    pedidoId, Domain.Enums.PedidoStatus.Finalizado);


            _pedidoRepoMock.Setup(s => s.Save(pedido)).Returns(true);

            //Act
            var saved = _pedidoRepoMock.Object.Save(pedido);

            //Assert
            _pedidoRepoMock.Verify(repo => repo.Save(pedido), Times.Once());
            saved.Should().BeTrue();
        }

        [Fact]
        public void ShouldUpdatedPedido()
        {
            //Arrange
            var pedidoId = 1;
            var pedido = new Domain.Entities.PedidoStatus(
                    pedidoId, Domain.Enums.PedidoStatus.Finalizado);

            _pedidoRepoMock.Setup(s => s.Update(pedido)).Returns(true);

            //Act
            var updated = _pedidoRepoMock.Object.Update(pedido);

            //Assert
            _pedidoRepoMock.Verify(repo => repo.Update(pedido), Times.Once());
            updated.Should().BeTrue();
        }

    }
}
