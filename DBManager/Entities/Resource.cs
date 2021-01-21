using System;
using System.Collections.Generic;

namespace Data.Entity
{
    public class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public double Price { get; set; }
        public int Value { get; set; }

        public override bool Equals(object obj)
        {
            var resource = obj as Resource;
            return resource != null &&
                   Id == resource.Id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + Id.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ResourceMapper
    {
        public static Resource Map(IList<Object> row)
        {
            var r = new Resource();
            r.Id = Int32.Parse(row[0].ToString());
            r.Name = row[1].ToString();
            r.Description = row[2].ToString();
            r.Unit = row[3].ToString();
            r.Price = Double.Parse(row[4].ToString());

            return r;
        }
    }
}