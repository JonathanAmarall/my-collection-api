namespace MyCollection.Domain.Dto
{
    public  class LocationDto
    {
       
        public LocationDto(Guid id, string initials, string description, Guid? parentId)
        {
            Id = id;
            Initials = initials;
            Description = description;
            ParentId = parentId;
        }

        public Guid Id { get; set; }
        public string Initials { get;  set; }
        public string Description { get;  set; }
        public Guid? ParentId { get; set; }

    }
}
