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
        public PizzaController()
        {
            _pizzaRepository = new DbPizzaRepository();    
        }
        public IActionResult Get()
        
        {
            return Ok(_pizzaRepository.Get());
        }
    }
}
