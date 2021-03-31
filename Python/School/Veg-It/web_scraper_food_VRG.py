from selenium import webdriver
from bs4 import BeautifulSoup
import unicodedata
import json
import copy

URL = 'https://www.vrg.org/ingredients/'
driver = webdriver.Chrome()
driver.get(URL)
soup = BeautifulSoup(driver.page_source, 'html.parser')
driver.quit()

class Food(object):
    name = ''
    diet_type = 6
    description = ''
    public_id = ''
    public_id_int = 0
    group = ''
    sub_group = None
    created_date = '1997-01-01T12:00:00+00:00'
    updated_date = '2010-01-01T12:00:00+00:00'
    data_source = 2

    def __init__(self, _name, _diet_type, _description, _public_id_int, _group):
        self.name = _name
        self.diet_type = _diet_type
        self.description = _description
        self.public_id = self.public_id
        self.public_id_int = _public_id_int
        self.group = _group
        self.sub_group = self.sub_group
        self.created_date = self.created_date
        self.updated_date = self.updated_date
        self.data_source = self.data_source

def make_food_obj(name, diet_type, desc, _n, group):
    _type = 6
    if diet_type == 'Typically Vegetarian':
        _type = 4
    elif diet_type == 'Vegan':
        _type = 1
    elif diet_type == 'Vegetarian':
        _type = 3
    elif diet_type == 'Typically Vegan':
        _type = 2
    elif diet_type == 'unspecified':
        _type = 5
    food = Food(name, _type, desc, _n, group)
    return food

mainContent = soup.find(id='content_2col')
names = mainContent.find_all('h2')
ignore = ['Copyright', 'October 2013', 'Used in', 'Exists in', 'More Information', 'Manufacturers', 'Used as', 'Naturally present']

N = -1000
data = {}
dupe = True

for name in names:
    clean_name = unicodedata.normalize("NFKD",name.text)
    data[clean_name] = {}
    for sibling in name.find_next_siblings():
        if sibling.name == 'h2':
            dupe = True
            break
        if sibling.name == 'p':
            continue
        if sibling.name == 'a' and dupe == True:
            data[clean_name]['dupe'] = sibling.text
            break
        if sibling.name == 'strong':
            dupe = False
            if sibling.text in 'Definition':
                data[clean_name][sibling.text] = sibling.next_element.next_element[2:]
            elif sibling.text in ignore:
                continue
            else:
                text = sibling.next_element.next_element[2:]
                text = text.split(',')
                text = [item.strip() for item in text]
                data[clean_name][sibling.text] = text
        if name.text == 'calcium phosphates': # needed because the web page is horribly made
            data[clean_name]['type'] = 'Vegan'    
        if sibling.name == 'em' or sibling.name == 'i':
            data[clean_name]['type'] = sibling.text

ignore_names = ['E number', 'Copyright Information', 'Related Articles', 'Sucralose']

food_list = []
food_dupe_list = {}
food_dupe_objs = {}

for name in data:
    food_data = data.get(name)
    if 'type' in food_data:
        if 'Commercial source' in food_data:
            group = food_data['Commercial source'][0]
            group = group.replace('.', '')
        else:
            group = None
            
        food_obj = make_food_obj(
            name,
            food_data['type'],
            food_data['Definition'],
            N,
            group)
        N = N - 1
        food_dupe_objs[name] = food_obj.__dict__
        food_list.append(food_obj.__dict__)

        if 'Also known as' in food_data:
            synonym_list = food_data['Also known as']
            if len(synonym_list) > 0 and synonym_list[0] != '':
                for synonym in synonym_list:
                    synonym = synonym.replace('.', '')
                    dupe_obj = copy.deepcopy(food_obj)
                    dupe_obj.name = synonym
                    dupe_obj.public_id_int = N
                    food_list.append(dupe_obj.__dict__)
                    N = N - 1
                    
    else:
        if name not in ignore_names:
            food_dupe_list[name] = food_data['dupe']

for key, value in food_dupe_list.items():
    food_dupe_obj = food_dupe_objs.get(value)
    if food_dupe_obj is not None:
        dupe_obj = copy.deepcopy(food_dupe_obj)
        dupe_obj['name'] = key
        dupe_obj['public_id_int'] = N
        food_list.append(dupe_obj)
        N = N - 1

print(food_list[0])
print(food_list[1])
print(food_list[2])
no_duplicates = []
N = 0
print("Length before: ", len(food_list))
print("Removing duplicates...")
for obj in food_list:   
    dupe = False
    # remove duplicates
    for x in no_duplicates:
        if x['name'] == obj['name']:
            dupe = True
            break
    if dupe == False:
        no_duplicates.append(obj)
    else:
        continue

print("Length after: ", len(no_duplicates))
with open('vrg_food_data.json', 'w') as outfile:
    json.dump(no_duplicates, outfile)
