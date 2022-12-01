using la_mia_pizzeria_static.Data;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class DbCommentRepository : IDbCommentRepository
    {
        private PizzaDbContext db;
        public DbCommentRepository(PizzaDbContext _db)
        {
            this.db = _db;
        }
        public List<Comment> Get(int pizzaId)
        {
            return db.Comments.Where(c => c.PizzaId == pizzaId).ToList();
        }
    }
}
