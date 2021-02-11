from selenium import webdriver
from bs4 import BeautifulSoup

URL = 'https://www.peta.org/living/food/animal-ingredients-list/'
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

mainContent = soup.find('main', class_='inner')

names = mainContent.find_all('b')

for name in names:
    food_name = name.text
    diet_type = "animal based product"
    test = make_food_obj(food_name, diet_type)
    print(test.name + " - " +  test.diet_type)
    print()

