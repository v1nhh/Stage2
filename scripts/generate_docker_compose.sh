connection=$(cat connection_strings.json | jq -c)
cat $1 | sed "s/<tenant_connection>/'$connection'/g"
