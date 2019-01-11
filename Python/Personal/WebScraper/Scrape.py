from selenium import webdriver
from lxml import html
import json
import gspread
from oauth2client.client import SignedJwtAssertionCredentials
import time

#retreving data from site
browser = webdriver.Chrome()
url = "https://tenno.zone/pricing"
browser.get(url)
source = browser.execute_script("return document.body.innerHTML")
htmlElem = html.document_fromstring(source)
divElems = htmlElem.cssselect("div.rt-tr-group")
data = {}
#parsing data into dictionary
for elem in divElems:
    name = ""
    price = 0
    inner = elem.cssselect("button.link")
    for e in inner:
        text = e.text_content()
        name = text
    innerPrice = elem.cssselect("div.rt-td a")
    for p in innerPrice:
        text = p.text_content()
        price = text
    data[name] = price
browser.quit()

#google sheet connection setup
json_key = json.load(open("creds.json"))
scope = ["https://spreadsheets.google.com/feeds",
         "https://www.googleapis.com/auth/drive"]
credentials = SignedJwtAssertionCredentials(json_key['client_email'], json_key['private_key'].encode(), scope)
file = gspread.authorize(credentials)
sheet = file.open("Warframe Checklist")
worksheet = sheet.worksheet("Data")

print("Updating data...")

#updating worksheet data with site data
index = 1;
for key, value in data.items():
    worksheet.update_acell("A" + str(index), key)
    worksheet.update_acell("B" + str(index), value)
    index += 1
    time.sleep(3)

print("Completed data update.")

    
