#include <cstdlib>
#include <cstdio>
#include <string>
#include <iostream> 
#include <vector>
#include <list>
#include <sstream>

using namespace std;
using ::vector;
typedef short int sInt;

class Call {
private:
    int phone_number;
    sInt call_duration;
    
public:
    Call(int number, sInt duration) {
        phone_number = number;
        call_duration = duration;
    }
    
    ~Call(){};

    sInt getDuration() { return call_duration; };
};

class Customer {
private:  
    string c_name;
    vector<Call> c_calls;
    float c_balance;
    
protected:
    sInt month_fee;
    
public:   
    Customer(){};
    
    ~Customer(){};
    
    void setName(string name) { c_name = name; };
    
    string getName() { return c_name; };
    
    void addCall(int number, sInt duration) {
        Call *c_call = new Call(number, duration);
        
        if (c_call) { 
            c_calls.push_back(*c_call);
        }
        else {
            cout << "Could not allocate space for a new call!" << endl; 
            exit(1);
        }
        
        delete c_call;
    }
    
    sInt getCallSize() { return c_calls.size(); };
    
    int getTotalCallDuration() {
        int total_duration = 0;
        
        for (int i=0; i < c_calls.size(); i++) {
            total_duration += c_calls[i].getDuration();
        }

        return total_duration;
    }
    
    void setBalance(float balance) { c_balance = balance; };
    
    float getBalance() { return c_balance; };
    
    virtual void computeBalance()=0;
};

class RegularCustomer : public Customer { 
private:
    sInt call_rate;
    float balance;
    
public:   
    RegularCustomer(){ month_fee = 5; call_rate = 1;};
    
    ~RegularCustomer(){};
    
    void computeBalance(){
        balance = month_fee + call_rate * getCallSize();
        setBalance(balance);
    }
};

class PremiumCustomer : public Customer {
private:
    float min_rate;
    float balance;
    
public:
    PremiumCustomer(){ month_fee = 30; min_rate = 0.05; };
    
    ~PremiumCustomer(){};
    
    void computeBalance(){
        balance = month_fee + (min_rate * (getTotalCallDuration() / 60));
        setBalance(balance);
    }
};

class Simulation {
private:
    list<RegularCustomer*> rc_list;
    list<PremiumCustomer*> pc_list;
    
    static bool deleteAllRC( RegularCustomer *cust ) { delete cust; return true; }
    static bool deleteAllPC( PremiumCustomer *cust ) { delete cust; return true; }

    
public:
    Simulation(){
        srand(time(NULL));
    };
    
    ~Simulation(){
        rc_list.remove_if(deleteAllRC);
        pc_list.remove_if(deleteAllPC);
    };
    
    int randomGen(int min, int max) {
        return rand() * 1.0 / RAND_MAX * (max-min+1) + min; 
    }
    
    string generateName() {
        //find a way to make it 1 loop, switch
        sInt f_size = randomGen(4, 6);
        sInt l_size = randomGen(4, 6);
        
        char fName[f_size], lName[l_size];
        int i;
        
        fName[0] = char(randomGen(65, 90));
        lName[0] = char(randomGen(65, 90));
        
        for (i = 1; i < f_size + 1; i++) {
            fName[i] = static_cast<char>(randomGen(97, 122));
        }
        
        for (i = 1; i < l_size + 1; i++) {
            lName[i] = static_cast<char>(randomGen(97, 122));
        }
        
        fName[f_size] = '\0';
        lName[l_size] = '\0';

        stringstream name;
        name << fName << ' ' << lName;

        return name.str();
    }
    
    int generateNumber() {
        return randomGen(1000000000, 2147483647);
    }
    
    sInt generateDuration() {
        return randomGen(20, 7200);
    }

    void generateCustomer(Customer *cust) {
        cust->setName(generateName());
         
        sInt num_of_calls = randomGen(20, 300);
        for (int i = 0; i < num_of_calls; i++) {
            cust->addCall(generateNumber(), generateDuration());
        }
    }
    
