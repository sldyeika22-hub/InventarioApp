using System;
using System.Collections.Generic;
using System.Linq;

namespace InventarioApp
{
    // Esta clase contiene los datos y las reglas del inventario.
    // No dibuja ventanas ni conoce TextBox o Button.
    public static class GestorInventario
    {
        // Lista compartida por toda la aplicación. Solo existe mientras
        // el programa permanece abierto; no es una base de datos.
        private static readonly List<Producto> productos = new List<Producto>();

        // El siguiente identificador que se asignará a un producto nuevo.
        private static int siguienteId = 1;

        // Devuelve los productos ordenados por ID, del más nuevo al más antiguo.
        public static List<Producto> ObtenerTodos()
        {
            return productos.OrderByDescending(p => p.Id).ToList();
        }

        // Busca por nombre, categoría o proveedor y también permite filtrar
        // por una categoría específica. Si texto está vacío, no busca texto.
        public static List<Producto> Buscar(string texto, string categoria)
        {
            IEnumerable<Producto> resultado = productos;

            if (!string.IsNullOrWhiteSpace(texto))
            {
                string palabra = texto.Trim();

                resultado = resultado.Where(p =>
                    ContieneTexto(p.Nombre, palabra) ||
                    ContieneTexto(p.Categoria, palabra) ||
                    ContieneTexto(p.Proveedor, palabra));
            }

            if (!string.IsNullOrWhiteSpace(categoria) && categoria != "Todas")
            {
                resultado = resultado.Where(p =>
                    p.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase));
            }

            return resultado.OrderByDescending(p => p.Id).ToList();
        }

        // Comprueba si un texto aparece dentro de otro, sin distinguir mayúsculas.
        private static bool ContieneTexto(string textoCompleto, string textoBuscado)
        {
            return textoCompleto.IndexOf(textoBuscado, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        // Busca un producto por su ID. Devuelve null si no lo encuentra.
        public static Producto ObtenerPorId(int id)
        {
            return productos.FirstOrDefault(p => p.Id == id);
        }

        // Comprueba nombres repetidos. Al modificar se excluye el ID actual,
        // para que un producto pueda conservar su propio nombre.
        public static bool ExisteNombre(string nombre, int idExcluir)
        {
            return productos.Any(p =>
                p.Id != idExcluir &&
                p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }

        // Valida los datos del formulario y convierte los números.
        // Devuelve una cadena vacía cuando todo está correcto.
        // Si hay un problema, devuelve el mensaje que debe mostrarse al usuario.
        public static string Validar(
            string nombre,
            string categoria,
            string proveedor,
            string stockTexto,
            string minimoTexto,
            int idExcluir,
            out int stock,
            out int minimo)
        {
            stock = 0;
            minimo = 0;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                return "El nombre es obligatorio.";
            }

            if (nombre.Trim().Length < 3)
            {
                return "El nombre debe tener al menos 3 caracteres.";
            }

            if (string.IsNullOrWhiteSpace(categoria))
            {
                return "Debe seleccionar una categoría.";
            }

            if (string.IsNullOrWhiteSpace(proveedor))
            {
                return "El proveedor es obligatorio.";
            }

            // TryParse intenta convertir texto en un número entero.
            // Si no puede, devuelve false y mostramos un mensaje.
            if (!int.TryParse(stockTexto, out stock))
            {
                return "El stock debe ser un número entero.";
            }

            if (!int.TryParse(minimoTexto, out minimo))
            {
                return "El stock mínimo debe ser un número entero.";
            }

            if (stock < 0 || minimo < 0)
            {
                return "El stock y el stock mínimo no pueden ser negativos.";
            }

            if (ExisteNombre(nombre.Trim(), idExcluir))
            {
                return "Ya existe un producto con ese nombre.";
            }

            return "";
        }

        // Crea un objeto Producto y lo guarda en la lista.
        public static Producto Agregar(string nombre, string categoria, string proveedor,
                                        int stock, int minimo)
        {
            Producto nuevo = new Producto();
            nuevo.Id = siguienteId;
            nuevo.Nombre = nombre.Trim();
            nuevo.Categoria = categoria.Trim();
            nuevo.Proveedor = proveedor.Trim();
            nuevo.Stock = stock;
            nuevo.StockMinimo = minimo;

            productos.Add(nuevo);
            siguienteId++;

            return nuevo;
        }

        // Modifica los datos generales de un producto que ya existe.
        // El stock se cambia desde las opciones de entrada y salida.
        public static bool Modificar(int id, string nombre, string categoria,
                                     string proveedor, int minimo)
        {
            Producto producto = ObtenerPorId(id);

            if (producto == null)
            {
                return false;
            }

            producto.Nombre = nombre.Trim();
            producto.Categoria = categoria.Trim();
            producto.Proveedor = proveedor.Trim();
            producto.StockMinimo = minimo;

            return true;
        }

        // Elimina el producto indicado por su ID.
        public static bool Eliminar(int id)
        {
            Producto producto = ObtenerPorId(id);

            if (producto == null)
            {
                return false;
            }

            productos.Remove(producto);
            return true;
        }

        // Suma unidades al stock. La cantidad del movimiento debe ser positiva.
        public static string RegistrarEntrada(int id, int cantidad)
        {
            if (cantidad <= 0)
            {
                return "La cantidad de entrada debe ser mayor que cero.";
            }

            Producto producto = ObtenerPorId(id);

            if (producto == null)
            {
                return "No se encontró el producto seleccionado.";
            }

            // Evita superar el valor máximo que puede guardar un int.
            if (producto.Stock > int.MaxValue - cantidad)
            {
                return "La cantidad supera el máximo permitido.";
            }

            producto.Stock += cantidad;
            return "";
        }

        // Resta unidades del stock, sin permitir que el inventario quede negativo.
        public static string RegistrarSalida(int id, int cantidad)
        {
            if (cantidad <= 0)
            {
                return "La cantidad de salida debe ser mayor que cero.";
            }

            Producto producto = ObtenerPorId(id);

            if (producto == null)
            {
                return "No se encontró el producto seleccionado.";
            }

            if (cantidad > producto.Stock)
            {
                return "No hay suficiente stock para realizar esa salida.";
            }

            producto.Stock -= cantidad;
            return "";
        }

        // Métodos para calcular las estadísticas que aparecen en pantalla.
        public static int ContarProductos()
        {
            return productos.Count;
        }

        public static long ObtenerTotalUnidades()
        {
            return productos.Sum(p => (long)p.Stock);
        }

        public static int ContarProductosConStockBajo()
        {
            return productos.Count(p => p.Stock <= p.StockMinimo);
        }
    }
}
