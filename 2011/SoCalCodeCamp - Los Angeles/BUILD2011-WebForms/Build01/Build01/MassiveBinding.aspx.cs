using System;
using System.Collections.Generic;
using Massive;

namespace Build01
{
    public partial class MassiveBinding : System.Web.UI.Page
    {
        // <asp:Repeater SelectMethod="GetItems" />
        public IEnumerable<dynamic> GetItems()
        {
            var products = new Products();
            return products.All(where: "WHERE CategoryID = @0", args: 4);
        }
    }

    public class Products : DynamicModel
    {
        public Products()
            : base("Northwind")
        {
            PrimaryKeyField = "ProductID";
        }
    }
}