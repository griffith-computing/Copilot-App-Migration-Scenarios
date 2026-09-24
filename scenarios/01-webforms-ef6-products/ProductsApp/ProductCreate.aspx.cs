using System;
using System.Globalization;
using ProductsApp.Models;

namespace ProductsApp
{
    public partial class ProductCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            var product = new Product
            {
                Name = NameTextBox.Text.Trim(),
                Category = CategoryTextBox.Text.Trim(),
                Price = decimal.Parse(PriceTextBox.Text, NumberStyles.Currency, CultureInfo.CurrentCulture),
                InStock = InStockCheckBox.Checked,
                CreatedDate = DateTime.UtcNow
            };

            using (var db = new ProductsContext())
            {
                db.Products.Add(product);
                db.SaveChanges();
            }

            Response.Redirect("~/Default.aspx");
        }
    }
}
