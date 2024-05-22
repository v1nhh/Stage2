#!/bin/bash
openssl genrsa -out key.pem 2048
openssl rsa -in key.pem -RSAPublicKey_out > public.pem
echo "Private Key"
cat key.pem | python3 convert.py
echo "Public Key"
cat public.pem | python3 convert.py
