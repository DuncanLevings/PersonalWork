import json
import re

class Compound(object):
    name = ''
    public_id = ''

    def __init__(self, _name, _public_id):
        self.name = _name
        self.public_id = _public_id
        
def make_compound_obj(name, _id):
    compound = Compound(name, _id)
    return compound

class Compound_Linked(object):
    name = ''
    public_id = ''
    food_id = 0
    created_date = ''
    updated_date = ''
    data_source = 1

    def __init__(self, _name, _public_id, _food_id, _created_date, _updated_date):
        self.name = _name
        self.public_id = _public_id
        self.food_id = _food_id
        self.created_date = _created_date
        self.updated_date = _updated_date
        self.data_source = 1
        
def make_compound_linked_obj(compound, _food_id, _orig_name, created, updated):
    if compound is None:
        compound_linked = Compound_Linked(_orig_name, None, _food_id, created, updated)
    else:
        compound_linked = Compound_Linked(compound['name'], compound['public_id'], _food_id, created, updated)

    return compound_linked

compound = []
comp_dict = {}

# open compound file and collect data into a single array
print("Reading Compound.json...")
for line in open('Compound.json', 'r', encoding='utf-8'):    
    compound.append(json.loads(line))

for line in compound:
    compound_obj = make_compound_obj(
        line['name'],
        line['public_id'])
    jsonCompound = json.dumps(compound_obj.__dict__)
    # remove ' ' around each json object
    comp = json.loads(jsonCompound)
    # extract number from public_id as dict key
    _id_int = int(re.findall('\d+', comp['public_id'])[0])
    comp_dict[_id_int] = comp

print("Finished creating compound dictionary")

N = 0
START = 0
END = 500000
data = []
compLinkData = []

print("Reading Content.json...")
# open main file and collect data into a single array
# limited by N, to avoid file size of 3GB
for line in open('Content.json', 'r', encoding='utf-8'):
    line_data = json.loads(line)
    if line_data['source_type'] == 'Nutrient': # skipping non compound lines
        continue

    N = N + 1

    #only start adding lines once # of read lines exceed START
    if N <= START:
        continue

    data.append(line_data)
    
    if N % 50000 == 0:
        print("Progress: ", N)
    if N == END:
        print("Done reading Content.json")
        break

N = 0
print("Parsing data...")
for line in data:
    N = N + 1
    if N % 50000 == 0:
        print("Progress: ", N)
    if N == END:
        print("Done parsing data.")
    
    if line['source_type'] != 'Compound':
        continue

    comp_link = make_compound_linked_obj(
        comp_dict.get(line['source_id']),
        line['food_id'],
        line['orig_source_name'],
        line['created_at'],
        line['updated_at'])
    jsonComp = json.dumps(comp_link.__dict__)
    jsonComp = json.loads(jsonComp);
    compLinkData.append(jsonComp)

# write data to json file
with open('compound_data_' + str(START) + '_' + str(END) + '.json', 'w') as outfile:
    json.dump(compLinkData, outfile)
    
print("Done")

