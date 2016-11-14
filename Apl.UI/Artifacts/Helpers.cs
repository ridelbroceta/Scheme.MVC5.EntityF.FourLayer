using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Web.Mvc;

namespace Apl.UI.Artifacts
{
    public class  Helper
    {

        public static List<SelectListItem> GetDropDownList<T>(IQueryable<T> lista,
               string text, string value, string selected) where T : class
        {
            if (lista != null)
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem { Text = "-Por favor seleccione-", Value = string.Empty });
                var lisData = (from items in lista
                               select items).AsEnumerable().Select(m => new SelectListItem
                               {
                                   Text = string.Format("{0}", m.GetType().GetProperty(text).GetValue(m)),
                                   Value = string.Format("{0}", m.GetType().GetProperty(value).GetValue(m)),
                                   Selected = (selected != "") && 
                                     (string.Format("{0}", m.GetType().GetProperty(text).GetValue(m)).ToUpper()
                                      == selected.ToUpper()),
                               }).ToList();
                list.AddRange(lisData);
                return list;
            }
            return new List<SelectListItem>();
        }

        public static string MyRetUrlToQueryString(string value)
        {
            //? => $ y & => !
            return value.Replace('$', '?').Replace('!', '&');
        }

    }

    public static class ReflectionExtensions
    {
        public static T GetAttribute<T>(this MemberInfo member, bool isRequired)
            where T : Attribute
        {
            var attribute = member.GetCustomAttributes(typeof(T), false).SingleOrDefault();

            if (attribute == null && isRequired)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The {0} attribute must be defined on member {1}",
                        typeof(T).Name,
                        member.Name));
            }

            return (T)attribute;
        }

        public static string MyGetPropertyDisplayName<T>(Expression<Func<T, object>> propertyExpression)
        {

            //ModelMetadata metadata = ModelMetadata.FromLambdaExpression(propertyExpression, T);
            var propertyInfo = GetPropertyInfoFromExpression<T>(propertyExpression);
            if (propertyInfo == null)
            {
                throw new ArgumentException(
                    "No property reference expression was found.",
                    "propertyExpression");
            }

            return GetDisplayName(propertyInfo, false);
        }

        public static string MyGetPropertyDisplayShortName<T>(Expression<Func<T, object>> propertyExpression)
        {

            //ModelMetadata metadata = ModelMetadata.FromLambdaExpression(propertyExpression, T);
            var propertyInfo = GetPropertyInfoFromExpression<T>(propertyExpression);
            if (propertyInfo == null)
            {
                throw new ArgumentException(
                    "No property reference expression was found.",
                    "propertyExpression");
            }

            return GetDisplayName(propertyInfo, true);
        }
 
        public static PropertyInfo GetPropertyInfoFromExpression<T>(Expression<Func<T, object>> propertyExpression)
        {
            MemberExpression exp;

            //this line is necessary, because sometimes the expression comes in as Convert(originalexpression)
            if (propertyExpression.Body is UnaryExpression)
            {
                var unExp = (UnaryExpression)propertyExpression.Body;
                if (unExp.Operand is MemberExpression)
                {
                    exp = (MemberExpression)unExp.Operand;
                }
                else
                    throw new ArgumentException();
            }
            else if (propertyExpression.Body is MemberExpression)
            {
                exp = (MemberExpression)propertyExpression.Body;
            }
            else
            {
                throw new ArgumentException();
            }

            return (PropertyInfo)exp.Member;
        }

        private static string GetDisplayName(PropertyInfo property, bool shortName)
        {
            var metaName = GetMetaDisplayName(property, shortName);
            if (!string.IsNullOrEmpty(metaName)) return metaName;

            var attrName = GetAttributeDisplayName(property, shortName);
            if (!string.IsNullOrEmpty(attrName)) return attrName;

            return property.Name;
        }

        private static string GetAttributeDisplayName(PropertyInfo property, bool shortName)
        {
            var displayAttribute = property.GetAttribute<DisplayAttribute>(false);
            if (displayAttribute != null)
            {
                var recurso = displayAttribute.ResourceType;
                if (recurso != null)
                {
                    var resourceManager = new ResourceManager(displayAttribute.ResourceType.FullName, displayAttribute.ResourceType.Assembly);
                    var entry =
                        resourceManager.GetResourceSet(Thread.CurrentThread.CurrentUICulture, true, true)
                          .OfType<DictionaryEntry>()
                          .FirstOrDefault(p => p.Key.ToString() == (shortName ? displayAttribute.ShortName : displayAttribute.Name));

                    var key = entry.Value.ToString();
                    return key;
                }

                return shortName ? displayAttribute.ShortName : displayAttribute.Name;
            }
            var displayNameAttribute = property.GetAttribute<DisplayNameAttribute>(false);
            if (displayNameAttribute != null)
            {

                return displayNameAttribute.DisplayName;
            }
            return null;
        }

        private static string GetMetaDisplayName(PropertyInfo property, bool shortName)
        {
            var declaracion = property.DeclaringType;
            if (declaracion == null) return null;
            var atts = declaracion.GetCustomAttributes(typeof(MetadataTypeAttribute), true);
            if (atts.Length == 0) return null;

            var metaAttr = atts[0] as MetadataTypeAttribute;
            if (metaAttr == null) return null;
            var metaProperty = metaAttr.MetadataClassType.GetProperty(property.Name);
            if (metaProperty == null) return null;

            return GetAttributeDisplayName(metaProperty, shortName);
        }


        public static string CreateQueryString(object obj)
        {
                var listaproper = obj.GetType().GetProperties();

                var cadQueryString = string.Empty;
                var enlace = "/?";
                foreach (var propertyInfo in listaproper)
                {
                    cadQueryString += string.Format("{0}{1}={2}", enlace, propertyInfo.Name, propertyInfo.GetValue(obj));
                    enlace = "&";
                }

            return cadQueryString;
        }

    }
}