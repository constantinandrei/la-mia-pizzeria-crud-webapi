using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string UserEmail { get; set; }
        public string CommentText { get; set; }
        [Range(0, 5)]
        public int Stars { get; set; }
        public int PizzaId { get; set; }
        public Pizza Pizza { get; set; }
    }
}
