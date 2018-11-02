using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyReflection
{
    public static class Reflection
    {
        public static List<KeyValuePair<string, object>> ReflectionObject(object target)
        {
            List<KeyValuePair<string, object>> results = new List<KeyValuePair<string, object>>();

            //foreach每一個欄位屬性及值,並進行判斷儲存
            ReflectionObject(target,
                element =>
                {
                    object obj;
                    obj = element.GetValue(target);
                    string propertyValue = obj?.ToString();
                    results.Add(new KeyValuePair<string, object>(element.Name, propertyValue));
                }
            );

            return results;
        }

        public static void ReflectionObject(object target, Action<PropertyInfo> action)
        {
            //foreach每一個欄位屬性及值,並進行判斷儲存
            foreach (PropertyInfo element in target.GetType().GetProperties())
            {
                action(element);
            }
        }
    }
}
