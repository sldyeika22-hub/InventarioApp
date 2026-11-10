namespace InventarioApp
{
    // Esta clase representa UN producto del inventario.
    // Cada objeto Producto guarda sus propios datos.
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public string Proveedor { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }

        // Estado no se almacena: se calcula usando el stock actual.
        public string Estado
        {
            get
            {
                if (Stock <= StockMinimo)
                {
                    return "Stock bajo";
                }

                return "Normal";
            }
        }

        // Constructor: prepara las propiedades de texto con valores vacíos.
        public Producto()
        {
            Nombre = "";
            Categoria = "";
            Proveedor = "";
        }
    }
}
