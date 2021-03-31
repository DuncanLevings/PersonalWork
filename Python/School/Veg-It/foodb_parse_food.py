import json
import re

class Food(object):
    name = ''
    diet_type = 1
    description = ''
    public_id = ''
    public_id_int = None
    group = ''
    sub_group = ''
    created_date = ''
    updated_date = ''
    data_source = 1

    def __init__(self, _name, _diet_type, _description, _public_id, _public_id_int, _group, _sub_group, _created_date, _updated_date):
        self.name = _name
        self.diet_type = _diet_type
        self.description = _description
        self.public_id = _public_id
        self.public_id_int = _public_id_int
        self.group = _group
        self.sub_group = _sub_group
        self.created_date = _created_date
        self.updated_date = _updated_date
        self.data_source = 1
        
def make_food_obj(name, desc, _id, group, sub_group, created, updated):
    diet_type = check_diet_type(group, sub_group)
    _id_int = int(re.findall('\d+', _id)[0])
    
    if desc is not None:
        desc = desc.replace("'", "")
    else:
        desc = None
        
    food = Food(name, diet_type, desc, _id, _id_int, group, sub_group, created, updated)
    return food

def check_diet_type(group, sub_group):
    if group in vegan_group_0:
        if sub_group in non_vegetarian_sub_group_2:
            return 6
        elif sub_group in vegan_sub_group_1:
            return 2
        return 1
    elif group in vegan_group_1:
        return 2
    elif group in vegetarian_group_0:
        return 3
    return 5

vegan_group_0 = ['Fats and oils', 'Baking goods']
vegan_group_1 = ['Coffee and coffee products', 'Beverages', 'Teas']
vegan_sub_group_1 = ['Sugars']
vegetarian_group_0 = ['Milk and milk products', 'Fruits', 'Vegetables', 'Herbs and Spices', 'Nuts']
non_vegetarian_group_2 = ['Aquatic foods']
non_vegetarian_sub_group_2 = ['Animal fats']
unclassified_group = ['Dishes', 'Confectionaries', 'Cereals and cereal products', 'Baking goods']

food = []
data = []

print("reading Food.json...")
# open file and collect data into a single array
for line in open('Food.json', 'r', encoding='utf-8'):
    data.append(json.loads(line))

for line in data:
    food_obj = make_food_obj(
        line['name'],
        line['description'],
        line['public_id'],
        line['food_group'],
        line['food_subgroup'],
        line['created_at'],
        line['updated_at'])
    jsonFood = json.dumps(food_obj.__dict__)
    food.append(jsonFood)

# remove ' ' around each json object in array
dict_list = [json.loads(x) for x in food]

# write data to json file
with open('food_data.json', 'w') as outfile:
    json.dump(dict_list, outfile)

print("Done")

