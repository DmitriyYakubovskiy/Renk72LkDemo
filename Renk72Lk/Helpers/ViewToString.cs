using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
namespace Renk72Lk.Helpers;

public class ViewToString
{
    public static async Task<string> RenderViewToStringAsync<TModel>(IRazorViewEngine viewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider, IModelMetadataProvider metadataProvider, ModelStateDictionary modelState, string viewName, TModel model)
    {
        var actionContext = new ActionContext(
            new DefaultHttpContext { RequestServices = serviceProvider },
            new RouteData(),
            new ActionDescriptor()
        );

        using (var writer = new StringWriter())
        {
            var viewResult = viewEngine.FindView(actionContext, viewName, false);
            var viewContext = new ViewContext(
            actionContext,
                viewResult.View!,
                new ViewDataDictionary<TModel>(metadataProvider, modelState) { Model = model },
                new TempDataDictionary(actionContext.HttpContext, tempDataProvider),
                writer,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext);
            return writer.ToString();
        }
    }
}
