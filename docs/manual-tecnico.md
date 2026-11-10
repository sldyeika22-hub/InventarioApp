# Manual técnico – InventarioApp

## 1. Información general

- **Nombre:** InventarioApp.
- **Lenguaje:** C#.
- **Interfaz:** Windows Forms.
- **Plataforma:** .NET Framework 4.7.2.
- **Almacenamiento:** lista estática de objetos `Producto` en memoria.
- **Base de datos:** no utiliza.

Los formularios se construyen mediante código C#. No se utilizan archivos `FormPrincipal.Designer.cs` ni `FormRegistro.Designer.cs`.

## 2. Estructura de archivos principales

| Archivo | Responsabilidad |
|---|---|
| `Program.cs` | Punto de entrada. Inicia Windows Forms y muestra `FormPrincipal`. |
| `FormPrincipal.cs` | Presentación del menú principal, apertura del formulario de inventario y salida. |
| `FormRegistro.cs` | Construcción de controles, eventos, lectura de datos, selección de productos y actualización de la interfaz. |
| `Producto.cs` | Modelo que define las propiedades de cada producto y calcula su estado. |
| `GestorInventario.cs` | Administración de la lista, reglas de validación, búsquedas y operaciones del inventario. |
| `InventarioApp.csproj` | Configuración de compilación, referencias y archivos que forman parte del proyecto. |
| `App.config` | Configuración de la aplicación para .NET Framework. |
| `README.md` y `docs/` | Documentación del proyecto. |

## 3. Arquitectura y responsabilidades

El proyecto separa tres responsabilidades principales:

1. **Modelo (`Producto.cs`):** representa un producto y sus datos.
2. **Gestión (`GestorInventario.cs`):** aplica las reglas y modifica los datos en memoria.
3. **Presentación (`FormPrincipal.cs` y `FormRegistro.cs`):** muestra ventanas, recibe acciones y presenta los resultados.

`FormRegistro` no administra directamente la colección. Cuando el usuario realiza una operación, el formulario llama a un método de `GestorInventario`.

## 4. Modelo Producto

La clase `Producto` contiene las propiedades:

- `Id`: identificador asignado al registrar el producto.
- `Nombre`: nombre del producto.
- `Categoria`: categoría seleccionada.
- `Proveedor`: proveedor indicado.
- `Stock`: unidades disponibles.
- `StockMinimo`: cantidad mínima definida para el producto.
- `Estado`: propiedad calculada; devuelve **Stock bajo** si `Stock <= StockMinimo`, y **Normal** en caso contrario.

El constructor inicializa las propiedades de texto con cadenas vacías.

## 5. GestorInventario

`GestorInventario` es una clase estática. No requiere crear un objeto para utilizar sus métodos.

La colección principal se declara así:

```csharp
private static readonly List<Producto> productos = new List<Producto>();
```

- `private`: impide acceder directamente a la lista desde otras clases.
- `static`: comparte la misma colección durante la ejecución de la aplicación.
- `readonly`: impide reemplazar la referencia de la lista después de inicializarla; no impide agregar o quitar elementos.

El campo `siguienteId` asigna identificadores consecutivos. Los identificadores eliminados no se reutilizan mientras la aplicación permanezca abierta.

### 5.1 Métodos principales

| Método | Propósito y resultado |
|---|---|
| `ObtenerTodos()` | Devuelve una nueva lista con los productos ordenados por ID descendente. |
| `Buscar(texto, categoria)` | Filtra por nombre, categoría o proveedor y, opcionalmente, por categoría. Devuelve una lista. |
| `ObtenerPorId(id)` | Busca por identificador. Devuelve el objeto o `null`. |
| `ExisteNombre(nombre, idExcluir)` | Comprueba si otro producto ya utiliza el nombre. Devuelve `bool`. |
| `Validar(...)` | Comprueba los datos y convierte stock y stock mínimo. Devuelve una cadena vacía si no encuentra errores o un mensaje de error. |
| `Agregar(...)` | Crea un `Producto`, lo añade a la lista y devuelve el objeto creado. |
| `Modificar(...)` | Modifica nombre, categoría, proveedor y stock mínimo. Devuelve `bool`. |
| `Eliminar(id)` | Quita de la lista el producto indicado. Devuelve `bool`. |
| `RegistrarEntrada(id, cantidad)` | Suma unidades si la cantidad es válida y el producto existe. Devuelve cadena vacía o un mensaje de error. |
| `RegistrarSalida(id, cantidad)` | Resta unidades si hay stock suficiente. Devuelve cadena vacía o un mensaje de error. |
| `ContarProductos()` | Devuelve la cantidad de productos. |
| `ObtenerTotalUnidades()` | Devuelve la suma del stock de todos los productos como `long`. |
| `ContarProductosConStockBajo()` | Cuenta los productos cuyo stock es menor o igual al mínimo. |

