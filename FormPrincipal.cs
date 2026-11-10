using System;
using System.Drawing;
using System.Windows.Forms;

namespace InventarioApp
{
    // Primera ventana que ve el usuario. Su trabajo es abrir el módulo principal.
    public class FormPrincipal : Form
    {
        private Button btnAdministrar;
        private Button btnSalir;

        public FormPrincipal()
        {
            Text = "Sistema de Inventario";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(560, 390);
            MinimumSize = new Size(560, 390);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 10F);

            Label lblTitulo = new Label();
            lblTitulo.Text = "SISTEMA DE INVENTARIO";
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(35, 55, 80);
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(95, 55);

            Label lblDescripcion = new Label();
            lblDescripcion.Text = "Registre, consulte y controle las existencias de sus productos.";
            lblDescripcion.Font = new Font("Segoe UI", 10F);
            lblDescripcion.ForeColor = Color.FromArgb(90, 100, 115);
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(53, 105);

            btnAdministrar = new Button();
            btnAdministrar.Text = "Administrar inventario";
            btnAdministrar.Location = new Point(155, 165);
            btnAdministrar.Size = new Size(240, 48);
            btnAdministrar.BackColor = Color.FromArgb(40, 105, 170);
            btnAdministrar.ForeColor = Color.White;
            btnAdministrar.FlatStyle = FlatStyle.Flat;
            btnAdministrar.FlatAppearance.BorderSize = 0;
            btnAdministrar.Cursor = Cursors.Hand;
            btnAdministrar.Click += btnAdministrar_Click;

            btnSalir = new Button();
            btnSalir.Text = "Salir";
            btnSalir.Location = new Point(155, 230);
            btnSalir.Size = new Size(240, 42);
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.BackColor = Color.White;
            btnSalir.ForeColor = Color.FromArgb(55, 65, 80);
            btnSalir.Cursor = Cursors.Hand;
            btnSalir.Click += btnSalir_Click;

            Label lblAviso = new Label();
            lblAviso.Text = "Los datos se conservan mientras la aplicación esté abierta.";
            lblAviso.Font = new Font("Segoe UI", 8.5F);
            lblAviso.ForeColor = Color.DimGray;
            lblAviso.AutoSize = true;
            lblAviso.Location = new Point(105, 315);

            Controls.AddRange(new Control[]
            {
                lblTitulo, lblDescripcion, btnAdministrar, btnSalir, lblAviso
            });
        }

        private void btnAdministrar_Click(object sender, EventArgs e)
        {
            // using garantiza que el formulario se libere al cerrarlo.
            using (FormRegistro formulario = new FormRegistro())
            {
                formulario.ShowDialog(this);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro de que desea salir?",
                "Confirmar salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
