using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;


namespace Apl.UI.Helpers
{
    public static class MvcHelper
    {
        public static string CustomValidationSummary(this HtmlHelper helper, string validationMessage = "")
        {
            var html = new StringBuilder(string.Empty);
            if (!helper.ViewData.ModelState.IsValid)
            {
                html.Append(@"<div class='box-content alerts'><div class='alert alert-danger alert-dismissable'><button type='button' class='close' data-dismiss='alert'>×</button><ul>");
                foreach (var key in helper.ViewData.ModelState.Keys)
                {
                    foreach (var error in helper.ViewData.ModelState[key].Errors)
                    {
                        html.Append(@"<li><i class='fa fa-exclamation-circle'></i> " + helper.Encode(error.ErrorMessage) + @"</li>");
                    }
                }
                html.Append(@"</ul></div></div>");
            }
            return html.ToString();
        }

        public static MvcHtmlString MyCustomLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return LabelFor(html, expression, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            if (String.IsNullOrEmpty(labelText))
            {
                return MvcHtmlString.Empty;
            }

            TagBuilder tag = new TagBuilder("label");
            tag.MergeAttributes(htmlAttributes);
            tag.Attributes.Add("for", html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));
            tag.SetInnerText(labelText);

            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }
    }
}
