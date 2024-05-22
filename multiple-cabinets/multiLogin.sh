#!/bin/bash

# Use './multiLogin' to login on all cabinets at once

for i in {1..9}
do
   curl -X POST "http://localhost:800$i/api/FakeTools/RFIDScanned?rfidcode=D6D5398B" -H  "accept: text/plain" -d ""
done

curl -X POST "http://localhost:8010/api/FakeTools/RFIDScanned?rfidcode=D6D5398B" -H  "accept: text/plain" -d ""
