# Medical Appointment Management System - San Vicente Hospital

This project is a web application developed in C# with ASP.NET Core MVC to digitize and optimize the management of medical appointments, patients, and doctors at San Vicente Hospital, replacing manual systems based on physical agendas and spreadsheets.

## Developer Information
- **Name:** Jhon sebastián villa 
- **Clan:** Linus - Ruta C#
- **Email:** Jhonvilla.2000@hotmail.com
- **ID Number:** 1007238614

---

## Project Description
San Vicente Hospital faced multiple issues with its manual appointment management system, including duplicate bookings, difficulty accessing patient information, lack of control over doctors, and the risk of information loss.

This system was developed to solve these problems by providing a centralized, robust, and efficient platform that ensures the integrity, consistency, and accessibility of all information related to the appointment scheduling process.

### Main Features
The system implements the following functionalities:

#### Patient Management
- **Registration and Editing:** Allows creating new patients and modifying their personal information (name, document, age, etc.).
- **Complete Listing:** Displays a list of all patients registered in the system.
- **Duplicate Validation:** Ensures that two patients cannot be registered with the same document number.

#### Doctor Management
- **Registration and Editing:** Allows registering new doctors with their basic data and specialty.
- **Listing and Filtering:** Displays a list of all doctors, with a key feature to filter the view by specialty.
- **Duplicate Validation:** Guarantees that each doctor's ID document is unique.

#### Medical Appointment Management
- **Smart Scheduling:** Allows scheduling appointments by associating a patient and a doctor with a specific date and time.
- **Conflict Validation:** The system automatically validates that neither the doctor nor the patient has another appointment at the same time, preventing conflicts.
- **Status Management:** Allows changing an appointment's status to "Attended" or "Canceled" through direct actions in the interface.
- **Advanced Filtering:** The main appointment list can be dynamically filtered to show only the appointments for a specific patient or doctor.

---

## Technologies Used
* **Language:** C# 10
* **Framework:** ASP.NET Core 6.0 MVC
* **ORM:** Entity Framework Core 6.0
* **Database:** MySQL
* **Frontend:** HTML, CSS, Bootstrap 5
* **Package Management:** NuGet

---

