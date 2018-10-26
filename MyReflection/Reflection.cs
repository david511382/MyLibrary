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
        public static List<KeyValuePair<string,object>> ReflectionObject(object target)
        {
            List<KeyValuePair<string, object>> results = new List<KeyValuePair<string, object>>();
            object obj;
            //foreach每一個欄位屬性及值,並進行判斷儲存
            foreach (PropertyInfo element in target.GetType().GetProperties())
            {
                obj = element.GetValue(target);
                string propertyName = obj?.ToString();
                results.Add(new KeyValuePair<string, object>(element.Name, propertyName));
            }

            return results;
        }
    }
}
