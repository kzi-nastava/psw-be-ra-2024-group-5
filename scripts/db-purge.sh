#!/bin/bash

ROOT_PATH="scripts"

DB_USER="postgres"
DB_HOST="localhost"
DB_PORT="5432"

export PGPASSWORD="super"

echo "Running SQL commands from file..."
psql -U "$DB_USER" -h "$DB_HOST" -p "$DB_PORT" -f $ROOT_PATH/purge.sql

unset PGPASSWORD

echo "Operation completed."