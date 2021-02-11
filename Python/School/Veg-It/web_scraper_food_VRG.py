from selenium import webdriver
from bs4 import BeautifulSoup

URL = 'https://www.vrg.org/ingredients/'
driver = webdriver.Chrome()
driver.get(URL)
soup = BeautifulSoup(driver.page_source, 'html.parser')
driver.quit()

class Food(object):
    name = ""
    diet_type = ""

    def __init__(self, name, diet_type):
        self.name = name
        self.diet_type = diet_type

def make_food_obj(name, diet_type):
    food = Food(name, diet_type)
    return food

mainContent = soup.find(id='content_2col')
names = mainContent.find_all('h2')

for name in names:
    food_name = name.text
    diet_type = "not found"
    for sibling in name.find_next_siblings(text=False):
        if sibling.name == "h2":
            break
        if sibling.name == "em":
            diet_type = sibling.text
    test = make_food_obj(food_name, diet_type)
    print(test.name + " - " +  test.diet_type)
    print()

