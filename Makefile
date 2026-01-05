.PHONY: \
  help \
  build build-frontend \
  up down restart logs logs-api logs-frontend logs-auth \
  clean dev dev-api dev-frontend \
  test test-frontend \
  migrate \
  backup backup-db backup-postgres restore-db \
  install status \
  shell-api shell-db shell-postgres \
  prune rebuild rebuild-api rebuild-frontend \
  env

# Variables
DOCKER_COMPOSE = docker-compose
BACKEND_DIR = src/Ruttero
FRONTEND_DIR = frontend

## help: Show available commands
help:
	@echo "Available commands:"
	@echo ""
	@echo "  make build             - Builds all Docker images"
	@echo "  make build-frontend    - Builds only the frontend"
	@echo "  make up                - Starts all services (detached)"
	@echo "  make down              - Stops all services"
	@echo "  make restart           - Restarts all services"
	@echo "  make logs              - Shows logs of all services"
	@echo "  make logs-api          - Shows API logs"
	@echo "  make logs-frontend     - Shows frontend logs"
	@echo "  make logs-auth         - Shows auth logs"
	@echo ""
	@echo "  make dev               - Starts development environment"
	@echo "  make dev-api           - Starts only the backend in development mode"
	@echo "  make dev-frontend      - Starts only the frontend in development mode"
	@echo ""
	@echo "  make test              - Runs backend tests"
	@echo "  make test-frontend     - Runs frontend tests"
	@echo ""
	@echo "  make migrate           - Runs database migrations"
	@echo "  make backup-db         - Creates MySQL database backup"
	@echo "  make backup-postgres   - Creates PostgreSQL database backup"
	@echo "  make restore-db        - Restores MySQL backup (FILE=...)"
	@echo ""
	@echo "  make install           - Installs backend and frontend dependencies"
	@echo "  make status            - Shows container status"
	@echo ""
	@echo "  make shell-api         - Opens shell in API container"
	@echo "  make shell-db          - Opens MySQL shell"
	@echo "  make shell-postgres    - Opens PostgreSQL shell"
	@echo ""
	@echo "  make rebuild           - Cleans, builds and restarts everything"
	@echo "  make rebuild-api       - Rebuilds only the API"
	@echo "  make rebuild-frontend  - Rebuilds only the frontend"
	@echo ""
	@echo "  make prune             - Removes all unused Docker resources"
	@echo "  make env               - Creates .env from .env.example"
	@echo ""

## build: Builds all Docker images
build:
	@echo "Building frontend..."
	cd $(FRONTEND_DIR) && npm run build
	@echo "Building Docker images..."
	$(DOCKER_COMPOSE) build

## build-frontend: Builds only the frontend
build-frontend:
	@echo "Building frontend..."
	cd $(FRONTEND_DIR) && npm run build
	@echo "Frontend built in frontend/dist/"

## up: Starts all services
up: build-frontend
	@echo "Starting services..."
	$(DOCKER_COMPOSE) up -d
	@echo "Services started"
	@echo ""
	@echo "Frontend: http://localhost"
	@echo "API:      http://localhost:5000"
	@echo "Auth:     http://localhost:9999"

## down: Stops all services
down:
	@echo "Stopping services..."
	$(DOCKER_COMPOSE) down
	@echo "Services stopped"

## restart: Restarts all services
restart: down up

## logs: Shows logs of all services
logs:
	$(DOCKER_COMPOSE) logs -f

## logs-api: Backend logs
logs-api:
	$(DOCKER_COMPOSE) logs -f api

## logs-frontend: Frontend logs
logs-frontend:
	$(DOCKER_COMPOSE) logs -f frontend

## logs-auth: Supabase Auth logs
logs-auth:
	$(DOCKER_COMPOSE) logs -f auth

## clean: Cleans containers, images, and volumes
clean:
	@echo "Cleaning Docker..."
	$(DOCKER_COMPOSE) down -v --rmi local
	@echo "Cleanup completed"

## dev: Starts development environment
dev:
	@echo "Starting dev environment..."
	$(DOCKER_COMPOSE) up

## dev-api: Only backend in development
dev-api:
	@echo "Starting backend in development..."
	cd $(BACKEND_DIR) && dotnet watch run

## dev-frontend: Only frontend in development
dev-frontend:
	@echo "Starting frontend in development..."
	cd $(FRONTEND_DIR) && npm start

## test: Runs backend tests
test:
	@echo "Running backend tests..."
	cd src/Ruttero.Tests && dotnet test

## test-frontend: Runs frontend tests
test-frontend:
	@echo "Running frontend tests..."
	cd $(FRONTEND_DIR) && npm test

## migrate: Runs migrations
migrate:
	@echo "Running migrations..."
	cd $(BACKEND_DIR) && dotnet ef database update

## backup-db: MySQL backup
backup-db:
	@echo "Creating MySQL backup..."
	@mkdir -p backups
	docker exec mysql_db mysqldump -u root -p$${MYSQL_ROOT_PASSWORD} $${MYSQL_DATABASE} > backups/mysql_backup_$$(date +%Y%m%d_%H%M%S).sql
	@echo "Backup created in backups/"

## backup-postgres: PostgreSQL backup
backup-postgres:
	@echo "Creating PostgreSQL backup..."
	@mkdir -p backups
	docker exec supabase_postgres pg_dump -U postgres postgres > backups/postgres_backup_$$(date +%Y%m%d_%H%M%S).sql
	@echo "Backup created in backups/"

## restore-db: Restores MySQL backup (use: make restore-db FILE=backups/file.sql)
restore-db:
	@echo "Restoring MySQL backup..."
	@if [ -z "$(FILE)" ]; then echo "Error: Specify FILE=backups/file.sql"; exit 1; fi
	docker exec -i mysql_db mysql -u root -p$${MYSQL_ROOT_PASSWORD} $${MYSQL_DATABASE} < $(FILE)
	@echo "Backup restored"

## install: Installs dependencies
install:
	@echo "Installing backend dependencies..."
	cd $(BACKEND_DIR) && dotnet restore
	@echo "Installing frontend dependencies..."
	cd $(FRONTEND_DIR) && npm install
	@echo "Dependencies installed"

## status: Shows the status of the containers
status:
	@echo "Service status:"
	@$(DOCKER_COMPOSE) ps

## shell-api: Opens shell in the API container
shell-api:
	docker exec -it ruttero_api bash

## shell-db: Opens shell in MySQL
shell-db:
	docker exec -it mysql_db mysql -u root -p

## shell-postgres: Opens shell in PostgreSQL
shell-postgres:
	docker exec -it supabase_postgres psql -U postgres

## prune: Cleans all unused Docker resources
prune:
	@echo "Are you sure? This will clean all the containers, images and volumes not used."
	@read -p "Press Enter to continue or Ctrl+C to cancel..."
	docker system prune -a --volumes -f

## rebuild: Rebuilds and restarts all services
rebuild: clean build up

## rebuild-api: Rebuilds only the API
rebuild-api:
	@echo "Rebuilding API..."
	$(DOCKER_COMPOSE) build api
	$(DOCKER_COMPOSE) up -d api

## rebuild-frontend: Rebuilds only the frontend
rebuild-frontend:
	@echo "Rebuilding frontend..."
	cd $(FRONTEND_DIR) && npm run build
	$(DOCKER_COMPOSE) restart nginx
	@echo "Frontend updated"

## env: Creates .env file from .env.example
env:
	@if [ ! -f .env ]; then \
		cp .env.example .env; \
		echo ".env file created. Please configure it before continuing."; \
	else \
		echo ".env file already exists."; \
	fi
