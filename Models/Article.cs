using System.ComponentModel;
using System.Data;

namespace containers.Models
{

    public enum UnitType
    {
        [Description("Kilogramo")]
        KG,
        [Description("Libra")]
        LB
    }

    public enum QuantityType
    {
        [Description("Unidad")]
        UNIT,
        [Description("Paleta")]
        PALLET,
        [Description("Caja")]
        BOX
    }

    public class Article
    {
        public string ID { get; private set; }
        public string Description { get; private set; }
        public float Weight { get; private set; }
        public UnitType Unit { get; private set; }
        public uint Quantity { get; private set; }
        public QuantityType QuantityType { get; private set; }
        public float UnitPrice { get; private set; }
        public string? ContainerID;
        public string? ClientID;

        public Article(string id, string description, float weight, UnitType unit, uint quantity, QuantityType quantityType, float unitPrice)
        {
            ID = id;
            Description = description;
            Weight = weight;
            Unit = unit;
            Quantity = quantity;
            QuantityType = quantityType;
            UnitPrice = unitPrice;
        }

        public Article(string description, float weight, UnitType unit, uint quantity, QuantityType quantityType, float unitPrice)
        {
            ID = Helpers.Helpers.GenerateID();
            Description = description;
            Weight = weight;
            Unit = unit;
            Quantity = quantity;
            QuantityType = quantityType;
            UnitPrice = unitPrice;
        }

        public Article(string description, float weight, UnitType unit, uint quantity, QuantityType quantityType, float unitPrice, string? containerId, string? clientId)
            : this(description, weight, unit, quantity, quantityType, unitPrice)
        {
            ContainerID = containerId;
            ClientID = clientId;
        }

        public Article(string id, string description, float weight, UnitType unit, uint quantity, QuantityType quantityType, float unitPrice, string? containerId, string? clientId)
            : this(id, description, weight, unit, quantity, quantityType, unitPrice)
        {
            ContainerID = containerId;
            ClientID = clientId;
        }

        public override string ToString()
        {
            return $"ID: {ID} - Description: {Description} - Quantity: {Quantity}";
        }
        public void Print()
        {
            Console.WriteLine($"ID: {ID} - Description: {Description} - Quantity: {Quantity}");
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetWeight(float weight)
        {
            Weight = weight;
        }

        public void SetUnit(UnitType unit)
        {
            Unit = unit;
        }

        public void SetQuantity(uint quantity)
        {
            Quantity = quantity;
        }

        public void SetQuantityType(QuantityType quantityType)
        {
            QuantityType = quantityType;
        }

        public void SetUnitPrice(float unitPrice)
        {
            UnitPrice = unitPrice;
        }

        public static void GenerateColumns(DataTable dataTable)
        {
            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("Contenedor", typeof(string));
            dataTable.Columns.Add("Cliente", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            dataTable.Columns.Add("Cantidad", typeof(uint));
            dataTable.Columns.Add("Tipo Cantidad", typeof(string));
            dataTable.Columns.Add("Peso Unitario", typeof(double));
            dataTable.Columns.Add("Tipo Unidad", typeof(string));
            dataTable.Columns.Add("Precio Unitario", typeof(double));
            dataTable.Columns.Add("-|- Desasignar -|-", typeof(string));
            dataTable.Columns.Add("-|- Eliminar -|-", typeof(string));
            
        }
        public void ToRow(DataTable dataTable)
        {
            dataTable.Rows.Add(ID, ContainerID, ClientID, Description, Quantity, QuantityType, Weight, Unit, UnitPrice, "  --> Sure? <--  ", "  --> Sure? <--  ");
        }
    }
}