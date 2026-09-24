using System;
using System.Linq;
using System.Windows.Forms;
using ProductsDesktop.Models;

namespace ProductsDesktop
{
    /// <summary>
    /// Main window: a DataGridView bound to the Products table, with
    /// toolbar/menu actions for Refresh/Add/Edit/Delete. Data access here
    /// is deliberately synchronous on the UI thread (a common legacy
    /// WinForms anti-pattern) rather than using async/await, which is one
    /// of the things worth modernizing during migration.
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            using (var db = new ProductsContext())
            {
                var products = db.Products
                    .OrderBy(p => p.Name)
                    .ToList();

                productsGridView.DataSource = products;
                rowCountStatusLabel.Text = $"{products.Count} product(s)";
            }
        }

        private void refreshToolStripButton_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void addToolStripButton_Click(object sender, EventArgs e)
        {
            using (var editForm = new ProductEditForm())
            {
                if (editForm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadProducts();
                }
            }
        }

        private void editToolStripButton_Click(object sender, EventArgs e)
        {
            EditSelectedProduct();
        }

        private void productsGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditSelectedProduct();
            }
        }

        private void EditSelectedProduct()
        {
            if (!(productsGridView.CurrentRow?.DataBoundItem is Product selected))
            {
                MessageBox.Show(this, "Select a product first.", "Edit Product",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var db = new ProductsContext())
            {
                var product = db.Products.Find(selected.Id);
                if (product == null)
                {
                    MessageBox.Show(this, "Product no longer exists.", "Edit Product",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadProducts();
                    return;
                }

                using (var editForm = new ProductEditForm(product))
                {
                    if (editForm.ShowDialog(this) == DialogResult.OK)
                    {
                        LoadProducts();
                    }
                }
            }
        }

        private void deleteToolStripButton_Click(object sender, EventArgs e)
        {
            if (!(productsGridView.CurrentRow?.DataBoundItem is Product selected))
            {
                MessageBox.Show(this, "Select a product first.", "Delete Product",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(this,
                $"Delete '{selected.Name}'?", "Delete Product",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            using (var db = new ProductsContext())
            {
                var product = db.Products.Find(selected.Id);
                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
            }

            LoadProducts();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
