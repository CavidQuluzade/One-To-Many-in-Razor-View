namespace ZayShop.Entities
{
    public class Category : Base
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
