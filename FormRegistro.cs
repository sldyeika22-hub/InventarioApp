using System;
using System.Drawing;
using System.Windows.Forms;

namespace InventarioApp
{
    // Ventana de administración: registra, busca, modifica y elimina productos,
    // controla entradas/salidas y muestra estadísticas.
    public class FormRegistro : Form
    {
        // Campos para guardar referencias a los controles de la ventana.
        private TextBox txtNombre;
        private ComboBox cboCategoria;
        private TextBox txtProveedor;
        private TextBox txtStock;
        private TextBox txtStockMinimo;

        private TextBox txtBuscar;
        private ComboBox cboFiltroCategoria;
        private TextBox txtCantidadMovimiento;

        private Button btnGuardar;
        private Button btnActualizar;
        private Button btnEliminar;
        private Button btnNuevo;
        private Button btnCerrar;
        private Button btnEntrada;
        private Button btnSalida;

        private DataGridView dgvProductos;
        private Label lblProductoSeleccionado;
        private Label lblTotalProductos;
        private Label lblTotalUnidades;
        private Label lblStockBajo;

        // Cero significa que no hay ningún producto seleccionado.
        // Un número mayor que cero es el ID del producto seleccionado.
        private int productoSeleccionadoId = 0;

        public FormRegistro()
        {
            ConstruirInterfaz();
            ActualizarTabla();
            ActualizarEstadisticas();
            PrepararModoNuevo();
        }

        // ------------------------------------------------------------
        // 1. CONSTRUCCIÓN DE LA INTERFAZ
        // ------------------------------------------------------------
        private void ConstruirInterfaz()
        {
            Text = "Administración del inventario";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(1120, 790);
            MinimumSize = new Size(1120, 790);
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 9.5F);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Administración de productos";
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(35, 55, 80);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 18);
            Controls.Add(lblTitulo);

            // Grupo de datos del producto.
            GroupBox grpProducto = new GroupBox();
            grpProducto.Text = "Datos del producto";
            grpProducto.Location = new Point(20, 60);
            grpProducto.Size = new Size(520, 225);
            grpProducto.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            AgregarEtiqueta(grpProducto, "Nombre:", 18, 32);
            txtNombre = CrearTexto(grpProducto, 105, 28, 155, 26, 50);

            AgregarEtiqueta(grpProducto, "Categoría:", 275, 32);
            cboCategoria = new ComboBox();
            cboCategoria.Location = new Point(355, 28);
            cboCategoria.Size = new Size(145, 26);
            cboCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategoria.Items.AddRange(new object[]
            {
                "Alimentos", "Bebidas", "Limpieza", "Tecnología", "Papelería", "Otros"
            });
            grpProducto.Controls.Add(cboCategoria);

            AgregarEtiqueta(grpProducto, "Proveedor:", 18, 72);
            txtProveedor = CrearTexto(grpProducto, 105, 68, 155, 26, 50);

            AgregarEtiqueta(grpProducto, "Stock:", 275, 72);
            txtStock = CrearTexto(grpProducto, 355, 68, 70, 26, 10);
            txtStock.Text = "0";

            AgregarEtiqueta(grpProducto, "Stock mínimo:", 18, 112);
            txtStockMinimo = CrearTexto(grpProducto, 105, 108, 155, 26, 10);
            txtStockMinimo.Text = "0";

            Label lblAyuda = new Label();
            lblAyuda.Text = "Para cambiar existencias use Entrada o Salida.";
            lblAyuda.Location = new Point(275, 112);
            lblAyuda.Size = new Size(225, 34);
            lblAyuda.ForeColor = Color.DimGray;
            grpProducto.Controls.Add(lblAyuda);

            btnGuardar = CrearBoton("Registrar", 18, 165, 92, 36, Color.FromArgb(40, 105, 170));
            btnActualizar = CrearBoton("Actualizar", 118, 165, 92, 36, Color.FromArgb(40, 135, 100));
            btnEliminar = CrearBoton("Eliminar", 218, 165, 85, 36, Color.FromArgb(180, 65, 65));
            btnNuevo = CrearBoton("Nuevo / Limpiar", 311, 165, 105, 36, Color.FromArgb(90, 105, 125));

            grpProducto.Controls.AddRange(new Control[]
            {
                btnGuardar, btnActualizar, btnEliminar, btnNuevo
            });
            Controls.Add(grpProducto);

