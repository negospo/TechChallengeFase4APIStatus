using NUnit.Framework;
using Domain.CustomExceptions;
using Domain.Enums;

namespace TestsBDD.StepDefinitions.UseCase
{
    [Binding]
    public class PedidoStatusSteps
    {
        private Domain.Entities.PedidoStatus _pedidoStatus;
        private Exception _capturedException;

        [Given(@"que eu crio um PedidoStatus com um PedidoId válido e um status")]
        public void GivenQueEuCrioUmPedidoStatusComUmPedidoIdValidoEUmStatus()
        {
            _pedidoStatus = new Domain.Entities.PedidoStatus(1, PedidoStatus.Pronto);
        }

        [Given(@"que eu crio um PedidoStatus com um PedidoId inválido")]
        public void GivenQueEuCrioUmPedidoStatusComUmPedidoIdInvalido()
        {
            try
            {
                _pedidoStatus = new Domain.Entities.PedidoStatus(0, PedidoStatus.EmPreparacao);
            }
            catch (Exception ex)
            {
                _capturedException = ex;
            }
        }

        [Then(@"o PedidoStatus é criado com sucesso")]
        public void ThenOPedidoStatusECriadoComSucesso()
        {
            Assert.IsNotNull(_pedidoStatus);
        }

        [Then(@"uma exceção de 'BadRequestException' com a mensagem 'O Id do pedido é inválido' deve ser lançada")]
        public void ThenUmaExcecaoDeBadRequestExceptionComAMensagemDeveSerLancada()
        {
            Assert.IsNotNull(_capturedException);
            Assert.That(_capturedException, Is.TypeOf<BadRequestException>());
            Assert.That(_capturedException.Message, Is.EqualTo("O Id do pedido é inválido"));
        }
    }

}
