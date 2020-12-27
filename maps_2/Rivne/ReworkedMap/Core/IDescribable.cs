namespace Maps.Core
{

    public interface IDescribable
    {
        string Type { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string CreatorFullName { get; set; }
        Data.Role CreatorRole { get; set; }
    }
}
