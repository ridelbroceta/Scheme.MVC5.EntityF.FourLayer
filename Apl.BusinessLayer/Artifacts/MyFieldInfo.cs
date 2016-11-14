using System;
using System.Reflection;

namespace Apl.BusinessLayer.Artifacts
{
    public class MyFieldInfo
    {
        public static void Trim(object item)
        {
            var myType = item.GetType();
            var myFieldInfo = myType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var t in myFieldInfo)
            {
                if (t.FieldType == typeof(string))
                {
                    t.SetValue(item, t.GetValue(item).ToString().Trim());
                }
            }
        }

        public static void Round2(object item)
        {
            var myType = item.GetType();
            var myFieldInfo = myType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var t in myFieldInfo)
            {
                if (t.FieldType == typeof(decimal))
                {
                    t.SetValue(item, decimal.Round((decimal)t.GetValue(item), 2, MidpointRounding.AwayFromZero));
                }
            }
        }

    }
}
