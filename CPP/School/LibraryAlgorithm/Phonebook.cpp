// Assignment4.cpp : This file contains the 'main' function. Program execution begins and ends there.
//Duncan Levings

#include "pch.h"
#include <iostream>
#include <iostream>
#include <fstream>
#include <string>
#include <sstream>

using namespace std;
const string FILE_NAME = "phonebook.txt";
short ARRAY_SIZE = 2;

struct record {
	string first_name;
	string last_name;
	int number;
};

class records {
	record *collection;
	int size;

public:
	records();
	~records();

	void addRecord(string fname, string lname, int number);
	void retrieveRecord(string lName);
	void deleteRecord(string lName);
	void load();
	void save();
	void sort();
	void print();
	void increaseCollectionSize();
};

records::records() {
	collection = new record[ARRAY_SIZE];
	size = 0;
	load();
}

records::~records()
{
	if (collection != nullptr)
	{
		delete[] collection;
	}
}

void records::addRecord(string fname, string lname, int number)
{
	cout << "Adding record..." << endl;
	if (size == ARRAY_SIZE)
	{
		increaseCollectionSize();
	}

	collection[size] = record{ fname, lname, number };

	size += 1;
}

void records::retrieveRecord(string lName)
{
	for (size_t i = 0; i < size; i++)
	{
		if (collection[i].last_name == lName)
		{
			cout << "Record found: " << endl;
			cout << collection[i].first_name << " " << collection[i].last_name << " " << collection[i].number << endl;
		}
	}
}

void records::deleteRecord(string lName)
{
	short index_to_be_deleted = 0;
	for (size_t i = 0; i < size; i++)
	{
		//find index to be deleted
		if (collection[i].last_name == lName)
		{
			cout << "Deleting record..." << endl;
			//shift array elements down from deleted index
			for (size_t j = i; j < size; ++j)
			{
				collection[j] = collection[j + 1];
			}

			size -= 1;
		}
	}
}

void records::load()
{
	string *values = new string[3];
	string line;
	short line_counter = 0;

	ifstream myfile(FILE_NAME);
	if (myfile.is_open()) 
	{
		delete[] collection;

		collection = new record[ARRAY_SIZE];
		size = 0;

		cout << "loading from file..." << endl;
		while (getline(myfile, line))
		{
			stringstream iss(line);
			string temp;
			
			//getting individual words of line for storing into struct
			while (getline(iss, temp, ' '))
			{
				values[line_counter] = temp;
				line_counter += 1;
			}

			line_counter = 0;

			if (size == ARRAY_SIZE)
			{
				increaseCollectionSize();
			}
			
			//adding record to array and increment size
			collection[size] = record{ values[0], values[1], stoi(values[2]) };

			size += 1;
		}
		myfile.close();
	}
	else 
	{
		cout << "file does not exist...";
		ofstream myfile(FILE_NAME);
		if (myfile.is_open())
		{
			cout << "created file phonebook.txt" << endl;
			myfile.close();
		}
	}

	delete[] values;
}

//when size has reached max array size, copy the array to temporary array of new size, delete old array and set old array to new array
void records::increaseCollectionSize() 
{
	ARRAY_SIZE += 2;
	record *collection_temp = new record[ARRAY_SIZE];

	for (size_t i = 0; i < size; i++)
	{
		collection_temp[i] = collection[i];
	}

	delete[] collection;
	collection = collection_temp;
}

void records::save()
{
	ofstream myFile(FILE_NAME, ofstream::out, ofstream::trunc);

	if (myFile.is_open())
	{
		cout << "Saving to file..." << endl;
		for (size_t i = 0; i < size; i++)
		{
			myFile << collection[i].first_name << " ";
			myFile << collection[i].last_name << " ";
			myFile << collection[i].number;
			myFile << "\n";
		}
		myFile.close();
	}
	else
	{
		cout << "Error! Unable to open file!" << endl;
	}
}

//insertion sort O(n*2)
void records::sort()
{
	if (size < 1)
	{
		return;
	}

	cout << "Sorting records..." << endl;

	for (size_t i = 0; i < size; ++i)
	{
		short j = i - 1;
		record temp = collection[i];
		//find index where last_name is < temp last_name, insert into this index when found
		while (j >= 0 && collection[j].last_name > temp.last_name)
		{
			collection[j + 1] = collection[j];
			--j;
		}

		//incrementing temp record
		collection[j + 1] = temp;
	}
}

void records::print()
{
	for (size_t i = 0; i < size; i++)
	{
		cout << collection[i].first_name << " " << collection[i].last_name << " " << collection[i].number << endl;
	}
}


int main()
{
	records *rec = new records();

	short input = 0;
	string first_name, last_name;
	int number;

	while (input < 9) {
		cout << "1 to Add Record, 2 to Retrieve Record, 3 to Delete Record" << endl;
		cout << "4 to Load File, 5 to Save File, 6 to Sort Records, 7 to print Records" << endl;
		cout << "8 to exit." << endl;
		std::cin >> input;
		cout << endl;

		switch (input)
		{
		case 1:
			cout << "Input value for first name: " << endl;
			std::cin >> first_name;
			cout << "Input value for last name: " << endl;
			std::cin >> last_name;
			cout << "Input value for number (7 digits no space) " << endl;
			std::cin >> number;
			rec->addRecord(first_name, last_name, number);
			cout << endl;
			break;
		case 2:
			cout << "Input value for last name to be retrieved: " << endl;
			std::cin >> last_name;
			rec->retrieveRecord(last_name);
			cout << endl;
			break;
		case 3:
			cout << "Input value for last name to be deleted: " << endl;
			std::cin >> last_name;
			rec->deleteRecord(last_name);
			cout << endl;
			break;
		case 4:
			rec->load();
			cout << endl;
			break;
		case 5:
			rec->save();
			cout << endl;
			break;
		case 6:
			rec->sort();
			cout << endl;
			break;
		case 7:
			rec->print();
			cout << endl;
			break;
		case 8:
			return 0;
		default:
			cout << "Incorrect input." << endl;
			break;
		}

	}

	delete rec;

	return 0;
}

