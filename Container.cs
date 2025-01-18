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
        public string ID { get; private set; }
        public ContainerType Type { get; private set; }
        public ContainerState State { get; private set; }
        public float Capacity { get; private set; }
        public float Tare { get; private set; }

        public Container(string id, ContainerType type, ContainerState state, float capacity, float tare)
        {
            ID = id;
            Type = type;
            State = state;
            Capacity = capacity;
            Tare = tare;
        }

        public Container(ContainerType type, ContainerState state, float capacity, float tare) {
            ID = Utils.GenerateID();
            Type = type;
            State = state;
            Capacity = capacity;
            Tare = tare;
        }
        
        public void SetType(ContainerType type) {
            Type = type;
        }

        public void SetState(ContainerState state) {
            State = state;
        }

        public void SetCapacity(float capacity) {
            Capacity = capacity;
        }

        public void SetTare(float tare) {
            Tare = tare;
        }
    }
}