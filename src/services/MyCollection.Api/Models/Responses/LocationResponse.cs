namespace MyCollection.Api.Models.Responses
{
    public record LocationResponse(Guid Id, string Initials, string Description, Guid? ParentId, int Level)
    {
    }
}
