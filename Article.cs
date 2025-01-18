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
        private string ID;
        private string Description;
        private float Weight;
        private UnitType Unit;
        private uint Quantity;
        private QuantityType QuantityType;

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
    }
}