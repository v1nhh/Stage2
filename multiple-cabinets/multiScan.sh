#!/bin/bash

# Use './multiScan' to add/remove all portofoon items

for i in {1..9}
do
   curl -X POST "http://localhost:800$i/api/FakeTools/RFIDScanned?rfidcode=MULTIPORTO$i" -H  "accept: text/plain" -d ""
done

curl -X POST "http://localhost:8010/api/FakeTools/RFIDScanned?rfidcode=MULTIPORTO10" -H  "accept: text/plain" -d ""
