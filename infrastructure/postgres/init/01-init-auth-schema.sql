-- Crear el esquema auth para Supabase
CREATE SCHEMA IF NOT EXISTS auth;

-- Dar permisos al usuario postgres
GRANT ALL ON SCHEMA auth TO postgres;
