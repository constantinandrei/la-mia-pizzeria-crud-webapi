namespace la_mia_pizzeria_static.Models.Repositories
{
    public interface IDbCommentRepository
    {
        List<Comment> Get(int pizzaId);
    }
}