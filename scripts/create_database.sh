#!/bin/bash
docker run \
            -e 'ACCEPT_EULA=Y' \
            -e 'SA_PASSWORD=C@ptur3T' \
            -v mssql-volume:/var/opt/mssql \
            -p 1433:1433 \
            --name CTAM \
            -d mcr.microsoft.com/mssql/server:2019-latest
