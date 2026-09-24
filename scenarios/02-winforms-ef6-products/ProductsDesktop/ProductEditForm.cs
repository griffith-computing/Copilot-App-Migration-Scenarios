using System;
using System.Globalization;
using System.Windows.Forms;
using ProductsDesktop.Models;

namespace ProductsDesktop
{
    /// <summary>
    /// Modal add/edit dialog. Uses the classic WinForms ErrorProvider
    /// validation pattern (distinct from the Web Forms validator controls
    /// used in the scenario 1 web app) and blocking, synchronous EF6 calls
    /// on the UI thread when saving.
    /// </summary>
    public partial class ProductEditForm : Form
    {
        private readonly Product _editingProduct;

        /// <summary>
        /// Creates the dialog in "add" mode.
        /// </summary>
        public ProductEditForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates the dialog in "edit" mode, pre-populated from an
        /// existing tracked product entity.
        /// </summary>
        public ProductEditForm(Product product) : this()
        {
            _editingProduct = product ?? throw new ArgumentNullException(nameof(product));
            Text = "Edit Product";
            nameTextBox.Text = product.Name;
            categoryTextBox.Text = product.Category;
            priceTextBox.Text = product.Price.ToString(CultureInfo.CurrentCulture);
            inStockCheckBox.Checked = product.InStock;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                return;
            }

            using (var db = new ProductsContext())
            {
                Product product;
                if (_editingProduct == null)
                {
                    product = new Product { CreatedDate = DateTime.UtcNow };
                    db.Products.Add(product);
                }
                else
                {
                    product = db.Products.Find(_editingProduct.Id);
                }

                product.Name = nameTextBox.Text.Trim();
                product.Category = categoryTextBox.Text.Trim();
                product.Price = decimal.Parse(priceTextBox.Text, NumberStyles.Number, CultureInfo.CurrentCulture);
                product.InStock = inStockCheckBox.Checked;

                db.SaveChanges();
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidateForm()
        {
            errorProvider.Clear();
            var isValid = true;

            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                errorProvider.SetError(nameTextBox, "Name is required.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(categoryTextBox.Text))
            {
                errorProvider.SetError(categoryTextBox, "Category is required.");
                isValid = false;
            }

            if (!decimal.TryParse(priceTextBox.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out var price)
                || price < 0 || price > 100000)
            {
                errorProvider.SetError(priceTextBox, "Enter a price between 0 and 100,000.");
                isValid = false;
            }

            return isValid;
        }
    }
}
