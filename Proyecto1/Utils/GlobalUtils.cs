using System.Reflection;

namespace Proyecto1.Utils
{
    public class GlobalUtils
    {



        public static Dictionary<string, object> ClaseADiccionario(object obj)
        {
            var dict = new Dictionary<string, object>();
            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                var valor = prop.GetValue(obj);
                dict[prop.Name] = valor;
            }
            return dict;
        }












    }
}
