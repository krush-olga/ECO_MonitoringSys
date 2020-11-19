using System;
using System.Collections.Generic;

namespace Data.Entity
{
    public class Environment
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Environment() : this(0, "")
        { }
        public Environment(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public static class EnvironmentMapper
    {
        public static Environment Map(List<Object> row)
        {
            Environment environment = new Environment()
            {
                Id = Int32.Parse(row[0].ToString()),
                Name = row[1].ToString(),
            };

            return environment;
        }
    }
}
