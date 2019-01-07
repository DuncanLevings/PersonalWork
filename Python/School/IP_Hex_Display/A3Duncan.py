"""
Title: ClassTakeOnHexUnwrapper
Name: Duncan Levings
Description:
Take a file containing internet Hex values, splits the file, reads the contents
and displays the IP, protocol, and port information
"""
#========================= Hex Holder Class =========================
#class to hold all hex data
class HexHolder(object):
    def __init__(self):
        self._destmac = None;   
        self._hostmac = None;   
        self._type = None;      
        self._pointer = None;   
        self._ip = None;        
        self._protocol = None;  
        self._sourceIP = None;  
        self._destIP = None;    
        self._sourcePort = None;
        self._destPort = None;
        self._protocol_type = None;
        self._ip_protocol_type = None;
        self._ethernet_type = None;

    #getter methods to safely access hex data values
    @property
    def destmac(self): return self._destmac;

    @property
    def hostmac(self): return self._hostmac;

    @property
    def type(self): return self._type;

    @property
    def pointer(self): return self._pointer;

    @property
    def ip(self): return self._ip;

    @property
    def protocol(self): return self._protocol;

    @property
    def sourceIP(self): return self._sourceIP;

    @property
    def destIP(self): return self._destIP;

    @property
    def sourcePort(self): return self._sourcePort;

    @property
    def destPort(self): return self._destPort;

    @property
    def protocolType(self): return self._protocol_type;

    @property
    def ipProtocolType(self): return self._ip_protocol_type;

    @property
    def ethernetType(self): return self._ethernet_type;
    
    #setter methods to safely set the hex data values
    @destmac.setter
    def destmac(self, value): self._destmac = value;

    @hostmac.setter
    def hostmac(self, value): self._hostmac = value;
    
    @type.setter
    def type(self, value): self._type = value;

    @pointer.setter
    def pointer(self, value): self._pointer = value;

    @ip.setter
    def ip(self, value): self._ip = value;

    @protocol.setter
    def protocol(self, value): self._protocol = value;

    @sourceIP.setter
    def sourceIP(self, value): self._sourceIP = value;

    @destIP.setter
    def destIP(self, value): self._destIP = value;

    @sourcePort.setter
    def sourcePort(self, value): self._sourcePort = value;

    @destPort.setter
    def destPort(self, value): self._destPort = value;

    @protocolType.setter
    def protocolType(self, value): self._protocol_type = value;

    @ipProtocolType.setter
    def ipProtocolType(self, value): self._ip_protocol_type = value;

    @ethernetType.setter
    def ethernetType(self, value): self._ethernet_type = value;

#========================= Hex Function Helper Class =========================
#class containing port value table and helper functions
class hexFunctionHelpers(object):
    def __init__(self):
        self.protocol_table = {'08 00': 'IPv4', '86 DD': 'IPv6', '08 06': 'ARP',
                                '1': 'ICMP', '2': 'IGMP', '6': 'TCP', '17': 'UDP',
                                '20': 'FTP', '22': 'SSH', '23': 'TELNET', '25': 'SMTP',
                                '53': 'DNS', '80': 'HTTP', '143': 'IMAP', '161': 'SNMP',
                                '179': 'BGP', '520': 'RIP', '443': 'HTTPS', '771': 'UDP',
                                 '903': 'WMware'};

    #loops port table dictionary until value is found, otherwise returns Unknown
    def checkPortTable(self, val):
        for key, value in self.protocol_table.items():
            if val == key:
                return value;

        return 'Unknown';

    #converts string of hex value from string to decimal and returns string
    def hexToDec(self, hex_val):
        return str(int(''.join(hex_val), 16));

    #converts string of hex value from string to decimal and returns int
    def hexToDecInt(self, hex_val):
        return int(''.join(hex_val), 16);

    #converts a list to string
    def listToString(self, list):
        return ' '.join(list);

    #converts 4 hex values to an IP address decimal
    def hexToIP(self, list):
        ip = '';
        for i in list:
            s = str(int(i, 16));
            ip += s + '.';
        return ip[:-1];

