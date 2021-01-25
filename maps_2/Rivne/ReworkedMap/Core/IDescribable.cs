namespace Maps.Core
{
    /// <include file='Docs/Core/DescribableDoc.xml' path='docs/members[@name="describable_interface"]/IDescribable/*'/>
    public interface IDescribable
    {
        /// <include file='Docs/Core/DescribableDoc.xml' path='docs/members[@name="describable_interface"]/Type/*'/>
        string Type { get; set; }
        /// <include file='Docs/Core/DescribableDoc.xml' path='docs/members[@name="describable_interface"]/Name/*'/>
        string Name { get; set; }
        /// <include file='Docs/Core/DescribableDoc.xml' path='docs/members[@name="describable_interface"]/Description/*'/>
        string Description { get; set; }
        /// <include file='Docs/Core/DescribableDoc.xml' path='docs/members[@name="describable_interface"]/CreatorFullName/*'/>
        string CreatorFullName { get; set; }
        /// <include file='Docs/Core/DescribableDoc.xml' path='docs/members[@name="describable_interface"]/CreatorRole/*'/>
        Data.Role CreatorRole { get; set; }
    }
}
