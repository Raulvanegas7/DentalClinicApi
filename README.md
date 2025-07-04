
# 🦷 DentalClinicApi

API RESTful para la gestión de un consultorio odontológico.  
Desarrollada en **C#** con **ASP.NET Core**, base de datos **MongoDB** y autenticación **JWT**.  
Actualmente solo incluye backend funcional, el frontend se implementará en el futuro.

---

## 📂 Estructura del proyecto

```
DentalClinicApi/
├── Configurations/
├── Contexts/
├── Controllers/
├── Dtos/
├── Models/
├── Services/
├── appsettings.json
├── Program.cs
└── ...
```

---

## 🚀 Tecnologías principales

- **C#**
- **ASP.NET Core**
- **MongoDB**
- **Swagger** para documentación interactiva
- **JWT** para autenticación

---

## 👥 Roles de usuarios

| Rol           | Descripción                                                  |
|---------------|--------------------------------------------------------------|
| **Admin**     | Superusuario que puede gestionar dentistas, recepcionistas, usuarios, etc. |
| **Dentist**   | Gestiona información de los pacientes, citas y tratamientos. |
| **Receptionist** | Administra agendamiento y recepción de pacientes.        |
| **Patient**   | Por ahora no tiene acciones directas sobre la API.          |

---

## 🖼️ Vista previa de Swagger
![Swagger UI](./backend/assets/swagger-demo.png)

---

## 🗂️ Endpoints principales

- CRUD de **Dentists**
- CRUD de **Receptionists**
- CRUD de **Admins**
- CRUD de **Patients**
- Autenticación y autorización con **JWT**
- Carga y obtención de imágenes (ruta de imagen incluida)

---

## 🔐 Autenticación

Todos los endpoints sensibles están protegidos por autenticación **JWT** y control de roles.

---

## 🔗 Documentación interactiva (Swagger)

Para probar todos los endpoints, usa **Swagger UI**:

```
http://localhost:{PUERTO}/swagger
```

---

## ⚡ Cómo correr el proyecto

```bash
# Restaurar dependencias
dotnet restore

# Ejecutar la API
dotnet run
```

---

## 📌 Nota

Este proyecto está en desarrollo. El rol **Patient** aún no tiene operaciones directas, pero se planea implementar en futuras versiones.

---

## 🧑‍💻 Autor

**Raul Vanegas**  
Backend Developer  
🚀 *Construyendo soluciones backend sólidas con C# y MongoDB.*

---

