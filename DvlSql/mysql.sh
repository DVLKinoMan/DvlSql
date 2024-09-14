#!/bin/bash

# MySQL credentials
USER="root"
PASSWORD="Dvl12345678"
HOST="localhost"  # Change if your MySQL server is on a different host
DB_NAME="PersonsDb"

# Backup directory and file name
BACKUP_DIR="C:\Users\dvali\Downloads\Persons"
DATE=$(date +%Y%m%d_%H%M%S)
BACKUP_FILE="$BACKUP_DIR/$DB_NAME_$DATE.sql"

# Create backup directory if it doesn't exist
mkdir -p $BACKUP_DIR

# Execute mysqldump to create the backup
mysqldump -u $USER -p$PASSWORD -h $HOST $DB_NAME > $BACKUP_FILE