#opens a file from file name, and returns a split list of it
def openFile():
    while True:
        filename = input("Enter name of file to open: ");

        try:
            fileList = open(filename, 'rt', encoding='latin1').read().split();
        except FileNotFoundError:
            print('No such file with name: {} exists!'.format(filename));
        else:
            return fileList;

#sets hex data values from filelist
def setHex(hex, hexHelper, file_list):
    hex.destmac = file_list[0:6];
    hex.hostmac = file_list[6:12];
    hex.type = file_list[12:14];
    hex.ip = file_list[14:34];
    hex.pointer = file_list[23:24];
    hex.sourceIP = file_list[26:30];
    hex.destIP = file_list[30:34];
    hex.sourcePort = file_list[34:36];
    hex.destPort = file_list[36:38];

    #sets hex protocols from filelist
    #if source port > 1024, means its ephemeral, then get protocol from destination
    if hexHelper.hexToDecInt(hex.sourcePort) > 1024:
        hex.protocolType = hexHelper.checkPortTable(hexHelper.hexToDec(hex.destPort));
    else:
        hex.protocolType = hexHelper.checkPortTable(hexHelper.hexToDec(hex.sourcePort));
    
    hex.ipProtocolType = hexHelper.checkPortTable(hexHelper.hexToDec(hex.pointer));
    hex.ethernetType = hexHelper.checkPortTable(hexHelper.listToString(hex.type));

#prints a --- line with + caps
def lineSeperator(color):
    dash = '-' * 45;
    print("\033[{}m+{}+".format(color, dash))

#prints a line bordered by | with content within margined to left
def lineBorder(msg, color, msgColor = 0):
    print("\033[{}m| \033[{}m{:<43} \033[{}m|".format(color, msgColor, msg, color))

#display data from hex class using lineborder and lineseperator functions
def displayData(hex, hexHelper):
    lineSeperator(31);
    lineBorder('', 31);
    lineBorder(hex.protocolType + ' Protocol', 31, 32);
    lineBorder('', 31);
    lineSeperator(31);
    lineBorder('', 31);
    lineBorder(hex.ipProtocolType + ' Protocol', 31, 34);   

    #prints differently for port number depending if its ephemeral or not
    source_port = hexHelper.hexToDec(hex.sourcePort);
    if hexHelper.hexToDecInt(hex.sourcePort) > 1024:
        lineBorder('Source Port: ' + hexHelper.hexToDec(hex.sourcePort) + ' : (ephemeral)', 31, 34); 
    else:
        lineBorder('Source Port: ' + hexHelper.hexToDec(hex.sourcePort), 31, 34);

    lineBorder('Destination Port: ' + hexHelper.hexToDec(hex.destPort), 31, 34); 
    lineBorder('', 31);
    lineSeperator(31);
    lineBorder('', 31);
    lineBorder(hex.ethernetType + ' Protocol', 31, 36);   
    lineBorder('Source IP Address: ' + hexHelper.hexToIP(hex.sourceIP), 31, 36); 
    lineBorder('Destination IP Address: ' + hexHelper.hexToIP(hex.destIP), 31, 36); 
    lineBorder('', 31);
    lineSeperator(31);
    lineBorder('', 31);
    lineBorder('Ethernet Protocol', 31, 35);   
    lineBorder('Destination MAC Address: ' + hexHelper.listToString(hex.destmac), 31, 35); 
    lineBorder('Source MAC Address: ' + hexHelper.listToString(hex.hostmac), 31, 35); 
    lineBorder('', 31);
    lineSeperator(31);

#========================= MAIN =========================

file = openFile();
hex = HexHolder();
hexHelper = hexFunctionHelpers();
setHex(hex, hexHelper, file);
displayData(hex, hexHelper);
