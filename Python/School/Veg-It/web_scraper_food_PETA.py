from selenium import webdriver
from bs4 import BeautifulSoup
import fnmatch
import json
import re

URL = 'https://www.peta.org/living/food/animal-ingredients-list/'
driver = webdriver.Chrome()
driver.get(URL)
soup = BeautifulSoup(driver.page_source, 'html.parser')
driver.quit()

class Food(object):
    name = ''
    diet_type = 1
    description = ''
    public_id = ''
    public_id_int = -1
    group = None
    sub_group = None
    created_date = '2012-01-01T12:00:00+00:00'
    updated_date = '2019-01-01T12:00:00+00:00'
    data_source = 3

    def __init__(self, _name, _diet_type, _description):
        self.name = _name
        self.diet_type = _diet_type
        self.description = _description
        self.public_id = self.public_id
        self.public_id_int = self.public_id_int
        self.group = self.group
        self.sub_group = self.sub_group
        self.created_date = self.created_date
        self.updated_date = self.updated_date
        self.data_source = self.data_source

def make_food_obj(name, diet_type, desc):
    food = Food(name, diet_type, desc)
    return food

def get_obj_by_key(obj, value):
    return [_item for _item in obj if _item['name'] == value]

mainContent = soup.find('main', class_='inner')

start = mainContent.find('hr')

food_dict = []
for child in start.find_next_siblings('p'):
    name = child.find('b', recursive=False)
    if name:
        food_name = name.text
        food_name = food_name.split('.')[0]

    desc = None
    br = child.find('br', recursive=False)
    if br:
        desc = br.next_element
        desc = desc.lstrip()
        
    food = make_food_obj(food_name, 6, desc)
    food_dict.append(food.__dict__)

# fix (see x) desc
for food in food_dict:
    for key, value in food.items():
        if key == 'description':
            if value is not None and fnmatch.fnmatch(value, '(See *)'):
                name = re.sub('[(*.)]', '', value)[4:]
                obj = get_obj_by_key(food_dict, name)
                if len(obj) > 0:
                    food['description'] = obj[0]['description']
                else:
                    food['description'] = None

with open('peta_food_data.json', 'w') as outfile:
    json.dump(food_dict, outfile)

print("Done")
