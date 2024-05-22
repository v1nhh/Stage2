import requests

# URL for the login and items API
login_url = "https://api.ctam-dev.nautaconnect.cloud/api/Login/web"
items_url = "https://api.ctam-dev.nautaconnect.cloud/api/items"

# Tenant ID, email, and password for login
tenant_id = 'tenant_id'
email = 'email'
password = 'password'

login_headers = {
    "Content-Type": "application/json",
    "X-Tenant-ID": tenant_id
}

login_payload = {
    "email": email,
    "password": password
}

# Authenticate and get token
response = requests.post(login_url, json=login_payload, headers=login_headers)
if response.status_code == 200:
    token = response.json().get('token')
    print("Successfully authenticated. Token obtained.")
else:
    print("Failed to authenticate. Check credentials and tenant ID.")
    exit()

item_headers = {
    "Accept": "application/json, text/plain, */*",
    "Authorization": f"Bearer {token}",
}

# List of barcodes to add
barcodes = [
"301327064975",
"301327064974",
"301327064951",
"301327063566",
"301327064980",
"301327064960",
"301327064996",
"301327064978",
"301327064976",
"301327064973",
"301327064995",
"301327064993",
"301327064998",
"301327064994",
"301327064999",
"301327064991",
"301327064992",
"301327064952",
"301327064977",
"301327064979",
"301327064956",
"801327077069",
"801327092770",
"301327161636",
"301327161633",
"801327077021",
"301327161647",
"301327161641",
"301327080892",
"301327065000",
"301327161640",
"801327077057",
"301327161632",
"301327161644",
"301327161643",
"301327161635",
"301327161638",
"801327089783",
"301327161607",
"301327161649",
"301327161645",
"801327092058",
"801327092824",
"301327161608",
"801327077100",
"301327161646",
"801327092836",
"801327077094",
"301327161606",
"301327161642",
"301327161650",
"301327161621",
"301327064837",
"301327161648",
"301327161631",
"301327161622",
"301327161623",
"301327161610",
"301327161609",
"301327161625",
"301327161624",
"301327161634",
"301327161637",
"301327109999",
"301327175148",
"301327091395",
"301327109776",
"301327186354",
"301327186353",
"301327091350",
"301327107000",
"301327093965",
"301327130480"
]

# Function to add each barcode
def add_barcode(barcode, description):
    payload = {
        "description": description,
        "itemTypeID": 22,
        "barcode": barcode,
        "id": None
    }
    response = requests.post(items_url, json=payload, headers=item_headers)
    if response.status_code == 200:
        print(f"Successfully added barcode {barcode}")
    else:
        print(f"Failed to add barcode {barcode} with status code {response.status_code}")

teller = 2

# Loop through each barcode and add it with a unique description
for barcode in barcodes:
    description = f"AD 0{teller}"
    add_barcode(barcode, description)
    teller += 1