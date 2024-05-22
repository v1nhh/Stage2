#!/bin/bash

for i in $(seq -w 01 20); do
  cabinetNumber=$(((231018100705 + 10#$i))
  port=$((8080 + 10#$i))
  localApiPort=$((6000 + 10#$i))
  curl "http://localhost:$localApiPort/api/cabinet/$cabinetNumber/initialize" \
  -X 'POST' \
  -H 'Accept: application/json, text/plain, */*' \
  -H 'Accept-Language: en-US,en;q=0.9,nl-NL;q=0.8,nl;q=0.7' \
  -H 'Connection: keep-alive' \
  -H 'Content-Length: 0' \
  -H "Origin: http://localhost:$port" \
  -H "Referer: http://localhost:$port/" \
  -H 'Sec-Fetch-Dest: empty' \
  -H 'Sec-Fetch-Mode: cors' \
  -H 'Sec-Fetch-Site: same-site' \
  -H 'User-Agent: Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Mobile Safari/537.36' \
  -H 'sec-ch-ua: "Chromium";v="118", "Google Chrome";v="118", "Not=A?Brand";v="99"' \
  -H 'sec-ch-ua-mobile: ?1' \
  -H 'sec-ch-ua-platform: "Android"' \
  --compressed
done