import pyodbc

server = 'Server=*******;'
database = 'Database=******;'
username = 'UID=*******;'
password = 'PWD=*******;'
conn = pyodbc.connect('Driver={SQL Server};' +
                      server +
                      database +
                      username +
                      password)

cursor = conn.cursor()
DIET_TABLE = "dbo.diet_types"
FOOD_TABLE = "dbo.foods"
COMPOUND_TABLE = "dbo.compounds"

def data_source_compound():
    return f"SELECT TOP(10) * FROM {COMPOUND_TABLE} c INNER JOIN {FOOD_TABLE} f ON(c.food_id = f.public_id_int) INNER JOIN {DIET_TABLE} d ON(f.diet_type = d.id) WHERE c.name LIKE '%{search}%'"

def data_source_food(num):
    return f"SELECT * FROM {FOOD_TABLE} f INNER JOIN {DIET_TABLE} d ON(f.diet_type = d.id) WHERE f.name LIKE '%{search}%' AND f.data_source = {num}"

def search_food():
    cursor.execute(data_source_food(2))

    if cursor.rowcount == 0:
        cursor.execute(data_source_food(3))

        if cursor.rowcount == 0:
            cursor.execute(data_source_food(1))

    return cursor

def search_DB(search):
    cursor = search_food()

    if cursor.rowcount == 0:
        command = data_source_compound()
        cursor.execute(command)
        
        if cursor.rowcount == 0:
            print("No data")
        else:
            for row in cursor:
                print("Compound: {:^20} | Food Name: {:^20} | group: {:^30} | sub: {:^30} | diet type: {:<15}".format(row[1], row[8], row[13], row[14], row[19]))
    else:
        for row in cursor:
            print("Food: {:^20} | group: {:^30} | sub: {:^30} | diet type: {:<15}".format(row[1], row[6] or "N/A", row[7] or "N/A", row[12]))

while True:
    search = input("Enter search term (type stop to end): ")
    if search is not None:
        if search == "stop":
            break

        search_DB(search)        
        search = None
