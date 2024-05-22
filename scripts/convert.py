"""
First run the generate.sh script to generate a private(key.pem) and public key(public.pem)

Then use this script to convert them to 1 string:
cat key.pem | python convert.py
cat public.pem | python convert.py
"""
import sys

data = sys.stdin.readlines()
data = "".join([line.strip() for line in data[1:-1]])
print(data)
