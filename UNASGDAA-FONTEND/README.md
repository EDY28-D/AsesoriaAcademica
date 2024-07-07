# GestionDeAsesoriaAcademica Frontend

## Descripción
Este proyecto es la interfaz frontend de "GestionDeAsesoriaAcademica", una plataforma diseñada para gestionar el proceso de asesoramiento de tesis, optimizando el tiempo de los profesores y equilibrando la carga laboral. La aplicación facilita la colaboración entre asesores y estudiantes, proporcionando herramientas para la gestión de citas, contratos de asesoría y el proceso completo de tesis.

## Requisitos del Sistema
- **Gestión de Citas**
- **Gestión del Contrato de Asesorías**
- **Gestión del Proceso de Tesis**
- **Propiciar Trabajo Colaborativo**

## Requisitos Funcionales

### Módulo Principal/Maestro
- **Registro de Usuarios:** Permitir a los usuarios registrarse y crear cuentas.
- **Asignación de Roles:** Asignar roles a los usuarios dentro del sistema.
- **Registro de Líneas de Investigación:** Registrar diferentes líneas de investigación.
- **Registro de Grupos de Investigación:** Registrar grupos de investigación.
- **Registro de Estudiantes (Asesorados):** Añadir estudiantes al sistema.
- **Registro de Asesores:** Añadir asesores al sistema, posiblemente migrando datos de sistemas académicos existentes.
- **Registro de Coordinadores de Grupo de Investigación:** Añadir coordinadores, con la posibilidad de migrar datos de sistemas académicos.

### Módulo de Contrato de Asesoría
- **Solicitar Asesoría:** Los estudiantes pueden solicitar asesoría a un asesor específico.
- **Aceptar Asesoría:** Los asesores pueden aceptar la solicitud de asesoría.
- **Rechazar Asesoría:** Los asesores pueden rechazar la solicitud de asesoría.

### Módulo de Citas
- **Registro de Progreso del Asesor:** Los asesores pueden registrar el progreso (avances, dificultades, entregables, tareas) de cada cita.
- **Registro de Progreso del Estudiante:** Los estudiantes pueden registrar su progreso (avances, dificultades, entregables, tareas) de cada cita.
- **Reporte de Citas en Riesgo de Abandono:** Mostrar reportes de citas en riesgo de ser abandonadas.
- **Reporte de Tesis Abandonadas:** Mostrar reportes de estudiantes que han abandonado su tesis.
- **Reporte General de Citas:** Proveer un reporte general de citas realizadas en un periodo, incluyendo el nombre del asesorado y si se realizó la cita.
- **Reporte de Estado de Citas:** Mostrar reportes de citas según su estado (programadas, canceladas, completadas) con indicadores de alerta.
- **Reporte de Citas por Estudiante:** Mostrar reportes de citas por cada estudiante en un periodo, útil para asesores y estudiantes.
- **Reporte Comparativo de Citas:** Comparar el número de citas realizadas en un periodo, incluyendo nombres de asesores, cantidad de citas, horas, etc.
- **Detalles de Citas:** Ver los detalles de cualquier cita de asesoría a partir de los reportes.

## Tecnologías Utilizadas
- **Angular:** Framework principal para construir la interfaz de usuario.
- **NgRx:** Gestión del estado de la aplicación.
- **Angular Router:** Navegación y enrutamiento.
- **HttpClient:** Cliente HTTP para realizar solicitudes a la API backend.
- **Angular Material:** Biblioteca de componentes para el diseño de la interfaz.

## Instalación y Configuración

1. **Instalar Angular CLI globalmente:**
   ```bash
   npm install -g @angular/cli
## Clonar el repositorio:

git clone https://github.com/giovanni-pe/SGAAFIISUNAS.git

## Installation
 
   $ npm install
   $ npm update

## Basic usage
# dev server with hot reload at http://localhost:4200
$ npm start
Navigate to http://localhost:4200. The app will automatically reload if you change any of the source files.

## Build
Run build to build the project. The build artifacts will be stored in the dist/ directory.

# build for production with minification
$ npm run build