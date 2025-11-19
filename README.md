
# AnaSerena pastry managment system



## Breve descripcion del proyecto

Sistema de gestion para pasteleria AnaSerena enfocado en el seiguimiento de ventas, clientes y productos que se generan cuando se lleva a cabo una feria.

## Insignias 

![Static Badge](https://img.shields.io/badge/.NET_Framework-4.7.2-purple%3Flogo%3Dgithub?logo=dotnet)

![Static Badge](https://img.shields.io/badge/ASP.NET-Web_Forms-purple)

![Static Badge](https://img.shields.io/badge/SQL_Server-2021-yellow)

![Static Badge](https://img.shields.io/badge/C%23-10.0-purple)

![Static Badge](https://img.shields.io/badge/Bootstrap-5.3-%237952B3%3Flogo%3Dgithub?logo=bootstrap&logoColor=white)

![Static Badge](https://img.shields.io/badge/JavaScript-yellow?logo=javascript&logoColor=white)

## Contexto Academico

- **Institucion Educativa**: Quality I.S.A.D
- **Carrera**: Tecnicatura Superior en Desarrollo de Software
- **Materia**: Programacion II & Modelado y Arquitectura de Software
- **Periodo Academico**: 2do año. Comision "B"
- **Docentes a cargo**: Karina Salto (Programacion II) & Fermin Stura (Modelado y Arquitectura de Software)
- **Integrantes del equipo de Desarrollo**: Joel Galera (Product Owner) - Santiago Nietto (Desarrollador Principal) 

## Caracteristicas
- Dashboard de recuento de clientes, productos y ventas
- Gestion de clientes registrados en el sistema
- Gestion de ventas y detalle de ventas
- Gestion de usuarios registrados (Administrador / Empleado)
- Gestion y control de stock de productos


## Stack de tecnologías utilizadas

### **Frontend**
- HTML5
- CSS3
- Bootstrap
- JavaScript

### **Backend** 
- C# 10.0 
- .NET Framework 4.7.2
- ASP.NET Web Forms 

### **Base de Datos**
- SQL Server 2022 

### **Herramientas de Desarrollo**
- Visual Studio 2022
- SQL Server Managment Studio
- Git & GitHub
- Jira
- LucidChart
- Figma

### **Metodología Utilizada**
- SCRUM
- UML

## Arquitectura del sistema

El proyecto sigue una *Arquitectura en Capas (N-tier)* separando responsabilidades:
```
anaserena-pastry-managment-system
│
├── C1_UI/ (Capa de presentacion)
│   ├── Pages/
│   │   ├── LoginUI.aspx
│   │   ├── DashboardUI.aspx
│   │   ├── ClientesUI.aspx
│   │   ├── ProductosUI.aspx
│   │   ├── VentasUI.aspx
│   │   ├── UsuariosUI.aspx
│   │   └── Site.Master
│   ├── Scripts/ (JS, Bootstrap)
│   ├── CSS/ (Estilos.css)
│   └── Resources/ (Imagenes)   
│
├── C2_BLL/ (Capa de logica de negocio)
│   ├── ClienteBLL.cs
│   ├── DashboardBLL.cs
│   ├── ProductoBLL.cs
│   ├── UsuariosBLL.cs
│   └── VentaBLL.cs
│
├── C3_DAL/ (Capa de acceso de datos)
│   ├── ClienteDAL.cs
│   ├── Conexion.cs
│   ├── DashboardDAL.cs
│   ├── DetalleVentaDAL.cs
│   ├── ProductoDAL.cs
│   ├── UsuariosDAL.cs
│   └── VentaDAL.cs
│
└── C4_ENTIDAD/ (Capa de modelo de dominios)
    ├── Cliente.cs
    ├── DetalleVenta.cs
    ├── Domicilio.cs
    ├── Ingredientes.cs
    ├── Producto.cs
    ├── Usuarios.cs
    └── Venta.cs
```
### Descripcion de las Capas

- **UI (Presentacion)**: Interfaz del formulario web con ASP.NET Web Forms, maneja la interaccion con el usuario.
- **BLL (Business Logic Layer)**: Contiene las reglas de negocio y validaciones de las historias de usuario.
- **DAL (Data Access Layer)**: Gestiona todas las operaciones con la base de datos y sus conexiones.
- **Entity (Modelos)**: Clases que representan las entidades del dominio.
## Inicio Rapido


### Requisitos Previos
- Windows 10 / 10 (preferentemente)
- Visual Studio 2022 o superior
- .NET Framework 4.7.2 o superior
- SQL Server Managment Studio
- Git


### Instalación
Clonar el repositorio
```bash
  git clone https://github.com/santiagonietto/anaserena-pastry-managment-system.  git
```
Abrir Visual Studio -> Clonar un repositorio -> Pegar el HTTPS del proyecto -> Clonar

## Cargar la Base de datos

### Crear Base de Datos
Abrir SQL Server Managment Studio -> Conectar instancia de SQL Server -> Navegar hasta carpeta Bases de datos -> Ejecutar el script.sql para realizar una correcta creacion de la base datos

### Verificar conexion a la Base de datos
 
Desde visual studio compilar el proyecto con Cntrl + F5 -> Interactuar con el programa en la carga de datos.
    
## Demo - Funcionalidades Destacadas

### GESTION DE PRODUCTOS
<img width="2541" height="1274" alt="pasteleria" src="https://github.com/user-attachments/assets/0b000a97-7348-4609-8f56-ac58111c17f3" />

### GESTION DE VENTAS
<img width="2545" height="1271" alt="Screenshot_1" src="https://github.com/user-attachments/assets/11f8f52f-0531-44dc-985b-1811e185f525" />


# CORRECTA INSTALACION DE LA BASE DE DATOS

## INSTALACION

### EN SSMS (SQL SERVER MANAGMENT STUDIO):
1. Abrir SQL Server Managment Studio
2. Archivo -> Abrir -> Archivo -> Seleccionar 'script_databases.sql'
3. Presionar F5

### CONNECTION STRING:
Data Source=localhost; Initial Catalog=anaserena_pms_db; Integrated Security=True;
