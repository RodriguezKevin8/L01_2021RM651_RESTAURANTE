using L01_2021RM651.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2021RM651.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class clienteController : ControllerBase
    {
        private readonly RestauranteBdContext _restauranteBdContext;

        public clienteController(RestauranteBdContext restauranteBdContext)
        {
            _restauranteBdContext = restauranteBdContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Cliente> cliente = (from e in _restauranteBdContext.Clientes select e).ToList();

            if (cliente.Count() > 0)
            {
                return Ok(cliente);
            }
            else
            {
                return NotFound();

            }

        }

        [HttpGet]
        [Route("Getbypalabra/{palabra}")]
        public IActionResult Getdireccionpalabra(string palabra)
        {
            List<Cliente> cliente = (from e in _restauranteBdContext.Clientes
                                 where e.Direccion.Contains(palabra)
                                 select e).ToList();


            if (cliente.Count() > 0)
            {
                return Ok(cliente);
            }
            else
            {
                return NotFound();

            }

        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Addcliente([FromBody] Cliente nuevoCliente)
        {
            try
            {
                _restauranteBdContext.Clientes.Add(nuevoCliente);
                _restauranteBdContext.SaveChanges();
                return Ok(nuevoCliente);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }


        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Actualizarcliente(int id, [FromBody] Cliente clientemodificado)
        {

            Cliente? clienteactual = (from e in _restauranteBdContext.Clientes where e.ClienteId == id select e).FirstOrDefault();

            if (clienteactual == null)
            {
                return NotFound();
            }

            clienteactual.ClienteId = clientemodificado.ClienteId;
            clienteactual.NombreCliente = clientemodificado.NombreCliente;
            clienteactual.Direccion = clientemodificado.Direccion;


            _restauranteBdContext.Entry(clienteactual).State = EntityState.Modified;
            _restauranteBdContext.SaveChanges();
            return Ok(clientemodificado);

        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Eliminarcliente(int id)
        {

            Cliente? cliente = (from e in _restauranteBdContext.Clientes where e.ClienteId == id select e).FirstOrDefault();
            if (cliente == null)
            {
                return NotFound();

            }

            _restauranteBdContext.Clientes.Attach(cliente);
            _restauranteBdContext.Clientes.Remove(cliente);
            _restauranteBdContext.SaveChanges();
            return Ok(cliente);

        }
    }
}
