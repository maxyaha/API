
Install-Package Microsoft.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools

--Microservice.DataAccress
--Add-Migration InitialCreate -Context PartyStoreContext -OutputDir Migrations\SqlServerMigrations
--Add-Migration InitialCreate -Context EventStoreContext -OutputDir Migrations\SqlServerMigrations
--
--Microservice.Application
--update-database


Add-Migration InitialCreate -Context IpdStoreContext -OutputDir IPD\Migrations
Add-Migration InitialCreate -Context TesterStoreContext -OutputDir Tester\Migrations

Microservice.DataAccress
Add-Migration init -Context EventStoreContext
Add-Migration init -Context FeatureStoreContext
Add-Migration init -Context TesterStoreContext


Update-Database -Context EventStoreContext
Update-Database -Context TesterStoreContext
Update-Database -Context IpdStoreContext
