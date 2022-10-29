namespace MyCollection.Api.DTO
{
    public class CollectionItemDTO
    {
        public string Title { get; set; }
        public string Autor { get; set; }
        public int Quantity { get; set; }
        public string? Edition { get; set; }
        public int ItemType { get; set; }
        public int Status { get; set; }
    }
}
