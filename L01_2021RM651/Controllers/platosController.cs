using L01_2021RM651.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2021RM651.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class platosController : ControllerBase
    {
        private readonly RestauranteBdContext _restauranteBdContext;

        public platosController(RestauranteBdContext restauranteBdContext)
        {
            _restauranteBdContext = restauranteBdContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<Plato> plato = (from e in _restauranteBdContext.Platos select e).ToList();

            if (plato.Count() > 0)
            {
                return Ok(plato);
            }
            else
            {
                return NotFound();

            }

        }

        [HttpGet]
        [Route("Getbypalabra/{palabra}")]
        public IActionResult Getplatopalabra(string palabra)
        {
            List<Plato> plato = (from e in _restauranteBdContext.Platos
                                 where e.NombrePlato.Contains(palabra)
                                 select e).ToList();




            if (plato.Count() > 0)
            {
                return Ok(plato);
            }
            else
            {
                return NotFound();

            }

        }

        [HttpPost]
        [Route("Add")]
        public IActionResult AddPlato([FromBody] Plato nuevoPlato)
        {
            try
            {
                _restauranteBdContext.Platos.Add(nuevoPlato);
                _restauranteBdContext.SaveChanges();
                return Ok(nuevoPlato);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }


        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult Actualizarplato(int id, [FromBody] Plato platomodificado)
        {

            Plato? platoActual = (from e in _restauranteBdContext.Platos where e.PlatoId == id select e).FirstOrDefault();

            if (platoActual == null)
            {
                return NotFound("no furula");
            }

            platoActual.PlatoId = platomodificado.PlatoId;
            platoActual.NombrePlato = platomodificado.NombrePlato;
            platoActual.Precio = platomodificado.Precio;
           

            _restauranteBdContext.Entry(platoActual).State = EntityState.Modified;
            _restauranteBdContext.SaveChanges();
            return Ok(platomodificado);

        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarPlato(int id)
        {

            Plato? plato = (from e in _restauranteBdContext.Platos where e.PlatoId == id select e).FirstOrDefault();
            if (plato == null)
            {
                return NotFound();

            }

            _restauranteBdContext.Platos.Attach(plato);
            _restauranteBdContext.Platos.Remove(plato);
            _restauranteBdContext.SaveChanges();
            return Ok(plato);

        }

    }
}
