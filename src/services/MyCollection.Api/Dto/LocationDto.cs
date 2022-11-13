namespace MyCollection.Api.Dto
{
    public class LocationDto
    {

        public LocationDto(Guid id, string initials, string description, Guid? parentId, int level)
        {
            Id = id;
            Initials = initials;
            Description = description;
            ParentId = parentId;
            Level = level;
        }

        public Guid Id { get; set; }
        public string Initials { get; set; }
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public int Level { get; set; }

    }
}
