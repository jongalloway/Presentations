using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace Build01.Model
{
    /// <summary>
    /// Instructs the model binding system to get the user name fo the current user from the CurrentUserValueProvider
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
    sealed class CurrentUserAttribute : Attribute, IModelNameProvider, IValueProviderSource
    {
        public IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
        {
            return new CurrentUserValueProvider(modelBindingExecutionContext.HttpContext);
        }

        public string GetModelName()
        {
            return "";
        }
    }
}