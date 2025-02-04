using System.ComponentModel;
using System.Data;

namespace containers.Models
{
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
    public class Client
    {
        public string ID { get; private set; }
        public string Name { get; private set; }
        public ClientType Type { get; private set; }
        public string Address { get; private set; }
        public string Phone { get; private set; }
        public string? ContainerID;

        public Client(string id, string name, ClientType type, string address, string phone, string idContainer)
        {
            ID = id;
            Name = name;
            Type = type;
            Address = address;
            Phone = phone;
            ContainerID = idContainer;
        }

        public Client(string name, ClientType type, string address, string phone)
        {
            ID = Helpers.Helpers.GenerateID();
            Name = name;
            Type = type;
            Address = address;
            Phone = phone;
        }

        public override string ToString()
        {
            return $"ID: {ID} - Name: {Name} - Type: {Type}";
        }

        public void Print()
        {
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

        public static void GenerateColumns(DataTable dataTable)
        {
            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("Contenedor", typeof(string));
            dataTable.Columns.Add("Nombre", typeof(string));
            dataTable.Columns.Add("Telefono", typeof(string));
            dataTable.Columns.Add("Direccion", typeof(string));
            dataTable.Columns.Add("Tipo", typeof(string));
            dataTable.Columns.Add("Articulos", typeof(string));
            dataTable.Columns.Add("-|- Eliminar -|-", typeof(string));
        }
        public void ToRow(DataTable dataTable)
        {
            dataTable.Rows.Add(ID, ContainerID, Name, Phone, Address, Type.ToString(), "  --> Click <--  ", "  --> Sure? <--  ");
        }
    }
}