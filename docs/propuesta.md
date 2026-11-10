# Propuesta del proyecto

## Nombre del proyecto

**InventarioApp: Sistema de gestión de inventario**

## Integrantes

- Yeika Macho
- Armando Chavarria M
- Yafeth Rodriguez Fernandez
- Keylor Diaz Davila
- Yeimy Villalobos Fernandez

## Problema

Los pequeños negocios pueden llevar el control de sus productos de forma manual, por ejemplo, mediante cuadernos o registros dispersos. Este procedimiento puede provocar errores en las existencias, dificultar la búsqueda de productos y hacer que se detecte tarde cuando un artículo está por agotarse.

## Justificación

Una aplicación de escritorio sencilla permite organizar los datos de los productos en una sola interfaz, consultar la información con rapidez y controlar las existencias. El desarrollo también permite aplicar conocimientos de C#, programación orientada a objetos, Windows Forms, validaciones, colecciones, eventos y trabajo colaborativo con Git y GitHub.

## Usuarios destinatarios

- Encargados o propietarios de pequeños negocios.
- Personal responsable de bodega o de registrar y consultar productos.

## Objetivo general

Desarrollar una aplicación de escritorio en C# con Windows Forms que permita administrar productos y controlar sus existencias de manera organizada.

## Objetivos específicos

1. Registrar productos con nombre, categoría, proveedor, stock inicial y stock mínimo.
2. Validar los datos ingresados para reducir errores y evitar nombres duplicados.
3. Mantener los productos en una lista en memoria y mostrarlos en un `DataGridView`.
4. Buscar productos por texto y filtrar por categoría.
5. Modificar los datos generales y eliminar productos registrados.
6. Actualizar las existencias mediante operaciones de entrada y salida.
7. Mostrar estadísticas generales y advertir visualmente cuando el stock sea bajo.
8. Documentar el uso y la estructura técnica del sistema en Markdown.

## Alcance

El sistema incluye:

- Una ventana principal para ingresar al módulo de inventario o salir de la aplicación.
- Un formulario para registrar, consultar, buscar, modificar y eliminar productos.
- Categorías predefinidas: Alimentos, Bebidas, Limpieza, Tecnología, Papelería y Otros.
- Validación de nombre obligatorio, longitud mínima de tres caracteres, categoría y proveedor obligatorios.
- Validación de stock y stock mínimo como números enteros no negativos.
- Control de nombres duplicados sin distinguir mayúsculas y minúsculas.
- Búsqueda por nombre, categoría o proveedor y filtro específico por categoría.
- Entradas y salidas de existencias mediante cantidades enteras positivas.
- Prevención de salidas superiores al stock disponible.
- Indicador de stock bajo cuando el stock actual es menor o igual al stock mínimo.
- Estadísticas de cantidad de productos, total de unidades y productos con stock bajo.
- Documentación del proyecto mediante `README.md` y tres documentos dentro de `docs/`.

## Fuera de alcance y limitaciones

- No se utiliza una base de datos.
- No se guarda información en archivos. Los productos se mantienen en una lista en memoria y se pierden al cerrar completamente la aplicación.
- No se mantiene un historial de movimientos; las entradas y salidas actualizan únicamente el stock actual.
- No incluye inicio de sesión, permisos de usuario, facturación, contabilidad, versión web o móvil ni exportación a Excel.

## Tecnologías y herramientas

| Elemento | Tecnología |
|---|---|
| Lenguaje | C# |
| Interfaz | Windows Forms |
| Plataforma | .NET Framework 4.7.2 |
| Entorno de desarrollo | Visual Studio |
| Control de versiones | Git y GitHub |
| Colección principal | `List<Producto>` |

## Descripción general del funcionamiento

`Program.cs` inicia `FormPrincipal`. Desde la ventana principal se abre `FormRegistro`. Este formulario recibe las acciones del usuario y llama a los métodos de `GestorInventario`. La clase `GestorInventario` administra la lista de objetos definidos por `Producto.cs`, aplica las validaciones y realiza los cambios sobre las existencias.

## Documentación incluida

La documentación del proyecto se limita a los siguientes archivos Markdown:

- `README.md`: descripción general, tecnologías, estructura e instrucciones iniciales.
- `docs/propuesta.md`: problema, justificación, objetivos y alcance.
- `docs/manual-usuario.md`: instrucciones para utilizar la aplicación.
- `docs/manual-tecnico.md`: arquitectura y explicación técnica del código.

## Distribución de tareas

La siguiente tabla conserva la distribución de trabajo indicada por el equipo y resume los módulos relacionados con el alcance del proyecto.

| Integrante | Responsabilidades indicadas |
|---|---|
| Yeika Macho         | Repositorio, formulario principal, navegación, salida, registro con validaciones, clase `Producto`, lista en memoria, tabla, alerta de stock bajo, README y propuesta; revisión general. |
| Armando Chavarria M | Redacción del problema y justificación; búsquedas y consultas por categoría o proveedor. |
| Integrante 3        | Apoyo en el control de entradas y salidas de existencias. |
| Yafeth Rodriguez    | Estadísticas del inventario; modificación y eliminación de productos. |
| Integrante 5        | Manual de usuario y manual técnico. |
| Todos | Commits propios en GitHub, revisión del código y documentación de sus módulos. |

> Actualice los nombres provisionales y la distribución de tareas si el equipo acuerda cambios. La tabla describe la asignación reportada por el equipo; no sustituye el historial de commits de GitHub.
