namespace MyCollection.Domain.Dto
{
    public  class LocationDto
    {
        public Guid Id { get; set; }
        public string Initials { get;  set; }
        public string Description { get;  set; }
        public Guid? ParentId { get; set; }

    }
}