            // Grupo para aumentar o disminuir el stock del producto seleccionado.
            GroupBox grpMovimientos = new GroupBox();
            grpMovimientos.Text = "Movimientos de existencias";
            grpMovimientos.Location = new Point(555, 60);
            grpMovimientos.Size = new Size(525, 225);
            grpMovimientos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            lblProductoSeleccionado = new Label();
            lblProductoSeleccionado.Text = "Seleccione un producto en la tabla.";
            lblProductoSeleccionado.Location = new Point(20, 35);
            lblProductoSeleccionado.Size = new Size(480, 28);
            lblProductoSeleccionado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpMovimientos.Controls.Add(lblProductoSeleccionado);

            AgregarEtiqueta(grpMovimientos, "Cantidad:", 20, 83);
            txtCantidadMovimiento = CrearTexto(grpMovimientos, 105, 78, 100, 28, 10);
            txtCantidadMovimiento.Text = "1";

            btnEntrada = CrearBoton("Registrar entrada (+)", 20, 135, 205, 42, Color.FromArgb(40, 135, 100));
            btnSalida = CrearBoton("Registrar salida (-)", 245, 135, 205, 42, Color.FromArgb(180, 95, 45));
            grpMovimientos.Controls.AddRange(new Control[] { btnEntrada, btnSalida });

            Label lblNotaMovimiento = new Label();
            lblNotaMovimiento.Text = "La salida nunca puede dejar el stock en negativo.";
            lblNotaMovimiento.Location = new Point(20, 185);
            lblNotaMovimiento.Size = new Size(480, 22);
            lblNotaMovimiento.ForeColor = Color.DimGray;
            grpMovimientos.Controls.Add(lblNotaMovimiento);
            Controls.Add(grpMovimientos);

            // Área de búsqueda y filtro.
            GroupBox grpBusqueda = new GroupBox();
            grpBusqueda.Text = "Buscar y filtrar";
            grpBusqueda.Location = new Point(20, 300);
            grpBusqueda.Size = new Size(1060, 72);
            grpBusqueda.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            AgregarEtiqueta(grpBusqueda, "Texto (nombre, categoría o proveedor):", 18, 31);
            txtBuscar = CrearTexto(grpBusqueda, 280, 27, 360, 27, 100);

            AgregarEtiqueta(grpBusqueda, "Categoría:", 675, 31);
            cboFiltroCategoria = new ComboBox();
            cboFiltroCategoria.Location = new Point(755, 27);
            cboFiltroCategoria.Size = new Size(245, 27);
            cboFiltroCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFiltroCategoria.Items.Add("Todas");
            cboFiltroCategoria.Items.AddRange(new object[]
            {
                "Alimentos", "Bebidas", "Limpieza", "Tecnología", "Papelería", "Otros"
            });
            cboFiltroCategoria.SelectedIndex = 0;
            grpBusqueda.Controls.Add(cboFiltroCategoria);
            Controls.Add(grpBusqueda);

            // Estadísticas generales: siempre cuentan todos los productos,
            // no solamente las filas que coinciden con la búsqueda.
            GroupBox grpEstadisticas = new GroupBox();
            grpEstadisticas.Text = "Resumen del inventario";
            grpEstadisticas.Location = new Point(20, 382);
            grpEstadisticas.Size = new Size(1060, 62);
            grpEstadisticas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            lblTotalProductos = CrearEtiquetaEstadistica("Productos: 0", 20, 25, 250);
            lblTotalUnidades = CrearEtiquetaEstadistica("Unidades: 0", 360, 25, 250);
            lblStockBajo = CrearEtiquetaEstadistica("Stock bajo: 0", 700, 25, 250);
            grpEstadisticas.Controls.AddRange(new Control[]
            {
                lblTotalProductos, lblTotalUnidades, lblStockBajo
            });
            Controls.Add(grpEstadisticas);

            // Tabla que muestra los productos.
            dgvProductos = new DataGridView();
            dgvProductos.Location = new Point(20, 455);
            dgvProductos.Size = new Size(1060, 275);
            dgvProductos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                                  AnchorStyles.Left | AnchorStyles.Right;
            dgvProductos.ReadOnly = true;
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AllowUserToResizeRows = false;
            dgvProductos.MultiSelect = false;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.AutoGenerateColumns = false;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.BackgroundColor = Color.White;
            dgvProductos.BorderStyle = BorderStyle.FixedSingle;
            dgvProductos.EnableHeadersVisualStyles = false;
            dgvProductos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(225, 233, 242);
            dgvProductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(35, 55, 80);
            dgvProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvProductos.RowTemplate.Height = 29;

