# 🦷 DentalClinicApi

API RESTful construida en **.NET 8 + MongoDB** para gestionar pacientes, citas, servicios y usuarios en una clínica odontológica. Implementa autenticación JWT, validaciones, roles y manejo global de errores.

## 🚀 Tecnologías utilizadas

- ASP.NET Core 8
- MongoDB (driver oficial)
- JWT (Json Web Tokens)
- Swagger / OpenAPI
- DataAnnotations
- Middleware global de manejo de errores
- Clean Architecture (Controllers, Services, Models, DTOs)

## 🔐 Funcionalidades destacadas

- Registro e inicio de sesión de usuarios (`/api/User/signup` y `/api/User/signin`)
- Emisión de tokens JWT con roles (`user`, `admin`)
- Protección de endpoints mediante `[Authorize]` y `[Authorize(Roles = "admin")]`
- Validaciones con DataAnnotations en los DTOs
- Middleware global para manejo de errores
- Configuración de Swagger para probar endpoints con JWT
- CRUD completo de pacientes, dentistas, servicios y citas

## 🖼️ Vista previa de Swagger

![Swagger UI](./assets/swagger-demo.png)

## ✅ Autenticación en Swagger

Para usar los endpoints protegidos:

1. Ejecuta `/api/User/signup` o `/api/User/signin` para obtener un token JWT.
2. Copia el token **sin incluir `Bearer`** (Swagger lo agrega automáticamente).
3. Haz clic en el botón **Authorize** arriba a la derecha.
4. Pega el token directamente y autoriza.

## 🛠️ Instalación y ejecución

1. Clona el repositorio:

```bash
git clone https://github.com/Raulvanegas7/DentalClinicApi.git
cd DentalClinicApi
```

2. Restaura los paquetes:

```bash
dotnet restore
```

3. Ejecuta el proyecto:

```bash
dotnet run
```

4. Abre en tu navegador:

```
https://localhost:5177/swagger
```

## 📁 Estructura del proyecto

```
DentalClinicApi/
│
├── Contexts/               # Configuración de MongoDB
├── Controllers/            # Controladores HTTP
├── DTOs/                   # Objetos de transferencia de datos
├── Middleware/             # Middleware global para manejo de errores
├── Models/                 # Modelos de las entidades
├── Services/               # Lógica de negocio
├── Swagger/                # Filtro para Swagger con seguridad JWT
└── Program.cs              # Configuración y punto de entrada
```

## 🧪 Ejemplo de credenciales

```json
// Registro
{
  "username": "RaulDev",
  "email": "raul@example.com",
  "password": "123456",
  "role": "admin"
}
```