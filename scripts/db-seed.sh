#!/bin/bash

ROOT_PATH="scripts/sql"

DB_USER="postgres"
DB_HOST="localhost"
DB_PORT="5432"
DB_NAME="explorer-v1"

export PGPASSWORD="super"

echo "Running SQL commands from files..."
psql -U $DB_USER -h $DB_HOST -p $DB_PORT -d $DB_NAME -f $ROOT_PATH/stakeholders-seed.sql"
psql -U $DB_USER -h $DB_HOST -p $DB_PORT -d $DB_NAME -f $ROOT_PATH/tours-seed.sql"

unset PGPASSWORD

echo "Operation completed."