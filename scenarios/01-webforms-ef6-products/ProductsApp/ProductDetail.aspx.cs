using System;
using System.Linq;
using ProductsApp.Models;

namespace ProductsApp
{
    public partial class ProductDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProduct();
            }
        }

        private void LoadProduct()
        {
            if (!int.TryParse(Request.QueryString["id"], out int id))
            {
                ShowNotFound();
                return;
            }

            using (var db = new ProductsContext())
            {
                var product = db.Products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    ShowNotFound();
                    return;
                }

                NameLiteral.Text = Server.HtmlEncode(product.Name);
                CategoryLiteral.Text = Server.HtmlEncode(product.Category);
                PriceLiteral.Text = product.Price.ToString("C");
                InStockLiteral.Text = product.InStock ? "Yes" : "No";
                CreatedLiteral.Text = product.CreatedDate.ToString("d");
                DetailPanel.Visible = true;
            }
        }

        private void ShowNotFound()
        {
            DetailPanel.Visible = false;
            NotFoundLabel.Visible = true;
        }
    }
}