### 5.2 Reglas de validación

- El nombre es obligatorio y debe tener al menos tres caracteres después de aplicar `Trim()`.
- La categoría es obligatoria.
- El proveedor es obligatorio.
- El nombre debe ser único en todo el inventario; la comparación no distingue mayúsculas y minúsculas.
- El stock inicial y el stock mínimo deben ser números enteros no negativos.
- Las cantidades de entrada y salida deben ser enteros positivos.
- Una salida no puede superar el stock actual.
- Una entrada no puede provocar un desbordamiento del tipo `int`.

## 6. Formularios y eventos

### 6.1 FormPrincipal

`Program.Main()` ejecuta `Application.Run(new FormPrincipal())`. El formulario principal muestra el botón **Administrar inventario**, que crea `FormRegistro` y lo muestra con `ShowDialog(this)`. El botón **Salir** solicita confirmación y cierra el formulario principal.

### 6.2 FormRegistro

La interfaz se construye en `ConstruirInterfaz()`. Los métodos auxiliares `AgregarEtiqueta()`, `CrearTexto()`, `CrearBoton()` y `CrearEtiquetaEstadistica()` evitan repetir la configuración de controles.

Los eventos principales son:

| Evento | Acción |
|---|---|
| `btnGuardar_Click` | Valida y registra un producto nuevo. |
| `btnActualizar_Click` | Valida y modifica los datos generales del producto seleccionado. |
| `btnEliminar_Click` | Solicita confirmación y elimina el producto seleccionado. |
| `btnNuevo_Click` | Limpia los controles y prepara un registro nuevo. |
| `btnEntrada_Click` | Valida la cantidad y registra una entrada. |
| `btnSalida_Click` | Valida la cantidad y registra una salida. |
| `btnCerrar_Click` | Cierra el formulario de administración. |
| `Filtros_TextChanged` | Actualiza los resultados cuando cambia el texto o la categoría del filtro. |
| `dgvProductos_CellClick` | Obtiene el objeto asociado a la fila y lo carga en el formulario. |

`productoSeleccionadoId` conserva el ID de la fila seleccionada. El valor `0` indica que no hay selección. Cuando un filtro oculta el producto seleccionado, se limpia el formulario para evitar operar sobre un elemento que ya no aparece en la tabla.

## 7. Enlace de datos con DataGridView

El control `dgvProductos` utiliza columnas creadas manualmente en `ConfigurarColumnas()` y `AgregarColumna()`. La propiedad `DataPropertyName` de cada columna coincide con una propiedad de `Producto` (por ejemplo, `Nombre`, `Stock` o `Estado`).

`ActualizarTabla()` asigna `null` a `DataSource` y después enlaza la lista devuelta por `GestorInventario.Buscar()`. A continuación recorre las filas y resalta las que tienen stock bajo.

Las estadísticas se actualizan mediante `ActualizarEstadisticas()` y se calculan sobre toda la lista, independientemente de los filtros aplicados a la tabla.

## 8. Flujos principales

### Registro

`btnGuardar_Click` → `ValidarFormulario` → `GestorInventario.Validar` → `GestorInventario.Agregar` → `ActualizarTabla` → `ActualizarEstadisticas` → `PrepararModoNuevo`.

### Modificación

`dgvProductos_CellClick` → `CargarProductoEnFormulario` → `btnActualizar_Click` → `ValidarFormulario` → `GestorInventario.Modificar` → actualización de tabla y estadísticas.

### Eliminación

`btnEliminar_Click` → confirmación del usuario → `GestorInventario.Eliminar` → actualización de tabla y estadísticas.

### Entrada y salida

`btnEntrada_Click` o `btnSalida_Click` → validación de cantidad con `int.TryParse` → `GestorInventario.RegistrarEntrada` o `GestorInventario.RegistrarSalida` → actualización de tabla y estadísticas.

## 9. Almacenamiento y limitaciones

La información se mantiene en `List<Producto>` durante la ejecución. No existe conexión a SQL Server, SQLite ni otro motor de base de datos, y tampoco se escribe información a archivos. Al cerrar completamente la aplicación, la lista y el contador de identificadores vuelven a su estado inicial.

El sistema tampoco conserva un historial de entradas y salidas. Solo mantiene el stock actualizado. Por tanto, las estadísticas y los datos son temporales y no deben considerarse registros permanentes.

## 10. Compilación y ejecución

1. Abrir `InventarioApp.slnx` o `InventarioApp.csproj` en Visual Studio.
2. Confirmar que el entorno dispone del soporte para Windows Forms y .NET Framework 4.7.2.
3. Seleccionar el proyecto `InventarioApp` como proyecto de inicio.
4. Compilar la solución y revisar la lista de errores y advertencias.
5. Ejecutar con **F5**.

La verificación final de compilación debe realizarse en Visual Studio con el entorno de .NET Framework correspondiente.
