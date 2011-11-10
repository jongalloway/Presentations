using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;

namespace Build01
{
    public static class ModelBindingExtensions
    {
        public static ModelBindingExecutionContext GetModelBindingExecutionContext(this Page page)
        {
            return new ModelBindingExecutionContext
            {
                HttpContext = new HttpContextWrapper(HttpContext.Current),
                ModelState = page.ModelState
            };
        }
    }
}