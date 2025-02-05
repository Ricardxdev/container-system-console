using containers.Models;
using Terminal.Gui;

namespace containers.Components
{
    public class ClientFilterForm : Window
    {
        private TextField clientCodeField;
        private TextField containerCodeField;
        private ComboBox typeBox;
        public string? ClientCode => clientCodeField.Text.ToString() != "" ? clientCodeField.Text.ToString() : null;
        public string? ContainerCode => containerCodeField.Text.ToString() != "" ? containerCodeField.Text.ToString() : null;
        public ClientType? ClientType => typeBox.SelectedItem >= 1 ? (ClientType)(typeBox.SelectedItem - 1) : null;
        public event Action OnFilterApplied;
        public ClientFilterForm()
        {
            InitializeComponents();
        }
        void InitializeComponents()
        {
            var clientCodeLabel = new Label("|-- ID del Cliente --|")
            {
                X = Pos.Percent(20) - 20,
                Y = 2,
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };
            clientCodeField = new TextField("")
            {
                X = Pos.Percent(20) - 20,
                Y = Pos.Bottom(clientCodeLabel) + 1,
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };

            var containerCodeLabel = new Label("|-- ID del Contenedor --|")
            {
                X = Pos.Percent(50) - 20,
                Y = 2,
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };
            containerCodeField = new TextField("")
            {
                X = Pos.Percent(50) - 20,
                Y = Pos.Bottom(containerCodeLabel) + 1,
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };

            var typeBoxLabel = new Label("|-- Tipo de Cliente --|")
            {
                X = Pos.Percent(80) - 20,
                Y = 2,
                Width = 40,
                TextAlignment = TextAlignment.Centered,
            };

            typeBox = new ComboBox()
            {
                X = Pos.Percent(80) - 20,
                Y = Pos.Bottom(typeBoxLabel) + 1,
                Width = 40,
                Height = 10,
                TextAlignment = TextAlignment.Centered,
            };

            var sourceTypes = new List<string>() { " " };
            sourceTypes.AddRange(Enum.GetNames(typeof(ClientType)));
            typeBox.SetSource(sourceTypes);

            var submitButton = new Button("Filtrar")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(containerCodeField) + 2
            };

            submitButton.Clicked += () =>
            {
                OnFilterApplied?.Invoke();
            };

            Add(clientCodeLabel, clientCodeField, containerCodeLabel, containerCodeField, typeBoxLabel, typeBox, submitButton);
        }
    }
}