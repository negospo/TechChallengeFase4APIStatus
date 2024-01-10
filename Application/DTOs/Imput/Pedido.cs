using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Imput
{
    public class Pedido
    {
        /// <summary>
        /// Id do pedido
        /// </summary>
        [Required]
        public int? PedidoId { get; set; }

        /// <summary>
        /// Status do pedido
        /// </summary>
        [Required]
        public Enums.PedidoStatus? Status { get; set; }
    }
}
