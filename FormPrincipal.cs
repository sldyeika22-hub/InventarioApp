using System;
using System.Drawing;
using System.Windows.Forms;

namespace InventarioApp
{
    // Formulario principal: menú de navegación
    public class FormPrincipal : Form
    {
        private Button btnRegistro;
        private Button btnSalir;

        public FormPrincipal()
        {
            Text = "Sistema de Inventario";
            Size = new Size(420, 320);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            Label lblTitulo = new Label
            {
                Text = "Sistema de Inventario",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(70, 40)
            };

            btnRegistro = new Button
            {
                Text = "Registrar productos",
                Location = new Point(110, 110),
                Size = new Size(200, 45)
            };

            btnSalir = new Button
            {
                Text = "Salir",
                Location = new Point(110, 175),
                Size = new Size(200, 45)
            };

            btnRegistro.Click += btnRegistro_Click;
            btnSalir.Click += btnSalir_Click;

            Controls.AddRange(new Control[] { lblTitulo, btnRegistro, btnSalir });
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            FormRegistro frm = new FormRegistro();
            frm.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Desea salir del sistema?", "Salir",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}