using Application.Enums;
using Application.Interfaces.Repositories;
using Application.Implementations;
using FluentAssertions;
using Moq;
using Application.DTOs.Output;
using Application.CustomExceptions;

namespace Tests
{
    public class PedidoUseCaseTests
    {
        private readonly Mock<IPedidoRepository> _pedidoRepository = new Mock<IPedidoRepository>();
        private readonly PedidoUseCase _useCase;

        public PedidoUseCaseTests()
        {
            _useCase = new PedidoUseCase(_pedidoRepository.Object);
        }


        [Fact]
        public void ShouldGetPedido()
        {
            // Arrange
            var pedidoId = 1;
            var pedido = new Pedido 
            {
                PedidoId = pedidoId,
                Status = PedidoStatus.Finalizado
            };

            _pedidoRepository.Setup(repo => repo.Get(pedidoId)).Returns(pedido);

            // Act
            var result = _useCase.Get(pedidoId);

            // Assert
            _pedidoRepository.Verify(repo => repo.Get(pedidoId), Times.Once());
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(pedido);
        }

        [Fact]
        public void ShouldThrowExceptionIfIdNotFound_WhenGetPedido()
        {
            var pedidoId = 1;
            // Arrange
            _pedidoRepository.Setup(s => s.Get(pedidoId)).Returns((Pedido)null);

            // Act
            Action act = () => _useCase.Get(pedidoId);

            // Assert
            act.Should().Throw<NotFoundException>()
                .WithMessage("Pedido não encontrado");

        }

        [Fact]
        public void ShouldListPedido()
        {
            // Arrange
            var pedidoIds = new List<int> { 1, 2 };
            var pedido1 = new Pedido
            {
                PedidoId = 1,
                Status = PedidoStatus.Finalizado
            };

            var pedido2 = new Pedido
            {
                PedidoId = 2,
                Status = PedidoStatus.Finalizado
            };

            var pedidos = new List<Pedido> { pedido1, pedido2 };

            _pedidoRepository.Setup(s => s.List(pedidoIds)).Returns(pedidos);

            // Act
            var result = _useCase.List(pedidoIds);

            // Assert
            _pedidoRepository.Verify(repo => repo.List(pedidoIds), Times.Once());
            result.Should().NotBeEmpty()
                .And.HaveCount(2)
                .And.BeEquivalentTo(new[] { pedido1, pedido2 });
        }

        [Fact]
        public void ShouldSavePedido()
        {
            // Arrange
            var pedido = new Application.DTOs.Imput.Pedido
            {
                PedidoId = 1,
                Status = PedidoStatus.Finalizado
            };

            _pedidoRepository.Setup(s => s.Save(It.IsAny<Domain.Entities.PedidoStatus>()))
                                    .Returns(true);

            // Act
            var result = _useCase.Save(pedido);

            // Assert
            _pedidoRepository.Verify(repo => repo.Save(
                It.IsAny<Domain.Entities.PedidoStatus>()), Times.Once());

            result.Should().BeTrue();
        }

        [Fact]
        public void ShouldUpdatePedido()
        {
            // Arrange
            var pedidoId = 1;
            _pedidoRepository.Setup(s => s.Update(
                It.IsAny<Domain.Entities.PedidoStatus>()))
                .Returns(true);

            //Act
            var result = _useCase.Update(pedidoId, Domain.Enums.PedidoStatus.Finalizado);

            //Assert
            _pedidoRepository.Verify(repo => repo.Update(
                It.IsAny<Domain.Entities.PedidoStatus>()), Times.Once());
            result.Should().BeTrue();
        }

    }
}
