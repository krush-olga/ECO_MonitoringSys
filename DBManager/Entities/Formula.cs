using System;
using System.Collections.Generic;

namespace Data.Entity
{
    public class Formula
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int IdOfExpert { get; set; }
        public string Measurment { get; set; }

        public Formula() : this(0, "", "", 0, "")
        { }
        public Formula(int id, string name, string description, int idOfExpert, string measurment)
        {
            Id = id;
            Name = name;
            Description = description;
            IdOfExpert = idOfExpert;
            Measurment = measurment;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class FormulaMapper
    {
        public static Formula Map(List<Object> row)
        {
            Formula formula = new Formula()
            {
                Id = Int32.Parse(row[0].ToString()),
                Name = row[1].ToString(),
                Description = row[2].ToString(),
                IdOfExpert = Int32.Parse(row[3].ToString()),
                Measurment = row[4].ToString()
            };

            return formula;
        }
    }
}
