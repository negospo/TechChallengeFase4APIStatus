using API.Controllers;
using Application.CustomExceptions;
using Application.DTOs.Output;
using Application.Interfaces.UseCases;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests
{
    public class PedidoControllerTest
    {
        private readonly Mock<IPedidoUseCase> _pedidoUseCase = new Mock<IPedidoUseCase>();
        private readonly PedidoController _pedidoController;

        public PedidoControllerTest()
        {
            _pedidoController = new PedidoController(_pedidoUseCase.Object);
        }

        [Fact]
        public void ShouldGetPedido()
        {
            // Arrange
            var pedidoId = 1;
            var pedido = new Mock<Pedido>();

            _pedidoUseCase.Setup(u => u.Get(pedidoId)).Returns(pedido.Object);

            // Act
            var result = _pedidoController.Get(pedidoId);

            // Assert
            _pedidoUseCase.Verify(u => u.Get(pedidoId), Times.Once());
            result.Should().NotBeNull();
            result.Should().BeOfType(
                typeof(ActionResult<IEnumerable<Pedido>>));
        }

        [Fact]
        public void ShouldListPedidos()
        {
            // Arrange
            var pedidosIds = new List<int>{1,2};
            var pedidos = new Mock<IList<Pedido>>();

            _pedidoUseCase.Setup(u => u.List(It.IsAny<List<int>>()))
                .Returns(pedidos.Object);

            // Act
            var result = _pedidoController.List(pedidosIds);

            // Assert
            _pedidoUseCase.Verify(u => u.List(It.IsAny<List<int>>()), Times.Once());
            result.Should().NotBeNull();
            result.Should().BeOfType(
                typeof(ActionResult<IEnumerable<Pedido>>));
        }

        [Fact]
        public void ShouldSavePedido()
        {
            //Arrange
            _pedidoUseCase.Setup(u => u.Save(
                It.IsAny<Application.DTOs.Imput.Pedido>()))
                .Returns(true);

            //Act
            var result = _pedidoController.Save(new Application.DTOs.Imput.Pedido());

            //Assert
            _pedidoUseCase.Verify(u => u.Save(
                It.IsAny<Application.DTOs.Imput.Pedido>()),
                Times.Once());

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<bool>>();
            result.Result.Should().BeOfType<OkObjectResult>();
            
            var okResult = (OkObjectResult)result.Result;
            okResult.Value.Should().Be(true);

        }

        [Fact]
        public void ShouldReturnBadRequestResult_WhenBadRequestException()
        {
            // Arrange
            _pedidoUseCase.Setup(x => x.Save(It.IsAny<Application.DTOs.Imput.Pedido>()))
                .Throws(new BadRequestException("Dados Inválidos"));

            // Act
            var result = _pedidoController.Save(new Application.DTOs.Imput.Pedido());

            // Assert
            result.Should().BeOfType<ActionResult<bool>>();
            result.Result.Should().BeOfType<BadRequestObjectResult>();

            var badRequestResult = (BadRequestObjectResult)result.Result;
            badRequestResult.Value.Should().Be("Dados Inválidos");
        }

        [Fact]
        public void ShouldReturnConflictResult_WhenConflictRequestException()
        {
            // Arrange
            _pedidoUseCase.Setup(x => x.Save(It.IsAny<Application.DTOs.Imput.Pedido>()))
                .Throws(new ConflictException("Pedido já realizado"));

            // Act
            var result = _pedidoController.Save(new Application.DTOs.Imput.Pedido());

            // Assert
            result.Should().BeOfType<ActionResult<bool>>();
            result.Result.Should().BeOfType<ConflictObjectResult>();

            var conflictRequestResult = (ConflictObjectResult)result.Result;
            conflictRequestResult.Value.Should().Be("Pedido já realizado");
        }

        [Fact]
        public void ShouldUpdatePedido() 
        {
            //Arrange
            var pedidoId = 1;

            _pedidoUseCase.Setup(u => u.Update(
                It.IsAny<int>(), It.IsAny<Domain.Enums.PedidoStatus>()))
                .Returns(true);

            //Act
            var result = _pedidoController.Update(pedidoId, Domain.Enums.PedidoStatus.Finalizado);

            //Assert
            _pedidoUseCase.Verify(u => u.Update(
                It.IsAny<int>(), It.IsAny<Domain.Enums.PedidoStatus>()),
                Times.Once());

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionResult<bool>>();
            var okResult = (OkObjectResult)result.Result;
            okResult.Value.Should().Be(true);
        }

    }
}
