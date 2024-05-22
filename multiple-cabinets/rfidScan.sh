#!/bin/bash

# Use './rfidScan.sh 02 D6D5398B' to scan on cabinet 2 with cardCode D6D5398B
echo $1
echo $2
curl -X POST "http://localhost:80$1/api/FakeTools/RFIDScanned?rfidcode=$2" -H  "accept: text/plain" -d ""