    template <typename T>
    sInt calcAvgCalls(T it, T end, sInt size) {
        int total_calls = 0;
        for (; it != end; it++) {
            total_calls += (*it)->getCallSize();
        }
        
        return (total_calls / size);
    }
    
    template <typename T>
    int calcAvgDuration(T it, T end, sInt size) {
        float total = 0;
        for (; it != end; it++) {
            total += ((*it)->getTotalCallDuration() / 60) / (*it)->getCallSize();
        }
        return total / size;
    }
    
    template <typename T>
    float calcAvgBalance(T it, T end, sInt size) {
        float total = 0;
        for (; it != end; it++) {
            (*it)->computeBalance();
            total += (*it)->getBalance();
        }
        return total / size;
    }
    
    template <typename T>
    void printMaxMinBal(T it, T end) {
        float min_bal = 9999999;
        float max_bal = 0;
        string min_name = "";
        string max_name = "";
        
        for (; it != end; it++) {
            if ((*it)->getBalance() <= min_bal) {
                min_bal = (*it)->getBalance();
                min_name = (*it)->getName();
            }
            if ((*it)->getBalance() >= max_bal) {
                max_bal = (*it)->getBalance();
                max_name = (*it)->getName();
            }
        }
        
        cout << "\tCustomer with largest balance: " << max_name;
        printf("($%.2f)\n", max_bal);
        cout << "\tCustomer with smallest balance: " << min_name;
        printf("($%.2f)\n", min_bal);
    }
    
    void printCustomerData(sInt num, sInt avg_call, sInt avg_dura, float bal) {
        cout << "\tNumber of customers in the group: " << num << endl;
        cout << "\tAverage number of calls/per customer: " << avg_call << endl;
        cout << "\tAverage duration of the call/per customer: " << avg_dura << " mins" << endl;
        printf("\tAverage balance/per customer: $%.2f\n", bal);
    }
    
    void printResult(){
        //generating regular customers
        sInt num_of_rcust = randomGen(300, 400);
        for (int i = 0; i < num_of_rcust; i++) {
            RegularCustomer *rcust = new RegularCustomer();
            generateCustomer(rcust);
            rc_list.push_back(rcust);
        }
        
        //generating premium customers
        sInt num_of_pcust = randomGen(300, 400);
        for (int i = 0; i < num_of_pcust; i++) {
            PremiumCustomer *pcust = new PremiumCustomer();
            generateCustomer(pcust);
            pc_list.push_back(pcust);
        }
        
        float avg_rcust_bal = calcAvgBalance(rc_list.begin(), rc_list.end(), num_of_rcust);
        float avg_pcust_bal = calcAvgBalance(pc_list.begin(), pc_list.end(), num_of_pcust);
        
        cout << "Simulation run:" << endl;
        cout << "---------------" << endl;
        
        cout << "Regular Customers: \n" << endl;
        printCustomerData(num_of_rcust, 
                calcAvgCalls(rc_list.begin(), rc_list.end(), num_of_rcust),
                calcAvgDuration(rc_list.begin(), rc_list.end(), num_of_rcust),
                avg_rcust_bal);
        printMaxMinBal(rc_list.begin(), rc_list.end());
        
        cout << "\nPremium Customers: ------\n" << endl;
        printCustomerData(num_of_pcust, 
                calcAvgCalls(pc_list.begin(), pc_list.end(), num_of_pcust),
                calcAvgDuration(pc_list.begin(), pc_list.end(), num_of_pcust),
                avg_pcust_bal);
        printMaxMinBal(pc_list.begin(), pc_list.end());

        if (avg_rcust_bal > avg_pcust_bal) {
            cout << "\nPremium customers on average save ";
            printf("$%.2f", (avg_rcust_bal - avg_pcust_bal));
            cout << " compared to Regular customers." << endl;
        }
        else
        {
            cout << "\nRegular customers on average save ";
            printf("$%.2f", (avg_pcust_bal - avg_rcust_bal));
            cout << " compared to Premium customers." << endl;
        }
    }
};

int main() {
    Simulation *sim = new Simulation();
    sim->printResult();
    delete sim;
    
    return 0;
}

