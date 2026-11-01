# InventarioApp

## Descripción
Aplicación de escritorio desarrollada en C# con Windows Forms para controlar el inventario de un pequeño negocio. Permite registrar productos con su categoría, proveedor y existencias, y mostrarlos en una tabla organizada, alertando cuando el stock es bajo.

## Objetivo
Desarrollar un sistema de inventario sencillo y funcional que permita administrar productos y sus existencias, aplicando los conceptos de programación en C#, interfaces gráficas, validaciones, colecciones y control de versiones con Git y GitHub.

## Integrantes
- Yeika Macho
- Armando Chavarria M
- Yafeth Rodriguez Fernandez 
- Keilor
- [Nombre del integrante 5]

## Tecnologías
| Elemento | Tecnología |
|---|---|
| Lenguaje | C# |
| Interfaz | Windows Forms |
| Plataforma | .NET / Visual Studio |
| Control de versiones | Git |
| Repositorio | GitHub |
| Documentación | Markdown |

## Funcionalidades iniciales (Avance 1)
- Formulario principal con navegación y botón de salida.
- Registro de productos (nombre, categoría, proveedor, stock y stock mínimo).
- Validaciones: campos obligatorios, datos numéricos, valores negativos y productos duplicados.
- Almacenamiento en memoria mediante una lista (List<Producto>).
- Presentación de los productos en un DataGridView.
- Alerta visual y mensaje cuando el stock es bajo.
- Limpieza de controles y mensajes con MessageBox.

## Estado
Avance 1: primera versión funcional. Pendiente para el Avance 2: consultas por categoría o proveedor, búsquedas, modificación, eliminación, entradas y salidas de existencias, y manuales.

## Estructura
```
InventarioApp/
├── Producto.cs           (modelo de datos)
├── GestorInventario.cs   (lógica, validaciones y lista en memoria)
├── FormPrincipal.cs      (formulario principal y navegación)
├── FormRegistro.cs       (registro de productos y DataGridView)
├── Program.cs            (punto de entrada)
├── README.md
└── docs/
    └── propuesta.md
```


## Ejecución
1. Clonar el repositorio: git clone https://github.com/sldyeika22-hub/InventarioApp.git
2. Abrir la solución en Visual Studio.
3. Ejecutar con F5 (el formulario de inicio es FormPrincipal).
