# Sistema de Gestión de Planes de Mejoramiento

Sistema web desarrollado para administrar, controlar y hacer seguimiento a los planes de mejoramiento académico asignados a aprendices. La plataforma permite gestionar usuarios, programas de formación, fichas, evidencias y evaluaciones, facilitando el acompañamiento entre administradores, instructores y aprendices.

---

## Descripción General

El Sistema de Gestión de Planes de Mejoramiento tiene como propósito centralizar el proceso académico relacionado con la creación, asignación, seguimiento y evaluación de planes de mejoramiento.

A través de esta aplicación, los administradores pueden organizar la información institucional, los instructores pueden revisar el avance de los aprendices y los aprendices pueden consultar sus planes asignados y cargar las evidencias correspondientes.

---

## Funcionalidades Principales

- Autenticación de usuarios mediante inicio de sesión.
- Control de acceso según rol: administrador, instructor y aprendiz.
- Gestión de administradores, instructores y aprendices.
- Administración de centros de formación.
- Registro y control de programas de formación.
- Gestión de competencias y resultados de aprendizaje.
- Administración de fichas.
- Asignación de planes de mejoramiento.
- Consulta de planes por instructor y aprendiz.
- Carga y visualización de evidencias.
- Evaluación de planes de mejoramiento.
- Cambio de credenciales de acceso.

---

## Roles del Sistema

### Administrador

El administrador cuenta con permisos para gestionar la información principal del sistema. Puede registrar, editar, consultar y eliminar datos relacionados con usuarios, centros de formación, programas, fichas, aprendices, instructores y planes de mejoramiento.

### Instructor

El instructor puede consultar los aprendices asociados, revisar los planes de mejoramiento asignados, verificar las evidencias entregadas y registrar la evaluación correspondiente.

### Aprendiz

El aprendiz puede ingresar al sistema para consultar sus planes de mejoramiento, revisar las actividades asignadas y cargar las evidencias necesarias para su evaluación.

---

## Estructura del Proyecto

```text
sistemaGestionPlanesDeMejoramiento/
│
├── modelo/       # Clases que representan las entidades del sistema
├── datos/        # Acceso a base de datos y consultas SQL
├── logica/       # Reglas de negocio y comunicación entre capas
├── vista/        # Interfaces web del sistema
│   ├── Admin/       # Módulo del administrador
│   ├── Instructor/  # Módulo del instructor
│   ├── Aprendiz/    # Módulo del aprendiz
│   ├── CSS/         # Estilos personalizados
│   └── JS/          # Scripts de apoyo
│
├── evidencias/   # Archivos cargados por los aprendices
├── Web.config    # Configuración general del proyecto
└── packages.config
