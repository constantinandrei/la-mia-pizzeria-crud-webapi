using la_mia_pizzeria_static.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        IDbPizzaRepository _pizzaRepository;
        public PizzaController(IDbPizzaRepository _pizza)
        {
            _pizzaRepository = _pizza;    
        }
        public IActionResult Get()
        
        {
            return Ok(_pizzaRepository.Get());
        }

        public IActionResult Search(string title)
        {
            return Ok(_pizzaRepository.SearchByTitle(title));
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            return Ok(_pizzaRepository.Get(id));
        }
    }
}
