using System.ComponentModel;
using System.Data;
using containers.Helpers;

namespace containers.Models
{

    public enum ContainerType
    {
        [Description("20 Feet")]
        TEU,
        [Description("40 Feet")]
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

    public class Container
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

        public Container(ContainerType[] types, ContainerState state, float capacity, float tare)
        {
            ID = Helpers.Helpers.GenerateID();
            Types = types;
            State = state;
            Capacity = capacity;
            Tare = tare;
        }

        public override string ToString()
        {
            return $"ID: {ID} - State: {State} - Capacity: {Capacity} - Types: {String.Join(", ", Types)}";
        }

        public void SetTypes(ContainerType[] types)
        {
            Types = types;
        }

        public void SetState(ContainerState state)
        {
            State = state;
        }

        public void SetCapacity(float capacity)
        {
            Capacity = capacity;
        }

        public void SetTare(float tare)
        {
            Tare = tare;
        }

        public static void GenerateColumns(DataTable dataTable)
        {
            dataTable.Columns.Add("Codigo", typeof(string));
            dataTable.Columns.Add("Tipos", typeof(string));
            dataTable.Columns.Add("Estado", typeof(string));
            dataTable.Columns.Add("Capacidad", typeof(double));
            dataTable.Columns.Add("Tara", typeof(double));
            dataTable.Columns.Add("Valor Total", typeof(double));
            dataTable.Columns.Add("-|- Clientes -|-", typeof(string));
            dataTable.Columns.Add("-|- Articulos -|-", typeof(string));
            dataTable.Columns.Add("-|- Eliminar -|-", typeof(string));
        }
    }
}