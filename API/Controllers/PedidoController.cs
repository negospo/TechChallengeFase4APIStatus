using API.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("Pedido")]
    public class PedidoController : ControllerBase
    {
        private readonly Application.Interfaces.UseCases.IPedidoUseCase _pedidoUseCase;

        public PedidoController(Application.Interfaces.UseCases.IPedidoUseCase pedidoUseCase)
        {
            this._pedidoUseCase = pedidoUseCase;
        }


        /// <summary>
        /// Lista o pedido status passado por parâmetro
        /// </summary>
        /// <response code="404" >Pedido não encontrado</response> 
        [HttpGet]
        [Route("{pedidoId}")]
        [ProducesResponseType(typeof(Application.DTOs.Output.Pedido), 200)]
        public ActionResult<IEnumerable<Application.DTOs.Output.Pedido>> Get(int pedidoId)
        {
            try
            {
                return Ok(_pedidoUseCase.Get(pedidoId));
            }
            catch (Application.CustomExceptions.NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            
        }

        /// <summary>
        /// Lista todos os pedidos status passados por parâmetro.
        /// </summary>
        /// <param name="pedidoIds">Ids dos pedidos</param>
        [HttpPost]
        [Route("list")]
        [ProducesResponseType(typeof(IEnumerable<Application.DTOs.Output.Pedido>), 200)]
        public ActionResult<IEnumerable<Application.DTOs.Output.Pedido>> List(List<int> pedidoIds)
        {
            return Ok(_pedidoUseCase.List(pedidoIds));
        }

        /// <summary>
        /// Lista todos os pedidos status de um status específico
        /// </summary>
        /// <param name="status">Status do pedido</param>
        [HttpPost]
        [Route("listByStatus")]
        [ProducesResponseType(typeof(IEnumerable<Application.DTOs.Output.Pedido>), 200)]
        public ActionResult<IEnumerable<Application.DTOs.Output.Pedido>> ListByStatus(Application.Enums.PedidoStatus status)
        {
            return Ok(_pedidoUseCase.ListByStatus(status));
        }

        /// <summary>
        /// Cria um novo pedido status
        /// </summary>
        /// <param name="pedido">Dados do pedido status</param>
        /// <response code="400" >Dados inválidos</response>
        /// <response code="401" >Não autorizado</response>
        /// <response code="409" >Pedido já existe</response>
        [HttpPost]
        [Route("save")]
        [CustonValidateModel]
        [ProducesResponseType(typeof(Validation.CustonValidationResultModel), 400)]
        //[Authorize]
        public ActionResult<bool> Save(Application.DTOs.Imput.Pedido pedido)
        {
            try
            {
                var sucess = _pedidoUseCase.Save(pedido);
                return Ok(sucess);
            }
            catch (Application.CustomExceptions.BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Application.CustomExceptions.ConflictException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza o status de um pedido
        /// </summary>
        /// <param name="pedidoId">Id do pedido</param>
        /// <param name="status">Status do pedido</param>
        /// <response code="400" >Dados inválidos</response>
        /// <response code="401" >Não autorizado</response>
        /// <response code="404" >Pedido não encontrado</response>
        [HttpPut]
        [Route("{pedidoId}/status/update")]
        [ProducesResponseType(typeof(Validation.CustonValidationResultModel), 400)]
        //[Authorize]
        public ActionResult<bool> Update(int pedidoId, Domain.Enums.PedidoStatus? status)
        {
            if (!status.HasValue)
            {
                return BadRequest("Dados inválidos");
            }

            try
            {
                var sucess = _pedidoUseCase.Update(pedidoId,status.Value);
                return Ok(sucess);
            }
            catch (Application.CustomExceptions.BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Application.CustomExceptions.NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
