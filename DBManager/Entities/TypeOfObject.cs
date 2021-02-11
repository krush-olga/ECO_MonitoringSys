using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class TypeOfObject
    {
        private string kved;

        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }

        public string KVED
        {
            get { return kved; }
            set 
            { 
                if (value.Length > 5)
                {
                    value = value.Remove(5);
                }

                kved = value; 
            }
        }

        public override string ToString()
        {
            return Name;
        }

    }

    public class TypeOfObjectMapper
    {
        public static TypeOfObject Map(IList<Object> row)
        {
            int id;
            int.TryParse(row[0].ToString(), out id);

            var typeOfObject = new TypeOfObject
            {
                Id = id,
                KVED = row[1].ToString(),
                Name = row[2].ToString(),
                ImageName = row[3].ToString()
            };

            return typeOfObject;
        }
    }
}
