namespace InventarioApp
{
    // Modelo de datos: representa un producto del inventario
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Categoria { get; set; } = "";
        public string Proveedor { get; set; } = "";
        public int Stock { get; set; }
        public int StockMinimo { get; set; }

        // Propiedad calculada: se muestra como columna en el DataGridView
        public string Estado
        {
            get { return Stock <= StockMinimo ? "Stock bajo" : "Normal"; }
        }
    }
}