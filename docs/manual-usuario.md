# Manual de usuario – InventarioApp

## 1. Descripción

InventarioApp es una aplicación de escritorio para registrar productos, consultar el inventario y actualizar las existencias mediante entradas y salidas. La aplicación no utiliza una base de datos: la información permanece en memoria mientras el programa está abierto.

## 2. Abrir el sistema

1. Abra la solución `InventarioApp.slnx` en Visual Studio. También puede abrir `InventarioApp.csproj` si su versión de Visual Studio no reconoce el archivo de solución.
2. Seleccione el proyecto `InventarioApp` como proyecto de inicio.
3. Presione **F5** o el botón **Iniciar**.
4. En la ventana principal, presione **Administrar inventario**.

## 3. Registrar un producto

1. En el grupo **Datos del producto**, escriba el nombre.
2. Seleccione una categoría: Alimentos, Bebidas, Limpieza, Tecnología, Papelería u Otros.
3. Escriba el proveedor.
4. Indique el stock inicial y el stock mínimo.
5. Presione **Registrar**.

El sistema verifica que el nombre no esté vacío y tenga al menos tres caracteres, que se haya seleccionado una categoría, que el proveedor no esté vacío y que el stock y el stock mínimo sean números enteros no negativos. Tampoco permite registrar otro producto con el mismo nombre, sin distinguir entre mayúsculas y minúsculas.

Después de un registro exitoso, la tabla y las estadísticas se actualizan y el formulario queda listo para otro producto.

## 4. Consultar, buscar y filtrar

- La tabla muestra el ID, nombre, categoría, proveedor, stock, stock mínimo y estado.
- Escriba una palabra en **Texto (nombre, categoría o proveedor)** para buscar coincidencias.
- Utilice el filtro **Categoría** para mostrar una categoría específica o seleccione **Todas**.
- Las estadísticas del resumen corresponden a todo el inventario, aunque la tabla tenga un filtro aplicado.
- Las filas con stock igual o inferior al mínimo se resaltan para facilitar su identificación.

## 5. Seleccionar un producto

Haga clic en una fila de la tabla. Los datos del producto se cargarán en los campos del formulario. Al seleccionar un producto, el campo de stock queda bloqueado para evitar cambios directos.

Si una búsqueda o un filtro oculta el producto seleccionado, el formulario se limpia y se desactiva la selección. Esto evita operar accidentalmente sobre un producto que ya no aparece en los resultados.

## 6. Modificar un producto

1. Seleccione el producto en la tabla.
2. Modifique el nombre, la categoría, el proveedor o el stock mínimo.
3. Presione **Actualizar**.

El stock actual no se modifica desde esta opción. Para cambiar las existencias utilice las operaciones de entrada y salida. Después de actualizar, el sistema limpia los filtros y vuelve a mostrar el producto modificado.

## 7. Eliminar un producto

1. Seleccione el producto en la tabla.
2. Presione **Eliminar**.
3. Lea el mensaje de confirmación.
4. Seleccione **Sí** para eliminarlo o **No** para cancelar.

La eliminación retira el producto de la lista en memoria y no puede deshacerse desde la aplicación.

## 8. Registrar una entrada

Una entrada aumenta las existencias de un producto.

1. Seleccione el producto en la tabla.
2. En **Movimientos de existencias**, escriba una cantidad entera mayor que cero.
3. Presione **Registrar entrada (+)**.

El sistema suma la cantidad al stock actual. Si la cantidad no es válida o se produce un error, se muestra un mensaje y no se aplica la operación.

## 9. Registrar una salida

Una salida disminuye las existencias de un producto.

1. Seleccione el producto en la tabla.
2. Escriba una cantidad entera mayor que cero.
3. Presione **Registrar salida (-)**.

El sistema resta la cantidad al stock actual. No permite retirar más unidades de las disponibles, por lo que el stock no puede quedar negativo.

## 10. Interpretar el estado y las estadísticas

- **Normal:** el stock actual es mayor que el stock mínimo.
- **Stock bajo:** el stock actual es menor o igual que el stock mínimo.
- **Productos:** cantidad de productos registrados.
- **Unidades:** suma de las existencias de todos los productos.
- **Stock bajo:** cantidad de productos que alcanzaron o están por debajo de su mínimo.

Las estadísticas se calculan sobre todos los productos, no solo sobre los resultados visibles de una búsqueda.

## 11. Limpiar el formulario y cerrar

- Presione **Nuevo / Limpiar** para quitar la selección y preparar un nuevo registro.
- Presione **Cerrar ventana** para cerrar el formulario de administración y regresar al menú principal.
- Presione **Salir** en el menú principal y confirme para cerrar la aplicación.

## 12. Advertencia sobre el almacenamiento

Los productos se guardan únicamente en una lista en memoria. Cerrar el formulario de administración y regresar al menú principal no elimina los datos mientras la aplicación siga abierta. Sin embargo, al cerrar completamente el programa, todos los productos y movimientos acumulados se pierden. No utilice esta versión para información que deba conservarse permanentemente.
