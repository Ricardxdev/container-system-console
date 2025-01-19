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
        public ContainerType[] Types { get; private set; }
        public ContainerState State { get; private set; }
        public float Capacity { get; private set; }
        public float Tare { get; private set; }

        public Container(string id, ContainerType[] types, ContainerState state, float capacity, float tare)
        {
            ID = id;
            Types = types;
            State = state;
            Capacity = capacity;
            Tare = tare;
        }

        public Container(ContainerType[] types, ContainerState state, float capacity, float tare) {
            ID = Utils.GenerateID();
            Types = types;
            State = state;
            Capacity = capacity;
            Tare = tare;
        }
        
        public void Print() {
            Console.WriteLine($"ID: {ID} - State: {State} - Capacity: {Capacity}");
        }

        public void SetTypes(ContainerType[] types) {
            Types = types;
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