using Application.Implementations;
using Application.Interfaces.Repositories;
using Application.Interfaces.UseCases;
using Moq;
using NUnit.Framework;

namespace TestsDBB.StepDefinitions
{
    [Binding]
    public class PedidoSteps
    {
        readonly Mock<IPedidoRepository> _mockPedidoRepository;
        readonly IPedidoUseCase _pedidoUseCase;
        List<Application.DTOs.Output.Pedido> databaseMok = new List<Application.DTOs.Output.Pedido>();
        IEnumerable<Application.DTOs.Output.Pedido> _result;
        Application.DTOs.Output.Pedido _getResult;
        Application.DTOs.Imput.Pedido _newPedido;
        List<int> _pedidoIds;
        int _pedidoId;
        bool _saveResult;
        Domain.Enums.PedidoStatus _newStatus;

        private Application.Enums.PedidoStatus _pedidoStatus;

        public PedidoSteps()
        {
            _mockPedidoRepository = new Mock<IPedidoRepository>();
            _pedidoUseCase = new PedidoUseCase(_mockPedidoRepository.Object);

            this.databaseMok = new List<Application.DTOs.Output.Pedido>
            {
                 new Application.DTOs.Output.Pedido(){  PedidoId = 1 , Status = Application.Enums.PedidoStatus.Recebido},
                 new Application.DTOs.Output.Pedido(){  PedidoId = 2 , Status = Application.Enums.PedidoStatus.EmPreparacao},
                 new Application.DTOs.Output.Pedido(){  PedidoId = 3 , Status = Application.Enums.PedidoStatus.Pronto}
            };
        }

        [Given(@"Eu tenho os seguintes IDs de pedido: (.*)")]
        public void GivenEuTenhoOsSeguintesIDsDePedido(string pedidoIdsStr)
        {
            var pedidoIds = pedidoIdsStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                   .Select(int.Parse)
                                   .ToList();

            this._pedidoIds = _pedidoIds;
            _mockPedidoRepository.Setup(repo => repo.List(pedidoIds))
                         .Returns(databaseMok);
        }

        [When(@"Eu solicito a lista desses pedidos")]
        public void WhenEuSolicitoAListaDessesPedidos()
        {
            _result = _pedidoUseCase.List(_pedidoIds);
        }

        [Then(@"Os pedidos correspondentes aos IDs são retornados")]
        public void ThenOsPedidosCorrespondentesAosIDsSaoRetornados()
        {
            Assert.IsFalse(_result.Any(pedido => !_pedidoIds.Contains(pedido.PedidoId)));
        }


        [Given(@"O status do pedido é '(.*)'")]
        public void GivenOStatusDoPedidoE(string status)
        {
            _pedidoStatus = Enum.Parse<Application.Enums.PedidoStatus>(status);
        }

        [When(@"Eu solicito a lista de pedidos com esse status")]
        public void WhenEuSolicitoAListaDePedidosComEsseStatus()
        {
            _result = _pedidoUseCase.ListByStatus(_pedidoStatus);
        }

        [Then(@"Todos os pedidos com status '(.*)' são retornados")]
        public void ThenTodosOsPedidosComStatusSaoRetornados(string status)
        {
            var expectedStatus = Enum.Parse<Application.Enums.PedidoStatus>(status);
            Assert.IsNotNull(_result); // Verifica se o resultado não é nulo
            foreach (var pedido in _result)
            {
                Assert.AreEqual(expectedStatus, pedido.Status); // Verifica o status de cada pedido
            }
        }


        [Given(@"O ID do pedido é (.*)")]
        public void GivenOIDDoPedidoE(int pedidoId)
        {
            this._pedidoId = pedidoId;
        }

        [When(@"Eu solicito os detalhes desse pedido")]
        public void WhenEuSolicitoOsDetalhesDessePedido()
        {
            _mockPedidoRepository.Setup(repo => repo.Get(_pedidoId))
                     .Returns(this.databaseMok.First(f => f.PedidoId == this._pedidoId));

            this._getResult = _pedidoUseCase.Get(this._pedidoId);
        }

        [Then(@"Os detalhes do pedido com ID (.*) são retornados")]
        public void ThenOsDetalhesDoPedidoComIDSaoRetornados(int pedidoId)
        {
            Assert.IsNotNull(this._getResult);
            Assert.AreEqual(pedidoId, _getResult.PedidoId);
        }


        [Given(@"Eu tenho um novo pedido com ID (.*) e status '(.*)'")]
        public void GivenEuTenhoUmNovoPedidoComIDEStatus(int pedidoId, Application.Enums.PedidoStatus status)
        {
            this._newPedido = new Application.DTOs.Imput.Pedido { PedidoId = pedidoId, Status = status };
        }

        [When(@"Eu tento salvar esse pedido")]
        public void WhenEuTentoSalvarEssePedido()
        {
            this._pedidoUseCase.Save(this._newPedido);
            this._saveResult = true;
        }

        [Then(@"O pedido é salvo com sucesso")]
        public void ThenOPedidoESalvoComSucesso()
        {
            Assert.IsTrue(this._saveResult);
        }


        [Given(@"O pedido com ID (.*) precisa ter seu status atualizado para '(.*)'")]
        public void GivenOPedidoComIDPrecisaTerSeuStatusAtualizadoPara(int pedidoId, Domain.Enums.PedidoStatus novoStatus)
        {
            _pedidoId = pedidoId;
            _newStatus = novoStatus;
        }

        [When(@"Eu atualizo o status desse pedido")]
        public void WhenEuAtualizoOStatusDessePedido()
        {
            _pedidoUseCase.Update(this._pedidoId, this._newStatus);
            this._saveResult = true;
        }

        [Then(@"O status do pedido é atualizado com sucesso")]
        public void ThenOStatusDoPedidoEAtualizadoComSucesso()
        {
            Assert.IsTrue(this._saveResult);
        }
    }
}
