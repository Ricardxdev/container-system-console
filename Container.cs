using System.ComponentModel;

namespace containers {

    public enum ContainerType
    {
        [Description("20 Pies")]
        TEU,
        [Description("40 Pies")]
        FEU,
        [Description("High Cube")]
        HC,
        [Description("Reefer")]
        RF,
        [Description("Open Top")]
        OT,
        [Description("Flat Rack")]
        FR,
        [Description("Tank Container")]
        TC
    }

    public enum ContainerState
    {
        IN_DEPOSIT,
        CHARGED,
        IN_TRANSIT,
        DISCHARGED,
        DELIVERED,
        IN_MAINTENANCE,
        EMPTY
    }

    class Container
    {
        private string Code;
        private ContainerType Type;
        private ContainerState State;
        private float Capacity;
        private float Tare;

        public Container(string code, ContainerType type, ContainerState state, float capacity, float tare)
        {
            Code = code;
            Type = type;
            State = state;
            Capacity = capacity;
            Tare = tare;
        }

        public Container(ContainerType type, ContainerState state, float capacity, float tare) {
            Code = Utils.GenerateID();
            Type = type;
            State = state;
            Capacity = capacity;
            Tare = tare;
        }


    }
}