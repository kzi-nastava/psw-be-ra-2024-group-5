# Remove migrations folders
Remove-Item -Recurse -Force "Modules/Stakeholders/Explorer.Stakeholders.Infrastructure/Migrations"
Remove-Item -Recurse -Force "Modules/Tours/Explorer.Tours.Infrastructure/Migrations"
Remove-Item -Recurse -Force "Modules/Blog/Explorer.Blog.Infrastructure/Migrations"
Remove-Item -Recurse -Force "Modules/Payments/Explorer.Payments.Infrastructure/Migrations"
Remove-Item -Recurse -Force "Modules/Encounters/Explorer.Encounters.Infrastructure/Migrations"

# Update StakeholdersContext
Add-Migration -Name Init -Context StakeholdersContext -Project Explorer.Stakeholders.Infrastructure -StartupProject Explorer.API
Update-Database -Context StakeholdersContext -Project Explorer.Stakeholders.Infrastructure -StartupProject Explorer.API

# Update ToursContext
Add-Migration -Name Init -Context ToursContext -Project Explorer.Tours.Infrastructure -StartupProject Explorer.API
Update-Database -Context ToursContext -Project Explorer.Tours.Infrastructure -StartupProject Explorer.API

# Update BlogContext
Add-Migration -Name Init -Context BlogContext -Project Explorer.Blog.Infrastructure -StartupProject Explorer.API
Update-Database -Context BlogContext -Project Explorer.Blog.Infrastructure -StartupProject Explorer.API

# Update PaymentsContext
Add-Migration -Name Init -Context PaymentsContext -Project Explorer.Payments.Infrastructure -StartupProject Explorer.API
Update-Database -Context PaymentsContext -Project Explorer.Payments.Infrastructure -StartupProject Explorer.API

# Update EncountersContext
Add-Migration -Name Init -Context EncountersContext -Project Explorer.Encounters.Infrastructure -StartupProject Explorer.API
Update-Database -Context EncountersContext -Project Explorer.Encounters.Infrastructure -StartupProject Explorer.API