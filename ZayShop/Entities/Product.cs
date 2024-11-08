namespace ZayShop.Entities
{
    public class Product: Base
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public Category Category { get; set; }
        public int CategoryId {  get; set; } 
    }
}
