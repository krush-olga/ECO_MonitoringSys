using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Helpers
{
    public static class Mapper<T>
        where T : new()
    {
        public static T Map(IList<T> values)
        {
            T obj = new T();
            Type type = typeof(T);
            System.Reflection.PropertyInfo[] propertyInfos = type.GetProperties(System.Reflection.BindingFlags.Public);
            int length = values.Count <= propertyInfos.Length ? values.Count : propertyInfos.Length;

            for (int i = 0; i < length; i++)
            {
                propertyInfos[i].SetValue(obj, values[i]);
            }

            return obj;
        }
    }
}
