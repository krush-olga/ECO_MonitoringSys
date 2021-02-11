using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Helpers
{
    public static class Mapper
    {
        public static T Map<T>(IList<object> values)
            where T : new()
        {
            T obj = new T();
            Type type = typeof(T);
            System.Reflection.PropertyInfo[] propertyInfos = type.GetProperties();
            int length = values.Count <= propertyInfos.Length ? values.Count : propertyInfos.Length;

            Type currentType = null;
            for (int i = 0; i < length; i++)
            {
                currentType = values[i].GetType();

                if (propertyInfos[i].CanWrite && currentType == propertyInfos[i].GetMethod.ReturnType)
                {
                    propertyInfos[i].SetValue(obj, values[i]);
                }
            }

            return obj;
        }
    }
}
