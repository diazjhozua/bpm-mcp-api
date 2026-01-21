# Database Configuration Setup

This document explains how to configure database connections for the BPM MCP API without exposing sensitive information in Git.

## 🔧 Development Environment

### Using User Secrets (Recommended for Development)

The project is configured to use .NET User Secrets for development. Connection strings are stored securely on your local machine and won't be committed to Git.

#### Setting Up User Secrets

1. **Initialize user secrets** (already done):
   ```bash
   dotnet user-secrets init
   ```

2. **Set your database connection string**:
   ```bash
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=(localdb)\\mssqllocaldb;Database=BpmMcpApiDb_Dev;Trusted_Connection=true;MultipleActiveResultSets=true"
   ```

3. **View current secrets** (optional):
   ```bash
   dotnet user-secrets list
   ```

4. **Remove a secret** (if needed):
   ```bash
   dotnet user-secrets remove "ConnectionStrings:DefaultConnection"
   ```

### Alternative: Local Configuration File

If you prefer using a local configuration file:

1. **Copy the example file**:
   ```bash
   cp appsettings.example.json appsettings.Local.json
   ```

2. **Update the connection string** in `appsettings.Local.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "YOUR_ACTUAL_CONNECTION_STRING"
     }
   }
   ```

3. **Note**: `appsettings.Local.json` is already excluded from Git in `.gitignore`

## 🚀 Production Environment

### Using Environment Variables

For production deployments, use environment variables to configure the database connection:

#### Set Environment Variable

**Windows:**
```cmd
set ConnectionStrings__DefaultConnection="Server=YOUR_PRODUCTION_SERVER;Database=BpmMcpApiDb;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;Encrypt=true;TrustServerCertificate=false"
```

**Linux/macOS:**
```bash
export ConnectionStrings__DefaultConnection="Server=YOUR_PRODUCTION_SERVER;Database=BpmMcpApiDb;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;Encrypt=true;TrustServerCertificate=false"
```

#### Docker Configuration

If using Docker, add the environment variable to your `docker-compose.yml`:

```yaml
version: '3.8'
services:
  api:
    image: bpm-mcp-api
    environment:
      - ConnectionStrings__DefaultConnection=Server=db;Database=BpmMcpApiDb;User Id=sa;Password=YOUR_PASSWORD;Encrypt=true;TrustServerCertificate=true
    depends_on:
      - db

  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YOUR_SECURE_PASSWORD
```

### Azure App Service

In Azure App Service, configure the connection string in:

1. **Azure Portal** → Your App Service → **Configuration** → **Connection strings**
2. **Name**: `DefaultConnection`
3. **Value**: Your SQL Server connection string
4. **Type**: `SQLServer`

### Other Cloud Providers

#### AWS Elastic Beanstalk
Set environment variables through the EB console or `.ebextensions` configuration.

#### Heroku
```bash
heroku config:set ConnectionStrings__DefaultConnection="YOUR_CONNECTION_STRING"
```

#### Google Cloud Platform
Configure through environment variables in `app.yaml` or using Secret Manager.

## 🗄️ Database Migration

### Initial Setup

1. **Run migrations** to create the database:
   ```bash
   dotnet ef database update
   ```

2. **Create new migrations** (when models change):
   ```bash
   dotnet ef migrations add YourMigrationName
   dotnet ef database update
   ```

### Production Migration

Ensure your production connection string is properly configured before running:
```bash
dotnet ef database update --environment Production
```

## 🔐 Connection String Formats

### SQL Server (LocalDB - Development)
```
Server=(localdb)\\mssqllocaldb;Database=BpmMcpApiDb_Dev;Trusted_Connection=true;MultipleActiveResultSets=true
```

### SQL Server (Windows Authentication)
```
Server=SERVER_NAME;Database=DATABASE_NAME;Trusted_Connection=true;MultipleActiveResultSets=true
```

### SQL Server (SQL Authentication)
```
Server=SERVER_NAME;Database=DATABASE_NAME;User Id=USERNAME;Password=PASSWORD;Encrypt=true;TrustServerCertificate=false
```

### Azure SQL Database
```
Server=tcp:your-server.database.windows.net,1433;Database=your-database;User ID=your-username;Password=your-password;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```

## 🛡️ Security Best Practices

1. **Never commit connection strings to Git**
2. **Use strong passwords for production databases**
3. **Enable SSL/TLS encryption for production connections**
4. **Use managed identities when possible (Azure, AWS IAM)**
5. **Regularly rotate database credentials**
6. **Use least-privilege database accounts**
7. **Monitor database access logs**

## ⚠️ Troubleshooting

### Common Issues

1. **"Cannot open database" error**
   - Check if the database server is running
   - Verify connection string parameters
   - Ensure database exists (run migrations)

2. **Authentication failed**
   - Verify username/password
   - Check if SQL Server allows the authentication method
   - For Windows Auth, ensure the app runs under correct user context

3. **User Secrets not working**
   - Ensure you're running in Development environment
   - Check UserSecretsId in the .csproj file
   - Verify secrets are set: `dotnet user-secrets list`

4. **Environment variables not recognized**
   - Use double underscores (__) instead of colons (:) in variable names
   - Example: `ConnectionStrings__DefaultConnection`
   - Restart your application after setting environment variables

For additional help, consult the [Entity Framework Core documentation](https://docs.microsoft.com/en-us/ef/core/).