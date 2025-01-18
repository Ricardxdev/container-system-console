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
        private string ID;
        private string Name;
        private ClientType Type;
        private string Address;
        private string Phone;

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
    }
}