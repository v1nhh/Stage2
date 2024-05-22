#!/bin/bash
# This script will remove the container with the CTAM database(CloudAPI) and the  volume attached to this container
# Run the create_database.sh script after you run this script if you want a new database
read -p "Press 'y' if you want to continue to delete the CTAM database: " -n 1 -r
echo
if [[ ! $REPLY =~ ^[Yy]$ ]]
then
    exit 1
fi
docker stop CTAM
docker rm CTAM
docker volume rm mssql-volume
echo "Done"