## Prerequisites
To clone and run this project on your local machine, you will need the following installed:
* [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) or later.
* A code editor such as [Visual Studio Code](https://code.visualstudio.com/) or Visual Studio 2022.
* [Git](https://git-scm.com/downloads).
* A MySQL database server (you can use [MySQL Community Server](https://dev.mysql.com/downloads/mysql/), XAMPP, or a Docker instance).
* A database management tool like DBeaver or MySQL Workbench (optional, but recommended).

---

## Installation and Execution Guide
Follow these detailed steps to get the application up and running.

### 1. Clone the Repository
Open a terminal and clone this repository to your local machine using the following command:
```bash
git clone [https://docs.github.com/en/repositories/creating-and-managing-repositories/about-repositories](https://docs.github.com/en/repositories/creating-and-managing-repositories/about-repositories)
```
Navigate to the newly cloned project folder:
```bash
cd [Project-Folder-Name]
```

### 2. Configure the Database Connection
Before running the application, you must configure the connection to your MySQL database.

1.  **Create an empty database** on your MySQL server. You can name it `HospitalRiwiDB` or whatever you prefer.
2.  **Open the `appsettings.json` file** located in the root of the project.
3.  **Modify the connection string** (`DefaultConnection`) with your own credentials.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=HospitalRiwiDB;User=your_mysql_user;Password=your_mysql_password;"
  }
}
```
Be sure to replace `your_mysql_user` and `your_mysql_password` with the correct credentials.

### 3. Apply the Migrations
Entity Framework migrations are responsible for creating all the tables and relationships in the database you just created. Run the following command in the terminal from the project root:
```bash
dotnet ef database update
```
This will read the code and build the database schema automatically.

### 4. Run the Application
Once the database is configured, run the application with the following command:
```bash
dotnet run
```
The application will compile and start. The terminal will show a message indicating the URL where it is running, typically `http://localhost:5090` or similar.

### 5. Access the System
Open your web browser and navigate to the URL indicated by the terminal (e.g., `http://localhost:5090`). You will be greeted by the home page. Use the navigation bar to access the **Patients**, **Doctors**, and **Appointments** modules.

---


#                     <---------**Readme en Español**-------->

# Sistema de Gestión de Citas Médicas - Hospital San Vicente

Este proyecto es una aplicación web desarrollada en C# con ASP.NET Core MVC para digitalizar y optimizar la gestión de citas médicas, pacientes y médicos del Hospital San Vicente, reemplazando los sistemas manuales basados en agendas físicas y hojas de cálculo.

## Información del Desarrollador
- **Nombre:** Jhon Sebastián Villa Peláez
- **Clan:** Linus - Ruta avanzada c#
- **Correo:** Jhonvilla.2000@hotmail.com
- **Documento de Identidad:** 1007238614

---

## Descripción del Proyecto
El Hospital San Vicente enfrentaba múltiples problemas con su sistema manual de gestión de citas, incluyendo duplicidad de horarios, dificultad para acceder a la información de los pacientes, falta de control sobre los médicos y riesgo de pérdida de información.

Este sistema fue desarrollado para solucionar estos problemas, proporcionando una plataforma centralizada, robusta y eficiente que garantiza la integridad, consistencia y accesibilidad de toda la información relacionada con el proceso de agendamiento de citas.

### Funcionalidades Principales
El sistema implementa las siguientes funcionalidades:

#### Gestión de Pacientes
- **Registro y Edición:** Permite crear nuevos pacientes y modificar su información personal (nombre, documento, edad, etc.).
- **Listado Completo:** Muestra una lista de todos los pacientes registrados en el sistema.
- **Validación de Duplicidad:** Asegura que no se puedan registrar dos pacientes con el mismo número de documento.

#### Gestión de Médicos
- **Registro y Edición:** Permite registrar nuevos médicos con sus datos básicos y especialidad.
- **Listado y Filtro:** Muestra una lista de todos los médicos, con una funcionalidad clave para filtrar la vista por especialidad.
- **Validación de Duplicidad:** Garantiza que el documento de identidad de cada médico sea único.

#### Gestión de Citas Médicas
- **Agendamiento Inteligente:** Permite agendar citas asociando un paciente y un médico a una fecha y hora específicas.
- **Validación de Conflictos:** El sistema valida automáticamente que ni el médico ni el paciente tengan otra cita en el mismo horario, evitando   conflictos.
- **Gestión de Estados:** Permite cambiar el estado de una cita a "Atendida" o "Cancelada" a través de acciones directas en la interfaz.
- **Filtros Avanzados:** La lista principal de citas se puede filtrar dinámicamente para mostrar solo las citas de un paciente o médico específico.

---

## Tecnologías Utilizadas
* **Lenguaje:** C# 
* **Framework:** ASP.NET Core 6.0 MVC
* **ORM:** Entity Framework Core 
* **Base de Datos:** MySQL -con Dbeaver
* **Frontend:** HTML, CSS, Bootstrap 5
* **Gestión de Paquetes:** NuGet

---

## Requisitos Previos
Para poder clonar y ejecutar este proyecto en tu máquina local, necesitarás tener instalado lo siguiente:
* [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) o superior.
* Un editor de código como [Visual Studio Code](https://code.visualstudio.com/) o Visual Studio 2022.
* [Git](https://git-scm.com/downloads).
* Un servidor de base de datos MySQL (puedes usar [MySQL Community Server](https://dev.mysql.com/downloads/mysql/), XAMPP, o una instancia en Docker).
* Una herramienta de gestión de bases de datos como DBeaver o MySQL Workbench (opcional, pero recomendada).

---

## Guía de Instalación y Ejecución
Sigue estos pasos detallados para poner en marcha la aplicación.

### 1. Clonar el Repositorio
Abre una terminal y clona este repositorio en tu máquina local usando el siguiente comando:
```bash
git clone ()
```
Navega a la carpeta del proyecto recién clonado:
```bash
cd [Nombre-De-La-Carpeta-Del-Proyecto]
```

### 2. Configurar la Base de Datos
Antes de ejecutar la aplicación, debes configurar la conexión a tu base de datos MySQL.

1.  **Crea una base de datos vacía** en tu servidor MySQL. Puedes llamarla `HospitalRiwiDB` o como prefieras.
2.  **Abre el archivo `appsettings.json`** que se encuentra en la raíz del proyecto.
3.  **Modifica la cadena de conexión** (`DefaultConnection`) con tus propias credenciales.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=HospitalRiwiDB;User=tu_usuario_mysql;Password=tu_contraseña_mysql;"
  }
}
```
Asegúrate de reemplazar `tu_usuario_mysql` y `tu_contraseña_mysql` con las credenciales correctas.

### 3. Aplicar las Migraciones
Las migraciones de Entity Framework se encargan de crear todas las tablas y relaciones en la base de datos que acabas de crear. Ejecuta el siguiente comando en la terminal desde la raíz del proyecto:
```bash
dotnet ef database update
```
Esto leerá el código y construirá el esquema de la base de datos automáticamente.

### 4. Ejecutar la Aplicación
Una vez configurada la base de datos, ejecuta la aplicación con el siguiente comando:
```bash
dotnet run
```
La aplicación se compilará y se iniciará. La terminal te mostrará un mensaje indicando en qué URL se está ejecutando, normalmente `http://localhost:5090` o similar.

### 5. Acceder al Sistema
Abre tu navegador web y navega a la URL que te indicó la terminal (ej: `http://localhost:5090`).