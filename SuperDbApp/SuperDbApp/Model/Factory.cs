namespace SuperDbApp.Model
{
    public class Factory
    {
        public int Id { get; set; }
        public string Adress { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
