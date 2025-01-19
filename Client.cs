using System.ComponentModel;

namespace containers {
    public enum ClientType
    {
        [Description("Importador")]
        IMPORTER,
        [Description("Exportador")]
        EXPORTER,
        [Description("Mayorista")]
        MAYORIST,
        [Description("Minorista")]
        MINORIST
    }
    class Client
    {
        public  string ID { get; private set; }
        public  string Name { get; private set; }
        public  ClientType Type { get; private set; }
        public  string Address { get; private set; }
        public  string Phone { get; private set; }

        public Client(string id, string name, ClientType type, string address, string phone)
        {
            ID = id;
            Name = name;
            Type = type;
            Address = address;
            Phone = phone;
        }

        public Client(string name, ClientType type, string address, string phone)
        {
            ID = Utils.GenerateID();
            Name = name;
            Type = type;
            Address = address;
            Phone = phone;
        }

        public void Print() {
            Console.WriteLine($"ID: {ID} - Name: {Name} - Type: {Type}");
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetType(ClientType type)
        {
            Type = type;
        }

        public void SetAddress(string address)
        {
            Address = address;
        }

        public void SetPhone(string phone)
        {
            Phone = phone;
        }
    }
}