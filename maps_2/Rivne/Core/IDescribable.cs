namespace UserMap.Core
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
        /// <include file='Docs/Core/DescribableDoc.xml' path='docs/members[@name="describable_interface"]/Creator/*'/>
        Data.Entity.Expert Creator { get; set; }
    }
}
