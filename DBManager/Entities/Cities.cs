using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class City
    {
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class CityMapper
    {
        public static City Map(IList<Object> row)
        {
            double lng, lat;
            double.TryParse(row[2].ToString(), out lng);
            double.TryParse(row[1].ToString(), out lat);

            return new City
            {
                Name = row[0].ToString(),
                Latitude = lat,
                Longitude = lng
            };
        }
    }
}
