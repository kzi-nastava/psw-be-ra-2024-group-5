$DB_USER = "postgres"
$DB_HOST = "localhost"
$DB_PORT = "5432"
$DB_NAME = "explorer-v1"

$env:PGPASSWORD = "super"

Write-Host "Running SQL commands from files..."
psql -U $DB_USER -h $DB_HOST -p $DB_PORT -d $DB_NAME -f "../scripts/sql/stakeholders-seed.sql"
psql -U $DB_USER -h $DB_HOST -p $DB_PORT -d $DB_NAME -f "../scripts/sql/tours-seed.sql"

Remove-Item Env:\PGPASSWORD

Write-Host "Operation completed."
