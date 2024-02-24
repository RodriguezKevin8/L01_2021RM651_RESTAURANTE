using L01_2021RM651.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2021RM651.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pedidoController : ControllerBase
    {
        private readonly RestauranteBdContext _restauranteBdContext;

        public pedidoController(RestauranteBdContext restauranteBdContext)
        {
            _restauranteBdContext = restauranteBdContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Pedido> pedidos = (from e in _restauranteBdContext.Pedidos select e).ToList();

            if (pedidos.Count() > 0)
            {
                return Ok(pedidos);
            }
            else
            {
                return NotFound();

            }

        }

        [HttpGet]
        [Route("GetbyMotorista/{id}")]
        public IActionResult Getpedidomoto(int id)
        {
            List<Pedido> pedidos = (from e in _restauranteBdContext.Pedidos
                                    where e.MotoristaId == id
                                    select e).ToList();



            if (pedidos.Count() > 0)
            {
                return Ok(pedidos);
            }
            else
            {
                return NotFound();

            }

        }

        [HttpGet]
        [Route("GetbyCliente/{id}")]
        public IActionResult Getpedidocliente(int id)
        {
            List<Pedido> pedidos = _restauranteBdContext.Pedidos
         .Where(e => e.ClienteId == id)
         .ToList();


            if (pedidos.Count() > 0)
            {
                return Ok(pedidos);
            }
            else
            {
                return NotFound();

            }

        }

        [HttpPost]
        [Route("Add")]
        public IActionResult AddPedido([FromBody] Pedido nuevoPedido)
        {
            try
            {
                _restauranteBdContext.Pedidos.Add(nuevoPedido);
                _restauranteBdContext.SaveChanges();
                return Ok(nuevoPedido);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }


        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Actualizarpedido(int id, [FromBody] Pedido Pedidomodificado)
        {

            Pedido? pdidoActual = (from e in _restauranteBdContext.Pedidos where e.PedidoId == id select e).FirstOrDefault();

            if (pdidoActual == null)
            {
                return NotFound("no furula");
            }

            pdidoActual.PedidoId = Pedidomodificado.PedidoId;
            pdidoActual.MotoristaId = Pedidomodificado.MotoristaId;
            pdidoActual.ClienteId = Pedidomodificado.ClienteId;
            pdidoActual.PlatoId = Pedidomodificado.PlatoId;
            pdidoActual.Cantidad = Pedidomodificado.Cantidad;
            pdidoActual.Precio = Pedidomodificado.Precio;

           _restauranteBdContext.Entry(pdidoActual).State = EntityState.Modified;
            _restauranteBdContext.SaveChanges();
            return Ok(Pedidomodificado);

        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarPedido(int id)
        {

            Pedido? pedido = (from e in _restauranteBdContext.Pedidos where e.PedidoId == id select e).FirstOrDefault();
            if (pedido == null)
            {
                return NotFound();

            }

            _restauranteBdContext.Pedidos.Attach(pedido);
            _restauranteBdContext.Pedidos.Remove(pedido);
            _restauranteBdContext.SaveChanges();
            return Ok(pedido);

        }


    }
}
