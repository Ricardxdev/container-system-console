using System.ComponentModel;

namespace containers {

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

    class Article
    {
        public string ID { get; private set; }
        public string Description { get; private set; }
        public float Weight { get; private set; }
        public UnitType Unit { get; private set; }
        public uint Quantity { get; private set; }
        public QuantityType QuantityType { get; private set; }

        public Article(string id, string description, float weight, UnitType unit, uint quantity, QuantityType quantityType)
        {
            ID = id;
            Description = description;
            Weight = weight;
            Unit = unit;
            Quantity = quantity;
            QuantityType = quantityType;
        }

        public Article(string description, float weight, UnitType unit, uint quantity, QuantityType quantityType)
        {
            ID = Utils.GenerateID();
            Description = description;
            Weight = weight;
            Unit = unit;
            Quantity = quantity;
            QuantityType = quantityType;
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
    }
}