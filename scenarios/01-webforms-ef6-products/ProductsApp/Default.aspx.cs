using System;
using System.Linq;
using ProductsApp.Models;

namespace ProductsApp
{
    /// <summary>
    /// Code-behind for the product list page. Uses a fresh EF6 DbContext
    /// per page load/postback -- a common (and leaky) legacy pattern since
    /// Web Forms has no built-in per-request DI/scoping like ASP.NET Core.
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProducts();
            }
        }

        private void BindProducts()
        {
            using (var db = new ProductsContext())
            {
                ProductsGridView.DataSource = db.Products
                    .OrderBy(p => p.Name)
                    .ToList();
                ProductsGridView.DataBind();
            }
        }

        protected void ProductsGridView_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetail")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"~/ProductDetail.aspx?id={id}");
            }
        }
    }
}
