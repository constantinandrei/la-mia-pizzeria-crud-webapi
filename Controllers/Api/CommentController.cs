using la_mia_pizzeria_static.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private IDbCommentRepository commentRepository;
        public CommentController(IDbCommentRepository _commentRepository)
        {
            commentRepository = _commentRepository;
        }

        public IActionResult Index(int pizzaId)
        {
            return Ok(commentRepository.Get(pizzaId));
        }
    }
}
