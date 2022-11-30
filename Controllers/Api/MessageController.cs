using la_mia_pizzeria_static.Models;
using la_mia_pizzeria_static.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers.Api
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        IDbMessageRepository messageRepository;
        public MessageController(IDbMessageRepository _messageRepository)
        {
            messageRepository = _messageRepository;
        }

        [HttpPost]
        public IActionResult Create(Message message)
        {
            messageRepository.Create(message);
            return Ok(message);
        }
    }
}
