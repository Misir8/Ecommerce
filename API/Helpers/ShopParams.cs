namespace API.Helpers
{
    public class ShopParams
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string Sort { get; set; }
        public string Search { get; set; }
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 6;
    }
}
