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

        public static object[] GetValuesByNames(object target, string[] names)
        {
            List<string> targetNames = new List<string>();
            List<object> targetValues = new List<object>();
            ReflectionObject(target,
                element =>
                {
                    targetNames.Add(element.Name);
                    targetValues.Add(element.GetValue(target));
                }
            );

            object[] result = new object[names.Length];
            for(int i = 0; i < names.Length; i++)
            {
                for(int j = 0; j < targetNames.Count; j++)
                {
                    if (names[i].Equals(targetNames[j]))
                    {
                        result[i] = targetValues[j];
                    }
                }
            }

            return result.ToArray();
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
