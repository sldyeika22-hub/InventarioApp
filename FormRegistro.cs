using System;
using System.Drawing;
using System.Windows.Forms;

namespace InventarioApp
{
    // Capa de PRESENTACIÓN: formulario de registro de productos.
    // Los controles se crean por código para poder copiar y pegar sin el diseñador.
    public class FormRegistro : Form
    {
        private TextBox txtNombre;
        private ComboBox cboCategoria;
        private TextBox txtProveedor;
        private TextBox txtStock;
        private TextBox txtStockMinimo;
        private Button btnGuardar;
        private Button btnLimpiar;
        private Button btnCerrar;
        private DataGridView dgvProductos;

        public FormRegistro()
        {
            ConstruirInterfaz();
            ActualizarTabla();
        }

        private void ConstruirInterfaz()
        {
            Text = "Registro de productos";
            Size = new Size(780, 580);
            StartPosition = FormStartPosition.CenterParent;

            AgregarEtiqueta("Nombre:", 20, 23);
            txtNombre = new TextBox { Location = new Point(130, 20), Width = 220, MaxLength = 50 };

            AgregarEtiqueta("Categoría:", 20, 58);
            cboCategoria = new ComboBox
            {
                Location = new Point(130, 55),
                Width = 220,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboCategoria.Items.AddRange(new object[] { "Alimentos", "Bebidas", "Limpieza", "Tecnología", "Papelería", "Otros" });

            AgregarEtiqueta("Proveedor:", 20, 93);
            txtProveedor = new TextBox { Location = new Point(130, 90), Width = 220, MaxLength = 50 };

            AgregarEtiqueta("Stock inicial:", 400, 23);
            txtStock = new TextBox { Location = new Point(520, 20), Width = 120 };

            AgregarEtiqueta("Stock mínimo:", 400, 58);
            txtStockMinimo = new TextBox { Location = new Point(520, 55), Width = 120 };

            btnGuardar = new Button { Text = "Guardar", Location = new Point(130, 135), Size = new Size(100, 35) };
            btnLimpiar = new Button { Text = "Limpiar", Location = new Point(240, 135), Size = new Size(100, 35) };
            btnCerrar = new Button { Text = "Cerrar", Location = new Point(350, 135), Size = new Size(100, 35) };

            // Eventos
            btnGuardar.Click += btnGuardar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
            btnCerrar.Click += btnCerrar_Click;

            dgvProductos = new DataGridView
            {
                Location = new Point(20, 190),
                Size = new Size(725, 340),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };

            Controls.AddRange(new Control[]
            {
                txtNombre, cboCategoria, txtProveedor, txtStock, txtStockMinimo,
                btnGuardar, btnLimpiar, btnCerrar, dgvProductos
            });
        }

        private void AgregarEtiqueta(string texto, int x, int y)
        {
            Controls.Add(new Label { Text = texto, Location = new Point(x, y), AutoSize = true });
        }

        // ---------- Eventos ----------

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int stock;
            int minimo;

            string error = GestorInventario.Validar(
                txtNombre.Text, cboCategoria.Text, txtProveedor.Text,
                txtStock.Text, txtStockMinimo.Text, out stock, out minimo);

            if (error != "")
            {
                MessageBox.Show(error, "Datos inválidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                GestorInventario.Agregar(txtNombre.Text, cboCategoria.Text, txtProveedor.Text, stock, minimo);
                ActualizarTabla();

                if (stock <= minimo)
                {
                    MessageBox.Show("Producto guardado, pero tiene stock bajo.", "Alerta de stock",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Producto registrado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        // ---------- Métodos ----------

        private void LimpiarControles()
        {
            txtNombre.Clear();
            txtProveedor.Clear();
            txtStock.Clear();
            txtStockMinimo.Clear();
            cboCategoria.SelectedIndex = -1;
            txtNombre.Focus();
        }

        private void ActualizarTabla()
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = GestorInventario.ObtenerTodos();

            // Resalta en rojo los productos con stock bajo
            foreach (DataGridViewRow fila in dgvProductos.Rows)
            {
                Producto p = (Producto)fila.DataBoundItem;
                if (p.Stock <= p.StockMinimo)
                {
                    fila.DefaultCellStyle.BackColor = Color.MistyRose;
                }
            }
        }
    }
}