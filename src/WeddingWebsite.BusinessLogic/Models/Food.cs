namespace WeddingWebsite.BusinessLogic.Models
{
    public class Food
    {
        public Food(bool? meat, bool? fish, bool? vegetarian, bool? vegan)
        {
            Meat = meat;
            Fish = fish;
            Vegetarian = vegetarian;
            Vegan = vegan;
        }

        public bool? Meat { get; }
        public bool? Fish { get; }        
        public bool? Vegetarian { get; }
        public bool? Vegan { get; }
    }
}