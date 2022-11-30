using la_mia_pizzeria_static.Data;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class DbMessageRepository : IDbMessageRepository
    {
        private PizzaDbContext db;
        public DbMessageRepository(PizzaDbContext db)
        {
            this.db = db;
        }

        public void Create(Message message)
        {
            db.Messages.Add(message);
            db.SaveChanges();
        }
    }
}
