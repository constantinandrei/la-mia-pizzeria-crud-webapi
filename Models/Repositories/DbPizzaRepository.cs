using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models.Form;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;

namespace la_mia_pizzeria_static.Models.Repositories
{
    public class DbPizzaRepository : IDbPizzaRepository
    {
        private PizzaDbContext db;
        private IDbIngredientRepository ingredientRepository;
        private IDbCategoryRepository categoryRepository;

        public DbPizzaRepository(PizzaDbContext _db, IDbIngredientRepository _ingredient, IDbCategoryRepository _category)
        {
            db = _db;
            ingredientRepository = _ingredient;
            categoryRepository = _category;
        }
        public bool Exists(int id)
        {
            return db.Pizzas.Any(p => p.Id == id);
        }
        public List<Pizza> Get()
        {
      
            return db.Pizzas.Include(p => p.Category).Include(p => p.Ingredients).ToList();
        }

        public List<Pizza> Get(bool relations)
        {
            if (relations)
                return Get();
            return db.Pizzas.ToList();
        }


        public Pizza Get(int id)
        {
            return db.Pizzas.Where(p => p.Id == id).Include(p => p.Category).Include(p => p.Ingredients).Include(p => p.Comments).FirstOrDefault();
        }

        public List<Pizza> GetByCategoryId(int categoryId)
        {
            return db.Pizzas.Where(p => p.CategoryId == categoryId).Include(p => p.Category).ToList();
        }

        // crea il model vuoto con i dati da dinviare alla view
        public PizzaForm CreateForm()
        {
            PizzaForm formData = new PizzaForm();
            formData.Pizza = new Pizza();
            formData.Categories = categoryRepository.Get();
            formData.Ingredients = ingredientRepository.SelectList();

            return formData;
        }

        // riaggiunge da database i dati di ingredienti e categorie mantenendo i vecchi dati della pizza
        public PizzaForm CreateForm(PizzaForm formData)
        {
            formData.Categories = categoryRepository.Get();
            formData.Ingredients = new List<SelectListItem>();


            return formData;
        }

        // crea un form data con una pizza da database
        public PizzaForm CreateForm(int id)
        {
            PizzaForm formData = CreateForm();
            formData.Pizza = Get(id);
            formData.Ingredients.Clear();
            List<Ingredient> IngredientsList = ingredientRepository.Get();
            foreach (Ingredient ingredient in IngredientsList)
            {
                bool selected = formData.Pizza.Ingredients.Any(i => i.Id == ingredient.Id);
                formData.Ingredients.Add(new SelectListItem(ingredient.Name, ingredient.Id.ToString(), selected));
            }
            return formData;
        }

        public void Create(Pizza pizza, List<int> selectedIngredients)
        {
            pizza.Ingredients = new List<Ingredient>();
            foreach (int ingredientId in selectedIngredients)
            {
                pizza.Ingredients.Add(ingredientRepository.Get(ingredientId));
            }
            db.Pizzas.Add(pizza);
            db.SaveChanges();
        }

        public void Update(Pizza updatedPizza, List<int> selectedIngredients)
        {
            Pizza pizza = Get(updatedPizza.Id);
            pizza.Name = updatedPizza.Name;
            pizza.Image = updatedPizza.Image;
            pizza.Price = updatedPizza.Price;
            pizza.Description = updatedPizza.Description;
            pizza.CategoryId = updatedPizza.CategoryId;
            if (pizza.Ingredients != null)
                pizza.Ingredients.Clear();
            ;
            if (selectedIngredients != null)
            {
                foreach (int ingredientId in selectedIngredients)
                {
                    pizza.Ingredients.Add(ingredientRepository.Get(ingredientId));

                }
            }

            db.SaveChanges();
        }

        public void Delete(Pizza pizza)
        {
            db.Pizzas.Remove(pizza);
            db.SaveChanges();
        }

        public List<Pizza> SearchByTitle(string title)
        {
            return db.Pizzas.Where(pizza => pizza.Name.ToLower().Contains(title.ToLower())).Include("Ingredients").Include("Category").ToList();
        }
    }
}
