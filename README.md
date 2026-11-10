# InventarioApp – Sistema de gestión de inventario

Aplicación de escritorio desarrollada en C# con Windows Forms y .NET Framework 4.7.2. Permite registrar, consultar y administrar productos, así como controlar sus existencias mediante entradas y salidas.

> **Almacenamiento:** el proyecto no utiliza una base de datos ni guarda información en archivos. Los productos permanecen en una lista en memoria mientras la aplicación está abierta. Al cerrar completamente el programa, los datos se pierden.

## Funcionalidades

- Registrar productos con nombre, categoría, proveedor, stock inicial y stock mínimo.
- Validar campos obligatorios, nombres duplicados y cantidades enteras no negativas.
- Buscar productos por nombre, categoría o proveedor.
- Filtrar productos por categoría.
- Modificar el nombre, la categoría, el proveedor y el stock mínimo de un producto.
- Eliminar productos con confirmación.
- Registrar entradas de unidades y aumentar el stock.
- Registrar salidas de unidades sin permitir que el stock quede negativo.
- Mostrar el total de productos, las unidades disponibles y los productos con stock bajo.
- Resaltar los productos cuyo stock sea igual o inferior al stock mínimo.

## Tecnologías

| Elemento | Tecnología |
|---|---|
| Lenguaje | C# |
| Interfaz gráfica | Windows Forms |
| Plataforma | .NET Framework 4.7.2 |
| IDE recomendado | Visual Studio con desarrollo de escritorio de .NET |
| Almacenamiento | `List<Producto>` en memoria |
| Base de datos | No utiliza |

## Estructura del proyecto

```text
InventarioApp/
├── Program.cs
├── FormPrincipal.cs
├── FormRegistro.cs
├── Producto.cs
├── GestorInventario.cs
├── InventarioApp.csproj
├── InventarioApp.slnx
├── App.config
├── README.md
├── Properties/
└── docs/
    ├── propuesta.md
    ├── manual-usuario.md
    └── manual-tecnico.md
```

## Cómo abrir y ejecutar

1. Descomprima el proyecto.
2. Abra `InventarioApp.slnx` en Visual Studio. Si la versión instalada no reconoce ese formato, abra `InventarioApp.csproj`.
3. Compruebe que está instalado el componente de desarrollo de escritorio con .NET y que el equipo dispone de .NET Framework 4.7.2 o su paquete de desarrollo.
4. Seleccione `InventarioApp` como proyecto de inicio.
5. Ejecute con **F5** o con el botón **Iniciar**.

## Uso básico

1. En el menú principal, presione **Administrar inventario**.
2. Para registrar un producto, complete los campos y presione **Registrar**.
3. Para consultar, seleccione una fila de la tabla. Puede utilizar el campo de búsqueda y el filtro de categoría.
4. Para modificar los datos generales, seleccione el producto, cambie los campos habilitados y presione **Actualizar**.
5. Para cambiar las existencias, seleccione el producto, escriba una cantidad y utilice **Registrar entrada (+)** o **Registrar salida (-)**.
6. Para eliminarlo, selecciónelo y presione **Eliminar**. La aplicación pedirá confirmación.
7. Utilice **Nuevo / Limpiar** para preparar el formulario para otro registro.

## Documentación

- [Propuesta del proyecto](docs/propuesta.md): problema, justificación, objetivos, alcance y distribución de trabajo.
- [Manual de usuario](docs/manual-usuario.md): instrucciones para utilizar las funciones del sistema.
- [Manual técnico](docs/manual-tecnico.md): arquitectura, clases, métodos, validaciones y flujo de ejecución.

## Limitaciones conocidas

- Los datos se conservan solamente durante la ejecución de la aplicación.
- Al cerrar la aplicación, los productos y los identificadores en memoria se reinician.
- No se guarda un historial de entradas y salidas; únicamente se actualiza el stock actual.
- Los nombres de productos deben ser únicos en todo el inventario, sin distinguir mayúsculas y minúsculas.
- Las estadísticas muestran el inventario completo, aunque la tabla esté filtrada.
