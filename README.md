This README contains both English and Spanish versions.
Versión en inglés arriba / Versión en español más abajo.

This application is designed as a logistics tracking system, featuring trip management with entities such as users, vehicles, drivers, and fares. It is currently under active development and will implement a full-stack RESTful API using ASP.NET Core with C#. The backend handles user management, authentication, and authorization via JWT tokens. Data persistence is managed with MySQL, accessed through Entity Framework Core as the ORM.

For infrastructure, Traefik is employed as a dynamic reverse proxy and SSL/TLS certificate manager via Let’s Encrypt integration. Nginx serves static assets such as frontend bundles. The frontend is developed with React, leveraging Vite as the build tool for fast development and optimized production builds.

All components are containerized using Docker, orchestrated with Docker Compose for seamless multi-container deployment on a VPS. Additional features include API documentation generated with Swagger UI and automated unit testing via xUnit to ensure code quality and reliability.

docker-images:
	- mcr.microsoft.com/dotnet/sdk:8.0
	- mcr.microsoft.com/dotnet/aspnet:8.0
	- traefik:v3.4.1
	- nginx:1.28.0
	- mysql:8.4.5


Esta aplicación está diseñada como un sistema de seguimiento logístico, que incluye la gestión de viajes con entidades como usuarios, vehículos, conductores y tarifas. Actualmente está en desarrollo activo y contará con una API RESTful full-stack desarrollada en ASP.NET Core con C#. El backend gestiona la creación de usuarios, autenticación y autorización mediante tokens JWT. La persistencia de datos se maneja con MySQL, accediendo a través de Entity Framework Core como ORM.

En cuanto a infraestructura, se utiliza Traefik como proxy inverso dinámico y gestor de certificados SSL/TLS con integración de Let’s Encrypt. Nginx se encarga de servir los archivos estáticos, como los bundles del frontend. El frontend está desarrollado en React, utilizando Vite como herramienta de construcción para un desarrollo rápido y builds optimizados para producción.

Todos los componentes están containerizados con Docker y orquestados con Docker Compose para un despliegue multi-contenedor fluido en un VPS. Entre las funcionalidades adicionales se incluyen la documentación de la API generada con Swagger UI y pruebas unitarias automatizadas con xUnit para garantizar la calidad y confiabilidad del código.
