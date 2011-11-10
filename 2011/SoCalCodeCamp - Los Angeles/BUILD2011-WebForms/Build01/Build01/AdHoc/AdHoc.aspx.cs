using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using Build01.Model;

namespace Build01.AdHoc
{
    public partial class AdHoc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Create a value provider to get values from
            var formValues = new FormValueProvider(this.GetModelBindingExecutionContext());

            // Create an instance of your model class
            var model = new PageModel();

            // Bind onto the model instance from the form collection
            TryUpdateModel(model, formValues);
            
            if (ModelState.IsValid)
            {
                // Do stuff

            }
            else
            {
                // Show errors

            }
        }

        class PageModel
        {
            public string Foo { get; set; }
            public string Bar { get; set; }
            public int Count { get; set; }
        }
    }
}