using System;
using System.Collections.Generic;
using System.Linq;

namespace InventarioApp
{
    // Capa de LÓGICA + DATOS: validaciones y lista en memoria.
    // Es estática para que todos los formularios compartan la misma lista.
    public static class GestorInventario
    {
        private static readonly List<Producto> productos = new List<Producto>();
        private static int siguienteId = 1;

        // Devuelve una copia de la lista para mostrarla en el DataGridView
        public static List<Producto> ObtenerTodos()
        {
            return new List<Producto>(productos);
        }

        public static bool ExisteNombre(string nombre)
        {
            return productos.Any(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }

        // Valida y convierte los datos. Devuelve "" si todo está bien,
        // o el mensaje de error si algo falla.
        public static string Validar(string nombre, string categoria, string proveedor,
                                     string stockTexto, string minimoTexto,
                                     out int stock, out int minimo)
        {
            stock = 0;
            minimo = 0;

            if (string.IsNullOrWhiteSpace(nombre))
                return "El nombre es obligatorio.";
            if (nombre.Trim().Length < 3)
                return "El nombre debe tener al menos 3 caracteres.";
            if (string.IsNullOrWhiteSpace(categoria))
                return "Debe seleccionar una categoría.";
            if (string.IsNullOrWhiteSpace(proveedor))
                return "El proveedor es obligatorio.";

            // Conversión de datos: texto -> número
            if (!int.TryParse(stockTexto, out stock))
                return "El stock debe ser un número entero.";
            if (!int.TryParse(minimoTexto, out minimo))
                return "El stock mínimo debe ser un número entero.";

            if (stock < 0 || minimo < 0)
                return "El stock y el stock mínimo no pueden ser negativos.";

            if (ExisteNombre(nombre.Trim()))
                return "Ya existe un producto con ese nombre.";

            return "";
        }

        public static void Agregar(string nombre, string categoria, string proveedor, int stock, int minimo)
        {
            Producto nuevo = new Producto
            {
                Id = siguienteId,
                Nombre = nombre.Trim(),
                Categoria = categoria.Trim(),
                Proveedor = proveedor.Trim(),
                Stock = stock,
                StockMinimo = minimo
            };

            productos.Add(nuevo);
            siguienteId++;
        }
    }
}