            ConfigurarColumnas();
            Controls.Add(dgvProductos);

            // Conectar eventos después de crear los controles.
            btnGuardar.Click += btnGuardar_Click;
            btnActualizar.Click += btnActualizar_Click;
            btnEliminar.Click += btnEliminar_Click;
            btnNuevo.Click += btnNuevo_Click;
            btnEntrada.Click += btnEntrada_Click;
            btnSalida.Click += btnSalida_Click;
            dgvProductos.CellClick += dgvProductos_CellClick;
            txtBuscar.TextChanged += Filtros_TextChanged;
            cboFiltroCategoria.SelectedIndexChanged += Filtros_TextChanged;

            // Botón para cerrar el formulario de administración.
            btnCerrar = CrearBoton("Cerrar ventana", 900, 18, 180, 34, Color.FromArgb(90, 105, 125));
            btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Controls.Add(btnCerrar);
            btnCerrar.Click += btnCerrar_Click;
        }

        // Crea un Label dentro del control que se recibe como contenedor.
        private void AgregarEtiqueta(Control contenedor, string texto, int x, int y)
        {
            Label etiqueta = new Label();
            etiqueta.Text = texto;
            etiqueta.Location = new Point(x, y);
            etiqueta.AutoSize = true;
            contenedor.Controls.Add(etiqueta);
        }

        // Crea un TextBox con propiedades comunes para no repetir código.
        private TextBox CrearTexto(Control contenedor, int x, int y,
                                  int ancho, int alto, int maximo)
        {
            TextBox caja = new TextBox();
            caja.Location = new Point(x, y);
            caja.Size = new Size(ancho, alto);
            caja.MaxLength = maximo;
            contenedor.Controls.Add(caja);
            return caja;
        }

        // Crea un botón y devuelve el objeto para poder conectarle un evento.
        private Button CrearBoton(string texto, int x, int y, int ancho, int alto, Color fondo)
        {
            Button boton = new Button();
            boton.Text = texto;
            boton.Location = new Point(x, y);
            boton.Size = new Size(ancho, alto);
            boton.BackColor = fondo;
            boton.ForeColor = Color.White;
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.Cursor = Cursors.Hand;
            return boton;
        }

        private Label CrearEtiquetaEstadistica(string texto, int x, int y, int ancho)
        {
            Label etiqueta = new Label();
            etiqueta.Text = texto;
            etiqueta.Location = new Point(x, y);
            etiqueta.Size = new Size(ancho, 25);
            etiqueta.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            etiqueta.ForeColor = Color.FromArgb(35, 75, 115);
            return etiqueta;
        }

        private void ConfigurarColumnas()
        {
            AgregarColumna("Id", "ID", 45);
            AgregarColumna("Nombre", "Nombre", 150);
            AgregarColumna("Categoria", "Categoría", 110);
            AgregarColumna("Proveedor", "Proveedor", 145);
            AgregarColumna("Stock", "Stock", 65);
            AgregarColumna("StockMinimo", "Stock mínimo", 85);
            AgregarColumna("Estado", "Estado", 95);
        }

        private void AgregarColumna(string propiedad, string encabezado, float ancho)
        {
            DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
            columna.DataPropertyName = propiedad;
            columna.Name = propiedad;
            columna.HeaderText = encabezado;
            columna.FillWeight = ancho;
            columna.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvProductos.Columns.Add(columna);
        }

        // ------------------------------------------------------------
        // 2. EVENTOS DE LOS BOTONES
        // ------------------------------------------------------------

        // Registra un producto nuevo.
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (productoSeleccionadoId != 0)
            {
                MessageBox.Show("Para guardar los cambios de un producto existente, use Actualizar.",
                    "Producto seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int stock;
            int minimo;
            string error = ValidarFormulario(0, out stock, out minimo);

            if (error != "")
            {
                MessageBox.Show(error, "No se puede registrar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Producto nuevo = GestorInventario.Agregar(
                txtNombre.Text, cboCategoria.Text, txtProveedor.Text, stock, minimo);

            ActualizarTabla();
            ActualizarEstadisticas();
            PrepararModoNuevo();

            MessageBox.Show("El producto " + nuevo.Nombre + " se registró correctamente.",
                "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Modifica los datos generales del producto seleccionado.
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (productoSeleccionadoId == 0)
            {
                MessageBox.Show("Primero seleccione un producto de la tabla.",
                    "No hay selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int stock;
            int minimo;
            string error = ValidarFormulario(productoSeleccionadoId, out stock, out minimo);

            if (error != "")
            {
                MessageBox.Show(error, "No se puede actualizar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool actualizado = GestorInventario.Modificar(
                productoSeleccionadoId, txtNombre.Text, cboCategoria.Text,
                txtProveedor.Text, minimo);

            if (!actualizado)
            {
                MessageBox.Show("No se encontró el producto que desea modificar.",
                    "Producto no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                PrepararModoNuevo();
                ActualizarTabla();
                return;
            }

            int idActualizado = productoSeleccionadoId;

            // La modificación puede cambiar el nombre o la categoría y hacer
            // que el producto deje de coincidir con los filtros activos.
            // Se limpian los filtros para mantener visible el producto editado.
            txtBuscar.Clear();
            cboFiltroCategoria.SelectedIndex = 0;
            ActualizarTabla();
            MostrarProductoSeleccionado(idActualizado);
            ActualizarEstadisticas();

            MessageBox.Show("Los datos del producto se actualizaron correctamente.",
                "Actualización exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Elimina el producto seleccionado después de pedir confirmación.
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (productoSeleccionadoId == 0)
            {
                MessageBox.Show("Primero seleccione un producto de la tabla.",
                    "No hay selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Desea eliminar el producto " + txtNombre.Text + "?",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (respuesta == DialogResult.Yes)
            {
                bool eliminado = GestorInventario.Eliminar(productoSeleccionadoId);

                if (eliminado)
                {
                    ActualizarTabla();
                    ActualizarEstadisticas();
                    PrepararModoNuevo();
                    MessageBox.Show("El producto fue eliminado.", "Eliminación exitosa",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontró el producto seleccionado.",
                        "Producto no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // Limpia los campos y prepara el formulario para registrar otro producto.
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            PrepararModoNuevo();
            dgvProductos.ClearSelection();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Suma unidades al stock del producto seleccionado.
        private void btnEntrada_Click(object sender, EventArgs e)
        {
            int cantidad;
            if (!int.TryParse(txtCantidadMovimiento.Text, out cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Escriba una cantidad entera mayor que cero.",
                    "Cantidad inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (productoSeleccionadoId == 0)
            {
                MessageBox.Show("Seleccione un producto antes de registrar una entrada.",
                    "No hay selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string error = GestorInventario.RegistrarEntrada(productoSeleccionadoId, cantidad);
            if (error != "")
            {
                MessageBox.Show(error, "No se pudo registrar la entrada",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ActualizarTabla();
            MostrarProductoSeleccionado(productoSeleccionadoId);
            ActualizarEstadisticas();
            txtCantidadMovimiento.Text = "1";
            MessageBox.Show("La entrada de " + cantidad + " unidad(es) fue registrada.",
                "Entrada registrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Resta unidades del stock del producto seleccionado.
        private void btnSalida_Click(object sender, EventArgs e)
        {
            int cantidad;
            if (!int.TryParse(txtCantidadMovimiento.Text, out cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Escriba una cantidad entera mayor que cero.",
                    "Cantidad inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (productoSeleccionadoId == 0)
            {
                MessageBox.Show("Seleccione un producto antes de registrar una salida.",
                    "No hay selección", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string error = GestorInventario.RegistrarSalida(productoSeleccionadoId, cantidad);
            if (error != "")
            {
                MessageBox.Show(error, "No se pudo registrar la salida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ActualizarTabla();
            MostrarProductoSeleccionado(productoSeleccionadoId);
            ActualizarEstadisticas();
            txtCantidadMovimiento.Text = "1";
            MessageBox.Show("La salida de " + cantidad + " unidad(es) fue registrada.",
                "Salida registrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ------------------------------------------------------------
        // 3. BÚSQUEDA Y SELECCIÓN DE FILAS
        // ------------------------------------------------------------
        private void Filtros_TextChanged(object sender, EventArgs e)
        {
            int idAnterior = productoSeleccionadoId;
            ActualizarTabla();

            // Conserva la selección solo si el producto sigue apareciendo
            // con los filtros actuales. Si quedó oculto, limpia el formulario
            // para evitar operar sobre un producto que no se ve en la tabla.
            if (idAnterior != 0 && !MostrarProductoSeleccionado(idAnterior))
            {
                PrepararModoNuevo();
                dgvProductos.ClearSelection();
            }
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            Producto producto = dgvProductos.Rows[e.RowIndex].DataBoundItem as Producto;
            if (producto != null)
            {
                CargarProductoEnFormulario(producto);
            }
        }

        private void CargarProductoEnFormulario(Producto producto)
        {
            productoSeleccionadoId = producto.Id;
            txtNombre.Text = producto.Nombre;
            cboCategoria.SelectedItem = producto.Categoria;
            txtProveedor.Text = producto.Proveedor;
            txtStock.Text = producto.Stock.ToString();
            txtStock.ReadOnly = true; // El stock se cambia mediante Entrada o Salida.
            txtStockMinimo.Text = producto.StockMinimo.ToString();

            lblProductoSeleccionado.Text = "Seleccionado: " + producto.Nombre +
                                            " | Stock actual: " + producto.Stock;
            btnGuardar.Enabled = false;
            btnActualizar.Enabled = true;
            btnEliminar.Enabled = true;
            btnEntrada.Enabled = true;
            btnSalida.Enabled = true;
        }

        private bool MostrarProductoSeleccionado(int id)
        {
            for (int i = 0; i < dgvProductos.Rows.Count; i++)
            {
                Producto producto = dgvProductos.Rows[i].DataBoundItem as Producto;

                if (producto != null && producto.Id == id)
                {
                    dgvProductos.ClearSelection();
                    dgvProductos.Rows[i].Selected = true;
                    dgvProductos.CurrentCell = dgvProductos.Rows[i].Cells[0];
                    CargarProductoEnFormulario(producto);
                    return true;
                }
            }

            return false;
        }

        // ------------------------------------------------------------
        // 4. ACTUALIZAR TABLA, ESTADÍSTICAS Y FORMULARIO
        // ------------------------------------------------------------
        private string ValidarFormulario(int idExcluir, out int stock, out int minimo)
        {
            return GestorInventario.Validar(
                txtNombre.Text,
                cboCategoria.Text,
                txtProveedor.Text,
                txtStock.Text,
                txtStockMinimo.Text,
                idExcluir,
                out stock,
                out minimo);
        }

        private void ActualizarTabla()
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = GestorInventario.Buscar(
                txtBuscar.Text,
                cboFiltroCategoria.Text);

            // Colorea las filas cuyo stock llegó al mínimo o está por debajo.
            foreach (DataGridViewRow fila in dgvProductos.Rows)
            {
                Producto producto = fila.DataBoundItem as Producto;

                if (producto != null && producto.Stock <= producto.StockMinimo)
                {
                    fila.DefaultCellStyle.BackColor = Color.MistyRose;
                    fila.DefaultCellStyle.SelectionBackColor = Color.IndianRed;
                }
                else
                {
                    fila.DefaultCellStyle.BackColor = Color.White;
                    fila.DefaultCellStyle.SelectionBackColor = Color.FromArgb(55, 115, 170);
                }
            }
        }

        private void ActualizarEstadisticas()
        {
            lblTotalProductos.Text = "Productos: " + GestorInventario.ContarProductos();
            lblTotalUnidades.Text = "Unidades: " + GestorInventario.ObtenerTotalUnidades();
            lblStockBajo.Text = "Stock bajo: " + GestorInventario.ContarProductosConStockBajo();
        }

        private void PrepararModoNuevo()
        {
            productoSeleccionadoId = 0;
            txtNombre.Clear();
            cboCategoria.SelectedIndex = -1;
            txtProveedor.Clear();
            txtStock.Text = "0";
            txtStock.ReadOnly = false;
            txtStockMinimo.Text = "0";
            txtCantidadMovimiento.Text = "1";

            lblProductoSeleccionado.Text = "Seleccione un producto en la tabla.";
            btnGuardar.Enabled = true;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnEntrada.Enabled = false;
            btnSalida.Enabled = false;
            txtNombre.Focus();
        }
    }
}
